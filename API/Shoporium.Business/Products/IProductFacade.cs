using Shoporium.Entities.DTO.Products;

namespace Shoporium.Business.Products
{
    public interface IProductFacade
    {
        long CreateProduct(ProductDTO model);
        IEnumerable<ProductDTO> GetAllProducts();
        Task<IEnumerable<ProductDTO>> GetMyProducts(long userId);
        Task<ProductDTO> GetProduct(int storeId);
    }
}