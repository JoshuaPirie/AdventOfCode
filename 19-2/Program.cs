using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace _19_2 {
    class Program {
        static void Main(string[] args) {
            string input = "";
            string result = "e";
            int steps = 0;
            List<Tuple<string, string>> equations = new List<Tuple<string, string>>();

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

            TryFindCompound(equations, input, result, ref steps);

            Console.WriteLine(steps);
            Console.ReadLine();
        }

        static bool TryFindCompound(List<Tuple<string, string>> equations, string input, string result, ref int steps, int level = 0) {
            if(input == result) {
                steps = level;
                return true;
            }
            else {
                HashSet<string> replacements = new HashSet<string>();

                for(int i = input.Length - 1; i >= 0; i--) {
                    foreach(Tuple<string, string> equation in equations) {
                        if(i + equation.Item2.Length <= input.Length && input.Substring(i, equation.Item2.Length) == equation.Item2) {
                            StringBuilder builder = new StringBuilder(input.Substring(0, i));
                            builder.Append(equation.Item1);
                            builder.Append(input, i + equation.Item2.Length, input.Length - i - equation.Item2.Length);
                            replacements.Add(builder.ToString());
                        }
                    }
                }

                foreach(string str in replacements)
                    if(TryFindCompound(equations, str, result, ref steps, level + 1))
                        return true;
            }
            return false;
        }
    }
}
