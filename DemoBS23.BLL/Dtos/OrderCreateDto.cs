using DemoBS23.DAL.Entities;
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

        [Required]
        public IList<ItemWithPriceAndQuantity> ListOfItems { get; set; }       
    }

    public static class OrderCreateDtoExtensions
    {
        public static Order ToEntity(this OrderCreateDto dto)
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,

            };

            return order;
        }
    }

    public class ItemWithPriceAndQuantity
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
    }
}

