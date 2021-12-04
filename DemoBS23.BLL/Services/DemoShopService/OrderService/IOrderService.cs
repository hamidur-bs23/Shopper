using DemoBS23.BLL.Dtos;
using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.BLL.Services.DemoShopService.OrderService
{
    public interface IOrderService
    {
        public Order AddOrder(OrderCreateDto orderCreateDto);
    }
}
