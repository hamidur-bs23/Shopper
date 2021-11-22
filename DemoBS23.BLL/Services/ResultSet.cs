using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.BLL.Services
{
    public class ResultSet<T>
    {
        //public ResultSet()
        //{
        //    Success = false;
        //    errorMessage = string.Empty;
        //    internalMessage = string.Empty;
        //    exception = null;

        //}
        public T Data { get; set; }
        public bool Success { get; set; } = false;
        public string errorMessage { get; set; } = String.Empty;
        internal string internalMessage { get; set; } = String.Empty;
        internal Exception exception { get; set; } = null;
    }
}
