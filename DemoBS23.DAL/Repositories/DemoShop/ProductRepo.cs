using DemoBS23.DAL.DatabaseContext;
using DemoBS23.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories.DemoShop
{
    public class ProductRepo : IProductRepo
    {
        private readonly ProductDbContext _productDbContext;

        public ProductRepo(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<Category> AddCategory(Category category)
        {
            try
            {
                await _productDbContext.Categories.AddAsync(category);
                await _productDbContext.SaveChangesAsync();

                return category;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Category> GetCategoryById(int id)
        {
            var categoryFromDb = _productDbContext.Categories.Where(x => x.Id == id).FirstOrDefault();
            return categoryFromDb;
        }



        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                await _productDbContext.Products.AddAsync(product);
                await _productDbContext.SaveChangesAsync();

                return product;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            var productFromDb = _productDbContext.Products.Where(x => x.Id == id).FirstOrDefault();
            return productFromDb;
        }
    }
}
