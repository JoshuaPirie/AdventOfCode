using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _09_1 {
    class Program {
        static void Main(string[] args) {
            Dictionary<string, Dictionary<string, int>> distances = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<string[], int> routeDistances = new Dictionary<string[], int>();

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                GroupCollection matches = Regex.Match(line, @"^(\w+) to (\w+) = (\d+)").Groups;
                if(!distances.ContainsKey(matches[1].Value))
                    distances[matches[1].Value] = new Dictionary<string, int>();
                if(!distances.ContainsKey(matches[2].Value))
                    distances[matches[2].Value] = new Dictionary<string, int>();
                distances[matches[1].Value][matches[2].Value] = int.Parse(matches[3].Value);
                distances[matches[2].Value][matches[1].Value] = int.Parse(matches[3].Value);
            }
            file.Close();

            IEnumerable<IEnumerable<string>> routes = Permutations(distances.Keys, distances.Count);

            foreach(IEnumerable<string> routeEnum in routes) {
                string[] route = routeEnum.ToArray();
                routeDistances[route] = RouteDistance(distances, route);
            }

            Console.WriteLine(routeDistances.Values.Min());
            Console.ReadLine();
        }

        static IEnumerable<IEnumerable<T>> Permutations<T>(IEnumerable<T> list, int length) {
            if(length == 1)
                return list.Select(t => new T[] { t });
            return Permutations(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        static int RouteDistance(Dictionary<string, Dictionary<string, int>> distances, string[] route) {
            int dist = 0;
            for(int i = 1; i < route.Length; i++)
                dist += distances[route[i - 1]][route[i]];
            return dist;
        }
    }
}
