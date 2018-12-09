using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_09_Exercise_02
{
    class Program
    {
        static void Main(string[] args)
        {
            /* input         : Instructions taken from a text file. Replace the filepath with your own input. *
             * scores        : Array of all the players's scores.                                             *
             * marbles       : LinkedList of all the marbles on the board.                                          *
             * lastMarble    : Value of the last marble to be played.                                         *
             * currentMarble : Marker for the marble to play on.                                              *
             * currentPlayer : Marker for the player playing a marble.                                        */

            string[] input = File.ReadAllText(path: @"D:\Data\Documents\Advent of Code 2018\Day09.txt").Split(' ');
            //string[] input = "13 players; last marble is worth 7999 points".Split(' ');
            long[] scores = new long[int.Parse(input[0])]; // Extract the number of players from the input.
            LinkedList<int> marbles = new LinkedList<int>();
            LinkedListNode<int> currentMarble = marbles.AddFirst(0);

            int lastMarble = int.Parse(input[6]) * 100; // Extract the value of the last marble from the input, then multiply it by 100.
            int currentPlayer = 0;

            /* This iterator adds marbles until we get to the final one.  */

            for (int marble = 1; marble <= lastMarble; marble++)
            {
                if (marble % 23 != 0) // Check if the marble to add is divisible by 23.
                {
                    currentMarble = currentMarble.Next ?? marbles.First;
                    marbles.AddAfter(currentMarble, marble);
                    currentMarble = currentMarble.Next;
                }
                else
                {
                    for (int backward = 0; backward < 7; backward++)
                    {
                        currentMarble = currentMarble.Previous ?? marbles.Last;
                    }
                    scores[currentPlayer] += marble + currentMarble.Value;

                    var temp = currentMarble;
                    currentMarble = currentMarble.Next ?? marbles.First;
                   
                    marbles.Remove(temp);
                }

                currentPlayer = (currentPlayer + 1) % scores.Length;
            }

            Console.WriteLine("The winning player's high score is : " + scores.Max());
            Console.ReadKey();
        }
    }
}
