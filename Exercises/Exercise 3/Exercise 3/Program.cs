using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Exercise_3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> myList = new List<int>();
            for (int i = 1; i <= 10; i++)
            {
                myList.Add(i);
            }

            for (int i = 0; i < myList.Count; i++)
            {
                Debug.Print(myList[i].ToString());
                if (myList[i] % 2 == 0)
                {
                    Debug.Print(myList[i].ToString());
                    myList.RemoveAt(i);
                    i--;
                    Debug.Print(myList[i].ToString());
                }
            }

            for (int i = 0; i < myList.Count; i++)
            {
                Console.WriteLine(myList[i]);
            }

        }
    }
}
