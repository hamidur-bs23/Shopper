using DemoBS23.BLL.Dtos.Admin;
using DemoBS23.DAL.Entities;
using DemoBS23.DAL.Repositories.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.BLL.Services.Admin
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;

        public AdminService(IAdminRepo adminRepo)
        {
            _adminRepo = adminRepo;
        }
        public async Task<ResultSet<ICollection<ProductWithStockReadDto>>> GetProductsWithStock()
        {
            ResultSet<ICollection<ProductWithStockReadDto>> resultSet = new ResultSet<ICollection<ProductWithStockReadDto>>();

            var dataFromDb = await _adminRepo.GetProductsWithStock();

            var data = dataFromDb.Select(x => x.ToEntity()).ToList();

            resultSet.Data = data;

            if (resultSet.Data != null)
            {
                resultSet.Success = true;
            }

            return resultSet;
        }
    }
}
