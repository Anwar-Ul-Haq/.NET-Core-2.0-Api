using CityInfoApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoApi
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                 new CityDto()
                {
                     Id = 1,
                     Name = "New York City",
                     Description = "The one with that big park.",

                     PointsOfInterest = new List<PointOfInterestDto>()
                     {
                         new PointOfInterestDto()
                         {
                             Id=1,
                             Name="London park 1 ",
                             Description = "London part detail"
                         },
                         new PointOfInterestDto()
                         {
                             Id=2,
                             Name="London park 2 ",
                             Description = "London part detail"
                         },
                         new PointOfInterestDto()
                         {
                             Id=3,
                             Name="London park 3",
                             Description = "London part detail"
                         },
                         new PointOfInterestDto()
                         {
                             Id=4,
                             Name="London park 4",
                             Description = "London part detail"
                         },
                     }

                     },

                   new CityDto()
                {
                     Id = 2,
                     Name = "London",
                     Description = "The tesdfdaf with that big park.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {


                    new PointOfInterestDto()
                    {
                        Id=1,
                        Name="Leeds park 1 ",
                        Description = "Leeds part detail"
                    },
                    new PointOfInterestDto()
                    {
                        Id=2,
                        Name="Leeds park 2 ",
                        Description = "Leeds part detail"
                    },
                    new PointOfInterestDto()
                    {
                        Id=3,
                        Name="Leeds park 3",
                        Description = "London part detail"
                    },
                    new PointOfInterestDto()
                    {
                        Id=4,
                        Name="Leeds park 4",
                        Description = "London part detail"
                    },
                        }

                     },

                     new CityDto()
                {
                     Id = 3,
                     Name = "Leeds",
                     Description = "The testdfasdf with that big park.",
                     }
                     };
        }
    }
}
