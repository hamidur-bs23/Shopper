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
        public static OrderDetailsReadDto ToReadDto(this OrderDetail model)
        {
            var dto = new OrderDetailsReadDto()
            {
                ProductName = model.Product.Name != null ? model.Product.Name : string.Empty,
                Quantity = model.Quantity,
                UnitPrice = model.UnitPrice,
                SubTotal = model.SubTotal
            };

            return dto;
        }
    }
}
