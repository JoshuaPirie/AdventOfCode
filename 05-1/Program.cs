using System;
using System.IO;

namespace _05_1 {
    class Program {
        static void Main(string[] args) {
            int niceStrings = 0;

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                int vowels = 0;
                bool doubleLetter = false;
                bool badString = false;

                for(int i = 0; i < line.Length; i++) {
                    if(line[i] == 'a' || line[i] == 'e' || line[i] == 'i' || line[i] == 'o' || line[i] == 'u')
                        vowels++;
                    if(i > 0 && line[i] == line[i - 1])
                        doubleLetter = true;
                    if(i > 0 && ((line[i - 1] == 'a' && line[i] == 'b') || (line[i - 1] == 'c' && line[i] == 'd') || (line[i - 1] == 'p' && line[i] == 'q') || (line[i - 1] == 'x' && line[i] == 'y')))
                        badString = true;
                }
                if(vowels >= 3 && doubleLetter && !badString)
                    niceStrings++;
            }
            file.Close();

            Console.WriteLine(niceStrings);
            Console.ReadLine();
        }
    }
}
