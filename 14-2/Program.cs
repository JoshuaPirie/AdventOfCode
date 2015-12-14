using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace _14_2 {
    class Program {
        static void Main(string[] args) {
            int maxPoints = 0;
            int time = 2503;
            Dictionary<string, int[]> reindeerStats = new Dictionary<string, int[]>();

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                GroupCollection matches = Regex.Match(line, @"^(\w+) can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds.").Groups;
                reindeerStats[matches[1].Value] = new int[5] { 0, 0, int.Parse(matches[2].Value), int.Parse(matches[3].Value), int.Parse(matches[4].Value) };
            }
            file.Close();

            for(int i = 0; i < time; i++) {
                int maxDist = 0;
                foreach(int[] stats in reindeerStats.Values) {
                    if(i % (stats[3] + stats[4]) < stats[3])
                        stats[1] += stats[2];
                    if(stats[1] > maxDist)
                        maxDist = stats[1];
                }
                foreach(int[] stats in reindeerStats.Values)
                    if(stats[1] == maxDist)
                        stats[0]++;
            }

            foreach(int[] stats in reindeerStats.Values)
                if(stats[0] > maxPoints)
                    maxPoints = stats[0];

            Console.WriteLine(maxPoints);
            Console.ReadLine();
        }
    }
}
