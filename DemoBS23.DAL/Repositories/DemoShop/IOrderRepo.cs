using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories.DemoShop
{
    public interface IOrderRepo
    {
        //public Task<Order> AddOrders();
        //public Task<Customer> GetCustomerById(i

        public void AddOrder(Order order);
    }
}
