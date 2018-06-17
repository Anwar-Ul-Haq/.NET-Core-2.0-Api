using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfoApi.Models;
using CityInfoApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;

        }

        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            var result = AutoMapper.Mapper.Map<IEnumerable<CityWithOutPointsOfInterestDto>>(cityEntities);           
             return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id , bool includePointsOfInterest = false)
        {

            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);
                        
            if(city == null)
            {
                return NotFound();
            }

            if(includePointsOfInterest)
            {
                var cityResult = AutoMapper.Mapper.Map<CityDto>(city);
               
                return Ok(cityResult);
            }

            var cityWithoutPointOfInterestResult = AutoMapper.Mapper.Map<CityWithOutPointsOfInterestDto>(city);

            return Ok(cityWithoutPointOfInterestResult);
        }
    }
}