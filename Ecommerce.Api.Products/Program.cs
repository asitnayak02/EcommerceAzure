using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Products
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddScoped<IProductsProvider, ProductsProvider>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddDbContext<ProductsDbContext>(options =>
            {
                options.UseInMemoryDatabase("Products");
            });
            

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