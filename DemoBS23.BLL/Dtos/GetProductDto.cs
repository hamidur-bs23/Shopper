using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoBS23.BLL.Dtos
{
    public class GetProductDto
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public Category Category { get; set; }
    }
}
