using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_08_Exercise_02
{
    class Program
    {
        static void Main(string[] args)
        {
            Methods methods = new Methods();

            string input = File.ReadAllText(path: @"D:\Data\Documents\Advent of Code 2018\Day08.txt");
            List<int> numbers = Array.ConvertAll(input.Split(' '), int.Parse).ToList();
            int pointer = 0;

            Node tree = methods.CreateNode(ref numbers, ref pointer, 0);
            int rootNodeValue = methods.CalculateNodeValue(tree);

            Console.WriteLine("");
            Console.WriteLine("The value of the root node is " + rootNodeValue.ToString());
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
        public Node CreateNode(ref List<int> numbers, ref int pointer, int level)
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

            for (int c = 0; c < node.QtyChildren; c++)
            {
                node.Children.Add(CreateNode(ref numbers, ref pointer, level + 1));
            }

            for (int m = 0; m < node.QtyMetadata; m++)
            {
                node.Metadata.Add(numbers[pointer + m]);
                Console.WriteLine(numbers[pointer + m].ToString("00") + " " +
                              new string('|', level) +
                              "X");
            }
            pointer += node.QtyMetadata;

            return node;
        }

        public int CalculateNodeValue(Node node)
        {
            if(node.Children.Count() == 0)
            {
                return node.Metadata.Sum();
            }
            else
            {
                int value = 0;

                foreach(int metadata in node.Metadata)
                {
                    if(metadata <= node.QtyChildren && metadata > 0)
                    {
                        value += CalculateNodeValue(node.Children[metadata - 1]);
                    }
                }

                return value;
            }
        }
    }
}
