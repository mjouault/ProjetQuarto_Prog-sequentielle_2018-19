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
            Console.WriteLine("");
            piece9();
            Console.WriteLine("");
            piece10();
            Console.WriteLine("");
            piece11();
            Console.WriteLine("");
            piece12();
            Console.WriteLine("");
            piece13();
            Console.WriteLine("");
            piece14();
            Console.WriteLine("");
            piece15();
            Console.WriteLine("");
            piece16();


            Console.ReadLine();
        }

        static void piece1()
        {
            
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i <7; i++)
            {
                Console.WriteLine("**************");
            }
            Console.ResetColor();
        }
        static void piece2()
        {

            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine("**************");
            }
            Console.ResetColor();
        }
        // static void piece2 ()
        /*   {
               for (int i=0; i<2; i++)
             Console.WriteLine("**************");
               for (int i = 0; i < 3; i++)
                   Console.WriteLine("****      ****");
               for (int i = 0; i < 2; i++)
                   Console.WriteLine("**************");
           }*/

        static void piece3 ()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("**************");
            for (int i = 0; i < 5; i++)
                Console.WriteLine("*            *");
            Console.WriteLine("**************");
            Console.ResetColor();
        }

        static void piece4()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("**************");
            for (int i = 0; i < 5; i++)
                Console.WriteLine("*            *");
            Console.WriteLine("**************");
            Console.ResetColor();
        }

        /*   static void piece4 ()
           {
               Console.WriteLine("**************");
               Console.WriteLine("*            *");
               for (int i = 0; i < 3; i++)
                   Console.WriteLine("*   ******   *");
               Console.WriteLine("*            *");
               Console.WriteLine("**************");
           }*/

        static void piece5 ()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("***********");
            }
            Console.ResetColor();

        }
        static void piece6()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("***********");
            }
            Console.ResetColor();

        }
        /*
        static void piece6()
        {
            for (int i = 0; i < 2; i++)
            Console.WriteLine("***********");
            for (int i = 0; i < 2; i++)
                Console.WriteLine("***     ***");
            for (int i = 0; i < 2; i++)
            Console.WriteLine("***********");
        }
        */
        static void piece7()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("***********");
            for (int i = 0; i < 4; i++)
                Console.WriteLine("*         *");
            Console.WriteLine("***********");
            Console.ResetColor();
        }
        static void piece8()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("***********");
            for (int i = 0; i < 4; i++)
                Console.WriteLine("*         *");
            Console.WriteLine("***********");
            Console.ResetColor();
        }
        /*
        static void piece8()
        {
            Console.WriteLine("***********");
            Console.WriteLine("*         *");
            for (int i = 0; i < 2; i++)
                Console.WriteLine("*  *****  *");
            Console.WriteLine("*         *");
            Console.WriteLine("***********");
        }
        */
        static void piece9 ()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("      * *     ");
            Console.WriteLine("   *       * ");
            Console.WriteLine("  *         * ");
            Console.WriteLine("  *         * ");
            Console.WriteLine("   *       * ");
            Console.WriteLine("      * *     ");
            Console.ResetColor();
        }
        static void piece10()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("      * *     ");
            Console.WriteLine("   *       * ");
            Console.WriteLine("  *         * ");
            Console.WriteLine("  *         * ");
            Console.WriteLine("   *       * ");
            Console.WriteLine("      * *     ");
            Console.ResetColor();
        }

        static void piece11()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("      ***     ");
            Console.WriteLine("   ********* ");
            Console.WriteLine("  *********** ");
            Console.WriteLine("  *********** ");
            Console.WriteLine("   ********* ");
            Console.WriteLine("      ***     ");
            Console.ResetColor();
        }
        static void piece12()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("      ***     ");
            Console.WriteLine("   ********* ");
            Console.WriteLine("  *********** ");
            Console.WriteLine("  *********** ");
            Console.WriteLine("   ********* ");
            Console.WriteLine("      ***     ");
            Console.ResetColor();
        }
        static void piece13()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("        *  *     ");
            Console.WriteLine("    *          * ");
            Console.WriteLine("  *             * ");
            Console.WriteLine(" *               * ");
            Console.WriteLine(" *               * ");
            Console.WriteLine("  *             * ");
            Console.WriteLine("    *         * ");
            Console.WriteLine("       *  *     ");
            Console.ResetColor();
        }
        static void piece14()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("        *  *     ");
            Console.WriteLine("    *          * ");
            Console.WriteLine("  *             * ");
            Console.WriteLine(" *               * ");
            Console.WriteLine(" *               * ");
            Console.WriteLine("  *             * ");
            Console.WriteLine("    *         * ");
            Console.WriteLine("        *  *     ");
            Console.ResetColor();
        }

        static void piece15()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("       ******  ");
            Console.WriteLine("     ********** ");
            Console.WriteLine("   ************** ");
            Console.WriteLine("  **************** ");
            Console.WriteLine("  **************** ");
            Console.WriteLine("  *************** ");
            Console.WriteLine("    *********** ");
            Console.WriteLine("      *******     ");
            Console.ResetColor();
        }
        static void piece16()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("       ******  ");
            Console.WriteLine("     ********** ");
            Console.WriteLine("   ************** ");
            Console.WriteLine("  **************** ");
            Console.WriteLine("  **************** ");
            Console.WriteLine("  *************** ");
            Console.WriteLine("    *********** ");
            Console.WriteLine("      *******     ");
            Console.ResetColor();
        }
    }
}
