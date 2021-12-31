using Shopper.API.Utilities;
using Shopper.BLL.Dtos;
using Shopper.BLL.Services;
using Shopper.BLL.Services.DemoShopService.ProductService;
using Shopper.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shopper.API.Controllers.DemoShop
{
    [Authorize]
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("category/add")]
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
        
       
        [HttpGet("category/get/{id}")]
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

        [HttpGet("category/getAll")]
        public async Task<ActionResult<ResultSet<ICollection<CategoryReadDto>>>> GetAllCategories()
        {
            ResultSet<ICollection<CategoryReadDto>> resultSet = new ResultSet<ICollection<CategoryReadDto>>();
            try
            {
                resultSet = await _productService.GetAllCategories();

                if (resultSet.Data == null || resultSet.Success == false)
                {
                    return NotFound();
                }

                return Ok(resultSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }




        [HttpPost("add")]
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

        [HttpGet("get/{id}")]
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



        //[AllowAnonymous]
        [HttpGet("getAll")]
        public async Task<ActionResult<ResultSet<ICollection<ProductReadDto>>>> GetAll()
        {
            ResultSet<ICollection<ProductReadDto>> resultSet = new ResultSet<ICollection<ProductReadDto>>();
            try
            {
                resultSet = await _productService.GetAll();

                if (resultSet.Data == null || resultSet.Success == false)
                {
                    return NotFound();
                }

                return Ok(resultSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }


        [HttpPut("update/{id}")]
        public async Task<ActionResult<ResultSet<ProductReadDto>>> Update(int id, ProductCreateDto updateProduct)
        {
            ResultSet<ProductReadDto> resultSet = new ResultSet<ProductReadDto>();
            try
            {
                resultSet = await _productService.Update(id, updateProduct);

                if (resultSet.Success == false)
                {
                    return NotFound();
                }

                return Ok(resultSet);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }


        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ResultSet<bool>>> Delete(int id)
        {
            ResultSet<bool> resultSet = new ResultSet<bool>();
            try
            {
                resultSet = await _productService.Delete(id);

                if (resultSet.Success == false)
                {
                    return NotFound();
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
