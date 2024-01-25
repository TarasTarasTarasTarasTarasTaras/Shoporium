using Shoporium.Data.Stores;
using Shoporium.Entities.DTO.Stores;

namespace Shoporium.Business.Stores
{
    public class StoreFacade : IStoreFacade
    {
        private readonly IStoreRepository _storeRepository;

        public StoreFacade(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public IEnumerable<StoreDTO> GetAllStores()
        {
            return _storeRepository.GetAllStores();
        }

        public void CreateStore(StoreDTO model)
        {
            _storeRepository.CreateStore(model);
        }
    }
}
