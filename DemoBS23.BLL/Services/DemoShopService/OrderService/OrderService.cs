﻿using DemoBS23.BLL.Dtos;
using DemoBS23.DAL.Entities;
using DemoBS23.DAL.Repositories.DemoShop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

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

            //TODO: mapping extension?
            Order order = new Order
            {
                CustomerId = orderCreateDto.CustomerId,
                DateCreated = DateTime.Now,
                Total = 0,
                Status = orderCreateDto.Status
            };

            try
            {
                using(var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        await _orderRepo.CreateOrder(order);

                        List<OrderDetail> orderDetails = new List<OrderDetail>();
                        int CalculatedTotal = 0;

                        List<Product> productsWithUpdatedStock = new List<Product>();
                        string er = "";

                        bool isStockOutAny = false;

                        try
                        {
                            foreach (var item in orderCreateDto.ListOfItems)
                            {
                                if (item.CurrentStock >= item.Quantity)
                                {
                                    //TODO: mapping Extension?
                                    productsWithUpdatedStock.Add(new Product
                                    {
                                        Id = item.ProductId,
                                        Quantity = item.CurrentStock - item.Quantity
                                    });
                                    //TODO: store only productId in a list for getting products from db,
                                    // then modify these products and save

                                    int subTotal = item.Quantity * item.UnitPrice;
                                    CalculatedTotal += subTotal;

                                    // TODO: Before adding OrderDetail in the list, checkig should be done for availability
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
                                    //stockOutErrorMsg.Add($"Product ({item.ProductId}): has not enough quantity!");
                                    isStockOutAny = true;

                                    er = er + $"Product({ item.ProductId}): has not enough quantity!";
                                }
                            }

                            if (isStockOutAny)
                            {
                                throw new Exception(er);
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

                            if (await _orderRepo.UpdateOrderWithTotal(order) == true)
                            {
                                resultSet.Data = order;
                                resultSet.Success = true;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        transactionScope.Complete();
                    }
                }
            }
            catch (TransactionAbortedException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

            return resultSet;
        }

        public async Task<ResultSet<OrderReadDto>> GetbyOrderId(int id)
        {
            ResultSet<OrderReadDto> resultSet = new ResultSet<OrderReadDto>();

            var dataFromDb = await _orderRepo.GetbyOrderId(id);

            if(dataFromDb != null)
            {
                try
                {
                    OrderReadDto data = dataFromDb.ToReadDto();
                    if (data != null)
                    {
                        resultSet.Data = data;
                        resultSet.Success = true;
                    }
                    return resultSet;
                }
                catch (Exception)
                {
                    throw;
                }
            }            
                       
            return null;
        }
    }
}
