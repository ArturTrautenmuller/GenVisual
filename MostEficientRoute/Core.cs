using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MostEficientRoute
{
    public class Core
    {
        public Population Population { get; set; }
        public Map Map { get; set; }
        public Log Log { get; set; }
        public int qtdTargets { get; set; }
        public int qtdPopulation { get; set; }
        public double MutationRate { get; set; }

        public Core(int targets, int population,double MutationRate)
        {
            this.qtdTargets = targets;
            this.qtdPopulation = population;
            this.MutationRate = MutationRate;
            this.Log = new Log();

        }

        public void GenerateMap()
        {

            this.Map = new Map();
            this.Map.Spawn(this.qtdTargets);

           
        }

        public void GeneratePopulation()
        {
            this.Population = new Population();
            this.Population.Spawn(this.qtdTargets, this.qtdPopulation);
            this.Population.Routes.ForEach(r => r.CalculateLength(this.Map));
        }

        public Route Mutation(Route Route)
        {
            Route changedRoute = new Route();
            Random rnd = new Random();
            int t1 = rnd.Next(1, this.qtdTargets);
            int t2 = rnd.Next(1, this.qtdTargets);

            List<int> Genome = new List<int>();

            for(int i = 0; i < Route.TargetsPoint.Count;i++)
            {
                if(i == t1)
                {
                    Genome.Add(Route.TargetsPoint[t2]);
                }
                else if(i == t2)
                {
                    Genome.Add(Route.TargetsPoint[t1]);
                }
                else
                {
                    Genome.Add(Route.TargetsPoint[i]);
                }
            }

            changedRoute.TargetsPoint = Genome;
            


            return changedRoute;
        }

        public Route CrossOver(Route Father,Route Mother)
        {
            Route child = new Route();
            Random rnd = new Random();
            int splitPoint = rnd.Next(1, this.qtdTargets - 1);
            List<int> Genome = Father.TargetsPoint.Take(splitPoint).ToList();
            foreach(int gene in Mother.TargetsPoint)
            {
                if (!Genome.Contains(gene))
                {
                    Genome.Add(gene);
                }
            }

            child.TargetsPoint = Genome;
            Random mrnd = new Random();
            if(mrnd.Next(1,100) <= this.MutationRate*100)
            {
                child = Mutation(child);
            }
            
            child.CalculateLength(this.Map);
            return child;
        }

        public List<Route> CrossOverAll(List<Route> group)
        {
            List<Route> children = new List<Route>();
            Random rnd = new Random();
            group = group.OrderBy(x => rnd.Next()).ToList();
            List<Route> Fathers = group.Take(group.Count / 2).ToList();
            List<Route> Mothers = group.Skip(group.Count / 2).ToList();

            for(int i = 0; i < Fathers.Count; i++)
            {
                
                children.Add(CrossOver(Fathers[i], Mothers[i]));
                children.Add(CrossOver(Fathers[i], Mothers[i]));
            }


            return children;
        }

        public void Advance()
        {
            this.Population.Routes = this.Population.Routes.OrderBy(r => r.Length).ToList();
            this.Population.Routes = this.Population.Routes.Take(this.Population.Routes.Count/2).ToList();

            this.Population.Routes.AddRange(CrossOverAll(this.Population.Routes));

            this.Population.Generation++;
            PlotConsole();
            this.Log.SaveGenStatus(this.Population);
        }

        public void Execute(int generations)
        {
            this.GenerateMap();
            this.GeneratePopulation();
            this.PlotConsole();
            this.Log.SaveGenStatus(this.Population);
            
            for (var i = 0; i < generations; i++)
            {
                
                this.Advance();
            }

            Log.SaveLog(@"C:\TERZ\Genetic");
        }

        public void PlotConsole()
        {
            Console.WriteLine("Map--------->");
            foreach(KeyValuePair<int,Target> target in Map.Targets)
            {
                Console.WriteLine($"{target.Key}. {target.Value.Coordenates.X} , {target.Value.Coordenates.Y}");
            }

            Console.WriteLine("Poputalion Data--------->");
            Console.WriteLine($"Generearion: {Population.Generation}");
            Console.WriteLine($"MaxLength: {Population.Routes.Max(r => r.Length)}");
            Console.WriteLine($"MinLength: {Population.Routes.Min(r => r.Length)}");

        }

        
    }
}
