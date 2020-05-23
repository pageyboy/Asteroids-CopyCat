using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    public class Dice
    {

        public Dice(int numSides, int seed)
        {
            sides = numSides;
            arrayResults = new int[numSides];
            random = new Random(seed);
        }
        private int sides;
        private int topSide;
        private Random random;
        private int[] arrayResults;

        public int Sides
        {
            get { return sides; }
        }

        public int TopSide
        {
            get { return topSide; }
        }

        public int[] AllResults
        {
            get { return arrayResults; }
        }

        public void Roll()
        {
            topSide = random.Next(1, sides + 1);
            arrayResults[topSide - 1] = arrayResults[topSide - 1] + 1;
        }
    }
}
