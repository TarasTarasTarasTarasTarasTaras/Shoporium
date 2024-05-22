using Shoporium.Entities.DTO.Products;

namespace Shoporium.Data.Products
{
    public interface IProductRepository
    {
        long CreateProduct(ProductDTO model);
        IEnumerable<ProductDTO> GetAllProducts(int count = 30);
        IEnumerable<ProductDTO> GetMyProducts(long userId);
        IEnumerable<ProductDTO> GetStoreProducts(long storeId);
        IEnumerable<ProductDTO> GetNewestProducts(int count = 20);
        IEnumerable<ProductDTO> GetTheMostPopularProducts(int count = 20);
        IEnumerable<ProductDTO> GetProductsByCategory(int categoryId, int count = 20);
        ProductDTO GetProduct(int productId);
    }
}