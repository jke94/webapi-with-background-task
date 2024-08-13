
namespace WebApiWithBackgroundTask.Services
{
    public class BackgroundProducts : BackgroundService
    {
        private readonly ILogger<BackgroundProducts> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IProductsCache _productsCache;

        public BackgroundProducts(
            ILogger<BackgroundProducts> logger,
            IServiceProvider serviceProvider,
            IProductsCache productsCache
        ) 
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _productsCache = productsCache;
        }

        public async override Task StartAsync(CancellationToken cancellationToken)
        {
            await UpdateProductsCacheAsync();
            await base.StartAsync(cancellationToken);
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                await UpdateProductsCacheAsync();
            }
        }

        private async Task UpdateProductsCacheAsync()
        {
            using(var scope = _serviceProvider.CreateScope())
            {
                _logger.LogInformation("Getting products...");

                var serviceProduct = scope.ServiceProvider
                    .GetRequiredService<IProductService>();

                var products = await serviceProduct.Get();

                _productsCache.Save(products);
            }
        }
    }
}
