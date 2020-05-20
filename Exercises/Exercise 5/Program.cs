using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***********************");
            Console.WriteLine("Menu:");
            Console.WriteLine("1 - New Game");
            Console.WriteLine("2 - Load Game");
            Console.WriteLine("3 - Options");
            Console.WriteLine("4 - Quit");
            Console.WriteLine("***********************");

            Console.Write("\nWhat option would you like to choose? ");

            int userSelection = int.Parse(Console.ReadLine());
            while (userSelection != 4)
            {
                Console.WriteLine("Invalid entry. Please try again.\n");
                Console.Write("\nWhat option would you like to choose? ");
                userSelection = int.Parse(Console.ReadLine());
            }

        }
    }
}
