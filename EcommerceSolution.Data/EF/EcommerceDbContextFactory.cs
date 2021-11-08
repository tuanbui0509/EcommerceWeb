using EcommerceSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceSolution.Data.EF
{
    class EcommerceDbContextFactory : IDesignTimeDbContextFactory<EcommerceDBContext>
    {
        public EcommerceDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json").Build();

            var connectionString = configuration.GetConnectionString("EcommerceDb");

            var optionsBuilder = new DbContextOptionsBuilder<EcommerceDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EcommerceDBContext(optionsBuilder.Options);
        }
    }
}
