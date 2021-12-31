using Shopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.DAL.Repositories.Admin
{
    public interface IAdminRepo
    {
        public Task<ICollection<Product>> GetProductsWithStock();
    }
}
