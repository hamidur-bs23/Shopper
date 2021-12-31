using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Shopper.BLL.Utils
{
    public static class ConvertDateTimeToReadExtension
    {
        public static string ConvertDateTimeToRead(this DateTime sourceTime)
        {
            var result = new DateTime(sourceTime.Ticks, DateTimeKind.Utc).ToLongDateString();
            //var result = sourceTime.ToString("G", CultureInfo.CreateSpecificCulture("en-US"));
            return result;
        }
    }
}
