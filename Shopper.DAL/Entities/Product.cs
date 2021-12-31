using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopper.DAL.Entities
{
    public class Product
    {
        //[Key]
        public int Id { get; set; }

        //[Required]
        //[StringLength(250, ErrorMessage = "length should be 3 to 250 characters!", MinimumLength = 3)]
        public string Name { get; set; }

        //[Required]       
        public int Price { get; set; }

        //[Required]
        //[Range(0, int.MaxValue)]
        public int StockInHand { get; set; }

        
        public string Description { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
