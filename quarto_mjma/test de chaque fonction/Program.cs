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
            Console.WriteLine("Que choisissez-vous comme pièce pour l'ordinateur?\n 0000= petite, creuse, carrée, noire et 1111=grande, pleine, ronde, blanche\n vous pouvez mixer plusieurs caractères évidemment.");
            int pièce = int.Parse(Console.ReadLine());//on récupère la pièce que le joueur choisi pour l'ordi
            Random R = new Random();// choisit aléatoirement la ligne et la colonne pour placer le pion

        
            int ligne = R.Next(0, 3);
            int col = R.Next(0, 3);
            for (int i = 0; i < 4; i++) //indice ligne
            {

                Console.WriteLine("      +-----+-----+-----+-----+");
                Console.Write("   " + i);
                Console.Write("  |");

                for (int j = 0; j < 4; j++) // i = indice colonne
                {
                if (i==ligne && j==col)
                    grille[i, j] = pièce;// la pièce est placée là où l'ordinateur veut
                 else grille[i, j] = 22222;// aucun caractère et pièce non présente
                           
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