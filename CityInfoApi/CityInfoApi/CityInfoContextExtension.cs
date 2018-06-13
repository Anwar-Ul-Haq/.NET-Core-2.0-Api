using CityInfoApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoApi
{
    public static class CityInfoContextExtension
    {

        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            if (context.Cities.Any())
            {
                return;

            }


            var cities = new List<City>()
            {
                 new City()
                {

                     Name = "New York City",
                     Description = "The one with that big park.",

                     PointsOfInterest = new List<PointOfInterest>()
                     {
                         new PointOfInterest()
                         {

                             Name="London park 1 ",
                             Description = "London part detail"
                         },
                         new PointOfInterest()
                         {

                             Name="London park 2 ",
                             Description = "London part detail"
                         },
                         new PointOfInterest()
                         {

                             Name="London park 3",
                             Description = "London part detail"
                         },
                         new PointOfInterest()
                         {
                             Name="London park 4",
                             Description = "London part detail"
                         },
                     }

                     },

                   new City()
                {

                     Name = "London",
                     Description = "The tesdfdaf with that big park.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {


                    new PointOfInterest()
                    {

                        Name="Leeds park 1 ",
                        Description = "Leeds part detail"
                    },
                    new PointOfInterest()
                    {

                        Name="Leeds park 2 ",
                        Description = "Leeds part detail"
                    },
                    new PointOfInterest()
                    {

                        Name="Leeds park 3",
                        Description = "London part detail"
                    },
                    new PointOfInterest()
                    {

                        Name="Leeds park 4",
                        Description = "London part detail"
                    },
                        }

                     },

                     new City()
                {

                     Name = "Leeds",
                     Description = "The testdfasdf with that big park.",
                     }
                     };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}
