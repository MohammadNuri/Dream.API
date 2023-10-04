using Dream.API.Models;

namespace Dream.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }
        //public static CitiesDataStore CurrentCities { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
               new CityDto() { Id = 1, Name = "Tehran", Description = "This is for Tehran",
               PointOfInterest = new List<PointOfInterestDto>()
               {
                   new PointOfInterestDto()
                   {
                       Id = 1,  
                       Name = "Jaye Didani 1",
                       Description = "This is Jaye Didani 1"
                   }, new PointOfInterestDto()
                   {
                       Id = 2,  
                       Name = "Jaye Didani 2",
                       Description = "This is Jaye Didani 2"
                   },
               } },
               new CityDto() { Id = 2, Name = "Shiraz", Description = "This is for Shiraz",
               PointOfInterest = new List<PointOfInterestDto>()
               {
                   new PointOfInterestDto()
                   {
                       Id = 3,
                       Name = "Jaye Didani 3",
                       Description = "This is Jaye Didani 3"
                   }, new PointOfInterestDto()
                   {
                       Id = 4,
                       Name = "Jaye Didani 4",
                       Description = "This is Jaye Didani 4"
                   },
               } },
               new CityDto() { Id = 3, Name = "Ahwaz", Description = "This is for Ahwaz",
               PointOfInterest = new List<PointOfInterestDto>()
               {
                   new PointOfInterestDto()
                   {
                       Id = 5,
                       Name = "Jaye Didani 5",
                       Description = "This is Jaye Didani 5"
                   }, new PointOfInterestDto()
                   {
                       Id = 6,
                       Name = "Jaye Didani 6",
                       Description = "This is Jaye Didani 6"
                   },
               } }
            };
        }
    }
}
