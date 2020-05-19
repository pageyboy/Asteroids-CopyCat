using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleCards;

namespace Exercise1
{
    /// <summary>
    /// Exercise 1 solution
    /// </summary>
    class Program
    {
        /// <summary>
        /// Practices using arrays
        /// </summary>
        /// <param name="args">command-line arguments</param>
        static void Main(string[] args)
        {

            Deck myDeck = new Deck();
            Card[] myCards = new Card[5];
            myDeck.Shuffle();
            myCards[0] = myDeck.TakeTopCard();
            myCards[0].FlipOver();
            Console.WriteLine(myCards[0].Rank + " of " + myCards[0].Suit);
            myCards[1] = myDeck.TakeTopCard();
            myCards[1].FlipOver();
            Console.WriteLine(myCards[1].Rank + " of " + myCards[1].Suit);

            Console.WriteLine();
        }
    }
}
