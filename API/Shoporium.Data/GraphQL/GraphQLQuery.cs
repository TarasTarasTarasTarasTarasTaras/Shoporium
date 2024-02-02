using HotChocolate.Authorization;
using Shoporium.Data._EntityFramework;
using Shoporium.Data._EntityFramework.Entities;

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
    }
}
