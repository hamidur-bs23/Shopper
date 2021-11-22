using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }


        public Category Category { get; set; }
    }
}
