using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoporium.Business.Products;
using Shoporium.Business.Services;
using Shoporium.Entities.DTO.Products;
using Shoporium.Web.Extensions;
using Shoporium.Web.Models.Products;

namespace Shoporium.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAWSService _azureService;
        private readonly IProductFacade _productFacade;
        private readonly IConfiguration _configuration;

        public ProductController(
            IMapper mapper,
            IAWSService azureService,
            IProductFacade productFacade,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _azureService = azureService;
            _productFacade = productFacade;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateProductModel model)
        {
            model.UserId = User.GetId();
            var productId = _productFacade.CreateProduct(_mapper.Map<ProductDTO>(model));
            var containerName = _configuration["AWSBucketName"]!;

            for (int i = 0; i < model.ProductPhotos.Count(); i++)
            {
                if (model.ProductPhotos.ElementAt(i) != null)
                {
                    var photo = model.ProductPhotos.ElementAt(i);
                    await _azureService.UploadBlobAsync(containerName, $"products/{model.Name}/{photo.FileName}", photo);
                }
            }

            return Ok(productId);
        }

        [HttpGet("my")]
        public async Task<ActionResult> GetMyProducts()
        {
            return Ok(await _productFacade.GetMyProducts(User.GetId()));
        }

        [HttpGet("store/{storeId}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetStoreProducts(int storeId)
        {
            return Ok(await _productFacade.GetStoreProducts(storeId));
        }

        [HttpGet("newest")]
        [AllowAnonymous]
        public async Task<ActionResult> GetNewestProducts()
        {
            return Ok(await _productFacade.GetNewestProducts());
        }

        [HttpGet("most-popular")]
        [AllowAnonymous]
        public async Task<ActionResult> GetTheMostPopularProducts()
        {
            return Ok(await _productFacade.GetTheMostPopularProducts());
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllProducts()
        {
            return Ok(await _productFacade.GetAllProducts());
        }

        [HttpGet("by-category/{categoryId}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetProductsByCategory(int categoryId)
        {
            return Ok(await _productFacade.GetProductsByCategory(categoryId));
        }

        [HttpGet("by-input/{input}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetProductsByInput(string input)
        {
            return Ok(await _productFacade.GetProductsByInput(input));
        }

        [HttpGet("details/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetProduct(int id)
        {
            return Ok(await _productFacade.GetProduct(id));
        }
    }
}
