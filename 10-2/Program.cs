using System;
using System.Text;

namespace _10_2 {
    class Program {
        static void Main(string[] args) {
            string input = "1321131112";

            for(int i = 0; i < 50; i++)
                input = GetLookSay(input);

            Console.WriteLine(input.Length);
            Console.ReadLine();
        }

        public static string GetLookSay(string str) {
            StringBuilder builder = new StringBuilder();
            int startIndex = 0;
            for(int i = 1; i <= str.Length; i++) {
                if(i == str.Length || str[i] != str[i - 1]) {
                    builder.Append((i - startIndex).ToString() + str[startIndex]);
                    startIndex = i;
                }
            }
            return builder.ToString();
        }
    }
}
