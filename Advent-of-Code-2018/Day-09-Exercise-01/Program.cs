using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_09_Exercise_01
{
    class Program
    {
        static void Main(string[] args)
        {
            /* input         : Instructions taken from a text file. Replace the filepath with your own input. *
             * scores        : Array of all the players's scores.                                             *
             * marbles       : List of all the marbles on the board.                                          *
             * lastMarble    : Value of the last marble to be played.                                         *
             * currentMarble : Marker for the marble to play on.                                              *
             * currentPlayer : Marker for the player playing a marble.                                        */

            string[] input = File.ReadAllText(path: @"D:\Data\Documents\Advent of Code 2018\Day09.txt").Split(' ');
            //string[] input = "13 players; last marble is worth 7999 points".Split(' ');
            int[] scores = new int[int.Parse(input[0])]; // Extract the number of players from the input.
            List<int> marbles = new List<int>() { 0 };
            int lastMarble = int.Parse(input[6]); // Extract the value of the last marble from the input.
            int currentMarble = 0;
            int currentPlayer = 0;

            /* This iterator adds marbles until we get to the final one.  */

            for(int marble = 1; marble <= lastMarble; marble++)
            {
                if (marble % 23 != 0) // Check if the marble to add is divisible by 23.
                {
                    int position = (marbles.IndexOf(currentMarble) + 1) % marbles.Count;

                    if (position == (marbles.Count - 1))
                    {
                        marbles.Add(marble);
                    }
                    else
                    {
                        marbles.Insert(position + 1, marble);
                    }

                    currentMarble = marble;
                }
                else
                {
                    int position = (marbles.IndexOf(currentMarble) - 7) < 0 
                                 ? marbles.Count + (marbles.IndexOf(currentMarble) - 7)
                                 : marbles.IndexOf(currentMarble) - 7;

                    scores[currentPlayer] += marble + marbles[position];
                    marbles.RemoveAt(position);
                    currentMarble = marbles[position % marbles.Count];
                }

                currentPlayer = (currentPlayer+ 1) % scores.Length;
            }

            Console.WriteLine("The winning player's high score is : " + scores.Max());
            Console.ReadKey();
        }
    }
}
