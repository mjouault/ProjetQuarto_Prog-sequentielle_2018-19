﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiecesStylees
{
    class Program
    {
        static int largeurGrandCarre = 7;
        static int hauteurGrandCarre = 4;
        static int largeurPetitCarre = 5;
        static int hauteurPetitCarre = 3;

       static int curseur = 7;

        static void AfficherGrille()
        {
            for (int i = 0; i < 7; i++) //indice ligne
            {
               
                Console.WriteLine("      +----------------+----------------+----------------+----------------+");
                Console.WriteLine("      |                |                |                |                |");
                Console.WriteLine("      |                |                |                |                |");
                Console.WriteLine("      |                |                |                |                | ");
                Console.WriteLine("      |                |                |                |                |");
               // Console.Write("{0}   ", i);
               // Console.Write("                    |");

                //Console.Write("\n");// sauter une ligne pour mettre la barre entre chaque case
                Console.WriteLine("      |                |                |                |                |");
            }

            Console.WriteLine("      +----------------+----------------+----------------+----------------+");
            Console.WriteLine("               0                1               2                 3");
        }

        static void Main(string[] args)
        {
            AfficherGrille();

            piece9();
            Console.WriteLine("");

            curseur = 7;
       
            piece2();

            /*Console.WriteLine("");
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
            piece16();*/


            Console.ReadLine();
        }

        static void piece1()
        {
            
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < hauteurGrandCarre; i++)
            {
                Console.SetCursorPosition(curseur,1+i);
                Console.WriteLine(string.Concat(Enumerable.Repeat("*", largeurGrandCarre)));
            }
            Console.ResetColor();
        }
        static void piece2()
         {
            // Console.BackgroundColor = ConsoleColor.White;
             Console.ForegroundColor = ConsoleColor.Red;
             for (int i = 0; i < hauteurGrandCarre; i++)
             {
                 Console.SetCursorPosition(curseur, 7 + i);
                 Console.WriteLine(string.Concat(Enumerable.Repeat("*", largeurGrandCarre)));
             }
             Console.ResetColor();
         }

       /* static void piece2 ()
         {
            Console.ForegroundColor = ConsoleColor.Red;
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
            Console.WriteLine(string.Concat(Enumerable.Repeat("*", largeurGrandCarre)));
            for (int i = 0; i < hauteurGrandCarre-2; i++)
                Console.WriteLine("*" + string.Concat(Enumerable.Repeat(" ", largeurGrandCarre - 2)) + "*");
            Console.WriteLine(string.Concat(Enumerable.Repeat("*", largeurGrandCarre)));
            Console.ResetColor();
        }

        static void piece4()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Concat(Enumerable.Repeat("*", largeurGrandCarre)));
            for (int i = 0; i < hauteurGrandCarre-2; i++)
                Console.WriteLine("*" + string.Concat(Enumerable.Repeat(" ", largeurGrandCarre-2)) + "*");
            Console.WriteLine(string.Concat(Enumerable.Repeat("*", largeurGrandCarre)));
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
            for (int i = 0; i < hauteurPetitCarre; i++)
            {
                Console.WriteLine(" " + string.Concat(Enumerable.Repeat("*", largeurPetitCarre)));
            }
            Console.ResetColor();

        }
        static void piece6()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < hauteurPetitCarre; i++)
            {
                Console.WriteLine(" " + string.Concat(Enumerable.Repeat("*", largeurPetitCarre)));
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
            Console.WriteLine(" " + string.Concat(Enumerable.Repeat("*", largeurPetitCarre)));
            for (int i = 0; i < hauteurPetitCarre-2; i++)
            {
                Console.WriteLine(" *" + string.Concat(Enumerable.Repeat(" ", largeurPetitCarre-2)) + "*");
            }
            Console.WriteLine(" " + string.Concat(Enumerable.Repeat("*", largeurPetitCarre)));
            Console.ResetColor();

        }

        static void piece8()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" " + string.Concat(Enumerable.Repeat("*", largeurPetitCarre)));
            for (int i = 0; i < hauteurPetitCarre - 2; i++)
            {
                Console.WriteLine(" *" + string.Concat(Enumerable.Repeat(" ", largeurPetitCarre - 2)) + "*");
            }
            Console.WriteLine(" " + string.Concat(Enumerable.Repeat("*", largeurPetitCarre)));
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

           /* Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(7, 1);
            Console.WriteLine("      ");
            Console.SetCursorPosition(7+9, 1);
            Console.WriteLine("      ");
            Console.SetCursorPosition(7, 2);
            Console.WriteLine("    ");
            Console.SetCursorPosition(7 +11, 2);
            Console.WriteLine("    ");
            Console.SetCursorPosition(7, 3);
            Console.WriteLine("  ");
            Console.SetCursorPosition(7+13, 3);
            Console.WriteLine("  ");
            Console.SetCursorPosition(7, 4);
            Console.WriteLine("    ");
            Console.SetCursorPosition(7+11, 4);
            Console.WriteLine("    ");
            Console.SetCursorPosition(7, 5);
            Console.WriteLine("      ");
            Console.SetCursorPosition(7+9, 5);
            Console.WriteLine("      ");*/
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
           /* Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("      ***     ");
            Console.WriteLine("   ********* ");
            Console.WriteLine("  *********** ");
            Console.WriteLine("  *********** ");
            Console.WriteLine("   ********* ");
            Console.WriteLine("      ***     ");
            Console.ResetColor();*/

            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(7 + 6, 1);
            Console.WriteLine("  ");
            Console.SetCursorPosition(7 + 4, 2);
            Console.WriteLine("      ");
            Console.SetCursorPosition(7 + 2, 3);
            Console.WriteLine("          ");
            Console.SetCursorPosition(7 + 4, 4);
            Console.WriteLine("      ");
            Console.SetCursorPosition(7 + 6, 5);
            Console.WriteLine("  ");
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
