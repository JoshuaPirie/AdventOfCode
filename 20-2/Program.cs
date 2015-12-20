using System;
using System.Collections.Generic;

namespace _20_2 {
    class Program {
        static void Main(string[] args) {
            int input = 29000000;
            int houseNum = 0;
            int numPresents = 0;

            while(numPresents < input) {
                List<int> factors = Factors(++houseNum);
                numPresents = 0;
                foreach(int factor in factors)
                    if(houseNum / factor <= 50)
                        numPresents += factor * 11;
            }

            Console.WriteLine(houseNum);
            Console.ReadLine();
        }

        static List<int> Factors(int number) {
            List<int> factors = new List<int>();
            int max = (int)Math.Sqrt(number);
            for(int factor = 1; factor <= max; ++factor) {
                if(number % factor == 0) {
                    factors.Add(factor);
                    if(factor != number / factor) {
                        factors.Add(number / factor);
                    }
                }
            }
            return factors;
        }
    }
}
