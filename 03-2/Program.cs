using System;
using System.IO;
using System.Drawing;
using System.Collections.Concurrent;

namespace _03_2 {
    class Program {
        static void Main(string[] args) {
            Point santaPos = new Point();
            Point roboPos = new Point();
            ConcurrentDictionary<Point, int> houses = new ConcurrentDictionary<Point, int>();

            houses.AddOrUpdate(santaPos, 1, (id, count) => count + 1);
            houses.AddOrUpdate(roboPos, 1, (id, count) => count + 1);

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                for(int i = 0; i < line.Length; i++) {
                    if(i % 2 == 0) {
                        if(line[i] == '>')
                            santaPos.X++;
                        if(line[i] == '<')
                            santaPos.X--;
                        if(line[i] == '^')
                            santaPos.Y++;
                        if(line[i] == 'v')
                            santaPos.Y--;
                        houses.AddOrUpdate(santaPos, 1, (id, count) => count + 1);
                    }
                    else {
                        if(line[i] == '>')
                            roboPos.X++;
                        if(line[i] == '<')
                            roboPos.X--;
                        if(line[i] == '^')
                            roboPos.Y++;
                        if(line[i] == 'v')
                            roboPos.Y--;
                        houses.AddOrUpdate(roboPos, 1, (id, count) => count + 1);
                    }
                }
            }
            file.Close();

            Console.WriteLine(houses.Count);
            Console.ReadLine();
        }
    }
}
