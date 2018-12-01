using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_01_Exercise_01
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputArray = File.ReadAllLines(path: @"D:\Data\Documents\Advent of Code 2018\Day01.txt");
            int frequency = 0;

            for (int i = 0; i < inputArray.Length; i++)
            {
                char operation = inputArray[i].First();
                int change = Int32.Parse(inputArray[i].Substring(1));

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
            }
            Console.WriteLine("The resulting frequency after all the changes is " + frequency);
            Console.ReadKey();
        }
    }
}
