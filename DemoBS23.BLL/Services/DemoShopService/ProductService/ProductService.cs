using DemoBS23.BLL.Dtos;
using DemoBS23.DAL.Entities;
using DemoBS23.DAL.Repositories.DemoShop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.BLL.Services.DemoShopService.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;

        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<ResultSet<Category>> AddCategory(CategoryCreateDto categoryCreateDto)
        {
            ResultSet<Category> resultSet = new ResultSet<Category>();

            Category category = categoryCreateDto.ToEntity();

            resultSet.Data = await _productRepo.AddCategory(category);

            if (resultSet.Data != null)
            {
                resultSet.Success = true;
            }

            return resultSet;
        }

        public async Task<ResultSet<Category>> GetCategoryById(int id)
        {
            ResultSet<Category> resultSet = new ResultSet<Category>();

            resultSet.Data = await _productRepo.GetCategoryById(id);

            if (resultSet.Data != null)
            {
                resultSet.Success = true;
            }

            return resultSet;
        }





        public async Task<ResultSet<Product>> AddProduct(ProductCreateDto productCreateDto)
        {
            ResultSet<Product> resultSet = new ResultSet<Product>();

            Product product = productCreateDto.ToEntity();

            resultSet.Data = await _productRepo.AddProduct(product);

            if (resultSet.Data != null)
            {
                resultSet.Success = true;
            }

            return resultSet;
        }

        public async Task<ResultSet<Product>> GetProductById(int id)
        {
            ResultSet<Product> resultSet = new ResultSet<Product>();

            resultSet.Data = await _productRepo.GetProductById(id);

            if (resultSet.Data != null)
            {
                resultSet.Success = true;
            }

            return resultSet;
        }
    }
}
