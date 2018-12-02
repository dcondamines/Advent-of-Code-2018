using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_02_Exercise_02
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputArray = File.ReadAllLines(path: @"D:\Data\Documents\Advent of Code 2018\Day02.txt").ToList();
            int inputLength = inputArray.First().Length - 1;
            bool found = false;
            StringBuilder commonLetters = new StringBuilder(inputLength);

            while(!found)
            {
                string inputOne = inputArray.First();
                inputArray.Remove(inputArray.First());

                foreach (string inputTwo in inputArray)
                {
                    commonLetters.Clear();
                    for (int i = 0; i < inputTwo.Length; i++)
                    {
                        if(inputOne[i] == inputTwo[i])
                        {
                            commonLetters.Append(inputOne[i]);
                        }
                    }

                    if(commonLetters.Length == inputLength)
                    {
                        found = true;
                        break;
                    }
                }
            }
            Console.WriteLine("The common letters between the two correct IDs are : " + commonLetters);
            Console.ReadKey();
        }
    }
}
