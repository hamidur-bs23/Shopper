using DemoBS23.API.Utilities;
using DemoBS23.BLL.Dtos.Admin;
using DemoBS23.BLL.Services;
using DemoBS23.BLL.Services.Admin;
using DemoBS23.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoBS23.API.Controllers.Admin
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("GetAllProductsWithStock")]
        public async Task<ActionResult<ResultSet<ICollection<ProductWithStockReadDto>>>> GetProductsWithStock()
        {
            ResultSet<ICollection<ProductWithStockReadDto>> resultSet = new ResultSet<ICollection<ProductWithStockReadDto>>();
            try
            {
                resultSet = await _adminService.GetProductsWithStock();

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
