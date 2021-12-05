using DemoBS23.DAL.Enum;
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

        public OrderStatus? Status { get; set; } = OrderStatus.Pending;


        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


        public IList<OrderDetail> OrderDetails { get; set; }



    }
}
