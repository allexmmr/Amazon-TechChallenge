using System;
using System.Collections.Generic;
using System.Linq;

namespace Steakhouses
{
    public class Steakhouse
    {
        public string Name { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            nearestXsteakHouses(
                3,
                new int[,] { { 1, -3 }, { 1, 2 }, { 3, 4 } },
                1);
        }

        public static List<List<int>> nearestXsteakHouses(int totalSteakhouses,
                                            int[,] allLocations,
                                               int numSteakhouses)
        {
            List<Steakhouse> steakhouseList = new List<Steakhouse>();

            for (int i = 0; i < totalSteakhouses; i++)
            {
                steakhouseList.Add(new Steakhouse
                {
                    Name = $"Steakhouse #{i + 1}",
                    Latitude = allLocations[i, 0],
                    Longitude = allLocations[i, 1]
                });
            }

            Steakhouse currentLocation = new Steakhouse
            {
                Latitude = 0,
                Longitude = 0
            };

            List<Steakhouse> nearByPlaces = steakhouseList
                .Where(q => q != currentLocation).
                OrderBy(q => CalculateDistance(currentLocation, q)).
                Take(numSteakhouses)
                .ToList();

            if (nearByPlaces == null)
                return null;

            return nearByPlaces.
                ConvertAll(q => new List<int> { q.Latitude, q.Longitude });
        }

        private static double CalculateDistance(Steakhouse currentLocation,
                                    Steakhouse targetLocation)
        {
            return Math.Pow(targetLocation.Latitude - currentLocation.Latitude, 2) + Math.Pow(targetLocation.Longitude - currentLocation.Longitude, 2);
        }
    }
}