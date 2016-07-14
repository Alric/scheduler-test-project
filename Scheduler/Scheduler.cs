using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Scheduler
{
    public static class Scheduler
    {
        public static List<Project> ReadInput(string filename)
        {
            string json;
            using (StreamReader sr = new StreamReader(filename))
            {
                json = sr.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(json))
                return JsonConvert.DeserializeObject<List<Project>>(json);

            return new List<Project>();
        }

        public static int ComputeCost(string inputFile)
        {
            var projects = ReadInput(inputFile).OrderBy(x => x.StartDate).ToList();
            List<DateTime> datesAccountedFor = new List<DateTime>();
            Func<DateTime, bool> IsMultiProjectDay = (date) =>
            {
                return projects.Count(p => p.StartDate <= date && p.EndDate >= date) > 1;
            };
            Func<DateTime, bool> IsMidProject = (date) =>
            {
                return projects.Any(p => p.StartDate < date && p.EndDate > date);
            };
            Func<DateTime, bool> HasEmptyNeighbor = (date) =>
            {
                return !projects.Any(p => p.EndDate == date.AddDays(-1) || p.StartDate == date.AddDays(1));
            };
            Func<DateTime, bool> IsTravelDay = (date) =>
            {
                return !IsMidProject(date) && HasEmptyNeighbor(date) && !IsMultiProjectDay(date);
            };
            var cost = projects.SelectMany(p => p.EachProjectDay())
                .GroupBy(d => d.Date)
                .Aggregate(0,(total, d) => 
                {
                    var date = d.Key;
                    var cityType = projects.Where(p => p.StartDate <= date && p.EndDate >= date)
                        .OrderByDescending(p => (int)p.City).First().City;
                    var travelCost = cityType == CityType.LowCost ? 45 : 55;
                    var fullCost = cityType == CityType.LowCost ? 75 : 85;
                    return total + (IsTravelDay(date) ? travelCost : fullCost);
                });
            
            return cost;
        }
    }
}
