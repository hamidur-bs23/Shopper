using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories.DemoShop
{
    public interface IOrderRepo
    {
        public Task<Order> CreateOrder(Order order);
        public Task<bool> AddOrderDetails(ICollection<OrderDetail> orderDetails);
        public Task<bool> UpdateOrderWithTotal(Order order);

        
    }
}
