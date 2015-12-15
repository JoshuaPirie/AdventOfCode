using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _15_1 {
    class Program {
        static void Main(string[] args) {
            int maxScore = 0;
            Dictionary<string, List<KeyValuePair<string, int>>> ingredients = new Dictionary<string, List<KeyValuePair<string, int>>>();

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                GroupCollection matches = Regex.Match(line, @"^(\w+):(?: (\w+) (-?\d+),?)+").Groups;
                ingredients[matches[1].Value] = new List<KeyValuePair<string, int>>();
                for(int i = 0; i < matches[2].Captures.Count; i++)
                    ingredients[matches[1].Value].Add(new KeyValuePair<string, int>(matches[2].Captures[i].Value, int.Parse(matches[3].Captures[i].Value)));
            }
            file.Close();

            ExploreRecipies(ingredients, ingredients.First().Value.Count, new int[0], 100, ref maxScore);

            Console.WriteLine(maxScore);
            Console.ReadLine();
        }

        static void ExploreRecipies(Dictionary<string, List<KeyValuePair<string, int>>> ingredients, int numStats, int[] quantities, int maxQuantity, ref int maxScore) {
            if(quantities.Length == ingredients.Count - 1) {
                int score = 1;
                int[] subScores = new int[numStats];

                for(int i = 0; i < subScores.Length; i++) {
                    int counter = 0;
                    foreach(List<KeyValuePair<string, int>> ingredient in ingredients.Values) {
                        subScores[i] += ((counter == ingredients.Count - 1) ? maxQuantity - quantities.Sum() : quantities[counter]) * ingredient[i].Value;
                        counter++;
                    }
                }

                for(int i = 0; i < subScores.Length - 1; i++) {
                    if(subScores[i] < 0)
                        subScores[i] = 0;
                    score *= subScores[i];
                }

                if(score > maxScore)
                    maxScore = score;
            }
            else {
                int quantityLeft = maxQuantity - quantities.Sum();
                for(int i = 0; i <= quantityLeft; i++) {
                    int[] newQuantities = new int[quantities.Length + 1];
                    quantities.CopyTo(newQuantities, 0);
                    newQuantities[quantities.Length] = i;
                    ExploreRecipies(ingredients, numStats, newQuantities, maxQuantity, ref maxScore);
                }
            }
        }
    }
}
