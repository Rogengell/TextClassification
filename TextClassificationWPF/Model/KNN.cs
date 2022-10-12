using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Interop;
using TextClassificationWPF.Business;
using TextClassificationWPF.Domain;
using TextClassificationWPF.FileIO;

namespace TextClassificationWPF.Model
{
    internal class KNN
    {
        string sport = "Sport";
        string fairyTale = "Fairy Tale";

        Knowledge knowledge;
        public KNN(Knowledge k) 
        {
            knowledge = k;
        }

        public string PredictTopic(string text)
        {
            List<int> vector = Vector(text);

            List<double> disA = CalDistanceA(vector);
            List<double> disB = CalDistanceB(vector);

            List<string> neighbors = NearestNeighbors(disA, disB);

            string predictiong = Predict(neighbors);
            return predictiong;
        }

        private List<int> Vector(string text) 
        {
            List<int> vector = new List<int>();
            List<string> wordsInFile = Tokenization.Tokenize(text);
            foreach (string key in knowledge.GetBagOfWords().GetAllWordsInDictionary())
            {
                if (wordsInFile.Contains(key))
                {
                    vector.Add(1);
                }
                else
                {
                    vector.Add(0);
                }
            }
            return vector;
        }

        private List<double> CalDistanceA(List<int> vetor)
        {
            List<double> disA = new List<double>();
            for (int i = 0; i < knowledge.GetFileLists().GetA().Count(); i++)
            {
                double sum = 0;
                int index = 0;
                foreach (bool item in knowledge.GetVectors().GetVectorA()[i])
                {
                    if (item == true)
                    {
                        sum += Math.Pow(vetor[index] - 1, 2);
                    }
                    else
                    {
                        sum += Math.Pow(vetor[index] - 0, 2);
                    }
                    index++;
                }
                double dis = Math.Sqrt(sum);
                disA.Add(dis);
            }
            return disA;
        }

        private List<double> CalDistanceB(List<int> vetor)
        {
            List<double> disA = new List<double>();
            for (int i = 0; i < knowledge.GetFileLists().GetB().Count(); i++)
            {
                double sum = 0;
                int index = 0;
                foreach (bool item in knowledge.GetVectors().GetVectorB()[i])
                {
                    if (item == true)
                    {
                        sum += Math.Pow(vetor[index] - 1, 2);
                    }
                    else
                    {
                        sum += Math.Pow(vetor[index] - 0, 2);
                    }
                    index++;
                }
                double dis = Math.Sqrt(sum);
                disA.Add(dis);
            }
            return disA;
        }

        private List<string> NearestNeighbors(List<double> a, List<double> b) 
        {
            List<string> disAndTopic = new List<string>();
            double[] aDis = new double[5];
            double[] bDis = new double[5];
            for (int i = 0; i < knowledge.GetFileLists().GetA().Count(); i++)
            {
                if (i < 5)
                {
                    aDis[0] = a[i];
                }
                else
                {
                    for (int y = 0; y < aDis.Length; y++)
                    {
                        if (aDis[y] > a[i])
                        {
                            aDis[y] = a[i];
                        }
                    } 
                }
            }

            for (int i = 0; i < knowledge.GetFileLists().GetB().Count(); i++)
            {
                if (i < 5)
                {
                    bDis[0] = b[i];
                }
                else
                {
                    for (int y = 0; y < bDis.Length; y++)
                    {
                        if (bDis[y] > b[i])
                        {
                            bDis[y] = b[i];
                        }
                    }
                }
            }

            foreach (double item in aDis) 
            {
                for (int y = 0; y < bDis.Length; y++)
                {
                    if (item < bDis[y])
                    {
                        disAndTopic.Add(sport);
                    }
                }
            }

            foreach (double item in bDis)
            {
                for (int y = 0; y < aDis.Length; y++)
                {
                    if (item < aDis[y])
                    {
                        disAndTopic.Add(fairyTale);
                    }
                }
            }

            return disAndTopic;
        }

        private string Predict(List<string> neighbor) 
        {
            string myPredict = "";
            int sportCount = 0;
            int fairyTaleCount = 0;

            foreach (string s in neighbor)
            {
                if (s.Equals(sport))
                {
                    sportCount++;
                }
                else if (s.Equals(fairyTale))
                {
                    fairyTaleCount++;
                }
            }

            if (sportCount > fairyTaleCount) 
            {
                myPredict = sport;
            }
            else
            {
                myPredict = fairyTale;
            }

            return myPredict;
        }
    }
}
