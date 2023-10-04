using Dream.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dream.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly CitiesDataStore _citiesDataStore;

        public CitiesController(CitiesDataStore citiesDataStore)
        {
            _citiesDataStore = citiesDataStore;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(_citiesDataStore.Cities);
        }


        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCities(int id)
        {

            var cityToReturn = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }
            
            return Ok(cityToReturn);

            //return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
        }
    }
}
