using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_12
{
    class Matherator
    {
        /// <summary>
        /// Returns the nth even number
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int GetNthEvenNumber(int n)
        {
            return n * 2;
        }

        /// <summary>
        /// Returns the tenth even number
        /// </summary>
        /// <returns></returns>
        public int GetTenthEvenNumber()
        {
            return 10 * 2;
        }

        /// <summary>
        /// Prints the numbers from min to max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void PrintMToN(int min, int max)
        {
            if (min >= max)
            {
                throw new ArgumentException("Min is greater than or equal to max.");
            } else
            {
                for (int i = min; i <= max; i++)
                {
                    Console.WriteLine(i);
                }
            }
        }

        public void PrintOneToTen()
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine(i);
            }
        }
    }
}
