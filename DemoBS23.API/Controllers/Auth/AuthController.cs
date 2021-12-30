using DemoBS23.BLL.Dtos.Auth;
using DemoBS23.BLL.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoBS23.API.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] AuthUserRegistrationCreateDto userToCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                AuthResultSet result = await _authService.RegistrationAsync(userToCreate);
                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception exception)
            {
                throw new Exception("ERROR: ", exception);
            }
        }

        /*[Route("registerAdmin")]
        [HttpPost]
        public async Task<IActionResult> RegisterAdminAsync([FromBody] AuthUserRegistrationCreateDto userToCreate)
        {
            try
            {
                AuthResultSet result = await _authService.RegistrationAdminAsync(userToCreate);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Try again later. Bad Request");
            }
            catch (Exception exception)
            {
                throw new Exception("ERROR: ", exception);
            }
        }


        [Route("registerManager")]
        [HttpPost]
        public async Task<IActionResult> RegisterManagerAsync([FromBody] AuthUserRegistrationCreateDto userToCreate)
        {
            try
            {
                AuthResultSet result = await _authService.RegistrationManagerAsync(userToCreate);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Try again later. Bad Request");
            }
            catch (Exception exception)
            {
                throw new Exception("ERROR: ", exception);
            }
        }
*/

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] AuthUserLoginCreateDto model)
        {
            try
            {
                AuthResultSet result = await _authService.LoginAsync(model);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest("Try again later. Bad Request");
            }
            catch (Exception exception)
            {
                throw new Exception("ERROR: " + exception.Message, exception);
            }
        }


        /*[Route("RefreshToken")]
        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] AuthRefreshTokenCreateDto model)
        {
            var result = new AuthResultSet();

            if (ModelState.IsValid)
            {
                try
                {
                    result = await _authService.RefreshAndGenerateTokenAsync(model);

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    result.Errors = new List<string>();

                    var inner = ex.InnerException;
                    while (inner != null)
                    {
                        result.Errors.Add(inner.StackTrace);
                        inner = inner.InnerException;
                    }
                    return BadRequest(result);
                }
            }
            else
            {
                result.Errors = new List<string>()
                {
                    "Something wrong on Controller"
                };

                return BadRequest(result);
            }

        }*/

        [Authorize]
        [Route("getUser")]
        [HttpGet]
        public async Task<IActionResult> GetUserAsync()
        {
            
            try
            {
                if(Request.Headers.TryGetValue("Authorization", out var authorizationToken))
                {
                    Request.Headers.TryGetValue("email", out var email);
                    return Ok(new {
                        Email = email
                    });
                }

                return BadRequest("Try again later. Bad Request");
            }
            catch (Exception exception)
            {
                throw new Exception("ERROR: " + exception.Message, exception);
            }
        }


    }
}
