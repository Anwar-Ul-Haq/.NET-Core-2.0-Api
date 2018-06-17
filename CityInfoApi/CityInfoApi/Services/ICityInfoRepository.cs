using CityInfoApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoApi.Services
{
   public interface ICityInfoRepository
    {
       IEnumerable<City>   GetCities();


        City GetCity(int cityId, bool includePointsOfInterest);


        IEnumerable<PointOfInterest> GetPointsOfInteresByCityId(int cityId);

        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);
        bool CityExists(int cityId);
        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
        bool save();
    }
}
