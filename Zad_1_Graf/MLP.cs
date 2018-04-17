using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zad_1_Graf
{
    class MLP
    {
        int inputAmount;
        int HiddenAmmount;
        int outputAmount;
        double globalError = 0.0;
        Perceptron[] inputPerc;
        Perceptron[] hiddenPerc;
        Perceptron[] outputPerc;
        //Do zad 2
        public MLP(int inputAmount, int HiddenAmmount, int outputAmount, double lerningRate, IFunc inp, IFunc hidd, IFunc outputfun, bool bias)
        {
            inputPerc = new Perceptron[inputAmount];
            hiddenPerc = new Perceptron[HiddenAmmount];
            outputPerc = new Perceptron[outputAmount];
            this.inputAmount = inputAmount;
            this.HiddenAmmount = HiddenAmmount;
            this.outputAmount = outputAmount;
            for (int i = 0; i < outputAmount; i++)
                outputPerc[i] = new CopyPerc(lerningRate, HiddenAmmount, outputfun, bias);
            for (int i = 0; i < inputAmount; i++)
                inputPerc[i] = new Perceptron(lerningRate, 1, inp, bias);
            for (int i = 0; i < HiddenAmmount; i++)
                hiddenPerc[i] = new HiddenPerc(lerningRate, inputAmount, hidd, bias);

        }
        public void MultiLayerPerceptron(int epoch, double[,] dataInput, double[,] expected)
        {

            double[] GlobalError = new double[epoch];
            for (int j = 0; j < epoch; j++)
            {
                double[] inp = new double[1];
                double[] input = new double[this.inputAmount];
                double[] hidden = new double[this.HiddenAmmount];
                double[] output = new double[this.outputAmount];
                for (int i = 0; i < inputAmount; i++)
                {
                    inp[0] = dataInput[j, i]; //[i,j]
                    input[i] = inputPerc[i].Count(inp);

                }
                for (int i = 0; i < HiddenAmmount; i++)
                {
                    hidden[i] = hiddenPerc[i].Count(input);
                }
                for (int i = 0; i < outputAmount; i++)
                {
                    output[i] = outputPerc[i].Count(hidden);
                }
                double error = 0;

                for (int i = 0; i < outputAmount; i++)
                {
                    error = output[i] - expected[j, i];//[i,j]
                    error = error * error;
                    GlobalError[j] += (error * 0.5);
                }
                double[] Delta = new double[HiddenAmmount];
                for (int i = 0; i < outputAmount; i++)
                {
                    outputPerc[i].Study(expected[j, i]);//[i,j]
                    for(int k=0;k<HiddenAmmount;k++)
                    Delta[k] += outputPerc[i].delta*outputPerc[i].getWeight(k);
                }

                for (int i = 0; i < HiddenAmmount; i++)
                {
                    hiddenPerc[i].Study(Delta[i]);
                }
               
            }
            globalError = GlobalError[0];
            for (int i = 1; i < epoch; i++)
                if (GlobalError[i] > globalError)
                    globalError = GlobalError[i];
        }
        //ZAD 2
        public void MultiLayerPerceptron(int epoch, double[] dataInput, double[] expected)
        {

            double[] GlobalError = new double[epoch];
            for (int j = 0; j < epoch; j++)
            {
                double[] inp = new double[1];
                double[] input = new double[this.inputAmount];
                double[] hidden = new double[this.HiddenAmmount];
                double[] output = new double[this.outputAmount];
               
                    inp[0] = dataInput[j];
                    input[0] = inputPerc[0].Count(inp);

                for (int i = 0; i < HiddenAmmount; i++)
                {
                    hidden[i] = hiddenPerc[i].Count(input);
                }
                for (int i = 0; i < outputAmount; i++)
                {
                    output[i] = outputPerc[i].Count(hidden);
                }
                double error = 0;

                for (int i = 0; i < outputAmount; i++)
                {
                    error = output[i] - expected[j];//[i,j]
                    error = error * error;
                    GlobalError[j] += (error * 0.5);
                }
                double[] Delta = new double[HiddenAmmount];
                for (int i = 0; i < outputAmount; i++)
                {
                    outputPerc[i].Study(expected[j]);//[i,j]    
                    for (int k = 0; k < HiddenAmmount; k++)
                        Delta[k] += outputPerc[i].delta * outputPerc[i].getWeight(k);
                }

                for (int i = 0; i < HiddenAmmount; i++)
                {
                    hiddenPerc[i].Study(Delta[i]);
                }
            }
            globalError = GlobalError[0];
            for (int i = 1; i < epoch; i++)
                if (GlobalError[i] > globalError)
                    globalError = GlobalError[i];
        }
        //
        public double GetGlobalError()
        {
            return globalError;
        }
        public double[] Count(double[] inputdane)
        {
            double[] temp = new double[outputAmount];
            double[] inp = new double[1];
            double[] input = new double[inputAmount];
            double[] hidden = new double[HiddenAmmount];
            double[] output1 = new double[outputAmount];
            for (int i = 0; i < inputAmount; i++)
            {
                inp[0] = inputdane[i];
                input[i] = inputPerc[i].Count(inp);
            }
            for (int i = 0; i < HiddenAmmount; i++)
            {
                hidden[i] = hiddenPerc[i].Count(input);
            }
            for (int i = 0; i < outputAmount; i++)
            {
                temp[i] = outputPerc[i].Count(hidden);
            }
            return temp;
        }
        //Zad 2
        public double[] Count2(double[] inputdane)
        {
            double[] temp = new double[outputAmount];
            double[] inp = new double[1];
            double[] input = new double[inputAmount];
            double[] hidden = new double[HiddenAmmount];
            double[] output1 = new double[outputAmount];
            for (int i = 0; i < inputAmount; i++)
            {
                inp[0] = inputdane[i];
                input[i] = inputPerc[i].Count(inp);
            }
            for (int i = 0; i < HiddenAmmount; i++)
            {
                hidden[i] = hiddenPerc[i].Count(input);
            }
            for (int i = 0; i < outputAmount; i++)
            {
                temp[i] = outputPerc[i].Count(hidden);
            }
            return hidden;
        }
        //Zad 2
        public double CountOne(double inputdane)
        {
            double temp = new double();
            double[] inp = new double[1];
            double[] input = new double[inputAmount];
            double[] hidden = new double[HiddenAmmount];
            double[] output1 = new double[outputAmount];
            for (int i = 0; i < inputAmount; i++)
            {
                inp[0] = inputdane;
                input[i] = inputPerc[i].Count(inp);
            }
            for (int i = 0; i < HiddenAmmount; i++)
            {
                hidden[i] = hiddenPerc[i].Count(input);
            }
            for (int i = 0; i < outputAmount; i++)
            {
                temp = outputPerc[i].Count(hidden);
            }
            return temp;
        }
        public List<double> getWeight()
        {
            List<double> weightList = new List<double>();
            foreach (Perceptron p in inputPerc)
            {
                double[] temp = p.getWeight();
                foreach(double x in temp)
                weightList.Add(x);
            }
            foreach (Perceptron p in hiddenPerc)
            {
                double[] temp = p.getWeight();
                foreach (double x in temp)
                    weightList.Add(x);
            }
            foreach (Perceptron p in outputPerc)
            {
                double[] temp = p.getWeight();
                foreach (double x in temp)
                    weightList.Add(x);
            }
            return weightList;
        }
    }

}
