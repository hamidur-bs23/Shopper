using DemoBS23.BLL.Dtos;
using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.BLL.Services.DemoShopService.OrderService
{
    public interface IOrderService
    {
        public Task<ResultSet<Order>> AddOrder(OrderCreateDto orderCreateDto);
    }
}
