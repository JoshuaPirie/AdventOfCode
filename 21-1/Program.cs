using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _21_1 {
    class Program {
        static void Main(string[] args) {
            int minGold = int.MaxValue;
            Player hero = new Player(100, 0, 0);
            Player boss = new Player(100, 8, 2);
            List<Item> items = new List<Item>();

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                GroupCollection matches = Regex.Match(line, @"(\w+):").Groups;
                while((line = file.ReadLine()) != null && line != "") {
                    GroupCollection subMatches = Regex.Match(line, @"^(.+?) +(\d+) +(\d+) +(\d+)").Groups;
                    items.Add(new Item(subMatches[1].Value, matches[1].Value, int.Parse(subMatches[2].Value), int.Parse(subMatches[3].Value), int.Parse(subMatches[4].Value)));
                }
            }
            file.Close();

            items.Add(new Item("Null", "Armor", 0, 0, 0));
            items.Add(new Item("Null", "Rings", 0, 0, 0));
            items.Add(new Item("Null", "Rings", 0, 0, 0));

            IOrderedEnumerable<List<Item>> combinations =
                items.Where(i => i.type == "Weapons").SelectMany(w =>
                items.Where(i => i.type == "Armor").SelectMany(a =>
                items.Where(i => i.type == "Rings").SelectMany(r1 =>
                items.Where(i => i.type == "Rings" && i != r1).Select(r2 =>
                new List<Item> { w, a, r1, r2 })))).OrderBy(c => c.Sum(i => i.cost));

            foreach(List<Item> combination in combinations) {
                hero.damage = combination.Sum(i => i.damage);
                hero.armor = combination.Sum(i => i.armor);

                if((boss.hitPoints + Math.Max(hero.damage - boss.armor, 1) - 1) / Math.Max(hero.damage - boss.armor, 1) <= (hero.hitPoints + Math.Max(boss.damage - hero.armor, 1) - 1) / Math.Max(boss.damage - hero.armor, 1)) {
                    minGold = combination.Sum(i => i.cost);
                    break;
                }
            }

            Console.WriteLine(minGold);
            Console.ReadLine();
        }
    }

    class Player {
        public int hitPoints;
        public int damage;
        public int armor;

        public Player(int hitPoints, int damage, int armor) {
            this.hitPoints = hitPoints;
            this.damage = damage;
            this.armor = armor;
        }
    }

    class Item {
        public string name;
        public string type;
        public int cost;
        public int damage;
        public int armor;

        public Item(string name, string type, int cost, int damage, int armor) {
            this.name = name;
            this.type = type;
            this.cost = cost;
            this.damage = damage;
            this.armor = armor;
        }
    }
}
