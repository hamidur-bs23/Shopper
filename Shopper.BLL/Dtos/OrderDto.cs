using Shopper.DAL.Entities;
using Shopper.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Shopper.BLL.Dtos
{
    public class OrderCreateDto
    {
        [Required]
        public int CustomerId { get; set; }
        //public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required]
        public IList<OrderDetailCreateDto> ListOfItems { get; set; }
    }

    public class OrderReadDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public string DateCreated { get; set; }
        public int TotalAmount { get; set; }
        public int TotalProducts { get; set; }


        public ICollection<OrderDetailReadDto> OrderedItems { get; set; }
    }

    public static class OrderDtoExtensions
    {
        public static Order ToEntity(this OrderCreateDto dto)
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                DateCreated = DateTime.UtcNow,
                Total = 0,
                Status = OrderStatus.Pending
            };

            return order;
        }

        public static OrderReadDto ToReadDto(this Order model)
        {
            try
            {
                var id = model.Id;
                var customerId = model.Customer.Id;
                var customerName = model.Customer.Name;
                var status = model.Status;
                var dateCreated = model.DateCreated.ToLocalTime();
                var total = model.Total;

                var totalProducts = model.OrderDetails.Select(x => x.Quantity).Sum();
                
                IList<OrderDetailReadDto> orderedItems = model.OrderDetails
                        .Select(orderDetail =>
                            orderDetail.ToReadDto()
                        ).ToList();

                var dto = new OrderReadDto
                {
                    Id = id,
                    CustomerName = customerName,
                    Status = status.ToString(),
                    DateCreated = dateCreated.ToString(),
                    TotalAmount = total,
                    TotalProducts = totalProducts,
                    OrderedItems = orderedItems
                };

                return dto;
            }
            catch (Exception ex)
            {

                throw new Exception("Data Mapping Failed", ex);
            }

            
        }
    }
}
