using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace _16_1 {
    class Program {
        static void Main(string[] args) {
            List<KeyValuePair<string, int>> results = new List<KeyValuePair<string, int>>();
            int validAuntNum = 0;

            string line;
            StreamReader file = new StreamReader("input1.txt");
            while((line = file.ReadLine()) != null) {
                GroupCollection matches = Regex.Match(line, @"^(\w+): (\d+)").Groups;
                results.Add(new KeyValuePair<string, int>(matches[1].Value, int.Parse(matches[2].Value)));
            }
            file.Close();

            file = new StreamReader("input2.txt");
            while((line = file.ReadLine()) != null) {
                GroupCollection matches = Regex.Match(line, @"^Sue (\d+):(?: (\w+): (\d+),?)+").Groups;
                bool containsCompound, validAunt = true;
                for(int i = 0; i < matches[2].Captures.Count; i++) {
                    containsCompound = false;
                    foreach(KeyValuePair<string, int> result in results)
                        if(result.Key == matches[2].Captures[i].Value && result.Value == int.Parse(matches[3].Captures[i].Value))
                            containsCompound = true;
                    if(!containsCompound)
                        validAunt = false;
                }
                if(validAunt)
                    validAuntNum = int.Parse(matches[1].Value);
            }
            file.Close();

            Console.WriteLine(validAuntNum);
            Console.ReadLine();
        }
    }
}
