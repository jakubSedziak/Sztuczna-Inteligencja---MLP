using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zad_1_Graf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        //ZAD 1
        private void button1_Click(object sender, EventArgs e)
        {

            double lerningSpeed = 0.3;
            //Wykresy
            double lerningSpeed2 = 0.3;
            double lerningSpeed3 = 0.3;
            double error2 = 0;
            double error3 = 0;
            bool osiagnieto1 = true, osiagnieto2 = true, osiagnieto3 = true;

            /////////////////////////////
            double[,] inputData = new double[4, 4]
            { {1,0,0,0}, {0,1,0,0}, {0,0,1,0}, {0,0,0,1}};
            double[][] testy = new double[4][];
            for (int i = 0; i < 4; i++)
            { testy[i] = new double[4] { 0, 0, 0, 0 };
                testy[i][i] = 1;
            }

            int inputAmount = 4;
            int outputAmount = 4;
            double[,] outputData = inputData;
            IFunc inputFunction = new Linear();
            IFunc sigmoidFunction = new Sigmoid();
            IFunc outputFunction = new Sigmoid();
            double error = 0;
            MLP s1 = new MLP(inputAmount, getNeuralAmount(), outputAmount, lerningSpeed, inputFunction, sigmoidFunction, outputFunction, Bias.Checked);
            //Wykresy
            //MLP s1 = new MLP(inputAmount, 1, outputAmount, lerningSpeed, inputFunction, sigmoidFunction, outputFunction, Bias.Checked);
            // MLP s2 = new MLP(inputAmount, getNeuralAmount(), outputAmount, lerningSpeed2, inputFunction, sigmoidFunction, outputFunction,  Bias.Checked);
            // MLP s3 = new MLP(inputAmount, getNeuralAmount(), outputAmount, lerningSpeed3, inputFunction, sigmoidFunction, outputFunction,  Bias.Checked);
            //////////////////////////
            //           Wykres.Series.Add("Lerning Rate 0.3");
            //           Wykres.Series.Add("Lerning Rate 0.6");
            //           Wykres.Series.Add("Lerning Rate 0.9");

            /*
            Wykres.Series["Lerning Rate 0.3"].Points.Clear();
            Wykres.Series["Lerning Rate 0.6"].Points.Clear();
            Wykres.Series["Lerning Rate 0.9"].Points.Clear();
            */

            Wykres.Series["Dane 1"].Points.Clear();
            Wykres.Series["Dane 2"].Points.Clear();
            Wykres.Series["Dane 3"].Points.Clear();

            for (int i = 0; i < 10000; i++)
            {
                

                s1.MultiLayerPerceptron(4, inputData, outputData);
                error = s1.GetGlobalError();
                Wykres.Series["Dane 1"].Points.AddXY(i, error);
              /*//  s2.MultiLayerPerceptron(4, inputData, outputData);
                error2 = s2.GetGlobalError();
                Wykres.Series["Dane 2"].Points.AddXY(i, error2);
                s3.MultiLayerPerceptron(4, inputData, outputData);
                error3 = s3.GetGlobalError();
                Wykres.Series["Dane 3"].Points.AddXY(i, error3);
                /////////*/
                
                // Do Labeli
                if (error <= 0.01 && osiagnieto1)
                { label22.Text = i.ToString(); osiagnieto1 = false; }
                if (error2 <= 0.01 && osiagnieto2)
                { label23.Text = i.ToString(); osiagnieto2 = false; }
                if (error3 <= 0.01 && osiagnieto3)
                { label24.Text = i.ToString(); osiagnieto3 = false; }


            }
            List<double> teemp = s1.getWeight();
            
            saveFile(@"wagi.txt", teemp);
            teemp.Clear();
            for (int i = 0; i < 4; i++)
            {
                double[] temp = s1.Count(testy[i]);
                foreach (double x in temp)
                    teemp.Add(x);
            }
            saveFile(@"wyniki.txt", teemp);
            double[] input = new double[4];
            double[] results = new double[16];
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    input[i] = inputData[i, j];
                }
                double[] output = new double[4];
                output = s1.Count2(input);
                for (int i = 0; i < getNeuralAmount(); i++)
                {
                    results[j * getNeuralAmount() + i] = output[i];
                }
                label1.Text = String.Format("{0:N3}", results[0]);
                label2.Text = String.Format("{0:N3}", results[1]);
                label3.Text = String.Format("{0:N3}", results[2]);
                label4.Text = String.Format("{0:N3}", results[3]);
                label8.Text = String.Format("{0:N3}", results[4]);
                label7.Text = String.Format("{0:N3}", results[5]);
                label6.Text = String.Format("{0:N3}", results[6]);
                label5.Text = String.Format("{0:N3}", results[7]);
                label9.Text = String.Format("{0:N3}", results[8]);
                label10.Text = String.Format("{0:N3}", results[9]);
                label11.Text = String.Format("{0:N3}", results[10]);
                label12.Text = String.Format("{0:N3}", results[11]);
            }

        }
        private int getNeuralAmount()
        {
            return Convert.ToInt32(InputNeuronAmount.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double lerningSpeed = 0.1;
            bool osiagnieto1 = true, osiagnieto2 = true, osiagnieto3 = true;
            /////WCZYTYWANIE DANYCH//////
            string path = @"1.txt";
            List<double> inputList = new List<double>(); ;
            List<double> outputList = new List<double>();
            ReadFile(@"1.txt", inputList, outputList);
            List<double> testNumbers = new List<double>();
            List<double> testOutputs = new List<double>();
            ReadFile(@"test.txt", testNumbers, testOutputs);
            /////////////////////////////
            int liczba_danych1 = inputList.Count;
            double[] inputData = new double[liczba_danych1];
            inputList.CopyTo(inputData);
            int inputAmount = 1;
            int outputAmount = 1;
            double[] outputData = new double[liczba_danych1];
            outputList.CopyTo(outputData);
            IFunc inputFunction = new Linear();
            IFunc sigmoidFunction = new Sigmoid();
            IFunc outputFunction = new Linear();
            double error = 0;
            MLP s1 = new MLP(inputAmount, getNeuralAmount(), outputAmount, lerningSpeed, inputFunction, sigmoidFunction, outputFunction, Bias.Checked);


            Wykres.Series["Dane 1"].Points.Clear();
            Wykres.Series["Dane 2"].Points.Clear();
            Wykres.Series["Dane 3"].Points.Clear();
            for (int i = 0; i < 10000; i++)
            {
                s1.MultiLayerPerceptron(1, inputData, outputData);
                error = s1.GetGlobalError();

 //               if (error > 0.001)
 //                   break;
            }
            //DANE TESTWOE

            double[] datain = new double[testNumbers.Count];
            testNumbers.CopyTo(datain);
            double[] dataout = new double[testNumbers.Count];
            testOutputs.CopyTo(dataout);
            for (int i = 0; i < testNumbers.Count; i++)
            {
                Wykres.Series["Dane 1"].Points.AddXY(datain[i], dataout[i]);
            }
            
            for (int i = 0; i < testNumbers.Count; i++)
            {
                Wykres.Series["Dane 2"].Points.AddXY(datain[i], s1.CountOne(datain[i]));
            }
        }
        public void ReadFile(string path, List<double> inputs,List<double> outputs)
        {
            string s;
            using (StreamReader sr = new StreamReader(path))//wczytanie
            {
                s = sr.ReadToEnd();

            }
            List<string> numb = new List<string>();
            int it = 0;
            string temp = "";
            while( s.Length!=0)
            {
                while(s[it]!=' ' && s[it]!='\r' && s[it]!='\n'&& s[it]!='\0' && s.Length>it+1)
                {
                    if (s[it] == '.')
                    {
                        temp+= ',';
                        it++;
                        continue;
                    }
                    temp += s[it];
                    it++;
                   
                }
                if (s.Length == temp.Length)
                {
                    temp += s[it];
                }
                if (temp != " ")
                {
                    if (s[it] == ' ')
                        inputs.Add(Convert.ToDouble(temp));
                    else
                        outputs.Add(Convert.ToDouble(temp));
                }
                if (s.Length != it)
                    s = s.Remove(0, it + 1);
                
                it = 0;
                if(!temp.Equals(" "))
               
                temp = " ";
            }
        }
        public void saveFile(string path, List<double> teemp)
        {
            using (StreamWriter sr = new StreamWriter(path))//wczytanie
            {
                foreach (double x in teemp)
                    sr.WriteLine(x.ToString() + " ");

            }
        }
    }
}
