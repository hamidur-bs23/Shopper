using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.BLL.Services.ProductService
{
    public interface IProductService
    {
        public Task<ResultSet<IList<Product>>> GetAll();

        public Task<ResultSet<Product>> GetById(int id);
    }
}
