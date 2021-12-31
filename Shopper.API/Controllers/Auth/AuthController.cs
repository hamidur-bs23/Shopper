using Shopper.BLL.Dtos.Auth;
using Shopper.BLL.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopper.API.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        /// <summary>
        /// Register a new user who can login this application
        /// </summary>
        /// <returns>Return JWT token</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/register
        ///     {
        ///          "userName": "user101",
        ///          "email": "user101@mail.com",
        ///          "password": "Pass@1234"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Success</response>
        /// <response code="400">Incorrect or null input</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Server Error</response>
        [HttpPost("register")]
        [Produces("application/json")]
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


        /// <summary>
        /// Login to the application
        /// </summary>
        /// <returns>Return JWT token</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /auth/login
        ///     {
        ///          "email": "user101@mail.com",
        ///          "password": "Pass@1234"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Success</response>
        /// <response code="400">Incorrect or null input</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Server Error</response>
        [HttpPost("login")]
        [Produces("application/json")]
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


        /// <summary>
        /// Retrieve logged-in user's profile
        /// </summary>
        /// <returns>Return logged-in user information</returns>
        /// <remarks>
        /// <response code="201">Success</response>
        /// <response code="400">Incorrect or null input</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Server Error</response>
        [HttpGet("getUser")]
        [Produces("application/json")]
        [Authorize]
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
