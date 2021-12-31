using Shopper.BLL.Dtos;
using Shopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.BLL.Services.DemoShopService.OrderService
{
    public interface IOrderService
    {
        public Task<ResultSet<OrderReadDto>> AddOrder(OrderCreateDto orderCreateDto);
        public Task<ResultSet<OrderReadDto>> GetbyOrderId(int id);
    }
}
