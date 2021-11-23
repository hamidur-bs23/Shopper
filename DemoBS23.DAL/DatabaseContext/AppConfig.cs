//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;

//namespace DemoBS23.DAL.DatabaseContext
//{
//    public class AppConfig
//    {
//        public AppConfig()
//        {
//            var configBuilder = new ConfigurationBuilder();
//            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
//            configBuilder.AddJsonFile(path, false);
//            var root = configBuilder.Build();
//            dbConnectionString = root.GetConnectionString("db_bs23_demo");
//        }

//        public string dbConnectionString { get; set; }
//    }
//}
