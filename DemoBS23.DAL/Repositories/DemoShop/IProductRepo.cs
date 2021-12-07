using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories.DemoShop
{
    public interface IProductRepo
    {
        public Task<Category> AddCategory(Category category);
        public Task<Category> GetCategoryById(int id);


        public Task<Product> AddProduct(Product product);
        public Task<Product> GetProductById(int id);
        public Task<ICollection<Product>> GetProductsByListOfIds(ICollection<int> listOfIds);

        public Task<ICollection<Product>> GetAll();
        public Task<Product> Update(Product updateProduct);
        public Task<bool> Delete(int id);
    }
}
