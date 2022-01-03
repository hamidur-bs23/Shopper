using Shopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopper.BLL.Dtos
{
    public class CategoryReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }

    public static class CategoryDtoExtensions
    {
        public static CategoryReadDto ToReadDto(this Category model)
        {
            try
            {
                CategoryReadDto dto = new CategoryReadDto
                {
                    Id = model.Id,
                    Name = model.Name,
                };

                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception("Data Mapping Failed", ex);
            }
        }
        
        public static Category ToEntity(this CategoryCreateDto dto)
        {
            try
            {
                Category model = new Category
                {
                    Name = dto.Name
                };

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Data Mapping Failed", ex);
            }
        }
    }
}
