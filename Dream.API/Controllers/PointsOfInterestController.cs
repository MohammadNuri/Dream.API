using Dream.API.Models;
using Dream.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
namespace Dream.API.Controllers
{
    [Produces("application/json")]  
    [Route("api/Cities/{cityId}/PointsOfInterest")]
    [ApiController]
  
    public class PointsOfInterestController : ControllerBase 
    {

        //Logging System (ILogger ctor Injection)
        //Mail Service (LocalMailService ctor Injection)
        #region Injections
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly LocalMailService _localMailService;

        public PointsOfInterestController(
            ILogger<PointsOfInterestController> logger,
            LocalMailService localMailService)

        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _localMailService = localMailService ?? throw new ArgumentNullException();
        }
        #endregion

        //Get All & Get With Id + log information
        #region Get
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {

            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == cityId);
            try
            {
                if (city == null)
                {
                    _logger.LogInformation($"City with id {cityId} does not found!");
                    return NotFound();
                }
                return Ok(city.PointOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Internal Server Error for City with {cityId}", ex);
                return StatusCode(500,"Internal Server Error");
            }
           
        }
        [HttpGet("{pointOfInterestId}", Name = "GetPointsOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointsOfInterest(int cityId, int pointOfInterestId)
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
        #endregion
        //----------------------------------------------------------------

        //Create
        #region Create
        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId,[FromBody]PointOfInterestForCreationDto pointOfInterest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

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
        #endregion
        //----------------------------------------------------------------

        //This is for Update Data in HTTP Put (Have to Change All Properties)
        #region Update (HttpPut)
        [HttpPut("{pointOfInterestId}")]

        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            // Find a City with cityId 
            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            // Find a Point of Interest with pointOfInterestId
            var point = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            if (point == null)
                return NotFound();

            point.Name = pointOfInterest.Name;
            point.Description = pointOfInterest.Description;

            return NoContent();
        }
        #endregion
        //----------------------------------------------------------------

        //----------------------------------------------------------------
        //This is for Edit Data in HTTP Patch (No Need to Change All Properties, it Keeps the Same Data When u Enter Null)
        //Requirement:
        //Microsoft.AspNetCore.JsonPatch + Microsoft.AspNetCore.Mvc.NewtonsoftJso
        //Get the Response from Body Like :
        //      "path": "/name",
        //      "op": "replace",
        //      "value": "TestForPatch2"
        //this will replace the Name Property with the entered value

        #region Edit (HttpPatch)
        [HttpPatch("{pointOfInterestid}")]
        public ActionResult EditPointsOfInterest(int cityId, int pointOfInterestid, JsonPatchDocument<PointOfInterestForUpdateDto> patchPointOfInterest)
        {
            // Find a City with cityId 
            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            // Find a Point of Interest with pointOfInterestId
            var point = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestid);
            if (point == null)
                return NotFound();

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = point.Name,
                Description = point.Description,
            };

            patchPointOfInterest.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            point.Name = pointOfInterestToPatch.Name;
            point.Description = pointOfInterestToPatch.Description;


            return NoContent();
        }
        #endregion
        //-----------------------------------------------------------------

        //Delete
        #region Delete
        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            // Find a City with cityId 
            var city = CitiesDataStore.CurrentCities.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            // Find a Point of Interest with pointOfInterestId
            var point = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
            if (point == null)
                return NotFound();

            city.PointOfInterest.Remove(point);

            _localMailService.Send(
                "Point Of Interest Deleted",
                $"Point of Interest {point.Name} with city {city.Name} with id {pointOfInterestId} has been Deleted"
                );

            return NoContent();
        }
        #endregion
        //-----------------------------------------------------------------
    }
}
