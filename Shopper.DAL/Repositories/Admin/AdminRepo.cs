using Shopper.DAL.DatabaseContext;
using Shopper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.DAL.Repositories.Admin
{
    public class AdminRepo : IAdminRepo
    {
        private readonly ProductDbContext _productDbContext;

        public AdminRepo(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }
        public async Task<ICollection<Product>> GetProductsWithStock()
        {
            try
            {
                var data = _productDbContext.Products.Include(x=>x.Category).ToList();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
