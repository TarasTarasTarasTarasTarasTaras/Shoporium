using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public StoreController(IMapper mapper, IStoreFacade storeFacade)
        {
            _mapper = mapper;
            _storeFacade = storeFacade;
        }

        [HttpPost]
        public ActionResult Create(CreateStoreModel model)
        {
            model.UserId = User.GetId();
            _storeFacade.CreateStore(_mapper.Map<StoreDTO>(model));
            return Ok();
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public ActionResult GetAll()
        {
            return Ok(_storeFacade.GetAllStores());
        }
    }
}
