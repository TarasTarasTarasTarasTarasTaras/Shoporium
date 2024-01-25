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

        public IEnumerable<StoreDTO> GetAllStores()
        {
            return _mapper.Map<IEnumerable<StoreDTO>>(Context.Stores);
        }

        public void CreateStore(StoreDTO model)
        {
            var entity = _mapper.Map<Store>(model);
            entity.CategoryId = 1;

            Context.Stores.Add(entity);
            Context.SaveChanges();
        }
    }
}
