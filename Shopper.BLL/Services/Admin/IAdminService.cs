using Shopper.BLL.Dtos.Admin;
using Shopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.BLL.Services.Admin
{
    public interface IAdminService
    {
        public Task<ResultSet<ICollection<ProductWithStockReadDto>>> GetProductsWithStock();
    }
}
