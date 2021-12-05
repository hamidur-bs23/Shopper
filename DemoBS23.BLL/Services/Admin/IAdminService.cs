using DemoBS23.BLL.Dtos.Admin;
using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.BLL.Services.Admin
{
    public interface IAdminService
    {
        public Task<ResultSet<ICollection<ProductWithStockReadDto>>> GetProductsWithStock();
    }
}
