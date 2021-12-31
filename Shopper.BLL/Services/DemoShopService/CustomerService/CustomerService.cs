using Shopper.BLL.Dtos;
using Shopper.DAL.Entities;
using Shopper.DAL.Repositories.DemoShop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.BLL.Services.DemoShopService.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<ResultSet<Customer>> AddCustomer(CustomerCreateDto customerCreateDto)
        {
            ResultSet<Customer> resultSet = new ResultSet<Customer>();

            Customer customer = customerCreateDto.ToEntity();

            resultSet.Data = await _customerRepo.AddCustomer(customer);

            if (resultSet.Data != null)
            {
                resultSet.Success = true;
            }

            return resultSet;
        }

        public async Task<ResultSet<Customer>> GetCustomerById(int id)
        {
            ResultSet<Customer> resultSet = new ResultSet<Customer>();

            resultSet.Data = await _customerRepo.GetCustomerById(id);

            if (resultSet.Data != null)
            {
                resultSet.Success = true;
            }

            return resultSet;
        }
    }
}
