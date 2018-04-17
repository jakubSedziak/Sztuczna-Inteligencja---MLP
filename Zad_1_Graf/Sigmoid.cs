using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad_1_Graf
{
    class Sigmoid: IFunc
    {
        public double ActivateFunction(double x,double B)
        {
            return 1.0 / (1.0 + Math.Exp(-B * x));
        }
        public double DerivativeFunction(double x,double B)
        {
            return B * Math.Exp(-B * x) / ((1.0 + Math.Exp(-B * x)) * (1.0 + Math.Exp(-B * x)));
        }
    }
}
