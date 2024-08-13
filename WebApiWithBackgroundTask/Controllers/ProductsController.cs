using Microsoft.AspNetCore.Mvc;
using WebApiWithBackgroundTask.Model;
using WebApiWithBackgroundTask.Services;

namespace WebApiWithBackgroundTask.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private readonly IProductsCache _productsCache;

        public ProductsController(
            IProductsCache productsCache
            )
        {
            _productsCache = productsCache;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _productsCache.Products!;
        }
    }
}
