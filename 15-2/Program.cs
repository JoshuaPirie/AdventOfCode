using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace _15_2 {
    class Program {
        static void Main(string[] args) {
            int bestScore = 0;
            List<int[]> ingredientStats = new List<int[]>();

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                GroupCollection matches = Regex.Match(line, @"^(\w+): \w+ (-?\d+), \w+ (-?\d+), \w+ (-?\d+), \w+ (-?\d+), \w+ (-?\d+)").Groups;
                ingredientStats.Add(new int[5] {
                    int.Parse(matches[2].Value),
                    int.Parse(matches[3].Value),
                    int.Parse(matches[4].Value),
                    int.Parse(matches[5].Value),
                    int.Parse(matches[6].Value)
                });
            }
            file.Close();

            for(int i = 0; i <= 100; i++) {
                for(int j = 0; j <= 100 - i; j++) {
                    for(int k = 0; k <= 100 - i - j; k++) {
                        int l = 100 - i - j - k;
                        int capacity = i * ingredientStats[0][0] + j * ingredientStats[1][0] + k * ingredientStats[2][0] + l * ingredientStats[3][0];
                        int durability = i * ingredientStats[0][1] + j * ingredientStats[1][1] + k * ingredientStats[2][1] + l * ingredientStats[3][1];
                        int flavor = i * ingredientStats[0][2] + j * ingredientStats[1][2] + k * ingredientStats[2][2] + l * ingredientStats[3][2];
                        int texture = i * ingredientStats[0][3] + j * ingredientStats[1][3] + k * ingredientStats[2][3] + l * ingredientStats[3][3];
                        int calories = i * ingredientStats[0][4] + j * ingredientStats[1][4] + k * ingredientStats[2][4] + l * ingredientStats[3][4];

                        if(calories == 500 && capacity > 0 && durability > 0 && flavor > 0 && texture > 0) {
                            int score = capacity * durability * flavor * texture;
                            if(score > bestScore)
                                bestScore = score;
                        }
                    }
                }
            }

            Console.WriteLine(bestScore);
            Console.ReadLine();
        }
    }
}
