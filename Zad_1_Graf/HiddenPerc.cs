using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad_1_Graf
{
    class HiddenPerc:Perceptron
    {
     public   HiddenPerc(double lerningRate, int inputAmount, IFunc fun,bool bias):base(lerningRate,inputAmount,fun,bias)
        {
         
        }
        public override void Study(double outputDelta)
        {
            try
            {
                //zakomentowane to wsteczna propagacja (z momentum)
                delta = fun.DerivativeFunction(sum, 1.0) * outputDelta;
                
                if (double.NaN.ToString() == delta.ToString()) throw new Exception();
                double x = 0.0;
                for (int i = 0; i < inputAmount + 1; i++)
                {
                    if (i < inputAmount)
                        x = input[i];
                    else x = 1;//bias
               //     delta = weight[i] - previousWeight[i];
               //     previousWeight[i] = weight[i];
               //     weight[i] += lerningRate * error * x + momentum * delta;

                    weight[i] = weight[i] + lerningRate * delta * x;

                }
            }
            catch(Exception error)
            {
                Console.WriteLine("Fatal Error");
            }
        }
    }
}
