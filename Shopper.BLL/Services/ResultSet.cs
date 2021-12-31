using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.BLL.Services
{
    public class ResultSet<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = false;
        public string errorMessage { get; set; } = String.Empty;
        internal string internalMessage { get; set; } = String.Empty;
        internal Exception exception { get; set; } = null;
    }
}
