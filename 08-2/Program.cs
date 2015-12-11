using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;

namespace _08_2 {
    class Program {
        static void Main(string[] args) {
            int difference = 0;

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                using(var writer = new StringWriter()) {
                    using(var provider = CodeDomProvider.CreateProvider("CSharp")) {
                        provider.GenerateCodeFromExpression(new CodePrimitiveExpression(line), writer, null);
                        difference += writer.ToString().Length - line.Length;
                    }
                }
            }

            Console.WriteLine(difference);
            Console.ReadLine();
        }
    }
}
