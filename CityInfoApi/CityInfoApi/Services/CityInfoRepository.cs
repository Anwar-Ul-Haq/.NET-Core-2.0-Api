using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfoApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfoApi.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.Include(p=>p.PointsOfInterest).OrderBy(x => x.Name).ToList();
        }

        public City GetCity(int cityId , bool includePointsOfInterest)
        {
           if(includePointsOfInterest)
            {
                return _context.Cities.Include(c => c.PointsOfInterest).Where(x => x.Id == cityId).FirstOrDefault();   
            }

            return _context.Cities.Where(x => x.Id == cityId).FirstOrDefault();

        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
        {
            return _context.PointsOfInterest.Where(x => x.CityId == cityId && x.Id == pointOfInterestId).FirstOrDefault();
        }

        public IEnumerable<PointOfInterest> GetPointsOfInteresByCityId(int cityId)
        {
            return _context.PointsOfInterest.OrderBy(x => x.CityId==cityId).ToList();
        }

        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(c => c.Id == cityId);
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityId, false);

            city.PointsOfInterest.Add(pointOfInterest);

            save();



        }

        public bool save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
