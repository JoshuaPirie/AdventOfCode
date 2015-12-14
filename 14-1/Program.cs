using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _14_1 {
    class Program {
        static void Main(string[] args) {
            int time = 2503;
            List<int> reindeerDists = new List<int>();

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                GroupCollection matches = Regex.Match(line, @"^\w+ can fly (\d+) km/s for (\d+) seconds, but then must rest for (\d+) seconds.").Groups;

                int speed = int.Parse(matches[1].Value);
                int duration = int.Parse(matches[2].Value);
                int resting = int.Parse(matches[3].Value);
                int leftover = time % (duration + resting);
                int distance = time / (duration + resting) * duration * speed + ((leftover >= duration) ? duration * speed : leftover * speed);

                reindeerDists.Add(distance);
            }
            file.Close();

            Console.WriteLine(reindeerDists.Max());
            Console.ReadLine();
        }
    }
}
