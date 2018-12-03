using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_03_Exercise_01
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputList = File.ReadAllLines(path: @"D:\Data\Documents\Advent of Code 2018\Day03.txt").ToList();
            List<Fabric> claimedFabric = new List<Fabric>();
            int overlaps = 0;

            foreach(string input in inputList)
            {
                // [0] = ID
                // [2] = Coordinates
                // [3] = Fabric Area
                string[] inputSplit = input.Split(' ');

                // Remove ':' from each coordinates.
                inputSplit[2] = inputSplit[2].Remove(inputSplit[2].Length - 1);

                int[] coordinates = Array.ConvertAll(inputSplit[2].Split(','), converter: int.Parse);
                int[] area = Array.ConvertAll(inputSplit[3].Split('x'), converter: int.Parse);

                for (int x = coordinates[0]; x < (coordinates[0] + area[0]); x++)
                {
                    for (int y = coordinates[1]; y < (coordinates[1] + area[1]); y++)
                    {
                        Fabric claim = claimedFabric.Where(c => c.CoordX == x && c.CoordY == y).FirstOrDefault();

                        if(claim == null)
                        {
                            claimedFabric.Add(new Fabric()
                            {
                                CoordX = x,
                                CoordY = y,
                                TimesClaimed = 1,
                            });

                        }
                        else
                        {
                            claim.TimesClaimed++;
                        }
                    }
                }
            }

            overlaps = claimedFabric.Where(o => o.TimesClaimed > 1).Count();

            Console.WriteLine("The number of overlaps in claims : " + overlaps);
            Console.ReadKey();
        }
    }

    class Fabric
    {
        public int CoordX { get; set; }
        public int CoordY { get; set; }
        public int TimesClaimed { get; set; }
    }

}
