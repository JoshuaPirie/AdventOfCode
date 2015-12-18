using System;
using System.IO;

namespace _18_2 {
    class Program {
        static void Main(string[] args) {
            int lightsOn = 0;
            bool[,] lightStates = new bool[100, 100];

            int counter = 0;
            string line;
            StreamReader file = new StreamReader("input.txt");
            while((line = file.ReadLine()) != null) {
                for(int i = 0; i < line.Length; i++) {
                    if(line[i] == '#')
                        lightStates[i, counter] = true;
                    else if(line[i] == '.')
                        lightStates[i, counter] = false;
                }
                counter++;
            }
            file.Close();

            lightStates[0, 0] = true;
            lightStates[lightStates.GetLength(0) - 1, 0] = true;
            lightStates[0, lightStates.GetLength(1) - 1] = true;
            lightStates[lightStates.GetLength(0) - 1, lightStates.GetLength(1) - 1] = true;

            for(int i = 0; i < 100; i++)
                lightStates = AnimateLights(lightStates);

            foreach(bool lightState in lightStates)
                if(lightState)
                    lightsOn++;

            Console.WriteLine(lightsOn);
            Console.ReadLine();
        }

        static bool[,] AnimateLights(bool[,] lightStates) {
            bool[,] newLightStates = new bool[lightStates.GetLength(0), lightStates.GetLength(1)];
            newLightStates[0, 0] = true;
            newLightStates[lightStates.GetLength(0) - 1, 0] = true;
            newLightStates[0, lightStates.GetLength(1) - 1] = true;
            newLightStates[lightStates.GetLength(0) - 1, lightStates.GetLength(1) - 1] = true;

            for(int x = 0; x < lightStates.GetLength(0); x++) {
                for(int y = 0; y < lightStates.GetLength(1); y++) {
                    if(!((x == 0 && y == 0) || (x == lightStates.GetLength(0) - 1 && y == 0) || (x == 0 && y == lightStates.GetLength(1) - 1) || (x == lightStates.GetLength(0) - 1 && y == lightStates.GetLength(1) - 1))) {
                        int onNeighbours = 0;

                        if(x > 0 && lightStates[x - 1, y])
                            onNeighbours++;
                        if(y > 0 && lightStates[x, y - 1])
                            onNeighbours++;
                        if(x > 0 && y > 0 && lightStates[x - 1, y - 1])
                            onNeighbours++;
                        if(x < lightStates.GetLength(0) - 1 && lightStates[x + 1, y])
                            onNeighbours++;
                        if(y < lightStates.GetLength(1) - 1 && lightStates[x, y + 1])
                            onNeighbours++;
                        if(x < lightStates.GetLength(0) - 1 && y < lightStates.GetLength(1) - 1 && lightStates[x + 1, y + 1])
                            onNeighbours++;
                        if(x < lightStates.GetLength(0) - 1 && y > 0 && lightStates[x + 1, y - 1])
                            onNeighbours++;
                        if(x > 0 && y < lightStates.GetLength(1) - 1 && lightStates[x - 1, y + 1])
                            onNeighbours++;

                        if(lightStates[x, y]) {
                            if(onNeighbours == 2 || onNeighbours == 3)
                                newLightStates[x, y] = true;
                            else
                                newLightStates[x, y] = false;
                        }
                        else {
                            if(onNeighbours == 3)
                                newLightStates[x, y] = true;
                            else
                                newLightStates[x, y] = false;
                        }
                    }
                }
            }
            return newLightStates;
        }
    }
}
