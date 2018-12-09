using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_04_Exercise_01
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> inputRaw = File.ReadAllLines(path: @"D:\Data\Documents\Advent of Code 2018\Day04.txt").ToList();
            List<Record> records = new List<Record>();
            List<Record> recordsSorted = new List<Record>();

            Dictionary<int, int[]> sleepCount = new Dictionary<int, int[]>();
            int currentGuard = 0;
            int lastAsleep = 0;

            int sleepiestGuardID = 0;
            int sleepiestMinute = 0;

            foreach (string raw in inputRaw)
            {
                string[] rawArray = raw.Split(new char[] { '-', ' ', ':' });

                Record record = new Record
                {
                    Year = int.Parse(rawArray[0].Substring(1)),
                    Month = int.Parse(rawArray[1]),
                    Day = int.Parse(rawArray[2]),
                    Hour = int.Parse(rawArray[3]),
                    Minute = int.Parse(rawArray[4].Remove(rawArray[4].Length - 1))
                };

                switch (rawArray[5])
                {
                    case "Guard":
                        record.GuardNumber = int.Parse(rawArray[6].Substring(1));
                        break;

                    case "falls":
                        record.IsAwake = false;
                        break;

                    case "wakes":
                        record.IsAwake = true;
                        break;

                    default:
                        break;

                }

                records.Add(record);
            }

            recordsSorted = records.OrderBy(x => x.Year)
                                   .ThenBy(x => x.Month)
                                   .ThenBy(x => x.Day)
                                   .ThenBy(x => x.Hour)
                                   .ThenBy(x => x.Minute).ToList();

            foreach (Record record in recordsSorted)
            {
                if (record.GuardNumber.HasValue)
                {
                    currentGuard = record.GuardNumber.Value;
                }
                if (record.IsAwake.HasValue)
                {
                    switch (record.IsAwake)
                    {
                        case false:
                            lastAsleep = record.Minute;
                            break;

                        case true:
                            if (!sleepCount.ContainsKey(currentGuard))
                            {
                                sleepCount.Add(currentGuard, new int[60]);
                            }

                            for (int s = lastAsleep; s < record.Minute; s++)
                            {
                                sleepCount[currentGuard][s]++;
                            }
                            break;

                        default:
                            break;

                    }
                }
            }

            KeyValuePair<int, int[]> sleepiest = (from rec in sleepCount
                                                  orderby rec.Value.Sum() descending
                                                  select rec).First();

            sleepiestGuardID = sleepiest.Key;
            sleepiestMinute = sleepiest.Value.ToList().IndexOf(sleepiest.Value.Max());

            Console.WriteLine("The Guard ID multiplied by the sleepiest minute : " + (sleepiestGuardID * sleepiestMinute).ToString());
            Console.ReadKey();
        }
    }

    class Record
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }

        public int? GuardNumber { get; set; }
        public bool? IsAwake { get; set; }
    }
}
