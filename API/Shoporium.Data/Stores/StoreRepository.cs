using AutoMapper;
using Shoporium.Data._EntityFramework;
using Shoporium.Data._EntityFramework.Entities;
using Shoporium.Entities.DTO.Stores;

namespace Shoporium.Data.Stores
{
    public class StoreRepository : IStoreRepository
    {
        protected readonly IMapper _mapper;
        protected readonly ShoporiumContext Context;

        public StoreRepository(ShoporiumContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }

        public IEnumerable<StoreDTO> GetMyStores(long userId)
        {
            return _mapper.Map<IEnumerable<StoreDTO>>(Context.Stores.Where(s => s.UserId == userId));
        }

        public IEnumerable<StoreDTO> GetAllStores()
        {
            return _mapper.Map<IEnumerable<StoreDTO>>(Context.Stores);
        }

        public StoreDTO GetStoreDetails(int storeId)
        {
            return _mapper.Map<StoreDTO>(Context.Stores.FirstOrDefault(s => s.Id == storeId));
        }

        public int CreateStore(StoreDTO model)
        {
            var entity = _mapper.Map<Store>(model);
            Context.Stores.Add(entity);
            Context.SaveChanges();

            return entity.Id;
        }
    }
}
