using Shoporium.Entities.DTO.Products;

namespace Shoporium.Business.Products
{
    public interface IProductFacade
    {
        long CreateProduct(ProductDTO model);
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<IEnumerable<ProductDTO>> GetMyProducts(long userId);
        Task<IEnumerable<ProductDTO>> GetStoreProducts(long storeId);
        Task<ProductDTO> GetProduct(int productId);
    }
}