using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day_07_Exercise_02
{
    class Program
    {
        static void Main(string[] args)
        {
            /* input   : A set of instructions taken from a text file. Replace the filepath with your own input.                             *
             * letters : Array of letters A to Z. The indexes are used to calculate required time to finish (A = 1, B = 2, ... Z = 26).      *
             * steps   : Detailed list of steps. For more information, check the Step class.                                                 *
             * order   : All the step designations, in the order the steps are completed. Not required for this part, just here to be fancy. *
             * workers : The number of workers. Each worker can work on one step.                                                            *
             * time    : Counts the time taken to complete all steps.                                                                        */

            List<string>  input   = File.ReadAllLines(path: @"Input.txt").ToList();
            char[]        letters = " ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray(); // THE SPACE IS MEANT TO BE AT INDEX ZERO.
            List<Step>    steps   = new List<Step>();
            StringBuilder order   = new StringBuilder(26);
            int           workers = 5;
            int           time    = -1; // +1 is added at the beginning of the loop.

            /* This iterator goes through the list of instructions and convert them into steps and prerequistes. */

            foreach (string line in input)
            {
                char[] designations = new char[2] 
                {
                    Convert.ToChar(line.Substring(5,1)),  // Extract the first designation.
                    Convert.ToChar(line.Substring(36,1)), // Extract the second designation.
                };

                foreach (char designation in designations)
                {
                    if(!steps.Exists(step => step.Designation == designation))
                    {
                        steps.Add(new Step()
                        {
                            Designation    = designation,
                            TimeWorkedOn   = 0,
                            TimeToComplete = Array.IndexOf(letters, designation) + 60, // (A = 1, B = 2, ... Z = 26) + 60
                            AssignedWorker = null,
                            Prerequistes   = new List<Step>(),
                        });
                    }
                }   

                steps.First(step => step.Designation == designations[1]).Prerequistes // designations[0] must be finished before designations[1] can begin.
                     .Add(steps.First(step => step.Designation == designations[0]));
            }

            /* This loop keeps running until all the steps are done (when time worked on equals time to complete) */

            while (!steps.All(step => step.TimeWorkedOn == step.TimeToComplete))
            {
                StringBuilder workerStatus = new StringBuilder(10); // Five statuses, five spaces.

                time++;

                /* This iterator updates the active workers. */

                for (int worker = 0; worker < workers; worker++)
                {
                    Step stepWorkedOn = steps.FirstOrDefault(step => step.AssignedWorker == worker);

                    if (stepWorkedOn != null) // Is the worker actually WORKING?
                    {
                        stepWorkedOn.TimeWorkedOn++;

                        if(stepWorkedOn.TimeWorkedOn == stepWorkedOn.TimeToComplete) // Are we done?
                        {
                            stepWorkedOn.AssignedWorker = null;
                            order.Append(stepWorkedOn.Designation);
                        }
                    } 
                }

                /* This iterator assigns steps to workers */

                for (int worker = 0; worker < workers; worker++)
                {
                    if(steps.FirstOrDefault(step => step.AssignedWorker == worker) == null)
                    {
                        Step stepToDo = (from step in steps
                                         where (step.Prerequistes.Count == 0 || 
                                                step.Prerequistes.All(prereq => prereq.TimeWorkedOn == prereq.TimeToComplete)) && //All prerequistes completed
                                               step.TimeWorkedOn == 0 && // Not worked on yet
                                               step.AssignedWorker == null // No workers assigned
                                         orderby step.Designation ascending // Order alphabetically
                                         select step).FirstOrDefault();

                        if(stepToDo != null)
                        {
                            stepToDo.AssignedWorker = worker;
                        }
                    }
                }

                /* This iterator construct the worker status line for this specific time. */

                for (int worker = 0; worker < workers; worker++)
                {
                    workerStatus.Append(((steps.Any(step => step.AssignedWorker == worker)) ? steps.First(w => w.AssignedWorker == worker).Designation.ToString() : ".") + " ");
                }

                /* Write the time line. */

                Console.WriteLine(time.ToString("0000") + " " + workerStatus.ToString() + " " + order.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("Time to finish : " + time + " seconds");
            Console.ReadKey();
        }
    }

    class Step
    {
        /* Designation    : The letter used to identify the step.                     *
         * AssignedWorker : The worker assigned to the step.                          *
         * TimeWorkedOn   : How much time has been spent working on this step.        *
         * TimeToComplete : The time needed to complete the step.                     *
         * Prerequistes   : The steps to complete before work can begin on this step. */

        public char Designation { get; set; }
        public int? AssignedWorker { get; set; }
        public int TimeWorkedOn { get; set; }
        public int TimeToComplete { get; set; }
        public List<Step> Prerequistes { get; set; }

    }
}
