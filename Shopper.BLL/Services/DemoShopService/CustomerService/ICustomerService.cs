using Shopper.BLL.Dtos;
using Shopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.BLL.Services.DemoShopService.CustomerService
{
    public interface ICustomerService
    {
        public Task<ResultSet<Customer>> AddCustomer(CustomerCreateDto customerCreateDto);
        public Task<ResultSet<Customer>> GetCustomerById(int id);
    }
}
