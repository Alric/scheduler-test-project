using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    public class Project
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CityType City { get; set; }

        public Project() { }
        public Project(DateTime startDate, DateTime endDate, CityType city) : this()
        {
            StartDate = startDate;
            EndDate = endDate;
            City = city;
        }
    }
}
