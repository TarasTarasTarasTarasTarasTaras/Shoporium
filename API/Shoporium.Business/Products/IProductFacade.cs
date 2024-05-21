using Shoporium.Entities.DTO.Products;

namespace Shoporium.Business.Products
{
    public interface IProductFacade
    {
        long CreateProduct(ProductDTO model);
        Task<IEnumerable<ProductDTO>> GetAllProducts(int count = 30);
        Task<IEnumerable<ProductDTO>> GetMyProducts(long userId);
        Task<IEnumerable<ProductDTO>> GetStoreProducts(long storeId);
        Task<IEnumerable<ProductDTO>> GetNewestProducts(int count = 20);
        Task<IEnumerable<ProductDTO>> GetTheMostPopularProducts(int count = 20);
        Task<ProductDTO> GetProduct(int productId);
    }
}