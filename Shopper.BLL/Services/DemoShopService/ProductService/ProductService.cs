﻿using Shopper.BLL.Dtos;
using Shopper.DAL.Entities;
using Shopper.DAL.Repositories.DemoShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.BLL.Services.DemoShopService.ProductService
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

            if (resultSet != null)
            {
                resultSet.Success = true;
            }

            return resultSet;
        }


        public async Task<ResultSet<ICollection<CategoryReadDto>>> GetAllCategories()
        {
            ResultSet<ICollection<CategoryReadDto>> resultSet = new ResultSet<ICollection<CategoryReadDto>>();

            var fromDb = await _productRepo.GetAllCategories();

            var data = fromDb.Select(x => x.ToReadDto()).ToList();

            resultSet.Data = data;

            if (resultSet.Data != null)
            {
                resultSet.Success = true;
            }

            return resultSet;
        }


        public async Task<ResultSet<CategoryReadDto>> UpdateCategory(int id, CategoryCreateDto categoryCreateDtoForUpdate)
        {
            ResultSet<CategoryReadDto> resultSet = new ResultSet<CategoryReadDto>();
            try
            {
                Category existedCategory = await _productRepo.GetCategoryById(id);

                if (existedCategory == null)
                {
                    resultSet.errorMessage = "Not found";
                    return resultSet;
                }

                existedCategory.Name = categoryCreateDtoForUpdate.Name;

                Category updatedCategory = await _productRepo.UpdateCategory(existedCategory);

                if (updatedCategory == null)
                {
                    resultSet.errorMessage = "Update failed";
                    return resultSet;
                }

                CategoryReadDto updatedCategoryReadDto = updatedCategory.ToReadDto();

                resultSet.Data = updatedCategoryReadDto;
                resultSet.Success = true;
                
                return resultSet;
            }
            catch (Exception ex)
            {
                resultSet.errorMessage = ex.Message;
                return resultSet;
            }
        }    
        
        public async Task<ResultSet<bool>> DeleteCategory(int id)
        {
            ResultSet<bool> resultSet = new ResultSet<bool>();
            try
            {
                Category existedCategory = await _productRepo.GetCategoryById(id);
                if(existedCategory == null)
                {
                    resultSet.errorMessage = "No category exists";
                    //return resultSet;
                }
                else
                {
                    try
                    {
                        var isDeleted = await _productRepo.DeleteCategory(existedCategory);

                        if (isDeleted)
                        {
                            resultSet.Success = true;
                            //return resultSet;
                        }
                    } catch (Exception ex)
                    {
                        throw;
                    }
                }                    
            }
            catch (Exception ex)
            {
                resultSet.errorMessage = ex.Message;
                return resultSet;
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


        public async Task<ResultSet<ICollection<ProductReadDto>>> GetAll()
        {
            ResultSet<ICollection<ProductReadDto>> resultSet = new ResultSet<ICollection<ProductReadDto>>();

            var fromDb = await _productRepo.GetAll();

            var data = fromDb.Select(x => x.ToReadDto()).ToList();
            resultSet.Data = data;

            if (resultSet.Data != null)
            {
                resultSet.Success = true;
            }

            return resultSet;
        }


        public async Task<ResultSet<ProductReadDto>> Update(int id, ProductCreateDto updateProduct)
        {
            ResultSet<ProductReadDto> resultSet = new ResultSet<ProductReadDto>();
            try
            {
                Product existedProduct = await _productRepo.GetProductById(id);
                if (existedProduct == null)
                {
                    resultSet.errorMessage = "Something wrong to find the desired product for update!";
                    return resultSet;
                }

                existedProduct.Name = updateProduct.Name;
                existedProduct.Price = updateProduct.Price;
                existedProduct.StockInHand = updateProduct.Quantity;
                existedProduct.Description = updateProduct.Description;
                existedProduct.CategoryId = updateProduct.CategoryId;

                Console.WriteLine("Existed Product - ", existedProduct);

                try
                {
                    var updatedProduct = await _productRepo.Update(existedProduct);
                    Console.WriteLine("Updated Product - ", updatedProduct);

                    if (updatedProduct != null)
                    {
                        resultSet.Data = updatedProduct.ToReadDto();
                        resultSet.Success = true;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultSet.errorMessage = ex.Message;
                return resultSet;
            }

            return resultSet;
        }

        public async Task<ResultSet<bool>> Delete(int id)
        {
            ResultSet<bool> resultSet = new ResultSet<bool>();
            try
            {
                Product existedProduct = await _productRepo.GetProductById(id);
                if (existedProduct == null)
                {
                    resultSet.errorMessage = "No product exists";
                    return resultSet;
                }

                try
                {
                    var isDeleted = await _productRepo.Delete(id);
                    if(isDeleted == true)
                    {
                        resultSet.Success = true;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                resultSet.errorMessage = ex.Message;
                return resultSet;
            }

            return resultSet;
        }
    }
}
