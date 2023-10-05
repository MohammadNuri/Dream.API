using AutoMapper;
using Dream.API.Models;
using Dream.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dream.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IDreamInfoRepository _infoRepository;  
        private readonly IMapper _mapper;   

        public CitiesController(IDreamInfoRepository infoRepository,
            IMapper mapper)
        {
            _infoRepository = infoRepository ?? throw new ArgumentNullException(nameof(infoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities()
        {
            var cities = await _infoRepository.GetCitiesAsync();

            return Ok(
                _mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cities)
                );
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCities(int id, bool includePointsOfInterest = false)
        {

            var city = await _infoRepository.GetCityAsync(id , includePointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));

            //return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
        }
    }
}
