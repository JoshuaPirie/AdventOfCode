using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _17_1 {
    class Program {
        static void Main(string[] args) {
            int counter = 0;
            int quantity = 150;
            List<int> containers = new List<int>();

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                containers.Add(int.Parse(line));
            }
            file.Close();

            for(int i = 0; i < Math.Pow(2, containers.Count); i++) {
                List<int> set = new List<int>();
                for(int bits = i, j = 0; bits != 0; bits >>= 1, j++)
                    if((bits & 1) != 0)
                        set.Add(containers[j]);
                if(set.Sum() == quantity)
                    counter++;
            }

            Console.WriteLine(counter);
            Console.ReadLine();
        }
    }
}
