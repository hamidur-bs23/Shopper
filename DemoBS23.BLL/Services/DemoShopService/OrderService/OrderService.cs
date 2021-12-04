using DemoBS23.BLL.Dtos;
using DemoBS23.DAL.Entities;
using DemoBS23.DAL.Repositories.DemoShop;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.BLL.Services.DemoShopService.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;

        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public Order AddOrder(OrderCreateDto orderCreateDto)
        {
            //TODO: generate OrderDetails
            int subT = 0;

            var ListOfItemsForPrice = new List<Tuple<int, int>>();

            foreach(var item in orderCreateDto.ListOfItemsWithQuantity)
            {
                ListOfItemsForPrice.Add(Tuple.Create(item.ProductId, -1));
            }

        }
    }
}
