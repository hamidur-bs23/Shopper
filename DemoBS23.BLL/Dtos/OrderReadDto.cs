using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DemoBS23.BLL.Dtos
{
    public class OrderReadDto
    {
        public string CustomerName{ get; set; }
        public string Status { get; set; }
        public string CreatedTime { get; set; }

        public ICollection<OrderDetailsReadDto> OrderedItems { get; set; }       
    }

    public static class OrderReadDtoExtensions
    {
        public static OrderReadDto ToReadDto(this Order model)
        {
            OrderReadDto dto = new OrderReadDto()
            {
                CustomerName = model.Customer.Name != null ? model.Customer.Name : string.Empty,
                Status = model.Status.ToString(),
                //CreatedTime = model.DateCreated.ToString("g", CultureInfo.CreateSpecificCulture("en-US")),
                CreatedTime = model.DateCreated.ToString(),

                OrderedItems = model.OrderDetails.Select(model => model.ToReadDto()).ToList()
            };

            return dto;
        }
    }
}

