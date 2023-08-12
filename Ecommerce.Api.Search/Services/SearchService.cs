using Ecommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int CustomerID)
        {
            var customersResult = await customersService.GetCustomerAsync(CustomerID);
            var ordersResult = await ordersService.GetOrdersAsync(CustomerID);
            var productsResult = await productsService.GetProductsAsync();

            foreach (var order in ordersResult.Orders)
            {
                foreach (var item in order.Items)
                {
                    item.ProductName = productsResult.IsSuccess ? productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name : "Product information is not available";


                }
            }
            if (ordersResult.IsSuccess)
            {
                var result = new
                {
                    Customer = customersResult.IsSuccess ? customersResult.customer : new { Name = "Customer info not available" },
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }
            return (false, null);
        }


    }
}
