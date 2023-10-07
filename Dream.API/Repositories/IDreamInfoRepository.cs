using Dream.API.Entities;

namespace Dream.API.Repositories
{
    public interface IDreamInfoRepository
    {
        // IEnumerable<City> GetCities();
        //Async Tasks--
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int cityId,bool includePointsOfInterest);    
        Task<bool> CityExistAsync(int cityId); 
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId);
        Task CreatePointOfInterestAsync(int cityId, PointOfInterest pointOfInterestId);
        Task<bool> SaveChangesAsync();
        Task UpdatePointOfInterestAsync(int cityId, int pointOfInterestId, PointOfInterest pointOfInterest);

        void DeletePointOfInterest(PointOfInterest pointOfInterest);
    }
}
