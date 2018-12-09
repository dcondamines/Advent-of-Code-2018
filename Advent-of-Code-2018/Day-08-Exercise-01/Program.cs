using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_08_Exercise_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Methods methods = new Methods();

            string input = File.ReadAllText(path: @"D:\Data\Documents\Advent of Code 2018\Day08.txt");
            List<int> numbers = Array.ConvertAll(input.Split(' '), int.Parse).ToList();
            List<int> totalMetadata = new List<int>();
            int pointer = 0;

            Node tree = methods.CreateNode(ref numbers, ref pointer, ref totalMetadata, 0);

            Console.WriteLine("");
            Console.WriteLine("The sum of all metadata entries is " + totalMetadata.Sum().ToString());
            Console.ReadKey();


        }    
    }

    class Node
    {
        public int QtyChildren { get; set; }
        public int QtyMetadata { get; set; }
        public List<Node> Children { get; set; }
        public List<int> Metadata { get; set; }
    }

    class Methods
    {
        public Node CreateNode(ref List<int> numbers, ref int pointer, ref List<int> totalMetadata, int level)
        {
            Node node = new Node
            {
                QtyChildren = numbers[pointer + 0],
                QtyMetadata = numbers[pointer + 1],
                Children = new List<Node>(),
                Metadata = new List<int>(),
            };

            Console.WriteLine(numbers[pointer + 0].ToString("00") + " " +
                              new string('|', level) +
                              "O");
            Console.WriteLine(numbers[pointer + 1].ToString("00") + " " +
                              new string('|', level) +
                              "!");

            pointer += 2;

            for(int c = 0; c < node.QtyChildren; c++)
            {
                node.Children.Add(CreateNode(ref numbers, ref pointer, ref totalMetadata, level + 1));
            }

            for(int m = 0; m < node.QtyMetadata; m++)
            {
                node.Metadata.Add(numbers[pointer + m]);
                totalMetadata.Add(numbers[pointer + m]);
                Console.WriteLine(numbers[pointer + m].ToString("00") + " " +
                              new string('|', level) +
                              "X");
            }
            pointer += node.QtyMetadata;

            return node;
        }
    }
}
