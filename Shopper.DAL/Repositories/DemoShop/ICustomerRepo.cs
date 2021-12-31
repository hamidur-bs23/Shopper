using Shopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.DAL.Repositories.DemoShop
{
    public interface ICustomerRepo
    {
        public Task<Customer> AddCustomer(Customer customer);
        public Task<Customer> GetCustomerById(int id);
    }
}
