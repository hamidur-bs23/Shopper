
using DemoBS23.API.Utilities;
using DemoBS23.BLL.Services;
using DemoBS23.BLL.Services.ProductService;
using DemoBS23.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBS23.API.Controllers
{
    [ApiController]
    [Route("api/Product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Sorry");
            }

            ResultSet<IList<Product>> result;

            try
            {
                result = await _productService.GetAll();
                if (result.Data != null)
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Sorry");
            }

            ResultSet<Product> result;

            try
            {
                result = await _productService.GetById(id);
                if (result.Data != null && result.Success == true)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> AddOne(Product newProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Sorry");
            }

            ResultSet<Product> result;

            try
            {
                result = await _productService.AddOne(newProduct);
                if (result.Data != null && result.Success == true)
                {
                    return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }

        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product updateProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Sorry");
            }

            try
            {
                var isUpdated = await _productService.Update(id, updateProduct);
                if (isUpdated)
                {
                    return NoContent();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }

        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Sorry");
            }

            try
            {
                var isDeleted = await _productService.Delete(id);
                if (isDeleted)
                {
                    return NoContent();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.AppExceptionHandler());
            }
        }


    }
}
