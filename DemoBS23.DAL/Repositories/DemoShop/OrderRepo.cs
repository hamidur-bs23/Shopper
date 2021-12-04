using DemoBS23.DAL.DatabaseContext;
using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.DAL.Repositories.DemoShop
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ProductDbContext _productDbContext;

        public OrderRepo(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }
        public void AddOrder(Order order)
        {
            _productDbContext.Orders.Add(order);
            _productDbContext.SaveChanges();


            IList<OrderDetail> orderList = new List<OrderDetail>();
            foreach(var item in order.OrderDetails)
            {
                var d = new OrderDetail
                {
                    OrderId = order.Id,

                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    SubTotal = item.SubTotal
                };

                orderList.Add(item);
            }

            _productDbContext.OrderDetails.AddRange(orderList);
            _productDbContext.SaveChanges();
        }

        public Tuple<int, int> GetPriceListOfItems(List<Tuple<int, int>> items)
        {
            List<int> products = new List<int>();
            foreach(var item in items)
            {
                products.Add(item.Item1);
            };

            _productDbContext.Products
        }
    }
}
