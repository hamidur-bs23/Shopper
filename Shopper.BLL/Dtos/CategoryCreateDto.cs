using Shopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopper.BLL.Dtos
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }

    public static class CategoryCreateDtoExtensions
    {
        public static Category ToEntity(this CategoryCreateDto dto)
        {
            Category entity = new Category
            {
                Name = dto.Name
            };
            return entity;
        }
    }
}
