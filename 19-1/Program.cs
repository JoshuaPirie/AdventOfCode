using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace _19_1 {
    class Program {
        static void Main(string[] args) {
            string input = "";
            List<Tuple<string, string>> equations = new List<Tuple<string, string>>();
            HashSet<string> results = new HashSet<string>();

            bool lineBreak = false;
            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                if(lineBreak)
                    input = line;
                else if(line == "")
                    lineBreak = true;
                else {
                    GroupCollection matches = Regex.Match(line, @"(\w+) => (\w+)").Groups;
                    equations.Add(Tuple.Create(matches[1].Value, matches[2].Value));
                }
            }
            file.Close();

            for(int i = 0; i < input.Length; i++) {
                foreach(Tuple<string, string> equation in equations) {
                    if(i + equation.Item1.Length <= input.Length && input.Substring(i, equation.Item1.Length) == equation.Item1) {
                        StringBuilder builder = new StringBuilder(input.Substring(0, i));
                        builder.Append(equation.Item2);
                        builder.Append(input, i + equation.Item1.Length, input.Length - i - equation.Item1.Length);
                        results.Add(builder.ToString());
                    }
                }
            }

            Console.WriteLine(results.Count);
            Console.ReadLine();
        }
    }
}
