using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfoApi.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        [HttpGet("{cityId}/pointsofinterest")]

        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);

        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name ="GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfIntest = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if (pointOfIntest == null)
            {
                return NotFound();
            }

            return Ok(pointOfIntest);

        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfIntest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfIntest)
        {
            
          

            if(pointOfIntest.Description == pointOfIntest.Name)
            {
                ModelState.AddModelError("Description", "description should be  different from name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfIntestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id); //todo : use auto generatedId

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfIntestId,
                Name = pointOfIntest.Name,
                Description = pointOfIntest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, id = finalPointOfInterest.Id }, finalPointOfInterest);
              
        }
    }
}