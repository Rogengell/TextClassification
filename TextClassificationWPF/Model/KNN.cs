using Microsoft.VisualStudio.PlatformUI;
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
        /**
         * Variables for later use in the KNN
         */
        string CatagoryOne = "Sport";
        string CatagoryTwo = "Fairy Tale";
        int KNNNeighbor = 5;

        Knowledge knowledge;
        /*
         * Saves the knowledge to the KNN for later use
         */
        public KNN(Knowledge k) 
        {
            knowledge = k;
        }

        /*
         * Here is the method that gets calles
         * it requeires the text
         * Then calles the vector method
         * Then both the calDIstance a and b 
         * Then the NearestNeighbors to get the 5 nearest
         * Then the predict method that returnes the predicted catagory
         */
        public string PredictTopic(string text)
        {
            List<int> vector = Vector(text);

            List<double> disA = CalDistanceA(vector);
            List<double> disB = CalDistanceB(vector);

            string[] neighbors = NearestNeighbors(disA, disB);

            string predictiong = Predict(neighbors);
            return predictiong;
        }

        /*
         * Here we get the text and tokenize it
         * Then create a vector for the text
         * The return the vector
         */
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

        /*
         * Here we get a vector
         * Then we calculate the disdanse between the vectors
         * The return a list of distanses
         */
        private List<double> CalDistanceA(List<int> vetor)
        {
            List<double> disA = new List<double>();
            // Here we go throug each file in A class
            for (int i = 0; i < knowledge.GetFileLists().GetA().Count(); i++)
            {
                double sum = 0;
                int index = 0;
                //Here we go throug each vector for each file in a Class
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
                // Here we add the distance from the current file to the unknown file
                disA.Add(dis);
            }
            return disA;
        }

        /*
         * Here we get a vector
         * Then we calculate the disdanse between the vectors
         * The return a list of distanses
         */
        private List<double> CalDistanceB(List<int> vetor)
        {
            List<double> disA = new List<double>();
            // Here we go throug each file in B class
            for (int i = 0; i < knowledge.GetFileLists().GetB().Count(); i++)
            {
                double sum = 0;
                int index = 0;
                //Here we go throug each vector for each file in B class
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
                // Here we add the distance from the current file to the unknown file
                disA.Add(dis);
            }
            return disA;
        }

        /*
         * Here we get both lists with the distance from A file and B file
         * Then we create 2 arrayes with max 5
         * Then we add the 5 lowest distance of each list to there coresponding arrayes
         * Last we take the 2 arrayes to make a string array, with the coresponding catagories
         */
        private string[] NearestNeighbors(List<double> a, List<double> b) 
        {
            string[] listResut = new string[KNNNeighbor];
            double[] aDis = new double[KNNNeighbor];
            double[] bDis = new double[KNNNeighbor];
            // Here we go throug each file in A class
            for (int i = 0; i < knowledge.GetFileLists().GetA().Count(); i++)
            {
                // Here we add the first distances
                if (i < KNNNeighbor)
                {
                    aDis[i] = a[i];
                }
                else
                {
                    //Here we ho throug the list of opential smallest distances
                    for (int y = 0; y < aDis.Length; y++)
                    {
                        /* Here we tjek if the distance in the opential smallest distances
                        * is larger then the next file distance
                        * if it is we replace it in the opential smallest distances
                        * and breack out of the inner loop
                        */
                        if (aDis[y] > a[i])
                        {
                            aDis[y] = a[i];
                            break;
                        }
                    } 
                }
            }

            // Here we go throug each file in B class
            for (int i = 0; i < knowledge.GetFileLists().GetB().Count(); i++)
            {
                // Here we add the first distances
                if (i < KNNNeighbor)
                {
                    bDis[i] = b[i];
                }
                else
                {
                    //Here we ho throug the list of opential smallest distances
                    for (int y = 0; y < bDis.Length; y++)
                    {
                        /* Here we tjek if the distance in the opential smallest distances
                        * is larger then the next file distance
                        * if it is we replace it in the opential smallest distances
                        * and breack out of the inner loop
                        */
                        if (bDis[y] > b[i])
                        {
                            bDis[y] = b[i];
                            break;
                        }
                    }
                }
            }
            // Here we sort both arrayes
            Array.Sort(aDis);
            Array.Sort(bDis);

            // Here we make both arrayes to a list
            List<double> aDisList = new List<double>(aDis);
            List<double> bDisList = new List<double>(bDis);

            // Here we throug a loop ass many times as K is mate to be
            for (int i = 0; i < KNNNeighbor; i++)
            {
                /*
                 * Here we tjeck if the first distane of A list is smaller the B list
                 * If true we add the coresponding catagory of A list to the string array
                 * Then we remove the forst distance of A list
                 * If not ture we add the coresponding catagory of B list to the string array
                 * Then we remove the forst distance of A list
                 */
                if (aDisList[0] < bDisList[0]) 
                {
                    listResut[i] = CatagoryOne;
                    aDisList.Remove(0);
                }
                else
                {
                    listResut[i] = CatagoryTwo;
                    bDisList.Remove(0);
                }
            }

            // Return a array of strings
            return listResut;
        }

        /*
         * Here we get a string array
         * Then we go throug each string, and count how many of each catagory
         * Then we se with of the two counts are highest
         * Then return rhe catagory
         */
        private string Predict(string[] neighbor) 
        {
            string myPredict = "";
            int sportCount = 0;
            int fairyTaleCount = 0;

            // Here we ho throug the string array, and count the catagorys
            foreach (string s in neighbor)
            {
                if (s.Equals(CatagoryOne))
                {
                    sportCount++;
                }
                else if (s.Equals(CatagoryTwo))
                {
                    fairyTaleCount++;
                }
            }

            // Here we compare the two counts
            // and set the return string to the count with the heigest number
            if (sportCount > fairyTaleCount) 
            {
                myPredict = CatagoryOne;
            }
            else
            {
                myPredict = CatagoryTwo;
            }

            // Return the String of the predicted catagory
            return myPredict;
        }
    }
}
