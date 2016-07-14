using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Scheduler
{
    public static class Scheduler
    {
        // In practice, these data would be in a database or something.
        // For the sake of this exercise, we'll hard-code the reference file
        // (in a production environment the cost configuration probably wouldn't
        // be runtime configurable, but in a stable, known environment, as it is
        // not expected to be mutable)
        private const string COST_CONFIGURATION_FILE = "cost_configuration.json";

        /// <summary>
        /// Read input JSON data specifying the work schedule
        /// </summary>
        /// <param name="filename">The JSON file to read</param>
        /// <returns>A project breakdown with start and end dates and city types</returns>
        public static List<Project> ReadInput(string filename)
        {
            string json;
            using (StreamReader sr = new StreamReader(filename))
            {
                json = sr.ReadToEnd();
            }

            // Fail loudly if there are any deserialization woes
            return JsonConvert.DeserializeObject<List<Project>>(json);
        }

        /// <summary>
        /// Load the cost configuration for city types from a configuration JSON file
        /// </summary>
        /// <returns>A dictionary, keyed by CityType (low or high cost) containing
        /// data for the cost of a travel day to/from and a full day working in said city
        /// </returns>
        private static Dictionary<CityType,Costs> LoadCostConfiguration()
        {
            string json;
            using (StreamReader sr = new StreamReader(COST_CONFIGURATION_FILE))
            {
                json = sr.ReadToEnd();
            }

            // Fail loudly if there are any deserialization woes
            return JsonConvert.DeserializeObject<Dictionary<CityType, Costs>>(json);
        }

        /// <summary>
        /// Compute the cost of a set of projects specified by an input file
        /// </summary>
        /// <param name="inputFile">A JSON file specifying a list of project details</param>
        /// <returns>An int representing the cost for that set</returns>
        public static int ComputeCost(string inputFile)
        {
            var costs = LoadCostConfiguration();
            var projects = ReadInput(inputFile).OrderBy(x => x.StartDate).ToList();
            Func<DateTime, bool> IsMultiProjectDay = (date) =>
            {
                // Spec: If two (or more) projects overlap on a day, it is a full day
                return projects.Count(p => p.StartDate <= date && p.EndDate >= date) > 1;
            };
            Func<DateTime, bool> IsMidProject = (date) =>
            {
                // Spec: any day in the middle of a project is a full day
                return projects.Any(p => p.StartDate < date && p.EndDate > date);
            };
            Func<DateTime, bool> HasNeighbor = (date) =>
            {
                // Spec: If two (or more) projects push up against each other,
                //       those days are full days
                bool result = false;
                if (projects.Any(p => p.StartDate == date))
                {
                    // Determine if another project's end abuts against this start date
                    result |= projects.Any(p => p.EndDate == date.AddDays(-1));
                }
                if (projects.Any(p => p.EndDate == date))
                {
                    // Determine if another project's start abuts against this end date
                    result |= projects.Any(p => p.StartDate == date.AddDays(1));
                }
                return result;
            };
            Func<DateTime, bool> IsTravelDay = (date) =>
            {
                // Therefore, a travel day is any day that  is
                // 1. Not in the middle of a project
                // 2. Not a day where multiple projects overlap
                // 3. Is does not have another project ending the day before
                //    or another project starting the day after
                return !IsMidProject(date) && !IsMultiProjectDay(date) && !HasNeighbor(date);
            };
            // Enumerate the days, check each day in the enumeration for whether it is a travel
            // day or not, and aggregate the cost of each individual day
            var cost = projects.SelectMany(p => p.EachProjectDay())
                .GroupBy(d => d.Date)
                .Aggregate(0,(total, d) => 
                {
                    var date = d.Key;
                    var cityType = projects.Where(p => p.StartDate <= date && p.EndDate >= date)
                        .OrderByDescending(p => (int)p.City).First().City;
                    return total + (IsTravelDay(date) ? costs[cityType].Travel : costs[cityType].Full);
                });
            
            return cost;
        }
    }
}
