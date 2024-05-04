using Shoporium.Entities.DTO.Products;

namespace Shoporium.Data.Products
{
    public interface IProductRepository
    {
        long CreateProduct(ProductDTO model);
        IEnumerable<ProductDTO> GetAllProducts();
        IEnumerable<ProductDTO> GetMyProducts(long userId);
        IEnumerable<ProductDTO> GetStoreProducts(long storeId);
        ProductDTO GetProduct(int productId);
    }
}