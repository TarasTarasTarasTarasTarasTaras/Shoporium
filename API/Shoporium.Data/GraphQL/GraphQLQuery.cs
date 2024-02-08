using HotChocolate.Authorization;
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
        [Authorize]
        public IQueryable<Product> GetProducts([Service] ShoporiumContext context)
        {
            return context.Products;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ProductCategory> GetProductCategories([Service] ShoporiumContext context)
        {
            return context.ProductCategories;
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
    }
}
