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
using Microsoft.AspNetCore.Http;

namespace Shopper.API.Controllers.DemoShop
{
    [Route("api/product")]
    [Authorize]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
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


        /// <summary>
        /// Add new product
        /// </summary>
        /// <returns>Return newly created product</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /product/add
        ///     {
        ///          "name": "Product - 101",
        ///          "price": 100,
        ///          "quantity": 12,
        ///          "description": "Description-101",
        ///          "categoryId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Success</response>
        /// <response code="400">Incorrect or null input</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        [HttpPost("add")]
        [Produces("application/json")]
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


        /// <summary>
        /// Retrieve single product based on identifier
        /// </summary>
        /// <param name="id">Identifier (Number)</param>
        /// <returns>Retrieve a collection of products</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Incorrect or null input</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
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


        /// <summary>
        /// Retrieve all products
        /// </summary>
        /// <returns>Retrieve a collection of products</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Incorrect or null input</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        //[AllowAnonymous]
        [HttpGet("getAll")]
        [Produces("application/json")]
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
