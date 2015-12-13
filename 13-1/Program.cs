using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _13_1 {
    class Program {
        static void Main(string[] args) {
            Dictionary<string, Dictionary<string, int>> indHappinesses = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<string[], int> arrHappinesses = new Dictionary<string[], int>();

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                GroupCollection matches = Regex.Match(line, @"^(\w+) would (\w+) (\d+) happiness units by sitting next to (\w+).").Groups;
                if(!indHappinesses.ContainsKey(matches[1].Value))
                    indHappinesses[matches[1].Value] = new Dictionary<string, int>();
                int happiness = int.Parse(matches[3].Value);
                if(matches[2].Value == "lose")
                    happiness = -happiness;

                indHappinesses[matches[1].Value][matches[4].Value] = happiness;
            }
            file.Close();

            IEnumerable<IEnumerable<string>> arrangements = Permutations(indHappinesses.Keys, indHappinesses.Count);

            foreach(IEnumerable<string> arrEnum in arrangements) {
                string[] arr = arrEnum.ToArray();
                arrHappinesses[arr] = ArrangementHappiness(indHappinesses, arr);
            }

            Console.WriteLine(arrHappinesses.Values.Max());
            Console.ReadLine();
        }

        static IEnumerable<IEnumerable<T>> Permutations<T>(IEnumerable<T> list, int length) {
            if(length == 1)
                return list.Select(t => new T[] { t });
            return Permutations(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        static int ArrangementHappiness(Dictionary<string, Dictionary<string, int>> indHappinesses, string[] arrangement) {
            int happiness = 0;
            for(int i = 0; i < arrangement.Length; i++)
                happiness += indHappinesses[arrangement[((i == 0) ? arrangement.Length : i) - 1]][arrangement[i]] + indHappinesses[arrangement[i]][arrangement[((i == 0) ? arrangement.Length : i) - 1]];
            return happiness;
        }
    }
}
