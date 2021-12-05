using DemoBS23.DAL.DatabaseContext;
using DemoBS23.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories.DemoShop
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly ProductDbContext _productDbContext;

        public CustomerRepo(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }
        public async Task<Customer> AddCustomer(Customer customer)
        {
            await _productDbContext.Customers.AddAsync(customer);
            await _productDbContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            var customerFromDb = _productDbContext.Customers.Where(x => x.Id == id).Include(x=>x.Orders).FirstOrDefault();

            return customerFromDb;
        }
    }
}
