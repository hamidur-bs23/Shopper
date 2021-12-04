using DemoBS23.BLL.Dtos;
using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.BLL.Services.DemoShopService.CustomerService
{
    public interface ICustomerService
    {
        public Task<ResultSet<Customer>> AddCustomer(CustomerCreateDto customerCreateDto);
        public Task<ResultSet<Customer>> GetCustomerById(int id);
    }
}
