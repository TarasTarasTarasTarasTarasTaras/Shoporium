using Shoporium.Entities.DTO.Stores;

namespace Shoporium.Data.Stores
{
    public interface IStoreRepository
    {
        void CreateStore(StoreDTO model);
        IEnumerable<StoreDTO> GetAllStores();
    }
}