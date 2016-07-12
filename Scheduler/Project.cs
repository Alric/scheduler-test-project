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

        public Project() { }
        public Project(string name, DateTime startDate,
            DateTime endDate, CityType city) : this()
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            City = city;
        }
    }
}
