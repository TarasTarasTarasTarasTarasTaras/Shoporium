﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shoporium.Data._EntityFramework;
using Shoporium.Data._EntityFramework.Entities;
using Shoporium.Entities.DTO.Products;
using System.Data.SqlTypes;

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
            return _mapper.Map<IEnumerable<ProductDTO>>(Context.Products.Include(p => p.ProductPhotos).Where(s => s.Store != null && s.Store.UserId == userId));
        }

        public IEnumerable<ProductDTO> GetAllProducts(int count = 30)
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(Context.Products.Take(count).Include(p => p.ProductPhotos));
        }

        public ProductDTO GetProduct(int productId, bool addView = false)
        {
            var product = Context.Products.Include(p => p.ProductPhotos).FirstOrDefault(s => s.Id == productId);

            if (product != null && addView)
            {
                product.NumberOfViews++;
                Context.Entry(product).State = EntityState.Modified;
                Context.SaveChanges();
            }

            return _mapper.Map<ProductDTO>(product);
        }

        public IEnumerable<ProductDTO> GetStoreProducts(long storeId)
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(Context.Products.Include(p => p.ProductPhotos).Where(s => s.StoreId == storeId));
        }

        public IEnumerable<ProductDTO> GetNewestProducts(int count = 20)
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(Context.Products.Include(p => p.ProductPhotos).OrderByDescending(p => p.CreatedDate).Take(count));
        }

        public IEnumerable<ProductDTO> GetTheMostPopularProducts(int count = 20)
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(Context.Products.Include(p => p.ProductPhotos).Take(count));
        }

        public IEnumerable<ProductDTO> GetProductsByCategory(int categoryId, int count = 20)
        {
            var innerCategories = Context.ProductCategories.Where(pc => pc.Id == categoryId || pc.MainCategoryId == categoryId);
            var mergedCategories = innerCategories.Concat(Context.ProductCategories
                .Where(pc => innerCategories.Any(ic => ic.Id == pc.MainCategoryId)));

            return _mapper.Map<IEnumerable<ProductDTO>>(
                Context
               .Products
               .Where(p => mergedCategories.Any(ic => ic.Id == p.CategoryId))
               .Include(p => p.ProductPhotos)
               //.Take(count)
               .AsEnumerable());
        }

        public IEnumerable<ProductDTO> GetProductsByInput(string input, int count = 20)
        {
            return _mapper.Map<IEnumerable<ProductDTO>>(
                Context
                .Products
                .Include(p => p.ProductPhotos)
                .ToList()  // todo
                .Where(p => p.Name.Contains(input, StringComparison.OrdinalIgnoreCase))
                //.Take(count)
            );
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
