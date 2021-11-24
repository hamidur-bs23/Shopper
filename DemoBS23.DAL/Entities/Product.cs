using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoBS23.DAL.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "length should be 3 to 250 characters!", MinimumLength = 3)]
        public string Name { get; set; }
        [Required]       
        public int Price { get; set; }
    }
}
