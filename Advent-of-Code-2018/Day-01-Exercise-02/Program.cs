using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_01_Exercise_02
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputArray = File.ReadAllLines(path: @"D:\Data\Documents\Advent of Code 2018\Day01.txt");
            int frequency = 0;

            List<int> frequenciesReached = new List<int>();
            bool found = false;

            while (!found)
            {
                foreach (string input in inputArray)
                {
                    char operation = input.First();
                    int change = Int32.Parse(input.Substring(1));

                    switch (operation)
                    {
                        case '+':
                            frequency += change;
                            break;

                        case '-':
                            frequency -= change;
                            break;

                        default:
                            break;

                    }

                    if (frequenciesReached.Contains(frequency))
                    {
                        found = true;
                        break;
                    }
                    else
                    {
                        frequenciesReached.Add(frequency);
                    }
                }
            }

            Console.WriteLine("The first frequency reached twice is " + frequency);
            Console.ReadKey();
        }
    }
}
