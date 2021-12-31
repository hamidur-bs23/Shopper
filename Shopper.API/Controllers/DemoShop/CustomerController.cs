using Shopper.API.Utilities;
using Shopper.BLL.Dtos;
using Shopper.BLL.Services;
using Shopper.BLL.Services.DemoShopService.CustomerService;
using Shopper.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Shopper.API.Controllers.DemoShop
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ResultSet<Customer>>> Add(CustomerCreateDto customerCreateDto)
        {
            ResultSet<Customer> resultSet = new ResultSet<Customer>();
            try
            {
                resultSet = await _customerService.AddCustomer(customerCreateDto);

                if (!resultSet.Success)
                {
                    throw new Exception("Failed!");
                }

                return Ok(resultSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }


        [HttpGet("get/{id}")]
        public async Task<ActionResult<ResultSet<Customer>>> GetCustomerById(int id)
        {
            ResultSet<Customer> resultSet = new ResultSet<Customer>();
            try
            {
                resultSet = await _customerService.GetCustomerById(id);

                if (resultSet.Data == null)
                {
                    return NotFound();
                }

                if (!resultSet.Success)
                {
                    throw new Exception("Failed!");
                }

                return Ok(resultSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }
    }
}
