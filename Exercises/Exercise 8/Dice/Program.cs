using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    class Program
    {
        static void Main(string[] args)
        {
            int numSides = 20;
            int numRolls = 4000000;
            int seed = Environment.TickCount;
            Random random = new Random(seed);

            for (int h = 0; h < 5; h++)
            {
                Dice dice = new Dice(numSides, random.Next());
                for (int numRoll = 0; numRoll < numRolls; numRoll++)
                {
                    dice.Roll();
                }

                int[] results = new int[numSides];
                results = dice.AllResults;
                double binSize = Math.Ceiling((numRolls / (numSides * 75d)));
                float[] stats = new float[numSides];
                for (int i = 0; i < numSides; i++)
                {
                    float percentOfRolls = ((float)(((float)results[i] / numRolls) * 100f));
                    stats[i] = percentOfRolls;
                    /*Console.Write(i + " (" + results[i] + "," + percentOfRolls.ToString("0.0000") + "%) \t\t");
                    // Creates histogram. Works best for low rolls (< 2000)
                    for (int a = 0; a < results[i]; a += (int)binSize)
                    {
                        Console.Write("|");
                    }
                    Console.Write("\n");*/
                }
                Console.WriteLine("min: " + stats.Min().ToString("0.0000"));
                Console.WriteLine("min: " + stats.Max().ToString("0.0000") + "\n");
                
            }
        }

    }
}
