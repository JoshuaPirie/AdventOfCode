using System;
using System.IO;

namespace _01_2 {
    class Program {
        static void Main(string[] args) {
            int floor = 0;
            int position = 0;

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                for(int i = 0; i < line.Length; i++) {
                    if(line[i] == '(')
                        floor++;
                    if(line[i] == ')')
                        floor--;
                    if(floor < 0) {
                        position = i + 1;
                        break;
                    }
                }
            }
            file.Close();

            Console.WriteLine(position);
            Console.ReadLine();
        }
    }
}
