using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories.DemoShop
{
    public interface ICustomerRepo
    {
        public Task<Customer> AddCustomer(Customer customer);
        public Task<Customer> GetCustomerById(int id);
    }
}
