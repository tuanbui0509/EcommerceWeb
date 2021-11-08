//using EcommerceSolution.Application.Services.Catalog;
using EcommerceSolution.Application.Catalog.Categories;
using EcommerceSolution.Application.Services.System;
using EcommerceSolution.InterfaceRepository.Interface;
using EcommerceSolution.Data.EF;
using EcommerceSolution.Data.Entities;
using EcommerceSolution.InterfaceService;
using EcommerceSolution.Repository.Repository;
using EcommerceSolution.Utilities.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceSolution.Application.Services.Catalog;
using EcommerceSolution.Application.Catolog.Orders;
using EcommerceSolution.Application.Common;
using EcommerceSolution.InterfaceRepository;
using EcommerceSolution.Utilities.Cache;

namespace EcommerceWeb.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime
            services.AddDbContext<EcommerceDBContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(SystemConstants.MainConectionString));
                //options.UseLazyLoadingProxies();
            }
            );

            services.AddScoped<Func<EcommerceDBContext>>((provider) => () => provider.GetService<EcommerceDBContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<UserManager<AppUser>, UserManager<AppUser>>()
                .AddScoped<SignInManager<AppUser>, SignInManager<AppUser>>()
                .AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IProductImageRepository, ProductImageRepository>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserService, UserService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IOrderService, OrderSevice>()
                .AddScoped<IProductImageService, ProductImageService>()
                .AddScoped<IStorageService, StorageService>()
                .AddScoped<IMemoryCacheHelper, MemoryCacheHelper>();
        }
    }
}
