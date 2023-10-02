using Microsoft.AspNetCore.Mvc;

namespace Dream.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]    
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(
                new List<object>
                {
                    new {id =  1, name = "Tehran"},
                    new {id =  2, name = "Shiraz"}
                });
        }
    }
}
