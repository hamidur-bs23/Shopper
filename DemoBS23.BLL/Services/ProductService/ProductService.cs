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

        public async Task<ResultSet<IList<Product>>> GetAllProducts()
        {
            //throw new NotImplementedException();
            ResultSet<IList<Product>> resultSet = new ResultSet<IList<Product>>();

            var fromDB = (IList<Product>)await _productRepo.GetAllProducts();

            resultSet.Data = _mapper.Map<IList<Product>>(fromDB);

            return resultSet;
        }
    }
}
