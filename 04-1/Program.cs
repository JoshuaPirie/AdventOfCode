using System;
using System.Security.Cryptography;
using System.Text;

namespace _04_1 {
    class Program {
        static MD5 md5 = MD5.Create();

        static void Main(string[] args) {
            string input = "yzbqklnj";
            int num = 0;
            string hash = CalculateMD5Hash(input + num);

            while(!hash.StartsWith("00000")) {
                num++;
                hash = CalculateMD5Hash(input + num);
            }

            Console.WriteLine(num);
            Console.ReadLine();
        }

        static string CalculateMD5Hash(string input) {
            byte[] hash = md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            StringBuilder builder = new StringBuilder(hash.Length * 2);
            foreach(byte b in hash)
                builder.AppendFormat("{0:x2}", b);
            return builder.ToString();
        }
    }
}
