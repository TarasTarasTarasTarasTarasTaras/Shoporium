﻿using Shoporium.Entities.DTO.Stores;

namespace Shoporium.Data.Stores
{
    public interface IStoreRepository
    {
        void CreateStore(StoreDTO model);
        IEnumerable<StoreDTO> GetMyStores(long userId);
        StoreDTO GetStoreDetails(int storeId);
        IEnumerable<StoreDTO> GetAllStores();
    }
}