using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MostEficientRoute
{
    public class Log
    {
        public List<GenInfo> genInfos { get; set; }
        public Map Map { get; set; }
        public Log()
        {
            this.genInfos = new List<GenInfo>();
        }

        public void SaveGenStatus(Population population)
        {
            GenInfo genInfo = new GenInfo();
            genInfo.Generation = population.Generation;
            genInfo.MaxLength = population.Routes.Max(r => r.Length);
            genInfo.MinLength = population.Routes.Min(r => r.Length);
            genInfo.AvgLength = population.Routes.Average(r => r.Length);
            genInfo.Routes = population.Routes;

            this.genInfos.Add(genInfo);
        }

        public void SaveLog(string path)
        {
            SaveGenLog(path);
            SaveRouteLog(path);
        }

        public void SaveGenLog(string path)
        {
            string content = "";
            string header = "Generation;MaxLength;MinLength;AvgLength";
            content += header + Environment.NewLine;

            foreach(GenInfo genInfo in this.genInfos)
            {
                content += $"{genInfo.Generation};{genInfo.MaxLength};{genInfo.MinLength},{genInfo.AvgLength}" + Environment.NewLine; 
            }

            System.IO.File.WriteAllText(Path.Combine(path, "generation.csv"), content);

        }

        public void SaveRouteLog(string path)
        {
            string content = "";
            string header = "Generation;Route;Length";
            content += header + Environment.NewLine;

            foreach(GenInfo genInfo in this.genInfos)
            {
                foreach(Route route in genInfo.Routes)
                {
                    content += $"{genInfo.Generation},{string.Join("-",route.TargetsPoint)},{route.Length}" + Environment.NewLine;
                }
            }

            System.IO.File.WriteAllText(Path.Combine(path, "routes.csv"), content);

        }
    }

    public class GenInfo
    {
        public int Generation { get; set; }
        public double MaxLength { get; set; }
        public double MinLength { get; set; }
        public double AvgLength { get; set; }
        public List<Route> Routes { get; set; }
    }
}
