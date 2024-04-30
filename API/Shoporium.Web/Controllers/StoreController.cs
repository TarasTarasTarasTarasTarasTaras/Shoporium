using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoporium.Business.Services;
using Shoporium.Business.Stores;
using Shoporium.Entities.DTO.Stores;
using Shoporium.Web.Extensions;
using Shoporium.Web.Models.Stores;

namespace Shoporium.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStoreFacade _storeFacade;
        private readonly IAWSService _azureService;
        private readonly IConfiguration _configuration;

        public StoreController(
            IMapper mapper,
            IStoreFacade storeFacade,
            IAWSService azureService,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _storeFacade = storeFacade;
            _azureService = azureService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateStoreModel model)
        {
            model.UserId = User.GetId();
            var storeId = _storeFacade.CreateStore(_mapper.Map<StoreDTO>(model));

            var containerName = _configuration["AWSBucketName"]!;

            if (model.MainPhoto != null)
                await _azureService.UploadBlobAsync(containerName, $"stores/{model.Name}/{model.MainPhoto.FileName}", model.MainPhoto);

            if (model.BackgroundPhoto != null)
                await _azureService.UploadBlobAsync(containerName, $"stores/{model.Name}/{model.BackgroundPhoto.FileName}", model.BackgroundPhoto);

            return Ok(storeId);
        }

        [HttpGet("my")]
        public async Task<ActionResult> GetMyStores()
        {
            return Ok(await _storeFacade.GetMyStores(User.GetId()));
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public ActionResult GetAllStores()
        {
            return Ok(_storeFacade.GetAllStores());
        }

        [HttpGet("details/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetStoreDetails(int id)
        {
            return Ok(await _storeFacade.GetStoreDetails(id));
        }
    }
}
