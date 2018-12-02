using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_02_Exercise_01
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputArray = File.ReadAllLines(path: @"D:\Data\Documents\Advent of Code 2018\Day02.txt");

            int twoCount = 0;
            int threeCount = 0;
            int checksum = 0;

            foreach (string input in inputArray)
            {
                Dictionary<char, int> letterCount = new Dictionary<char, int>();

                foreach (char letter in input)
                {
                    if (!letterCount.ContainsKey(letter))
                    {
                        letterCount.Add(letter, 1);
                    }
                    else
                    {
                        letterCount[letter]++;
                    }
                }

                if (letterCount.ContainsValue(2))
                    twoCount++;
                if (letterCount.ContainsValue(3))
                    threeCount++;
            }

            checksum = twoCount * threeCount;

            Console.WriteLine("The checksum is " + checksum);
            Console.ReadKey();
        }
    }
}
