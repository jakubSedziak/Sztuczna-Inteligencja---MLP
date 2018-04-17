using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad_1_Graf
{
    class CopyPerc : Perceptron
    {
       public CopyPerc(double lerningRate, int inputAmount, IFunc fun,bool bias) :
            base(lerningRate, inputAmount, fun,bias)
        {

        }
        public override void Study(double expected)
        {
            try
            {
                error = expected - outputAmount;
                delta = error * fun.DerivativeFunction(sum, 1);
                if(delta == Double.NaN) throw new Exception();
                double x = 0.0;
                for (int i = 0; i < inputAmount + 1; i++)
                {
                    if (i < inputAmount)
                        x = input[i];
                    else x = 1;
                  //  delta = weight[i] - previousWeight[i];
                  //  previousWeight[i] = weight[i];
                  //  weight[i] += lerningRate * error * x + momentum * delta;

                     weight[i] =  weight[i] + lerningRate * delta * x;
                }
            }
            catch(Exception error)
            {
                Console.Write("Fatal Error");
            }
        }
    }
}
