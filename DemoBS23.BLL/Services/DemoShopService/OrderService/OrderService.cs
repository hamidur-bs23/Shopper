using DemoBS23.BLL.Dtos;
using DemoBS23.DAL.Entities;
using DemoBS23.DAL.Repositories.DemoShop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.BLL.Services.DemoShopService.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;

        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public async Task<ResultSet<Order>> AddOrder(OrderCreateDto orderCreateDto)
        {
            ResultSet<Order> resultSet = new ResultSet<Order>();

            Order order = new Order
            {
                CustomerId = orderCreateDto.CustomerId,
                DateCreated = DateTime.Now,
                Total = -1
            };

            await _orderRepo.CreateOrder(order);

            List<OrderDetail> orderDetails = new List<OrderDetail>();
            int CalculatedTotal = 0;

            List<Product> productsWithUpdatedStock = new List<Product>();
            List<string> stockOutErrorMsg = new List<string>();
            string er = "";

            bool isStockOutAny = false;

            try
            {
                foreach (var item in orderCreateDto.ListOfItems)
                {
                    if (item.CurrentStock >= item.Quantity)
                    {
                        productsWithUpdatedStock.Add(new Product
                        {
                            Id = item.ProductId,
                            Quantity = item.CurrentStock - item.Quantity
                        });

                        int subTotal = item.Quantity * item.UnitPrice;
                        CalculatedTotal += subTotal;

                        orderDetails.Add(new OrderDetail
                        {
                            OrderId = order.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            SubTotal = subTotal
                        });
                    }
                    else
                    {
                        stockOutErrorMsg.Add($"Product ({item.ProductId}): has not enough quantity!");
                        isStockOutAny = true;

                        er = er + $"Product({ item.ProductId}): has not enough quantity!";
                    }
                }
                if (isStockOutAny)
                {
                    throw new Exception(er);
                    //throw new Exception(stockOutErrorMsg.ToString());
                }

            }
            catch (Exception)
            {
                throw;
            }
            
            bool isOrderDetailsSaved = await _orderRepo.AddOrderDetails(orderDetails);

            if (isOrderDetailsSaved)
            {
                order.Total = CalculatedTotal;

                if(await _orderRepo.UpdateOrderWithTotal(order) == true)
                {
                    resultSet.Data = order;
                    resultSet.Success = true;
                }
            }

            return resultSet;
        }
    }
}
