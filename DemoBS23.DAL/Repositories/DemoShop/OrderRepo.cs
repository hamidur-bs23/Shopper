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
            /*if (await _productDbContext.SaveChangesAsync() > 0)
                return true;
            return false;*/
            //TODO: how to check if the data saved/modified?
            await _productDbContext.SaveChangesAsync();
            return true;
        }
    }
}
