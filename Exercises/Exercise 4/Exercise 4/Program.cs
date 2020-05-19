using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleCards;

namespace Exercise_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello\n\n");
            Console.Write("Please enter a number to count from: ");
            int startNum = int.Parse(Console.ReadLine());
            Console.Write("Please enter a number to count to: ");
            int endNum = int.Parse(Console.ReadLine());
            Console.WriteLine();
            for (int i = startNum; i < endNum + 1; i++)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            Deck myDeck = new Deck();
            List<Card> myHand = new List<Card>();
            myDeck.Shuffle();
            for (int i = 0; i < 5; i++)
            {
                myHand.Add(myDeck.TakeTopCard());
            }
            for (int i = 0; i < myHand.Count; i++)
            {
                myHand[i].FlipOver();
            }
            foreach (Card myCard in myHand)
            {
                myCard.Print();
            }
        }
    }
}
