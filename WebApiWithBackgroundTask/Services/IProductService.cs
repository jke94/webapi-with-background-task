using WebApiWithBackgroundTask.Model;

namespace WebApiWithBackgroundTask.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> Get();
    }

    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(
            HttpClient httpClient    
        )
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Product>> Get()
        {
            await Task.Delay(5000);
            var products = await _httpClient.
                GetFromJsonAsync<IEnumerable<Product>>("https://jsonplaceholder.typicode.com/posts");

            return products!;
        }
    }
}
