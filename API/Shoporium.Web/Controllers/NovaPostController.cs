using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shoporium.Web.Models.NovaPost;
using System.Linq;
using System.Text;

namespace Shoporium.Web.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class NovaPostController : ControllerBase
    {
        private const string apiUrl = "https://api.novaposhta.ua/v2.0/json/";
        private const string apiKey = "1fe261d98780c22bc96f272803b50822";

        [HttpGet("post-offices/{cityName}")]
        public async Task<ActionResult> GetPostOffices(string cityName)
        {
            string? cityRef = null;

            var searchSettlementsRequestData = new
            {
                apiKey = apiKey,
                modelName = "AddressGeneral",
                calledMethod = "searchSettlements",
                methodProperties = new
                {
                    CityName = cityName,
                    Limit = "50",
                    Page = "1"
                }
            };

            string json = JsonConvert.SerializeObject(searchSettlementsRequestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                string result = await response.Content.ReadAsStringAsync();
                NovaPostAdresses? model = JsonConvert.DeserializeObject<NovaPostAdresses>(result);

                if (model == null)
                    return NotFound();

                cityRef = model.Data.FirstOrDefault()?.Addresses?.FirstOrDefault()?.DeliveryCity;

                if(string.IsNullOrEmpty(cityRef))
                    return NotFound();
            }

            var getWarehousesRequestData = new
            {
                apiKey = apiKey,
                modelName = "AddressGeneral",
                calledMethod = "getWarehouses",
                methodProperties = new
                {
                    CityRef = cityRef
                }
            };

            json = JsonConvert.SerializeObject(getWarehousesRequestData);
            content = new StringContent(json, Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                string result = await response.Content.ReadAsStringAsync();
                NovaPostPostOffices? model = JsonConvert.DeserializeObject<NovaPostPostOffices>(result);

                if (model == null)
                    return NotFound();

                var postOffices = model?.Data?.Select(w => new PostOffice() { Id = w.SiteKey, Description = w.Description });

                if (postOffices == null || !postOffices.Any())
                    return NotFound();

                return Ok(postOffices);
            }
        }
    }

    public class PostOffice
    {
        public string Id { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
