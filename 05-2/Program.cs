using System;
using System.IO;

namespace _05_2 {
    class Program {
        static void Main(string[] args) {
            int niceStrings = 0;

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                bool letterPair = false;
                bool letterRepeat = false;

                for(int i = 0; i < line.Length - 3; i++)
                    for(int j = i + 2; j < line.Length - 1; j++)
                        if(line[i] == line[j] && line[i + 1] == line[j + 1])
                            letterPair = true;

                for(int i = 0; i < line.Length - 2; i++)
                    if(line[i] == line[i + 2])
                        letterRepeat = true;

                if(letterPair && letterRepeat)
                    niceStrings++;
            }
            file.Close();

            Console.WriteLine(niceStrings);
            Console.ReadLine();
        }
    }
}
