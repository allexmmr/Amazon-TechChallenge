using System.Collections.Generic;
using System.Linq;

namespace Devices
{
    public class App
    {
        public int Id { get; set; }
        public int MemoryRequired { get; set; }
        public AppTypeEnum AppType { get; set; }
    }

    public enum AppTypeEnum
    {
        Foreground = 1,
        Background = 2
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            #region Example 1

            List<List<int>> foregroundAppList1 = new List<List<int>>()
            {
                new List<int> { 1, 2 },
                new List<int> { 2, 4 },
                new List<int> { 3, 6 }
            };

            List<List<int>> backgroundAppList1 = new List<List<int>>()
            {
                new List<int> { 1, 2 }
            };

            // Output: [[2,1]]
            optimalUtilization(
            7, //deviceCapacity
            foregroundAppList1,
            backgroundAppList1);

            #endregion

            #region Example 2

            List<List<int>> foregroundAppList2 = new List<List<int>>()
            {
                new List<int> { 1, 3 },
                new List<int> { 2, 5 },
                new List<int> { 3, 7 },
                new List<int> { 4, 10 }
            };

            List<List<int>> backgroundAppList2 = new List<List<int>>()
            {
                new List<int> { 1, 2 },
                new List<int> { 2, 3 },
                new List<int> { 3, 4 },
                new List<int> { 4, 5 }
            };

            // Output: [[2,4], [3,2]]
            optimalUtilization(
            10, //deviceCapacity
            foregroundAppList2,
            backgroundAppList2);

            #endregion
        }

        public static List<List<int>> optimalUtilization(int deviceCapacity,
                                                List<List<int>> foregroundAppList,
                                                List<List<int>> backgroundAppList)
        {
            // Foreground Apps
            List<App> foregroundList = new List<App>();

            foregroundAppList.ForEach(app =>
                foregroundList.Add(new App
                {
                    Id = app[0],
                    MemoryRequired = app[1],
                    AppType = AppTypeEnum.Foreground
                }));

            // Background Apps
            List<App> backgroundList = new List<App>();

            backgroundAppList.ForEach(app =>
                backgroundList.Add(new App
                {
                    Id = app[0],
                    MemoryRequired = app[1],
                    AppType = AppTypeEnum.Background
                }));

            // Order apps by memory required
            foregroundList = foregroundList.OrderByDescending(q => q.MemoryRequired).ToList();
            backgroundList = backgroundList.OrderByDescending(q => q.MemoryRequired).ToList();

            List<List<int>> result = new List<List<int>>();
            int currentCapacity = 0;

            foreach (App foregroundApp in foregroundList)
            {
                foreach (App backgroundApp in backgroundList)
                {
                    int sum = foregroundApp.MemoryRequired + backgroundApp.MemoryRequired;

                    if (sum <= deviceCapacity)
                    {
                        // Add an item to the result list
                        result.Add(new List<int> { foregroundApp.Id, backgroundApp.Id });

                        if (sum != currentCapacity)
                        {
                            // Increase the current capacity
                            currentCapacity = currentCapacity + sum;
                        }
                    }

                    if (currentCapacity > deviceCapacity)
                    {
                        // Remove an item if there is not enough capacity
                        result.RemoveAt(result.Count - 1);
                        // Decrease the current capacity
                        currentCapacity = currentCapacity - sum;
                    }
                }
            }

            // Reverse the list
            result.Reverse();
            
            return result;
        }
    }
}