using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shopper.DAL.Entities
{
    public class Customer
    {
        //[Key]
        public int Id { get; set; }
     
        //[Required]
        public string Name { get; set; }
        
        //[Required]
        public string ContactNumber { get; set; }



        public ICollection<Order> Orders { get; set; }


    }
}
