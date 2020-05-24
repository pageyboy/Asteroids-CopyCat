using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_12
{
    class Program
    {
        static void Main(string[] args)
        {
            Matherator matherator = new Matherator();
            int x = matherator.GetNthEvenNumber(400);
            Console.WriteLine(x);
            x = matherator.GetTenthEvenNumber();
            Console.WriteLine(x);
            matherator.PrintMToN(10, 20);
            Console.WriteLine();
            matherator.PrintOneToTen();
            Console.WriteLine();
        }
    }
}
