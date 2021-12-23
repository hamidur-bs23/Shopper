using DemoBS23.DAL.DatabaseContext;
using DemoBS23.DAL.Entities.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories.Auth
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<AuthUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AuthDbContext _authDbContext;

        public AuthRepo(
            UserManager<AuthUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AuthDbContext authDbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authDbContext = authDbContext;
        }

        public async Task<AuthDbResponse<AuthUser>> RegisterAsync(AuthUser newUserToCreate, string password)
        {
            AuthDbResponse<AuthUser> result = new AuthDbResponse<AuthUser>();

            try
            {
                var userExist = await _userManager.FindByNameAsync(newUserToCreate.UserName);
                var userExistWithEmail = await _userManager.FindByEmailAsync(newUserToCreate.Email);
                //TODO: checking for duplicate email entry
                
                if (userExist != null && userExistWithEmail != null)
                {
                    result.Errors.Add("User already exist");
                    return result;
                }

                var isCreated = await _userManager.CreateAsync(newUserToCreate, password);
                if (isCreated.Succeeded)
                {
                    // Role
                    if (!await _roleManager.RoleExistsAsync(AuthUserRoles.User))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(AuthUserRoles.User));
                    }

                    var userRoles = new List<string> { AuthUserRoles.User };

                    var addRoles = await _userManager.AddToRolesAsync(newUserToCreate, userRoles);
                    //

                    result.Data = newUserToCreate;
                    result.Success = true;
                }
                else
                {
                    result.Errors = isCreated.Errors.Select(err => $"{err.Description} - {err.Code}").ToList();
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public async Task<AuthDbResponse<AuthUser>> RegisterAdminAsync(AuthUser newUserToCreate, string password)
        {
            AuthDbResponse<AuthUser> result = new AuthDbResponse<AuthUser>();

            try
            {
                var userExist = await _userManager.FindByNameAsync(newUserToCreate.UserName);
                if (userExist != null)
                {
                    result.Errors.Add("User already exist");
                    return result;
                }

                var isCreated = await _userManager.CreateAsync(newUserToCreate, password);
                if (isCreated.Succeeded)
                {
                    // Role
                    if (!await _roleManager.RoleExistsAsync(AuthUserRoles.Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(AuthUserRoles.Admin));
                    }
                    if (!await _roleManager.RoleExistsAsync(AuthUserRoles.Manager))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(AuthUserRoles.Manager));
                    }
                    if (!await _roleManager.RoleExistsAsync(AuthUserRoles.User))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(AuthUserRoles.User));
                    }

                    var userRoles = new List<string> { AuthUserRoles.Admin, AuthUserRoles.Manager, AuthUserRoles.User };

                    var addRoles = await _userManager.AddToRolesAsync(newUserToCreate, userRoles);
                    //

                    result.Data = newUserToCreate;
                    result.Success = true;
                }
                else
                {
                    result.Errors = isCreated.Errors.Select(err => $"{err.Description} - {err.Code}").ToList();
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public async Task<AuthDbResponse<AuthUser>> RegisterManagerAsync(AuthUser newUserToCreate, string password)
        {
            AuthDbResponse<AuthUser> result = new AuthDbResponse<AuthUser>();

            try
            {
                var userExist = await _userManager.FindByNameAsync(newUserToCreate.UserName);
                if (userExist != null)
                {
                    result.Errors.Add("User already exist");
                    return result;
                }

                var isCreated = await _userManager.CreateAsync(newUserToCreate, password);
                if (isCreated.Succeeded)
                {
                    // Role
                    if (!await _roleManager.RoleExistsAsync(AuthUserRoles.Manager))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(AuthUserRoles.Manager));
                    }
                    if (!await _roleManager.RoleExistsAsync(AuthUserRoles.User))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(AuthUserRoles.User));
                    }

                    var userRoles = new List<string> { AuthUserRoles.Manager, AuthUserRoles.User };

                    var addRoles = await _userManager.AddToRolesAsync(newUserToCreate, userRoles);
                    //

                    result.Data = newUserToCreate;
                    result.Success = true;
                }
                else
                {
                    result.Errors = isCreated.Errors.Select(err => $"{err.Description} - {err.Code}").ToList();
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public async Task<AuthDbResponse<AuthUser>> LoginAsync(AuthUser userForLogin, string password)
        {
            AuthDbResponse<AuthUser> result = new AuthDbResponse<AuthUser>();

            try
            {
                var existingUser = await _userManager.FindByEmailAsync(userForLogin.Email);
                if (existingUser == null)
                {
                    result.Errors.Add("Please, try with valid Username");
                    return result;
                }

                var isAuthenticateUser = await _userManager.CheckPasswordAsync(existingUser, password);
                if (isAuthenticateUser)
                {
                    result.Data = existingUser;
                    result.Success = true;
                }
                else
                {
                    result.Errors.Add("Please, try with correct password");
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public async Task<AuthDbResponse<AuthUser>> GetUserAsync()
        {
            AuthDbResponse<AuthUser> result = new AuthDbResponse<AuthUser>();

            return result;

        }



        public async Task<IList<string>> GetUserRolesAsync(AuthUser user)
        {
            IList<string> userRoles = null;

            try
            {
                userRoles = await _userManager.GetRolesAsync(user);

                return userRoles;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthDbResponse<AuthRefreshToken>> InsertRefreshTokenAsync(AuthRefreshToken refreshToken)
        {
            var result = new AuthDbResponse<AuthRefreshToken>();
            try
            {
                var insertedRefreshTokenTracking = await _authDbContext.AuthRefreshTokens.AddAsync(refreshToken);
                await _authDbContext.SaveChangesAsync();

                if (insertedRefreshTokenTracking.Entity != null)
                {
                    result.Data = insertedRefreshTokenTracking.Entity;
                    result.Success = true;

                    return result;
                }

                result.Errors = new List<string>()
                {
                    "Refresh Token - Saving Failed"
                };

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<AuthDbResponse<AuthRefreshToken>> RetrivedToken(string refreshToken)
        {
            var result = new AuthDbResponse<AuthRefreshToken>();
            try
            {
                var existedToken = _authDbContext.AuthRefreshTokens
                    .Include(rt => rt.User)
                    .FirstOrDefault(t => t.RefreshToken == refreshToken);

                if (existedToken != null)
                {
                    result.Data = existedToken;
                    result.Success = true;
                }
                else
                {
                    result.Errors = new List<string>()
                    {
                        "RetrivedToken() - Error"
                    };
                }

                return Task.FromResult(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AuthDbResponse<AuthRefreshToken>> UpdateRefreshToken(AuthRefreshToken refreshToken)
        {
            var result = new AuthDbResponse<AuthRefreshToken>();
            try
            {
                var updatedRefreshTokenTracking = _authDbContext.AuthRefreshTokens.Update(refreshToken);
                await _authDbContext.SaveChangesAsync();

                if (updatedRefreshTokenTracking.Entity != null)
                {
                    result.Data = updatedRefreshTokenTracking.Entity;
                    result.Success = true;
                }
                else
                {
                    result.Errors.Add("UpdateToken() - Error");
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
