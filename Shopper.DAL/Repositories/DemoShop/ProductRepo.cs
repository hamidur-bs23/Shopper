using Shopper.DAL.DatabaseContext;
using Shopper.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.DAL.Repositories.DemoShop
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
            var categoryFromDb = _productDbContext.Categories.Include(x=>x.Products).Where(x => x.Id == id).FirstOrDefault();
            return categoryFromDb;
        }


        public async Task<ICollection<Category>> GetAllCategories()
        {
            var dataFromDb = _productDbContext.Categories
                .Include(x => x.Products)
                .OrderBy(x => x.Name)
                .ToList();
            return dataFromDb;
        }


        public async Task<Category> UpdateCategory(Category categoryForUpdate)
        {
            try
            {
                Category updatedCategory = _productDbContext.Categories.Update(categoryForUpdate).Entity;
                if(await _productDbContext.SaveChangesAsync() > 0)
                {
                    return updatedCategory;
                }
                else
                {
                    return null;
                }
            } catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> DeleteCategory(Category categoryForDelete)
        {
            _productDbContext.Categories.Remove(categoryForDelete);
            if (await _productDbContext.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
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


        public async Task<ICollection<Product>> GetProductsByListOfIds(ICollection<int> listOfIds)
        {
            var data = _productDbContext.Products
                .Where(e => listOfIds.Contains(e.Id))
                .Include(e=>e.Category)
                .Distinct()
                .ToList();

            if(data.Count > 0){
                return data; 
            }
            return null;
        }


        public async Task<ICollection<Product>> GetAll()
        {
            var productsFromDb = _productDbContext.Products
                .Include(x => x.Category)
                .OrderByDescending(x => x.CreatedOn)
                .ToList();

            return productsFromDb;
        }


        public async Task<Product> Update(Product updateProduct)
        {
            Product updatedProduct = _productDbContext.Products.Update(updateProduct).Entity;
            await _productDbContext.SaveChangesAsync();

            Product updatedProductWithCategory = _productDbContext.Products.Where(x => x.Id == updatedProduct.Id).Include(x => x.Category).FirstOrDefault();

            return updatedProductWithCategory;
        }


        public async Task<bool> Delete(int id)
        {
            var productFromDb = await GetProductById(id);
            if(productFromDb != null)
            {
                _productDbContext.Products.Remove(productFromDb);
                if(await _productDbContext.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }
            return false;
        }
    
    }
}
