using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad_1_Graf
{
    interface IFunc
    {
        double ActivateFunction(double x, double B);
        double DerivativeFunction(double x, double B);
    }
}
