using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.DAL.Entities
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int SubTotal { get; set; }


        public int OrderId { get; set; }
        public Order Order { get; set; }


        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
