using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;
using CityInfoApi.Entities;
using CityInfoApi.Models;
using CityInfoApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityInfoApi.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;
        private ICityInfoRepository _cityInfoRepository;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger , IMailService mailService,ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{cityId}/pointsofinterest")]

        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {            
                               
                if (!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"city does not exists with city id {cityId}");
                    return NotFound();
                }

                var pointsOFInterestForCity = _cityInfoRepository.GetPointsOfInteresByCityId(cityId);

                var pointsOFInterestForCityResult = AutoMapper.Mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOFInterestForCity);
                
                return Ok(pointsOFInterestForCityResult);
                
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception thrown while getting  point of interest for city id {cityId}.", ex);
                return StatusCode(500, "A  proglem happend while handling  your request");
            }
            
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
          
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfIntest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

            if (pointOfIntest == null)
            {
                return NotFound();
            }

            var pointOfInterestResult = AutoMapper.Mapper.Map<PointOfInterestDto>(pointOfIntest);      

            return Ok(pointOfInterestResult);

        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfIntest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfIntest)
        {
            
            if (pointOfIntest.Description == pointOfIntest.Name)
            {
                ModelState.AddModelError("Description", "description should be  different from name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = AutoMapper.Mapper.Map<PointOfInterest>(pointOfIntest);
            _cityInfoRepository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            if(!_cityInfoRepository.save())
            {
                return StatusCode(500, "Error while handling your request .Please try again");
            }

            var createdPointOfInterestToReturn = AutoMapper.Mapper.Map<PointOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new {cityId, id = createdPointOfInterestToReturn.Id }, createdPointOfInterestToReturn);

        }


        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
            [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p =>
            p.Id == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }


        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch =
                   new PointOfInterestForUpdateDto()
                   {
                       Name = pointOfInterestFromStore.Name,
                       Description = pointOfInterestFromStore.Description
                   };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError("Description", "The provided description should be different from the name.");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestFromStore);

            _mailService.Send("Point of Interest deleted", $"Point of  interest with Id {pointOfInterestFromStore.Id} has been deleted");

            return NoContent();
        }
    }
}