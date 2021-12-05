using DemoBS23.API.Utilities;
using DemoBS23.BLL.Dtos;
using DemoBS23.BLL.Services;
using DemoBS23.BLL.Services.DemoShopService.OrderService;
using DemoBS23.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DemoBS23.API.Controllers.DemoShop
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult<ResultSet<Order>>> Add(OrderCreateDto orderCreateDto)
        {
            ResultSet<Order> resultSet = new ResultSet<Order>();
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
    }
}
