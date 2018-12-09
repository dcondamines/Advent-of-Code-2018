using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_07_Exercise_01
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputRaw = File.ReadAllLines(path: @"D:\Data\Documents\Advent of Code 2018\Day07.txt").ToList();
            List<Step> steps = new List<Step>();
            StringBuilder order = new StringBuilder(26);

            foreach(string raw in inputRaw)
            {
                string[] rawArray = raw.Split(' ');
                char[] stepArray = new char[2] { Convert.ToChar(rawArray[1]), Convert.ToChar(rawArray[7]) };

                if(!steps.Exists(d => d.Designation == stepArray[0]))
                {
                    steps.Add(new Step() { Designation = stepArray[0], Done = false, Prerequistes = new List<Step>() });
                }

                if (!steps.Exists(d => d.Designation == stepArray[1]))
                {
                    steps.Add(new Step() { Designation = stepArray[1], Done = false, Prerequistes = new List<Step>() });
                }

                steps.Where(d => d.Designation == stepArray[1]).First().Prerequistes.Add(steps.Where(d => d.Designation == stepArray[0]).First());

            }

            while(!steps.All(x => x.Done))
            {
                Step toDo = (from step in steps
                             where (step.Prerequistes.Count == 0 || step.Prerequistes.All(x => x.Done)) && step.Done == false
                             orderby step.Designation ascending
                             select step).FirstOrDefault();

                order.Append(toDo.Designation);
                toDo.Done = true;
            }

            Console.WriteLine("The order of completion : " + order.ToString());
            Console.ReadKey();
        }
    }

    class Step
    {
        public char Designation { get; set; }
        public bool Done { get; set; }
        public List<Step> Prerequistes { get; set; }

    }
}
