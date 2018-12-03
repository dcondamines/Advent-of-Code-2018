using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_03_Exercise_02
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputRaw = File.ReadAllLines(path: @"D:\Data\Documents\Advent of Code 2018\Day03.txt").ToList();
            List<Input> sanitizedList = new List<Input>();
            int[,] fabric = new int[1000, 1000];

            bool found = false;
            int foundID = 0;

            // Clean up the raw input.
            foreach(string input in inputRaw)
            {
                Input sanitized = new Input();
                string[] inputSplit = input.Split(' ');
                inputSplit[2] = inputSplit[2].Remove(inputSplit[2].Length - 1);

                sanitized.ID    = int.Parse(inputSplit[0].Substring(1));
                sanitized.Coord = Array.ConvertAll(inputSplit[2].Split(','), converter: int.Parse);
                sanitized.Area  = Array.ConvertAll(inputSplit[3].Split('x'), converter: int.Parse);
                sanitizedList.Add(sanitized);
            }

            foreach(Input input in sanitizedList)
            {
                for(int x =  input.Coord[0];
                        x < (input.Coord[0] + input.Area[0]);
                        x++)
                {
                    for (int y = input.Coord[1];
                             y < (input.Coord[1] + input.Area[1]);
                             y++)
                    {
                        fabric[x, y]++;
                    }
                }
            }

            while(!found)
            {

                foreach (Input input in sanitizedList)
                {
                    List<int> fabricUsed = new List<int>();
                    for (int x = input.Coord[0];
                             x < (input.Coord[0] + input.Area[0]);
                             x++)
                    {
                        for (int y = input.Coord[1];
                                 y < (input.Coord[1] + input.Area[1]);
                                 y++)
                        {
                            fabricUsed.Add(fabric[x, y]);
                        }
                    }

                    if(fabricUsed.All(x => x.Equals(1)))
                    {
                        foundID = input.ID;
                        found = true;
                        break;
                    }
                }

            }

            Console.WriteLine("The ID of the claim with no overlaps : " + foundID);
            Console.ReadKey();

        }
    }

    class Input
    {
        public int ID { get; set; }
        public int[] Coord { get; set; }
        public int[] Area { get; set; }
    }
}
