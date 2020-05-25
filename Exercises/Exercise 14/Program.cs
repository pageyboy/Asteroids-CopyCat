using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("What is your birth month? (i.e. March): ");
            string month = Console.ReadLine();
            Console.Write("What day were you born on? (i.e. 2): ");
            int day = int.Parse(Console.ReadLine());
            Console.WriteLine("\nYou will receive a reminder on " + (day - 1) + " " + month + " that you receive 20% off on your birthday!");
        }
    }
}
