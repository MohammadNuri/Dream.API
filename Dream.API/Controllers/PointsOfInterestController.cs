using Dream.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dream.API.Controllers
{

    [Produces("application/json")]  
    [Route("api/Cities/{cityId}/PointsOfInterest")]
    [ApiController]
  
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]   
        public ActionResult<PointOfInterestDto> GetPointsOfInterest(int cityId)
        {

            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound(); 
            }

            return Ok(city.PointOfInterest);
        }

        [HttpGet("{pointOfInterestId}" , Name = "GetPointsOfInterest")]   
        public ActionResult<PointOfInterestDto> GetPointsOfInterest(int cityId , int pointOfInterestId)
        {
            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == cityId);
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

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId,[FromBody]PointOfInterestForCreationDto pointOfInterest)
        {

            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)   
            {
                return NotFound();
            }

            var maxPointOfInterestId =
                CitiesDataStore.CurrentCities.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);

            var createPoint = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointOfInterest.Add(createPoint);


            return CreatedAtAction("GetPointsOfInterest", 
                new 
            {
                cityId = cityId,
                pointOfInterestId = createPoint.Id
            },
            createPoint
            );
        }
    }
}
