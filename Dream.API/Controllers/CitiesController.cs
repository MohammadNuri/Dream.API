using Microsoft.AspNetCore.Mvc;

namespace Dream.API.Controllers
{
    [ApiController]
    [Route("api/Cities")]    
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(CitiesDataStore.Current.Cities);
        }
    }
}
