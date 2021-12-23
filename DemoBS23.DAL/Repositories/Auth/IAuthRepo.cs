using DemoBS23.DAL.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DemoBS23.DAL.Repositories.Auth
{
    public interface IAuthRepo
    {
        Task<AuthDbResponse<AuthUser>> RegisterAsync(AuthUser newUserToCreate, string password);
        Task<AuthDbResponse<AuthUser>> RegisterAdminAsync(AuthUser newUserToCreate, string password);
        Task<AuthDbResponse<AuthUser>> RegisterManagerAsync(AuthUser newUserToCreate, string password);
        Task<AuthDbResponse<AuthUser>> LoginAsync(AuthUser userForLogin, string password);

        Task<IList<string>> GetUserRolesAsync(AuthUser user);

        Task<AuthDbResponse<AuthRefreshToken>> InsertRefreshTokenAsync(AuthRefreshToken refreshToken);
        Task<AuthDbResponse<AuthRefreshToken>> RetrivedToken(string refreshToken);

        Task<AuthDbResponse<AuthRefreshToken>> UpdateRefreshToken(AuthRefreshToken refreshToken);
        Task<AuthDbResponse<AuthUser>> GetUserAsync();
    }
}
