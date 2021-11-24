using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DemoBS23.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public ICollection<Product> Products { get; set; }
    }
}
