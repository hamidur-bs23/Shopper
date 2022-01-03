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

    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        //public int Quantity { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
    }

    public static class ProductDtoExtensions
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

        public static ProductReadDto ToReadDto(this Product dto)
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
