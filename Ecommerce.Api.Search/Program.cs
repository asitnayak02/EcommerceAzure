using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Services;
using ECommerce.Api.Search.Interfaces;
using Polly;

namespace Ecommerce.Api.Search
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;
            // Add services to the container.
            //builder.Services.AddRazorPages();
            builder.Services.AddScoped<ISearchService, SearchService>();
            builder.Services.AddScoped<IOrdersService, OrdersService>();
            builder.Services.AddScoped<IProductsService, ProductsService>();
            builder.Services.AddScoped<ICustomersService, CustomersService>();
            builder.Services.AddControllers();
            builder.Services.AddHttpClient("OrdersService", config =>
            {
                config.BaseAddress = new Uri(configuration["Services:Orders"]);
            });
            builder.Services.AddHttpClient("CustomersService", config =>
            {
                config.BaseAddress = new Uri(configuration["Services:Customers"]);
            });
            builder.Services.AddHttpClient("ProductsService", config =>
            {
                config.BaseAddress = new Uri(configuration["Services:Products"]);
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(5, _ => TimeSpan.FromMilliseconds(500)));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            //app.MapRazorPages();
            app.MapControllers();

            app.Run();
        }
    }
}