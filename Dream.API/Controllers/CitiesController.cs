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

        public CitiesController(IDreamInfoRepository infoRepository)
        {
            _infoRepository = infoRepository ?? throw new ArgumentNullException(nameof(infoRepository));   
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities()
        {
            var cities = await _infoRepository.GetCitiesAsync();
            var result = new List<CityWithoutPointOfInterestDto>();

            foreach (var city in cities)
            {
                result.Add(new CityWithoutPointOfInterestDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description,
                });
            }

            return Ok(result);
        }


        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCities(int id)
        {

            var cityToReturn = _infoRepository.GetCitiesAsync();
            if (cityToReturn == null)
            {
                return NotFound();
            }
            
            return Ok(cityToReturn);

            //return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
        }
    }
}
