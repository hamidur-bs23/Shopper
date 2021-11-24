using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories
{
    public interface IProductRepo
    {
        public Task<IEnumerable<Product>> GetAll();
        public Task<Product> GetById(int id);

        public Task<Product> AddOne(Product newProduct);

        public Task<Product> Update(Product newProduct);

    }
}
