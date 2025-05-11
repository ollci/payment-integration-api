using BalanceManagementAPI.Interfaces;
using BalanceManagementAPI.Services;
using PaymentIntegration.App.Interfaces;
using PaymentIntegration.App.Services;
using PaymentIntegration.DataAccess.Interfaces;
using PaymentIntegration.DataAccess.Services;

namespace PaymentIntegration.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // App Services
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            // Db Services
            services.AddScoped<IProductDbService, ProductDbService>();
            services.AddScoped<IOrderDbService, OrderDbService>();
            services.AddScoped<IBalanceDbService, BalanceDbService>();

            // External Services
            services.AddHttpClient<IBalanceManagementService, BalanceManagementService>()
                    .ConfigureHttpClient((sp, client) =>
                    {
                        var config = sp.GetRequiredService<IConfiguration>();
                        client.BaseAddress = new Uri(config["BalanceApi:BaseUrl"]!);
                    });
        }
    }
}
