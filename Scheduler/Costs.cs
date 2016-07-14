using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler
{
    /// <summary>
    /// A data container class for the cost of a city
    /// Normally I'd use a struct here, but I didn't find it worthwhile
    /// writing a custom JSON converter just for this
    /// </summary>
    public class Costs
    {
        public int Travel { get; set; }
        public int Full { get; set; }
    }
}
