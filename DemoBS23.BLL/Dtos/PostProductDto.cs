using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoBS23.BLL.Dtos
{
    class PostProductDto
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
