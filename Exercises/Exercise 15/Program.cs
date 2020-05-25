using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_15
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter Pyramid Slot Number (slot number, block letter, lit): ");
            string input = Console.ReadLine();
            string[] splitInput = input.Split(',');
            int slotNumber = int.Parse(splitInput[0]);
            char blockLetter = char.Parse(splitInput[1]);
            bool lit = bool.Parse(splitInput[2]);
            Console.WriteLine("Slot Number: " + slotNumber);
            Console.WriteLine("Block Letter: " + blockLetter);
            Console.WriteLine("Lit: " + lit);
        }
    }
}
