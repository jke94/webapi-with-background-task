namespace WebApiWithBackgroundTask.Services
{
    #region using

    using WebApiWithBackgroundTask.Model;

    #endregion

    public interface IProductsCache
    {
        IEnumerable<Product>? Products { get; }

        void Save(IEnumerable<Product>? products);
    }

    public class ProductsCache : IProductsCache
    {
        #region Fields

        private IEnumerable<Product>? products_;

        #endregion

        #region Properties

        public IEnumerable<Product>? Products => products_;

        #endregion

        #region Methods

        public void Save(IEnumerable<Product>? products)
        {
            Interlocked.Exchange(ref products_, products);
        }

        #endregion
    }
}
