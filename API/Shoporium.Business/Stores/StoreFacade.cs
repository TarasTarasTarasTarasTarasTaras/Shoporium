using Microsoft.Extensions.Configuration;
using Shoporium.Business.Services;
using Shoporium.Data.Stores;
using Shoporium.Entities.DTO.Stores;

namespace Shoporium.Business.Stores
{
    public class StoreFacade : IStoreFacade
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IAzureService _azureService;
        private readonly IConfiguration _configuration;

        public StoreFacade(
            IStoreRepository storeRepository,
            IAzureService azureService,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _azureService = azureService;
            _storeRepository = storeRepository;
        }

        public async Task<IEnumerable<StoreDTO>> GetMyStores(long userId)
        {
            var stores = _storeRepository.GetMyStores(userId);

            await Task.WhenAll(stores
                .Select(async store =>
                {
                    var containerName = _configuration["AWSBucketName"]!;

                    if (!string.IsNullOrEmpty(store.MainPhoto))
                    {
                        store.DownloadedMainPhoto = await _azureService.DownloadBlobAsync(containerName, $"stores/{store.Name}/{store.MainPhoto}");
                    }

                    if (!string.IsNullOrEmpty(store.BackgroundPhoto))
                    {
                        store.DownloadedBackgroundPhoto = await _azureService.DownloadBlobAsync(containerName, $"stores/{store.Name}/{store.BackgroundPhoto}");
                    }
                }));

            return stores;
        }

        public IEnumerable<StoreDTO> GetAllStores()
        {
            return _storeRepository.GetAllStores();
        }

        public void CreateStore(StoreDTO model)
        {
            _storeRepository.CreateStore(model);
        }

        public async Task<StoreDTO> GetStoreDetails(int storeId)
        {
            var store = _storeRepository.GetStoreDetails(storeId);

            var containerName = _configuration["AWSBucketName"]!;
            if (!string.IsNullOrEmpty(store.MainPhoto))
            {
                store.DownloadedMainPhoto = await _azureService.DownloadBlobAsync(containerName, $"stores/{store.Name}/{store.MainPhoto}");
            }

            if (!string.IsNullOrEmpty(store.BackgroundPhoto))
            {
                store.DownloadedBackgroundPhoto = await _azureService.DownloadBlobAsync(containerName, $"stores/{store.Name}/{store.BackgroundPhoto}");
            }

            return store;
        }
    }
}
