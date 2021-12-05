using DemoBS23.API.Utilities;
using DemoBS23.BLL.Dtos;
using DemoBS23.BLL.Services;
using DemoBS23.BLL.Services.DemoShopService.CustomerService;
using DemoBS23.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DemoBS23.API.Controllers.DemoShop
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("Add")]
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


        [HttpGet("GetById{id}")]
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
