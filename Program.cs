using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quarto
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("========================================");
            Console.WriteLine("              QUARTO");
            Console.WriteLine("========================================\n");


            int[,] grille = new int[4, 4];
            initialiserGrille(grille);
            afficherGrille();
        }

        static void initialiserGrille(int[,] grille)
        {

            for (int i = 0; i < 4; i++) //indice ligne
            {
                Console.WriteLine("  +-----+-----+-----+-----+");
                Console.Write(i);
                Console.Write(" |");

                for (int j = 0; j < 4; j++) // i = indice colonne
                {
                    grille[i, j] =22222; // aucun caractère et pièce non présente
                    Console.Write(grille[i, j] + "|");
                }
                Console.Write("\n");// sauter une ligne pour mettre la barre entre chaque case
            }
            Console.WriteLine("  +-----+-----+-----+-----+");
            Console.WriteLine("     0     1     2     3");
            Console.ReadLine();
        }

        static void afficherGrille()
        {

        }
        

        static void jouer()
        {
            int col;
            int line;
            int[,] grille = new int[4, 4];
            
            Console.WriteLine("Choisir une ligne (entre 0 et 3)");
            line = int.Parse(Console.ReadLine());
            Console.WriteLine("Choisir une ligne (entre 0 et 3)");
            col = int.Parse(Console.ReadLine());
            grille[line,col]=;

            // for (int i=0; i<4; i++) Console.Write( " | " + )
        }
    }
}

