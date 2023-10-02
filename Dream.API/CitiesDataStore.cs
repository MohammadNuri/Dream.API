using Dream.API.Models;

namespace Dream.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
               new CityDto() { Id = 1, Name = "Tehran", Description = "This is for Tehran" },
               new CityDto() { Id = 2, Name = "Shiraz", Description = "This is for Shiraz" },
               new CityDto() { Id = 3, Name = "Ahwaz", Description = "This is for Ahwaz" }
            };
        }
    }
}
