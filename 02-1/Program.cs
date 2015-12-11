using System;
using System.IO;

namespace _02_1 {
    class Program {
        static void Main(string[] args) {
            int paper = 0;

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                int[] dimensions = Array.ConvertAll(line.Split('x'), s => int.Parse(s));
                Array.Sort(dimensions);
                paper += 3 * dimensions[0] * dimensions[1] + 2 * dimensions[1] * dimensions[2] + 2 * dimensions[0] * dimensions[2];
            }
            file.Close();

            Console.WriteLine(paper);
            Console.ReadLine();
        }
    }
}
