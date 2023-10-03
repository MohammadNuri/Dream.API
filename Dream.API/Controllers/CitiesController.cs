using Dream.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dream.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.CurrentCities.Cities);
        }


        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCities(int id)
        {

            var cityToReturn = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }
            
            return Ok(cityToReturn);

            //return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
        }
    }
}
