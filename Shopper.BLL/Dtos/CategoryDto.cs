using Shopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.BLL.Dtos
{
    public class CategoryReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }

    public static class CategoryDtoExtensions
    {
        public static CategoryReadDto ToReadDto(this Category model)
        {
            try
            {
                var id = model.Id;
                var name = model.Name;

                var dto = new CategoryReadDto
                {
                    Id = id,
                    Name = name,
                };

                return dto;
            }
            catch (Exception ex)
            {

                throw new Exception("Data Mapping Failed", ex);
            }
        }
    }
}
