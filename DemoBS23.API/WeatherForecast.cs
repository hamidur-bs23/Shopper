using System;

namespace DemoBS23.API
{
    public class WeatherForecast
    {
        /// <summary>
        /// Date for weather
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Temp in C
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// Temp in F
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Summery
        /// </summary>
        public string Summary { get; set; }
    }
}
