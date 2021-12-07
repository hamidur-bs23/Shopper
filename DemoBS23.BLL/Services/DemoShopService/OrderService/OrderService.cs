using DemoBS23.BLL.Dtos;
using DemoBS23.DAL.Entities;
using DemoBS23.DAL.Repositories.DemoShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DemoBS23.BLL.Services.DemoShopService.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IProductRepo _productRepo;

        public OrderService(IOrderRepo orderRepo, IProductRepo productRepo)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }
        public async Task<ResultSet<OrderReadDto>> AddOrder(OrderCreateDto orderCreateDto)
        {
            var resultSet = new ResultSet<OrderReadDto>();

            Order order = orderCreateDto.ToEntity();

            try
            {
                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        await _orderRepo.CreateOrder(order);

                        var orderDetails = new List<OrderDetail>();
                        int CalculatedTotal = 0;
                        string errorMsgsForStockOutAny = "";
                        bool isStockOutAny = false;


                        var selecteProductIds = new List<int>();
                        foreach (var item in orderCreateDto.ListOfItems)
                        {
                            if (selecteProductIds.Contains(item.ProductId) == false)
                            {
                                selecteProductIds.Add(item.ProductId);
                            }
                        }

                        var selectedProducts = await _productRepo.GetProductsByListOfIds(selecteProductIds);

                        try
                        {
                            var onlyDistinctItems = new List<int>();
                            foreach (var item in orderCreateDto.ListOfItems)
                            {
                                var productFromDb = selectedProducts.Where(e => e.Id == item.ProductId).FirstOrDefault();

                                if (productFromDb.StockInHand >= item.Quantity)
                                {
                                    productFromDb.StockInHand -= item.Quantity;

                                    var subTotal = productFromDb.Price * item.Quantity;
                                    CalculatedTotal += subTotal;

                                    var newOrderDetail = item.ToEntity();
                                    newOrderDetail.SubTotal = subTotal;
                                    newOrderDetail.OrderId = order.Id;
                                    newOrderDetail.UnitPrice = productFromDb.Price;
                                    orderDetails.Add(newOrderDetail);
                                }
                                else
                                {
                                    isStockOutAny = true;

                                    errorMsgsForStockOutAny += $"Product [ Id = {item.ProductId} ] has not enough quantity!  ";
                                }

                            }

                            if (isStockOutAny)
                            {
                                throw new Exception(errorMsgsForStockOutAny);
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
                                var data = new OrderReadDto();
                                try
                                {
                                    data = order.ToReadDto();
                                }
                                catch (Exception)
                                {
                                    throw;
                                }

                                resultSet.Data = data;
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

            if (dataFromDb != null)
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
