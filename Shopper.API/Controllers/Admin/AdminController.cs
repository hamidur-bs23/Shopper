//using Shopper.API.Utilities;
//using Shopper.BLL.Dtos.Admin;
//using Shopper.BLL.Services;
//using Shopper.BLL.Services.Admin;
//using Shopper.DAL.Entities;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace Shopper.API.Controllers.Admin
//{

//    [Route("api/admin")]
//    [ApiController]
//    public class AdminController : ControllerBase
//    {
//        private readonly IAdminService _adminService;

//        public AdminController(IAdminService adminService)
//        {
//            _adminService = adminService;
//        }

//        [HttpGet("getAllProductsWithStock")]
//        public async Task<ActionResult<ResultSet<ICollection<ProductWithStockReadDto>>>> GetProductsWithStock()
//        {
//            ResultSet<ICollection<ProductWithStockReadDto>> resultSet = new ResultSet<ICollection<ProductWithStockReadDto>>();
//            try
//            {
//                resultSet = await _adminService.GetProductsWithStock();

//                if (!resultSet.Success)
//                {
//                    throw new Exception("Failed!");
//                }

//                return Ok(resultSet);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(ex.AppExceptionHandler());
//            }

//        }
//    }
//}
