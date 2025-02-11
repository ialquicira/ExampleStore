using ExampleStore.Data;
using ExampleStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ExampleStore.Core
{
    public static partial class ServiceInitializer
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddDbContext<IdentityDBContext>(options => options.UseSqlServer(configuration["ConnectionStrings:IdentityConnection"]));

            services.AddScoped<ISqlClientConnectionBD, SqlClientConnectionBD>();

            services.AddIdentityCore<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<IdentityDBContext>();


            //Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IClientService,  ClientService>();
            services.AddScoped<IStoreService, StoreService>();

            return services;
        }
    }
}
