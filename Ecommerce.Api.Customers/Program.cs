using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Ecommerce.Api.Customers.Providers;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Customers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddRazorPages();

            builder.Services.AddScoped<ICustomersProvider, CustomersProvider>();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddDbContext<CustomersDbContext>(options =>
            {
                options.UseInMemoryDatabase("Customers");
            });
            builder.Services.AddControllers();
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
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            app.Run();
        }
    }
}