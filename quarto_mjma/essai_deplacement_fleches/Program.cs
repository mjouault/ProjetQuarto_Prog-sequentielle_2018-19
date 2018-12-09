using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace essai_deplacement_fleches
{
    class Program
    {
        static string[,] Grille= new string [4,4];    // Grille de jeu
        static string caseVide = "2222"; //pourquoi ça nous souligne les 2 premières lettres?
        static string[,] TabPieces = new string[,] { { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" } };
        static void Main(string[] args)
        {
         InitialiserGrille();
         AfficherGrille();
           // SelectionnerCase();

        }

        static void InitialiserGrille()
        {

            for (int i = 0; i < 4; i++) //indice ligne
            {
                for (int j = 0; j < 4; j++) // i = indice colonne
                {
                    Grille[i, j] = caseVide; // aucun caractère et pièce non présente
                }
            }
        }
        static void AfficherGrille ()
        { 
         for (int i = 0; i< 4; i++) //indice ligne
            {

                Console.WriteLine("      +----+----+----+----+");
                Console.Write("   " + i);
                Console.Write("  |");

                for (int j = 0; j< 4; j++) // i = indice colonne
                {
                    Console.Write(Grille[i, j] + "|");
                     if (i==0 && j==0)
                      {
                          Console.BackgroundColor = ConsoleColor.White;
                          Console.ForegroundColor = ConsoleColor.Black;
                          Console.WriteLine(Grille[0, 0]);
                          Console.ResetColor();
                      }
                }
                Console.Write("\n");// sauter une ligne pour mettre la barre entre chaque case
            }

            Console.WriteLine("      +----+----+----+----+");
            Console.WriteLine("         0    1    2    3");
            Console.ReadLine();
        }
        public static int SelectionnerCase ()
            {
            int i = 0;
            int j = 0;
            int currentSelection = 0;

                ConsoleKey key = ConsoleKey.LeftArrow;

                Console.CursorVisible = false;

                do
            {
                
                //for (i = 0; i < 4; i++) //indice ligne
                //{
                    Console.WriteLine("      +----+----+----+----+");
                    Console.Write("   " + i);
                    Console.Write("  |");

                   // for (j = 0; j < 4; j++) // i = indice colonne
                   // {
                        
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(Grille[0, j] + "|");


                        Console.ResetColor();
                    //}
               // }
                    Console.Write("\n");// sauter une ligne pour mettre la barre entre chaque case

                Console.WriteLine("      +----+----+----+----+");
                Console.WriteLine("         0    1    2    3");
                Console.ReadLine();

                    key = Console.ReadKey(true).Key;

                    // Choix avec les flèches
                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            if (j < 4)
                            {
                                j++;
                            }
                            break;

                        case ConsoleKey.RightArrow:
                            if (j < 4 - 1)
                            {
                                currentSelection--;
                            }
                            break;

                            case ConsoleKey.UpArrow:
                                if (1 <= 4)
                                {
                                    currentSelection -= 4;
                                }
                                break;

                            case ConsoleKey.DownArrow:
                                if (currentSelection + 4 < 4)
                                {
                                    currentSelection += 4;
                                }
                                break;
                    }
            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;
            Console.Write("\n");

            return currentSelection;
        }
    }
}
