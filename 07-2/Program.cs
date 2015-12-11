using System;
using System.Collections.Generic;
using System.IO;

namespace _07_2 {
    class Program {
        static List<string> instructions = new List<string>();

        static void Main(string[] args) {
            ushort a = 0;
            Dictionary<string, ushort> wires = new Dictionary<string, ushort>();

            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                instructions.Add(line);
            }

            a = EvalWire(wires, "a");
            wires.Clear();
            for(int i = 0; i < instructions.Count; i++)
                if(instructions[i].EndsWith(" -> b"))
                    instructions[i] = a + " -> b";

            a = EvalWire(wires, "a");

            Console.WriteLine(a);
            Console.ReadLine();
        }

        static ushort EvalWire(Dictionary<string, ushort> wires, string key) {
            string instruction = GetInst(key);
            string[] parts = instruction.Split(new string[] { " -> " }, StringSplitOptions.None)[0].Split(new string[] { " " }, StringSplitOptions.None);
            ushort tempParseA;
            ushort tempParseB;

            if(!wires.ContainsKey(key)) {
                switch(parts.Length) {
                    case 1: // no operation
                        if(ushort.TryParse(parts[0], out tempParseA))
                            wires[key] = tempParseA;
                        else {
                            if(!wires.ContainsKey(parts[0]))
                                wires[parts[0]] = EvalWire(wires, parts[0]);
                            wires[key] = wires[parts[0]];
                        }
                        break;
                    case 2: // NOT operation
                        if(ushort.TryParse(parts[1], out tempParseA))
                            wires[key] = (ushort)~tempParseA;
                        else {
                            if(!wires.ContainsKey(parts[1]))
                                wires[parts[1]] = EvalWire(wires, parts[1]);
                            wires[key] = (ushort)~wires[parts[1]];
                        }
                        break;
                    case 3: // other operations
                        if(ushort.TryParse(parts[0], out tempParseA)) {
                            if(ushort.TryParse(parts[2], out tempParseB))
                                wires[key] = Calculate(tempParseA, tempParseB, parts[1]);
                            else {
                                if(!wires.ContainsKey(parts[2]))
                                    wires[parts[2]] = EvalWire(wires, parts[2]);
                                wires[key] = Calculate(tempParseA, wires[parts[2]], parts[1]);
                            }
                        }
                        else {
                            if(ushort.TryParse(parts[2], out tempParseB)) {
                                if(!wires.ContainsKey(parts[0]))
                                    wires[parts[0]] = EvalWire(wires, parts[0]);
                                wires[key] = Calculate(wires[parts[0]], tempParseB, parts[1]);
                            }
                            else {
                                if(!wires.ContainsKey(parts[0]))
                                    wires[parts[0]] = EvalWire(wires, parts[0]);
                                if(!wires.ContainsKey(parts[2]))
                                    wires[parts[2]] = EvalWire(wires, parts[2]);
                                wires[key] = Calculate(wires[parts[0]], wires[parts[2]], parts[1]);
                            }
                        }
                        break;
                }
            }
            return wires[key];
        }

        static string GetInst(string key) {
            foreach(string instruction in instructions) {
                if(instruction.EndsWith(" -> " + key))
                    return instruction;
            }
            return null;
        }

        static ushort Calculate(ushort num1, ushort num2, string operation) {
            switch(operation) {
                case "AND":
                    return (ushort)(num1 & num2);
                case "OR":
                    return (ushort)(num1 | num2);
                case "LSHIFT":
                    return (ushort)(num1 << num2);
                case "RSHIFT":
                    return (ushort)(num1 >> num2);
                default:
                    throw new Exception("Invalid Operation");
            }
        }
    }
}
