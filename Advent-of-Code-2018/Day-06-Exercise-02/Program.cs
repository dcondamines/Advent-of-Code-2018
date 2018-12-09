using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_06_Exercise_02
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputRaw = File.ReadAllLines(path: @"D:\Data\Documents\Advent of Code 2018\Day06.txt").ToList();
            List<Coordinate> coordinates = new List<Coordinate>();

            int coordMinX = 0;
            int coordMinY = 0;
            int coordMaxX = 0;
            int coordMaxY = 0;

            foreach (string raw in inputRaw)
            {
                string[] rawArray = raw.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                coordinates.Add(new Coordinate()
                {
                    CoordX = int.Parse(rawArray[0]),
                    CoordY = int.Parse(rawArray[1]),
                });
            }

            coordMinX = coordinates.Min(c => c.CoordX) - 1;
            coordMinY = coordinates.Min(c => c.CoordY) - 1;
            coordMaxX = coordinates.Max(c => c.CoordX) + 1;
            coordMaxY = coordinates.Max(c => c.CoordY) + 1;

            int[,] grid = new int[coordMaxX - coordMinX, coordMaxY - coordMinY];

            for (int y = coordMinY; y < coordMaxY; y++)
            {
                for (int x = coordMinX; x < coordMaxX; x++)
                {
                    int[] distanceArray = new int[coordinates.Count];

                    for (int c = 0; c < coordinates.Count; c++)
                    {
                        distanceArray[c] = Math.Abs(x - coordinates[c].CoordX) +
                                           Math.Abs(y - coordinates[c].CoordY);
                    }

                    grid[x - coordMinX, y - coordMinY] = distanceArray.Sum();


                }
            }

            var size = (from coord in grid.Cast<int>()
                        where coord < 10000
                        select coord).Count();

            Console.WriteLine("The region size is : " + size.ToString());
            Console.ReadKey();

        }
    }

    class Coordinate
    {
        public int CoordX { get; set; }
        public int CoordY { get; set; }
    }
}
