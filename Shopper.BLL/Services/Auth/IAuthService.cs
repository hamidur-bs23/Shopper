using Shopper.BLL.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.BLL.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthResultSet> RegistrationAsync(AuthUserRegistrationCreateDto model);
        Task<AuthResultSet> RegistrationAdminAsync(AuthUserRegistrationCreateDto model);
        Task<AuthResultSet> RegistrationManagerAsync(AuthUserRegistrationCreateDto model);
        Task<AuthResultSet> LoginAsync(AuthUserLoginCreateDto model);
        Task<AuthResultSet> RefreshAndGenerateTokenAsync(AuthRefreshTokenCreateDto refreshToken);
        Task<AuthResultSet> GetUserAsync();
    }
}
