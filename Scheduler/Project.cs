using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Project
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CityType City { get; set; }

        /// <summary>
        /// Build an Enumerator over the date range contained in this project
        /// </summary>
        /// <returns>A structure that can be enumerated over where each element
        /// represents a date in the range
        /// </returns>
        public IEnumerable<DateTime> EachProjectDay()
        {
            for (var day = StartDate.Date; day.Date <= EndDate.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
