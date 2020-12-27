using System;
using System.Collections.Generic;
using System.Text;

namespace MostEficientRoute
{
    public class Route
    {
        public List<int> TargetsPoint { get; set; }
        public double Length { get; set; }
        public Route()
        {
            this.TargetsPoint = new List<int>();
        }

        public void CalculateLength(Map map)
        {
           
            this.Length = 0;
            for(int i = 0; i < this.TargetsPoint.Count - 1; i++)
            {
                Target start = map.Targets[this.TargetsPoint[i]];
                Target end = map.Targets[this.TargetsPoint[i + 1]];

                this.Length += Math.Pow(Math.Pow(end.Coordenates.X - start.Coordenates.X,2) + Math.Pow(end.Coordenates.Y - start.Coordenates.Y,2), 0.5);
            }

        }
    }
}
