using Shopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopper.BLL.Dtos
{
    public class ProductCreateDto
    {
        [Required]
        [StringLength(250, ErrorMessage = "Name must be at least 3 characters long.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]       
        public int Price { get; set; }

        [Required]
        //[Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 0;

        [StringLength(250)]
        public string Description { get; set; }


        [Required]
        public int CategoryId { get; set; }
    }

    public static class ProductCreateDtoExtensions
    {
        public static Product ToEntity(this ProductCreateDto dto)
        {
            Product product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                StockInHand = dto.Quantity,
                Description = dto.Description,
                CategoryId = dto.CategoryId
            };

            return product;
        }
        
        public static ProductReadDto ToReadDto_bak(this Product model)
        {
            ProductReadDto dto = new ProductReadDto
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                CategoryName = model.Category.Name
            };

            return dto;
        }
    }
}
