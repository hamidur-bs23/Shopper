using Shopper.BLL.Dtos;
using Shopper.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.BLL.Services.DemoShopService.ProductService
{
    public interface IProductService
    {
        public Task<ResultSet<Category>> AddCategory(CategoryCreateDto categoryCreateDto);
        public Task<ResultSet<Category>> GetCategoryById(int id);

        public Task<ResultSet<ICollection<CategoryReadDto>>> GetAllCategories();


        public Task<ResultSet<Product>> AddProduct(ProductCreateDto productCreateDto);
        public Task<ResultSet<Product>> GetProductById(int id);


        public Task<ResultSet<ICollection<ProductReadDto>>> GetAll();

        public Task<ResultSet<ProductReadDto>> Update(int id, ProductCreateDto updateProduct);

        public Task<ResultSet<bool>> Delete(int id);
    }
}
