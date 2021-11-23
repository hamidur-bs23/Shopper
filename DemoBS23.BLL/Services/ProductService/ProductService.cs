using AutoMapper;
using DemoBS23.DAL.Entities;
using DemoBS23.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.BLL.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ResultSet<Product>> AddOne(Product newProduct)
        {
            ResultSet<Product> resultSet = new ResultSet<Product>();
            try
            {
                resultSet.Data = (Product) await _productRepo.AddOne(newProduct);

                if (resultSet.Data != null)
                {
                    resultSet.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultSet.errorMessage = ex.Message;
            }

            return resultSet;
        }

        public async Task<ResultSet<IList<Product>>> GetAll()
        {
            ResultSet<IList<Product>> resultSet = new ResultSet<IList<Product>>();
            try
            {
                resultSet.Data = (IList<Product>) await  _productRepo.GetAll();

                if (resultSet.Data != null)
                {
                    resultSet.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultSet.errorMessage = ex.Message;
            }

            return resultSet;
        }

        public async Task<ResultSet<Product>> GetById(int id)
        {
            ResultSet<Product> resultSet = new ResultSet<Product>();
            try
            {
                resultSet.Data = await _productRepo.GetById(id);

                if (resultSet.Data != null)
                {
                    resultSet.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultSet.errorMessage = ex.Message;
            }
            
            return resultSet;

        }
    }
}
