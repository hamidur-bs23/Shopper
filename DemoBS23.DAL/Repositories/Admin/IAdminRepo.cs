using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories.Admin
{
    public interface IAdminRepo
    {
        public Task<ICollection<Product>> GetProductsWithStock();
    }
}
