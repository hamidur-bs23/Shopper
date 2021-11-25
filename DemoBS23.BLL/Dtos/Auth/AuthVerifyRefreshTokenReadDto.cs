using DemoBS23.DAL.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.BLL.Dtos.Auth
{
    public class AuthVerifyRefreshTokenReadDto
    {
        public AuthVerifyRefreshTokenReadDto()
        {
            RefreshToken = null;
            Success = false;
            Errors = new List<string>();
        }
        public AuthRefreshToken RefreshToken { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
