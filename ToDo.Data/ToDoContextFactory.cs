using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace P683.Archives.Database
{
    public class ToDoContextFactory : IDesignTimeDbContextFactory<ToDoContext>
    {
        public ToDoContextFactory()
        {
        }

        public ToDoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ToDoContext>();

            if (optionsBuilder.IsConfigured)
                return new ToDoContext(optionsBuilder.Options);

            var environmentName = Environment.GetEnvironmentVariable("EnvironmentName") ?? "Development";

            //var connectionString =
            //    new ConfigurationBuilder()
            //        .SetBasePath(AppContext.BaseDirectory + "../../../../P683.Archives.WebAPI")
            //        .AddJsonFile("appsettings.json")
            //        .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: false)
            //        .AddEnvironmentVariables()
            //        .Build()
            //        .GetConnectionString("Archives");

            optionsBuilder.UseSqlServer("Server=DESKTOP-PO9SUII;Database=ToDo;Trusted_Connection=True;");

            return new ToDoContext(optionsBuilder.Options);
        }
    }
}
