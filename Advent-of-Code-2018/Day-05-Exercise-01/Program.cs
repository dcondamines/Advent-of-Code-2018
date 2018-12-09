using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_05_Exercise_01
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char> input = File.ReadAllText(path: @"D:\Data\Documents\Advent of Code 2018\Day05.txt").ToList();
            input.Remove('\n');
            //List<char> input = "dabAcCaCBAcCcaDA".ToList();

            bool reaction = true;

            while(reaction)
            {
                reaction = false;

                for (int unit = 1; unit < input.Count; unit++)
                {
                    if(input[unit] != input[unit - 1])
                    {
                        if (input[unit] == char.ToLower(input[unit - 1]) ||
                            input[unit] == char.ToUpper(input[unit - 1]))
                        {
                            input.RemoveAt(unit);
                            input.RemoveAt(unit - 1);
                            reaction = true;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine("The amount of units remaining is " + input.Count.ToString());
            Console.ReadKey();

        }
    }
}
