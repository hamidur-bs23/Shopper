using DemoBS23.DAL.Entities;
using DemoBS23.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoBS23.BLL.Dtos
{
    public class OrderCreateDto
    {
        [Required]
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required]
        public IList<OrderDetailCreateDto> ListOfItems { get; set; }       
    }

    public static class OrderCreateDtoExtensions
    {
        public static Order ToEntity(this OrderCreateDto dto)
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                DateCreated = DateTime.UtcNow,
                Total = 0,
                Status = dto.Status
            };

            return order;
        }
    }
}

