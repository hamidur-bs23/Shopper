using DemoBS23.DAL.DatabaseContext;
using DemoBS23.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories.DemoShop
{
    public class OrderRepo : IOrderRepo
    {
        private readonly ProductDbContext _productDbContext;

        public OrderRepo(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }
        /*public async Task<void> AddOrder(Order order)
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
        }*/

        public async Task<bool> AddOrderDetails(ICollection<OrderDetail> orderDetails)
        {
            await _productDbContext.OrderDetails.AddRangeAsync(orderDetails);
            if( await _productDbContext.SaveChangesAsync() > 0)
                return true;
            return false;   
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _productDbContext.Orders.AddAsync(order);
            if(await _productDbContext.SaveChangesAsync() > 0)
                return order;
            return null;
        }

        public async Task<Order> GetbyOrderId(int id)
        {
            /*var order = _productDbContext.Orders
                .Where(x=>x.Id == id)
                .Include(x => x.OrderDetails)
                    .ThenInclude(y=>y.Product)
                .Include(x=>x.Customer)
                .FirstOrDefault();*/
            var order = _productDbContext.Orders.Where(x => x.Id == id)
                    .Include(x => x.Customer)
                    .Include(x => x.OrderDetails)
                        .ThenInclude(y => y.Product)
                            .ThenInclude(z => z.Category)
                    .FirstOrDefault();

            return order;
        }

        public async Task<bool> UpdateOrderWithTotal(Order order)
        {
            _productDbContext.Orders.Update(order);
            if (await _productDbContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
