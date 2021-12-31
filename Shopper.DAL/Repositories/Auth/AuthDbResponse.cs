using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.DAL.Repositories.Auth
{
    public class AuthDbResponse<T> where T : class
    {
        public AuthDbResponse()
        {
            Data = null;
            Errors = new List<string>();
            Success = false;
        }

        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public bool Success { get; set; }
    }
}
