using Shopper.API.Utilities;
using Shopper.BLL.Dtos;
using Shopper.BLL.Services;
using Shopper.BLL.Services.DemoShopService.OrderService;
using Shopper.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Shopper.API.Controllers.DemoShop
{

    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ResultSet<OrderReadDto>>> Add(OrderCreateDto orderCreateDto)
        {
            var resultSet = new ResultSet<OrderReadDto>();
            try
            {
                resultSet = await _orderService.AddOrder(orderCreateDto);

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
        public async Task<ActionResult<ResultSet<Order>>> GetbyOrderId(int id)
        {
            ResultSet<OrderReadDto> resultSet = new ResultSet<OrderReadDto>();
            try
            {
                resultSet = await _orderService.GetbyOrderId(id);

                if(resultSet == null)
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
