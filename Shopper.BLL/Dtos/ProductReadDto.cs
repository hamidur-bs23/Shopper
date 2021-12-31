using Shopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.BLL.Dtos
{
    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        //public int Quantity { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
    }

    public static class ProductReadDtoExtensions
    {
        public static ProductReadDto ToEntity(this Product dto)
        {
            return new ProductReadDto
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                //Quantity = dto.Quantity,
                Description = dto.Description,
                CategoryName = dto.Category.Name
            };
        }
    }
}
