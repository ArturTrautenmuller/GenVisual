using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MostEficientRoute
{
    public class Population
    {
        public List<Route> Routes { get; set; }
        public int Generation { get; set; }

        public Population()
        {
            this.Routes = new List<Route>();
            this.Generation = 1;
        }

        public void Spawn(int Length,int Qtd)
        {
            Random rnd = new Random();
            for (int q = 1; q <= Qtd; q++)
            {
                Route route = new Route();
                List<int> numbers = new List<int>();
                for (int i = 1; i <= Length; i++)
                {
                    numbers.Add(i);
                }


                route.TargetsPoint = numbers.OrderBy(x => rnd.Next()).ToList();
                this.Routes.Add(route);
            }
        }
    }
}
