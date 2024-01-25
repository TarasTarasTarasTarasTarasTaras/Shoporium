using Shoporium.Entities.DTO.Stores;

namespace Shoporium.Business.Stores
{
    public interface IStoreFacade
    {
        void CreateStore(StoreDTO model);
        IEnumerable<StoreDTO> GetAllStores();
    }
}