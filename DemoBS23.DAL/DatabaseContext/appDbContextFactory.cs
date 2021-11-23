
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace DemoBS23.DAL.DatabaseContext
//{
//    public class appDbContextFactory : IDesignTimeDbContextFactory<appDbContext>
//    {
//        public appDbContext CreateDbContext(string[] args)
//        {
//            AppConfig appConfig = new AppConfig();
//            var optionsBuilder = new DbContextOptionsBuilder<appDbContext>();
//            optionsBuilder.UseSqlServer(appConfig.dbConnectionString);
//            return new appDbContext(optionsBuilder.Options);
//        }
//    }
//}
