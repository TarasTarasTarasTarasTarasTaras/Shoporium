using Microsoft.Extensions.Configuration;
using Shoporium.Business.Services;
using Shoporium.Data.Products;
using Shoporium.Entities.DTO.Products;

namespace Shoporium.Business.Products
{
    public class ProductFacade : IProductFacade
    {
        private readonly IAWSService _azureService;
        private readonly IConfiguration _configuration;
        private readonly IProductRepository _productRepository;

        public ProductFacade(
            IAWSService azureService,
            IConfiguration configuration,
            IProductRepository productRepository)
        {
            _configuration = configuration;
            _azureService = azureService;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetMyProducts(long userId)
        {
            var products = _productRepository.GetMyProducts(userId);

            await Task.WhenAll(products
                .Select(async product =>
                {
                    var containerName = _configuration["AWSBucketName"]!;

                    for (int i = 0; i < product.ProductPhotos.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(product.ProductPhotos.ElementAt(i)))
                        {
                            var downloadedPhoto = await _azureService.DownloadBlobAsync(containerName, $"products/{product.Name}/{product.ProductPhotos.ElementAt(i)}");
                            product.DownloadedPhotos.Add(downloadedPhoto);
                        }
                    }
                }));

            return products;
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        public long CreateProduct(ProductDTO model)
        {
            var productId = _productRepository.CreateProduct(model);
            return productId;
        }

        public async Task<ProductDTO> GetProduct(int storeId)
        {
            var product = _productRepository.GetProduct(storeId);
            var containerName = _configuration["AWSBucketName"]!;

            for (int i = 0; i < product.ProductPhotos.Count(); i++)
            {
                if (!string.IsNullOrEmpty(product.ProductPhotos.ElementAt(i)))
                {
                    var downloadedPhoto = await _azureService.DownloadBlobAsync(containerName, $"products/{product.Name}/{product.ProductPhotos.ElementAt(i)}");
                    product.DownloadedPhotos.Add(downloadedPhoto);
                }
            }

            return product;
        }
    }
}
