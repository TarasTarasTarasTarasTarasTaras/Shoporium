using Shoporium.Entities.DTO.Stores;

namespace Shoporium.Business.Stores
{
    public interface IStoreFacade
    {
        int CreateStore(StoreDTO model);
        Task<IEnumerable<StoreDTO>> GetMyStores(long userId);
        IEnumerable<StoreDTO> GetAllStores();
        Task<StoreDTO> GetStoreDetails(int storeId);
    }
}