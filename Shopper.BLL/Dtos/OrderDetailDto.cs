using Shopper.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Shopper.BLL.Dtos
{
    public class OrderDetailReadDto
    {
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int SubTotal { get; set; }
    }

    public class OrderDetailCreateDto
    {
        //[Required]
        //public int OrderId { get; set; }

        [Required] 
        public int ProductId { get; set; }

        [Required] 
        public int Quantity { get; set; }

        //public int UnitPrice { get; set; }
        //public int SubTotal { get; set; }
    }


    public static class OrderDetailDtoExtensions
    {
        public static OrderDetailReadDto ToReadDto(this OrderDetail model)
        {
            var dto = new OrderDetailReadDto()
            {
                ProductName = model.Product.Name != null ? model.Product.Name : string.Empty,
                CategoryName = model.Product.Category.Name != null ? model.Product.Category.Name : string.Empty,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice,
                SubTotal = model.SubTotal
            };

            return dto;
        }

        public static OrderDetail ToEntity(this OrderDetailCreateDto dto)
        {
            var model = new OrderDetail
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
            };

            return model;
        }
    }
}
