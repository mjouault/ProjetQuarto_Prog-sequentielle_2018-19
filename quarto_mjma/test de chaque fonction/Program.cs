using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_de_chaque_fonction
{
    class Program
    {
        static void Main(string[] args)
        {
            InitialiserGrille(new int[4, 4]);
            AfficherGrille(new int [4,4]);
        }
        static void InitialiserGrille(int[,] grille)
        {
            grille = new int[4, 4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    grille[i, j] = 0;
                }
            }
        }


        static void AfficherGrille(int[,] grille)
        {
            for (int i = 0; i < 4; i++) //indice ligne
            {

                Console.WriteLine("      +-----+-----+-----+-----+");
                Console.Write("   " + i);
                Console.Write("  |");

                for (int j = 0; j < 4; j++) // i = indice colonne
                {

                    grille[i, j] = 22222; // aucun caractère et pièce non présente
                    Console.Write(grille[i, j] + "|");

                }
                Console.Write("\n");// sauter une ligne pour mettre la barre entre chaque case
            }

            Console.WriteLine("      +-----+-----+-----+-----+");
            Console.WriteLine("         0     1     2     3");
            Console.ReadLine();
        }
    }
}