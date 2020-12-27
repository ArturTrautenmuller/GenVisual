using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MostEficientRoute;

namespace GenVisual.Controllers
{
    public class GenController : Controller
    {
        public Log Execute([FromQuery(Name = "target")] int target, [FromQuery(Name = "population")] int population, [FromQuery(Name = "generations")] int generations, [FromQuery(Name = "mutation")] int mutation)
        {
            Log log = new Log();
            Core core = new Core(target,population,(double)mutation/100);
            core.GenerateMap();
            core.GeneratePopulation();
            log.Map = core.Map;
            log.SaveGenStatus(core.Population);

            for(int i = 0; i < generations - 1; i++)
            {
                core.Population.Routes = core.Population.Routes.OrderBy(r => r.Length).ToList();
                core.Population.Routes = core.Population.Routes.Take(core.Population.Routes.Count / 2).ToList();

                core.Population.Routes.AddRange(core.CrossOverAll(core.Population.Routes));

                core.Population.Generation++;
               
                log.SaveGenStatus(core.Population);
            }



            return log;
        }
    }
}