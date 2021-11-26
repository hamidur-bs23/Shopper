using DemoBS23.BLL.Dtos.Auth;
using DemoBS23.DAL.Entities.Auth;
using DemoBS23.DAL.Repositories.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DemoBS23.BLL.AppConfig;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;

namespace DemoBS23.BLL.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;
        private readonly TokenValidationParameters _tokenValidationParams;
        private readonly JwtConfig _jwtConfig;

        public AuthService(IAuthRepo authRepo, IOptionsMonitor<JwtConfig> jwtConfig, TokenValidationParameters tokenValidationParams)
        {
            _authRepo = authRepo;
            _jwtConfig = jwtConfig.CurrentValue;
            _tokenValidationParams = tokenValidationParams;
        }

        public async Task<AuthResultSet> RegistrationAsync(AuthUserRegistrationCreateDto model)
        {
            AuthResultSet result = new AuthResultSet();

            AuthUser authUser = new AuthUser
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var resultFromDb = await _authRepo.RegisterAsync(authUser, model.Password);

            if (resultFromDb.Success)
            {
                result = await GenerateJwtToken(resultFromDb.Data);

                result.Success = true;
            }
            else
            {
                result.Errors = resultFromDb.Errors;
            }

            return result;
        }

        public async Task<AuthResultSet> RegistrationAdminAsync(AuthUserRegistrationCreateDto model)
        {
            AuthResultSet result = new AuthResultSet();

            AuthUser authUser = new AuthUser
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var resultFromDb = await _authRepo.RegisterAdminAsync(authUser, model.Password);

            if (resultFromDb.Success)
            {
                result = await GenerateJwtToken(resultFromDb.Data);

                result.Success = true;
            }
            else
            {
                result.Errors = resultFromDb.Errors;
            }

            return result;
        }

        public async Task<AuthResultSet> RegistrationManagerAsync(AuthUserRegistrationCreateDto model)
        {
            AuthResultSet result = new AuthResultSet();

            AuthUser authUser = new AuthUser
            {
                UserName = model.UserName,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var resultFromDb = await _authRepo.RegisterManagerAsync(authUser, model.Password);

            if (resultFromDb.Success)
            {
                result = await GenerateJwtToken(resultFromDb.Data);

                result.Success = true;
            }
            else
            {
                result.Errors = resultFromDb.Errors;
            }

            return result;
        }

        public async Task<AuthResultSet> LoginAsync(AuthUserLoginCreateDto model)
        {
            AuthResultSet result = new AuthResultSet();

            AuthUser authUser = new AuthUser
            {
                Email = model.Email
            };

            var authenticUser = await _authRepo.LoginAsync(authUser, model.Password);

            if (authenticUser.Success)
            {
                result = await GenerateJwtToken(authenticUser.Data);

                result.Success = true;
            }
            else
            {
                result.Errors = authenticUser.Errors;
            }

            return result;

        }

        private async Task<AuthResultSet> GenerateJwtToken(AuthUser user)
        {
            var authResultSet = new AuthResultSet();
            try
            {
                var authSecretKey = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

                var jwtTokenHandler = new JwtSecurityTokenHandler();

                // process auth claims
                var userRoles = await GetUserClaimsAsync(user);
                List<Claim> authClaims = new List<Claim>
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                //


                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _jwtConfig.ValidAudience,
                    Issuer = _jwtConfig.ValidIssuer,
                    Subject = new ClaimsIdentity(authClaims),
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(authSecretKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = jwtTokenHandler.WriteToken(token);

                var refreshToken = new AuthRefreshToken()
                {
                    UserId = user.Id,
                    RefreshToken = RandomString(30) + Guid.NewGuid(),
                    JwtTokenId = token.Id,
                    IsUsed = false,
                    IsRevoked = false,
                    ExpiryDate = DateTime.UtcNow.AddMinutes(60)
                };

                var insertedRefreshToken = await SaveRefreshTokenAsync(refreshToken);

                if (insertedRefreshToken != null) //TODO: check (insertRefreshToken.TokenId != null) or not?
                {
                    authResultSet.Token = jwtToken;
                    authResultSet.RefreshToken = insertedRefreshToken.RefreshToken;
                    authResultSet.Success = true;
                }

                return authResultSet;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<AuthResultSet> RefreshAndGenerateTokenAsync(AuthRefreshTokenCreateDto refreshToken)
        {
            var result = new AuthResultSet();
            try
            {
                var varifiedToken = await VerifyToken(refreshToken);

                if (varifiedToken.Success)
                {
                    var updatedRefreshToken = await UpdateRefreshToken(varifiedToken.RefreshToken);
                    if (updatedRefreshToken != null)
                    {
                        result = await GenerateJwtToken(updatedRefreshToken.User);
                        result.Success = true;
                    }
                    else
                    {
                        result.Errors.Add("Update failed");
                    }
                }
                else
                {
                    foreach (var err in varifiedToken.Errors)
                    {
                        result.Errors.Add(err);
                    }
                }

                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<IList<string>> GetUserClaimsAsync(AuthUser user)
        {
            var userClaims = await _authRepo.GetUserRolesAsync(user);

            return userClaims;
        }

        private async Task<AuthRefreshToken> SaveRefreshTokenAsync(AuthRefreshToken refreshToken)
        {
            var authRefreshToken = new AuthRefreshToken();

            var dataFromDb = await _authRepo.InsertRefreshTokenAsync(refreshToken);
            if (dataFromDb.Success)
            {
                authRefreshToken = dataFromDb.Data;
            }

            return authRefreshToken;
        }

        private async Task<AuthVerifyRefreshTokenReadDto> VerifyToken(AuthRefreshTokenCreateDto tokenRequest)
        {
            var result = new AuthVerifyRefreshTokenReadDto();
            var storedRefreshToken = new AuthRefreshToken();

            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();

                // Validation 1 - Validation JWT token format
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.Token, _tokenValidationParams, out SecurityToken validatedToken);

                // Validation 2 - Validate encryption algo
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    //var isValidEncryption = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase);
                    var isValidEncryption = jwtSecurityToken.Header.Alg.Equals("HS256");

                    if (isValidEncryption == false)
                    {
                        result.Errors.Add("Token is not legal");
                        return result;
                    }
                }

                // Validation 3 - Validate expiry date
                var utcExpiryDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Exp).Value);

                var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);

                if (expiryDate > DateTime.UtcNow)
                {
                    result.Errors.Add("Token has not expired yet. It's alive");
                    return result;
                }

                // Validation 4 - Validate existance of token
                var storedRefreshTokenFromDb = await _authRepo.RetrivedToken(tokenRequest.RefreshToken);
                storedRefreshToken = storedRefreshTokenFromDb.Data;

                if (storedRefreshTokenFromDb.Success == false)
                {
                    result.Errors.Add("Token does not exist");
                    return result;
                }

                // Validation 5 - Validate if used
                if (storedRefreshToken.IsUsed)
                {
                    result.Errors.Add("Token has been used");
                    return result;
                }

                // Validation 6 - Validate if revoked
                if (storedRefreshToken.IsRevoked)
                {
                    result.Errors.Add("Token has been revoked");
                    return result;
                }

                // Validation 7 - Validate the id
                var jti = tokenInVerification.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Jti).Value;

                if (storedRefreshToken.JwtTokenId != jti)
                {
                    result.Errors.Add("Token does not match");
                    return result;
                }

                if (storedRefreshToken != null)
                {
                    result.RefreshToken = storedRefreshToken;
                    result.Success = true;
                }

                return result;

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Lifetime validation failed. The token is expired"))
                {
                    result.Errors.Add("Token is expired");
                }
                else
                {
                    result.Errors.Add("Token verification failed");
                }
                return result;
            }
        }

        private async Task<AuthRefreshToken> UpdateRefreshToken(AuthRefreshToken tokenRequest)
        {
            try
            {
                tokenRequest.IsUsed = true;
                tokenRequest.ModifiedDate = DateTime.UtcNow;

                var data = await _authRepo.UpdateRefreshToken(tokenRequest);

                if (data.Success && data.Data != null)
                {
                    return data.Data;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

            return dateTimeVal;
        }
        private string RandomString(int length)
        {
            var random = new Random();
            var charStr = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789~!@#$%^&*";
            var randomString = new String(Enumerable.Repeat(charStr, length).Select(x => x[random.Next(x.Length)]).ToArray());

            return randomString;
        }
    }
}
