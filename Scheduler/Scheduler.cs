﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Scheduler
{
    public static class Scheduler
    {
        private const string COST_CONFIGURATION_FILE = "cost_configuration.json";

        public static List<Project> ReadInput(string filename)
        {
            string json;
            using (StreamReader sr = new StreamReader(filename))
            {
                json = sr.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<List<Project>>(json);
        }

        private static Dictionary<CityType,Costs> LoadCostConfiguration()
        {
            string json;
            using (StreamReader sr = new StreamReader(COST_CONFIGURATION_FILE))
            {
                json = sr.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<Dictionary<CityType, Costs>>(json);
        }

        public static int ComputeCost(string inputFile)
        {
            var costs = LoadCostConfiguration();
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
                    var travelCost = costs[cityType].Travel;
                    var fullCost = costs[cityType].Full;
                    return total + (IsTravelDay(date) ? travelCost : fullCost);
                });
            
            return cost;
        }
    }
}
