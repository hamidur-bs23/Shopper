using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.BLL.Dtos.Admin
{
    public class ProductWithStockReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
    }

    public static class ProductWithStockReadDtoExtensions
    {
        public static ProductWithStockReadDto ToEntity(this Product dto)
        {
            return new ProductWithStockReadDto
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                Quantity = dto.Quantity,
                Description = dto.Description,
                CategoryName = dto.Category.Name
            };
        }
    }
}
