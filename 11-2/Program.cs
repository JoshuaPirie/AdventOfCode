using System;
using System.Text;

namespace _11_2 {
    class Program {
        static void Main(string[] args) {
            string input = "hepxcrrq";

            string nextPass = NextPass(NextPass(input));

            Console.WriteLine(nextPass);
            Console.ReadLine();
        }

        static string NextPass(string str) {
            do
                str = NextString(str);
            while(!ValidPass(str));
            return str;
        }

        static string NextString(string str) {
            StringBuilder builder = new StringBuilder(str);
            bool incrementing = true;
            int position = builder.Length - 1;

            while(position > 0 && incrementing) {
                if(builder[position] == 'z') {
                    builder[position] = 'a';
                    position--;
                }
                else {
                    builder[position]++;
                    incrementing = false;
                }
            }

            return builder.ToString();
        }

        static bool ValidPass(string str) {
            bool hasStraight = false;
            bool hasInvalid = false;
            int pairs = 0;

            for(int i = 0; i < str.Length; i++) {
                if(i < str.Length - 2)
                    if(str[i] + 1 == str[i + 1] && str[i + 1] + 1 == str[i + 2])
                        hasStraight = true;

                if(str[i] == 'i' || str[i] == 'o' || str[i] == 'l')
                    hasInvalid = true;

                if(i < str.Length - 1)
                    if(str[i] == str[i + 1] && (i == 0 || str[i] != str[i - 1]))
                        pairs++;
            }

            if(hasStraight && !hasInvalid && pairs >= 2)
                return true;
            return false;
        }
    }
}
