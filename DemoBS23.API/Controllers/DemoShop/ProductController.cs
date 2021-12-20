using DemoBS23.API.Utilities;
using DemoBS23.BLL.Dtos;
using DemoBS23.BLL.Services;
using DemoBS23.BLL.Services.DemoShopService.ProductService;
using DemoBS23.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoBS23.API.Controllers.DemoShop
{
    //[Authorize]
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



        [AllowAnonymous]
        [HttpGet("getall")]
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
