using System;
using System.IO;
using System.Text.RegularExpressions;

namespace _08_1 {
    class Program {
        static void Main(string[] args) {
            int difference = 0;
            string line;

            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                difference += line.Length - Regex.Unescape(line).Length + 2;
            }

            Console.WriteLine(difference);
            Console.ReadLine();
        }
    }
}
