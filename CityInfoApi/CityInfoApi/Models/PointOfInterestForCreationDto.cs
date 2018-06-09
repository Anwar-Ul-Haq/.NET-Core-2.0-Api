using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoApi.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage ="Name can't be null")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
       
        
    }
}
