using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int Total { get; set; }



        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


        public ICollection<OrderDetail> OrderDetails { get; set; }



    }
}
