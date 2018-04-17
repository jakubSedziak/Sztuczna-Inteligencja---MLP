using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad_1_Graf
{
    class Linear:IFunc
    {
       public double ActivateFunction(double x, double B)
        {
            return x;
        }
        public double DerivativeFunction(double x, double B)
        {
            return 1;
        }
    }
}
