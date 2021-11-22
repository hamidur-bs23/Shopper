using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories
{
    public class ProductRepo : IProductRepo
    {
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = new List<Product>
            {
                new Product{Id=1, Name="A", Price=100},
                new Product{Id=2, Name="B", Price=200},
                new Product{Id=3, Name="C", Price=300},
            };

            return products;
        }
    }
}
