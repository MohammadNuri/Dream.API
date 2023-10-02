using Dream.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dream.API.Controllers
{
    [Route("api/Cities/{id}/PointsOfInterest")]
    [ApiController]
  
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]   
        public ActionResult<PointOfInterestDto> GetPointsOfInterest(int id)
        {

            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == id);

            if (city == null)
            {
                return NotFound(); 
            }

            return Ok(city.PointOfInterest);
        }

        [HttpGet("{pointOfInterestId}")]   
        public ActionResult<PointOfInterestDto> GetPointsOfInterest(int id , int pointOfInterestId)
        {
            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();  
            }
            var point = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            if (point == null)
            {
                return NotFound();  
            }

            return Ok(point);
        }
    }
}
