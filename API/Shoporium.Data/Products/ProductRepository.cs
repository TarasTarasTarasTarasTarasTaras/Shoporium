using AutoMapper;
using Shoporium.Data._EntityFramework;
using Shoporium.Data._EntityFramework.Entities;
using Shoporium.Entities.DTO.Products;

namespace Shoporium.Data.Products
{
    public class ProductRepository : IProductRepository
    {
        protected readonly IMapper _mapper;
        protected readonly ShoporiumContext Context;

        public ProductRepository(ShoporiumContext context, IMapper mapper)
        {
            Context = context;
            _mapper = mapper;
        }

        public IEnumerable<ProductDTO> GetMyProducts(long userId)
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(Context.Products.Where(s => s.Store != null && s.Store.UserId == userId));
        }

        public IEnumerable<ProductDTO> GetAllProducts()
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(Context.Products);
        }

        public ProductDTO GetProduct(int storeId)
        {
            return _mapper.Map<ProductDTO>(Context.Stores.FirstOrDefault(s => s.Id == storeId));
        }

        public long CreateProduct(ProductDTO model)
        {
            var entity = _mapper.Map<Product>(model);
            Context.Products.Add(entity);
            Context.SaveChanges();

            return entity.Id;
        }
    }
}
