using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiecesStylees
{
    class Program
    {
        static int largeurCarre=5;
        static void Main(string[] args)
        {
            piece1();
            Console.WriteLine("");
            piece2();
            Console.WriteLine("");
            piece3();
            Console.WriteLine("");
            piece4();
            Console.WriteLine("");
            piece5();
            Console.WriteLine("");
            piece6();
            Console.WriteLine("");
            piece7();
            Console.WriteLine("");
            piece8();


            Console.ReadLine();
        }

        static void piece1()
        {
            for (int i = 0; i <7; i++)
            {
                Console.WriteLine("**************");
            }
        }

        static void piece2 ()
        {
            for (int i=0; i<2; i++)
          Console.WriteLine("**************");
            for (int i = 0; i < 3; i++)
                Console.WriteLine("****      ****");
            for (int i = 0; i < 2; i++)
                Console.WriteLine("**************");
        }

        static void piece3 ()
        {
            Console.WriteLine("**************");
            for (int i = 0; i < 5; i++)
                Console.WriteLine("*            *");
            Console.WriteLine("**************");
        }

        static void piece4 ()
        {
            Console.WriteLine("**************");
            Console.WriteLine("*            *");
            for (int i = 0; i < 3; i++)
                Console.WriteLine("*   ******   *");
            Console.WriteLine("*            *");
            Console.WriteLine("**************");
        }

        static void piece5 ()
        {
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("***********");
            }

        }

        static void piece6()
        {
            for (int i = 0; i < 2; i++)
            Console.WriteLine("***********");
            for (int i = 0; i < 2; i++)
                Console.WriteLine("***     ***");
            for (int i = 0; i < 2; i++)
            Console.WriteLine("***********");
        }

        static void piece7()
        {
            Console.WriteLine("***********");
            for (int i = 0; i < 4; i++)
                Console.WriteLine("*         *");
            Console.WriteLine("***********");
        }

        static void piece8()
        {
            Console.WriteLine("***********");
            Console.WriteLine("*         *");
            for (int i = 0; i < 2; i++)
                Console.WriteLine("*  *****  *");
            Console.WriteLine("*         *");
            Console.WriteLine("***********");
        }

    }
}
