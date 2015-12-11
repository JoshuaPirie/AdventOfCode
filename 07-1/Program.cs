using System;
using System.Collections.Generic;
using System.IO;

namespace _07_1 {
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

            Console.WriteLine(a);
            Console.ReadLine();
        }

        static ushort EvalWire(Dictionary<string, ushort> wires, string wire) {
            string instruction = GetInst(wire);
            string[] parts = instruction.Split(new string[] { " -> " }, StringSplitOptions.None)[0].Split(new string[] { " " }, StringSplitOptions.None);
            ushort tempParseA;
            ushort tempParseB;

            if(!wires.ContainsKey(wire)) {
                switch(parts.Length) {
                    case 1: // no operation
                        if(ushort.TryParse(parts[0], out tempParseA))
                            wires[wire] = tempParseA;
                        else {
                            if(!wires.ContainsKey(parts[0]))
                                wires[parts[0]] = EvalWire(wires, parts[0]);
                            wires[wire] = wires[parts[0]];
                        }
                        break;
                    case 2: // NOT operation
                        if(ushort.TryParse(parts[1], out tempParseA))
                            wires[wire] = (ushort)~tempParseA;
                        else {
                            if(!wires.ContainsKey(parts[1]))
                                wires[parts[1]] = EvalWire(wires, parts[1]);
                            wires[wire] = (ushort)~wires[parts[1]];
                        }
                        break;
                    case 3: // other operations
                        if(ushort.TryParse(parts[0], out tempParseA)) {
                            if(ushort.TryParse(parts[2], out tempParseB))
                                wires[wire] = Calculate(tempParseA, tempParseB, parts[1]);
                            else {
                                if(!wires.ContainsKey(parts[2]))
                                    wires[parts[2]] = EvalWire(wires, parts[2]);
                                wires[wire] = Calculate(tempParseA, wires[parts[2]], parts[1]);
                            }
                        }
                        else {
                            if(ushort.TryParse(parts[2], out tempParseB)) {
                                if(!wires.ContainsKey(parts[0]))
                                    wires[parts[0]] = EvalWire(wires, parts[0]);
                                wires[wire] = Calculate(wires[parts[0]], tempParseB, parts[1]);
                            }
                            else {
                                if(!wires.ContainsKey(parts[0]))
                                    wires[parts[0]] = EvalWire(wires, parts[0]);
                                if(!wires.ContainsKey(parts[2]))
                                    wires[parts[2]] = EvalWire(wires, parts[2]);
                                wires[wire] = Calculate(wires[parts[0]], wires[parts[2]], parts[1]);
                            }
                        }
                        break;
                }
            }
            return wires[wire];
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
