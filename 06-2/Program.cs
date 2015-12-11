using System;
using System.IO;
using System.Text.RegularExpressions;

namespace _06_2 {
    class Program {
        static void Main(string[] args) {
            int totalBrightness = 0;
            int[,] lightBrightnesses = new int[1000, 1000];

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                GroupCollection parts = Regex.Match(line, @"^(.*) (\d+),(\d+) through (\d+),(\d+)").Groups;

                switch(parts[1].Value) {
                    case "turn on":
                        for(int x = int.Parse(parts[2].Value); x <= int.Parse(parts[4].Value); x++)
                            for(int y = int.Parse(parts[3].Value); y <= int.Parse(parts[5].Value); y++)
                                lightBrightnesses[x, y]++;
                        break;
                    case "turn off":
                        for(int x = int.Parse(parts[2].Value); x <= int.Parse(parts[4].Value); x++)
                            for(int y = int.Parse(parts[3].Value); y <= int.Parse(parts[5].Value); y++)
                                if(lightBrightnesses[x, y] > 0)
                                    lightBrightnesses[x, y]--;
                        break;
                    case "toggle":
                        for(int x = int.Parse(parts[2].Value); x <= int.Parse(parts[4].Value); x++)
                            for(int y = int.Parse(parts[3].Value); y <= int.Parse(parts[5].Value); y++)
                                lightBrightnesses[x, y] += 2;
                        break;
                }
            }
            file.Close();

            foreach(int brightness in lightBrightnesses)
                totalBrightness += brightness;

            Console.WriteLine(totalBrightness);
            Console.ReadLine();
        }
    }
}
