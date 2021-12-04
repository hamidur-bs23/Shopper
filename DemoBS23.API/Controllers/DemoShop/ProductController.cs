using DemoBS23.API.Utilities;
using DemoBS23.BLL.Dtos;
using DemoBS23.BLL.Services;
using DemoBS23.BLL.Services.DemoShopService.ProductService;
using DemoBS23.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DemoBS23.API.Controllers.DemoShop
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("Category/Add")]
        public async Task<ActionResult<ResultSet<Category>>> AddCategory(CategoryCreateDto categoryCreateDto)
        {
            ResultSet<Category> resultSet = new ResultSet<Category>();
            try
            {
                resultSet = await _productService.AddCategory(categoryCreateDto);

                if (!resultSet.Success)
                {
                    throw new Exception("Failed!");
                }

                return Ok(resultSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }
        
       
        [HttpGet("Category/GetById{id}")]
        public async Task<ActionResult<ResultSet<Category>>> GetCategoryById(int id)
        {
            ResultSet<Category> resultSet = new ResultSet<Category>();
            try
            {
                resultSet = await _productService.GetCategoryById(id);

                if (resultSet.Data == null)
                {
                    return NotFound();
                }

                if (!resultSet.Success)
                {
                    throw new Exception("Failed!");
                }

                return Ok(resultSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }


        [HttpPost("Add")]
        public async Task<ActionResult<ResultSet<Product>>> AddProduct(ProductCreateDto productCreateDto)
        {
            ResultSet<Product> resultSet = new ResultSet<Product>();
            try
            {
                resultSet = await _productService.AddProduct(productCreateDto);

                if (!resultSet.Success)
                {
                    throw new Exception("Failed!");
                }

                return Ok(resultSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }

        [HttpGet("GetById{id}")]
        public async Task<ActionResult<ResultSet<Product>>> GetProductById(int id)
        {
            ResultSet<Product> resultSet = new ResultSet<Product>();
            try
            {
                resultSet = await _productService.GetProductById(id);

                if (resultSet.Data == null)
                {
                    return NotFound();
                }

                if (!resultSet.Success)
                {
                    throw new Exception("Failed!");
                }

                return Ok(resultSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }
    }
}
