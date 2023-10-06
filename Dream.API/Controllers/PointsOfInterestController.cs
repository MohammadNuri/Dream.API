using AutoMapper;
using Dream.API.Models;
using Dream.API.Repositories;
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
        //DreamInfoRepository Injection
        #region Injections
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly IDreamInfoRepository _dreamInfoRepository;
        private readonly IMapper _mapper;   
        public PointsOfInterestController(
            ILogger<PointsOfInterestController> logger,
            IMailService mailService,
            IDreamInfoRepository dreamInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException();
            _mapper = mapper ?? throw new ArgumentNullException();
            _dreamInfoRepository = dreamInfoRepository ?? throw new ArgumentNullException();
        }
        #endregion

        //Get All & Get With Id + log information
        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            if (!await _dreamInfoRepository.CityExistAsync(cityId))
            {
                _logger.LogInformation($"{cityId} Not Found");
                return NotFound();
            }

            var pointsOfInterest = await _dreamInfoRepository.GetPointsOfInterestAsync(cityId);

            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest));
        }
        [HttpGet("{pointOfInterestId}", Name = "GetPointsOfInterest")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointsOfInterest(int cityId, int pointOfInterestId)
        {
            if (!await _dreamInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            var pointOfInterest = await _dreamInfoRepository.GetPointOfInterestAsync(cityId, pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));

        }
        #endregion
        //----------------------------------------------------------------

        //Create
        #region Create
        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            if (!await _dreamInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            var result = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await _dreamInfoRepository.CreatePointOfInterestAsync(cityId, result);

            await _dreamInfoRepository.SaveChangesAsync();

            var createdpoint = _mapper.Map<Models.PointOfInterestDto>(result);

            return CreatedAtRoute("GetPointsOfInterest",new
            {
                cityId = cityId,    
                pointOfInterestId = createdpoint.Id,   
            },createdpoint);
        }
        #endregion
        //----------------------------------------------------------------

        //This is for Update Data in HTTP Put (Have to Change All Properties)
        #region Update (HttpPut)
        [HttpPut("{pointOfInterestId}")]

        public async Task<ActionResult> UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            if (!await _dreamInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            var point = await _dreamInfoRepository.GetPointOfInterestAsync(cityId,pointOfInterestId);
            if (point == null)
            {
                return NotFound();
            }

            _mapper.Map(pointOfInterest, point);

            await _dreamInfoRepository.SaveChangesAsync();

            return CreatedAtRoute("GetPointsOfInterest", new
            {
                cityId = cityId,
                pointOfInterestId = point.Id,
                city = point.City,  
            }, point);
        }
        #endregion
        //----------------------------------------------------------------

        //----------------------------------------------------------------
        //This is for Edit Data in HTTP Patch (No Need to Change All Properties, it Keeps the Same Data When u Enter Null)
        //Requirement:
        //Microsoft.AspNetCore.JsonPatch + Microsoft.AspNetCore.Mvc.NewtonsoftJso
        #region Edit (HttpPatch)
        [HttpPatch("{pointOfInterestid}")]
        public async Task<ActionResult> EditPointsOfInterest(int cityId, int pointOfInterestid, JsonPatchDocument<PointOfInterestForUpdateDto> patchPointOfInterest)
        {
            if (!await _dreamInfoRepository.CityExistAsync(cityId))
            {
                return NotFound();
            }

            var point = await _dreamInfoRepository.GetPointOfInterestAsync(cityId, pointOfInterestid);

            if (point == null)
            {
                return NotFound();
            }

            var pointToPatch = _mapper.Map<PointOfInterestForUpdateDto>(point);

            patchPointOfInterest.ApplyTo(pointToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pointToPatch, point);

            await _dreamInfoRepository.SaveChangesAsync();  

            return NoContent();
        }
        #endregion
        //-----------------------------------------------------------------

        //Delete
        #region Delete
        //[HttpDelete("{pointOfInterestId}")]
        //public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        //{
        //    // Find a City with cityId 
        //    var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);
        //    if (city == null)
        //        return NotFound();
        //    // Find a Point of Interest with pointOfInterestId
        //    var point = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
        //    if (point == null)
        //        return NotFound();

        //    city.PointOfInterest.Remove(point);

        //    //Mail Notice Service For Deleted PointOfInterest
        //    _mailService.Send(
        //        "Point Of Interest Deleted",
        //        $"Point of Interest {point.Name} with city {city.Name} with id {pointOfInterestId} has been Deleted"
        //        );

        //    return NoContent();
        //}
        #endregion
        //-----------------------------------------------------------------
    }
}
