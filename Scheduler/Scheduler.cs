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
            return 0;
        }
    }
}
