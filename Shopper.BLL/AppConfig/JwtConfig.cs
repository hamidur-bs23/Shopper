using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.BLL.AppConfig
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
    }
}
