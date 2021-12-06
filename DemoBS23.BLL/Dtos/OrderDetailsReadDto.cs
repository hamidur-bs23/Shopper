using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.BLL.Dtos
{
    public class OrderDetailsReadDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int SubTotal { get; set; }
    }

    public static class OrderDetailsReadDtoExtensions
    {
        public static OrderDetailsReadDto ToReadDto(this OrderDetail soure)
        {
            var dto = new OrderDetailsReadDto()
            {
                ProductName = soure.Product.Name != null ? soure.Product.Name : string.Empty,
                Quantity = soure.Quantity,
                UnitPrice = soure.UnitPrice,
                SubTotal = soure.SubTotal
            };

            return dto;
        }
    }
}
