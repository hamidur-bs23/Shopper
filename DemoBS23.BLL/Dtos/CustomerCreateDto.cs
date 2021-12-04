using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoBS23.BLL.Dtos
{
    public class CustomerCreateDto
    {

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(20)]
        public string ContactNumber { get; set; }

    }

    public static class CustomerCreateDtoExtensions
    {
        public static Customer ToEntity(this CustomerCreateDto dto)
        {
            Customer customer = new Customer
            {
                Name = dto.Name,
                ContactNumber = dto.ContactNumber,
            };
            return customer;
        }
    }
}
