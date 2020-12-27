using System;
using System.Collections.Generic;
using System.Text;

namespace MostEficientRoute
{
    public class Map
    {
        public Dictionary<int,Target> Targets { get; set; }

        public Map()
        {
            this.Targets = new Dictionary<int, Target>();
        }

        public void Spawn(int Qtd)
        {
            if (Qtd < 2) throw new Exception("Quantity must be greater then 2");
            for(int i = 1; i <= Qtd; i++)
            {
                Random random = new Random();
                Target target = new Target(random.Next(0,10000),random.Next(0,10000));
                this.Targets.Add(i,target);
            }
        }
    }
}
