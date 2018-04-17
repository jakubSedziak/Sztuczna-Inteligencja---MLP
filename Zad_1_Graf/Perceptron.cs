using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad_1_Graf
{
    class Perceptron
    {
        public double delta { get; protected set; }
        protected bool bias;
        protected static Random r = new Random();
        protected double sum;    
        protected int inputAmount;
        protected double outputAmount;
        protected double[] input;
        protected double error;
        protected double lerningRate;
        protected double[] weight;
        protected double[] previousWeight;
        protected double momentum = 0.01;
        protected IFunc fun;
        public Perceptron(double lerningRate,int inputAmount, IFunc fun,bool bias)
        {
            this.bias = bias;
            this.fun = fun;
            input = new double[inputAmount];
            weight = new double[inputAmount+1];
            previousWeight = new double[inputAmount + 1];
            this.inputAmount = inputAmount;
            for (int i = 0; i < inputAmount+1; i++)
            {
                weight[i] = GRandom(-1, 1);
            }
          
            this.lerningRate = lerningRate;
        }
        public double Count(double[] inputTable)
        {
            for (int i = 0; i < inputAmount; i++)
                input[i] = inputTable[i];
            sum = countSum(weight, inputTable);
            outputAmount = fun.ActivateFunction(sum, 1.0);
            return outputAmount;
        }
        public  double countSum(double[] weight, double[] inputTable)
        {
            double sum = 0; 
            double x = 0.0;
            for (int i=0;i<inputAmount+1;i++)
            {
                if (i < inputAmount)
                    x = inputTable[i];
                else if (bias)
                    x = 1;                    
                else if (!bias) x = 0;
                sum += x * weight[i];
            }
            
            return sum;
        }
        public double GRandom(double min, double max)
        {
            double x = new double();
            x = (r.NextDouble() * (max - min)) + min;
            x = Math.Round(x, 4);
            return x;
        }
        public double getWeight(int i)
        {
            return weight[i];
        }
        public double getSum()
        { return sum; }
        public virtual void Study(double expect)
        {
        }
        public double[] getWeight() {
            return weight;
        }


    }
}
