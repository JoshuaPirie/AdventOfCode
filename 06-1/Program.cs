using System;
using System.IO;
using System.Text.RegularExpressions;

namespace _06_1 {
    class Program {
        static void Main(string[] args) {
            int litLights = 0;
            bool[,] lightStates = new bool[1000,1000];

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                GroupCollection parts = Regex.Match(line, @"^(.*) (\d+),(\d+) through (\d+),(\d+)").Groups;

                switch(parts[1].Value) {
                    case "turn on":
                        for(int x = int.Parse(parts[2].Value); x <= int.Parse(parts[4].Value); x++)
                            for(int y = int.Parse(parts[3].Value); y <= int.Parse(parts[5].Value); y++)
                                lightStates[x, y] = true;
                        break;
                    case "turn off":
                        for(int x = int.Parse(parts[2].Value); x <= int.Parse(parts[4].Value); x++)
                            for(int y = int.Parse(parts[3].Value); y <= int.Parse(parts[5].Value); y++)
                                lightStates[x, y] = false;
                        break;
                    case "toggle":
                        for(int x = int.Parse(parts[2].Value); x <= int.Parse(parts[4].Value); x++)
                            for(int y = int.Parse(parts[3].Value); y <= int.Parse(parts[5].Value); y++)
                                lightStates[x, y] = !lightStates[x, y];
                        break;
                }
            }
            file.Close();

            foreach(bool state in lightStates)
                if(state)
                    litLights++;

            Console.WriteLine(litLights);
            Console.ReadLine();
        }
    }
}
