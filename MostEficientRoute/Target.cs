using System;
using System.Collections.Generic;
using System.Text;

namespace MostEficientRoute
{
    public class Target
    {
        public Coordenates Coordenates { get; set; }

        public Target(double X,double Y)
        {
            this.Coordenates = new Coordenates(X,Y);
        }
    }
}
