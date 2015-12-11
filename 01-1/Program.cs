using System;
using System.IO;

namespace _01_1 {
    class Program {
        static void Main(string[] args) {
            int floor = 0;

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                for(int i = 0; i < line.Length; i++) {
                    if(line[i] == '(')
                        floor++;
                    if(line[i] == ')')
                        floor--;
                }
            }
            file.Close();
            
            Console.WriteLine(floor);
            Console.ReadLine();
        }
    }
}
