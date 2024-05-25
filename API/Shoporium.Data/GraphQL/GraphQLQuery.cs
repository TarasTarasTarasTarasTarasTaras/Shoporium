using HotChocolate.Authorization;
using Microsoft.EntityFrameworkCore;
using Shoporium.Data._EntityFramework;
using Shoporium.Data._EntityFramework.Entities;
using System.Security.Claims;

namespace Shoporium.Data.GraphQL
{
    public class GraphQLQuery
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ProductCategory> GetProductCategories([Service] ShoporiumContext context)
        {
            return context.ProductCategories.Include(p => p.Subcategories);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<StoreCategory> GetStoreCategories([Service] ShoporiumContext context)
        {
            return context.StoreCategories;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<Store> GetMyStores([Service] ShoporiumContext context, ClaimsPrincipal user)
        {
            var userId = Convert.ToInt64(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            return context.Stores.Where(s => s.UserId == userId);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        [Authorize]
        public IQueryable<Product> GetMyProducts([Service] ShoporiumContext context, ClaimsPrincipal user)
        {
            var userId = Convert.ToInt64(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            return context.Products.Where(s => s.Store != null && s.Store.UserId == userId);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<City> GetCities([Service] ShoporiumContext context)
        {
            return context.Cities;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<InnerCity> GetInnerCities([Service] ShoporiumContext context)
        {
            return context.InnerCities;
        }
    }
}
