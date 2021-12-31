using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.BLL.Dtos.Auth
{
    public class AuthResultSet
    {
        public AuthResultSet()
        {
            Token = "";
            RefreshToken = "";
            Success = false;
            Errors = new List<string>();
        }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
    }
}
