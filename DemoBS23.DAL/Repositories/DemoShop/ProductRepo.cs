﻿using DemoBS23.DAL.DatabaseContext;
using DemoBS23.DAL.Entities;
using Microsoft.EntityFrameworkCore;
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
            var categoryFromDb = _productDbContext.Categories.Include(x=>x.Products).Where(x => x.Id == id).FirstOrDefault();
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



        public async Task<ICollection<Product>> GetAll()
        {
            var productsFromDb = _productDbContext.Products.Include(x => x.Category).ToList();

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