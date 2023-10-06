using Dream.API.DbContext;
using Dream.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dream.API.Repositories
{
    public class DreamInfoRepository : IDreamInfoRepository
    {
        //DbContext
        #region Ctor Injections
        private readonly DreamApiDbContext _context;

        public DreamInfoRepository(DreamApiDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        #endregion

        //Get Cities
        #region Get All Cities
        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(c => c.Name).ToListAsync();
        }
        #endregion

        //Get One City (Question is do you want to Include PointsOfInterest?)
        #region Get City by Id
        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await _context.Cities.Include(c => c.PointOfInterest).Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }

            return await _context.Cities.Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }
        #endregion


        //Get Points Of Interest for a City by CityId
        #region Get Points Of Interest
        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestAsync(int cityId)
        {
            return await _context.PointOfInterests.Where(p => p.CityId == cityId).ToListAsync();
        }
        #endregion

        //Get a Point Of Interest for a City 
        #region Get a Point Of Interest
        public async Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointOfInterests.Where(p => p.CityId == cityId && p.Id == pointOfInterestId).FirstOrDefaultAsync();
        }
        #endregion

        public async Task<bool> CityExistAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task CreatePointOfInterestAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);

            if (city != null)
            {
                city.PointOfInterest.Add(pointOfInterest);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0); 
        }

        public Task UpdatePointOfInterestAsync(int cityId, int pointOfInterestId, PointOfInterest pointOfInterest)
        {
            throw new NotImplementedException();
        }
    }
}
