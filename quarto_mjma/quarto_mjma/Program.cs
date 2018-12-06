using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quarto_mjma
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Jeu de Quarto"; //la fenêtre d'exécution s'appellera jeu de quarto 
            Console.WriteLine("==============================================");
            Console.WriteLine("            VOUS JOUEZ AU QUARTO");
            Console.WriteLine("==============================================\n");
            
            int[,] grille = new int[4, 4];
            
            InitialiserGrille(grille);
            AfficherGrille(grille);
            
            Jouer();
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


        static void Jouer()
        {
            int col;
            int ligne;
            int[,] grille = new int[4, 4];//grille de 4 par 4


            //cas où l'ordinateur joue
            Console.WriteLine("Que choisissez-vous comme pièce pour l'ordinateur?\n 0000= petite, creuse, carrée, noire et 1111=grande, pleine, ronde, blanche\n vous pouvez mixer plusieurs caractères évidemment.");
            int pièce = int.Parse(Console.ReadLine());//on récupère la pièce que le joueur choisi pour l'ordi
            Random R = new Random();// choisit aléatoirement la ligne et la colonne pour placer le pion


            ligne = R.Next(0, 3);
            col = R.Next(0, 3);
            while (grille[ligne, col] != 2222)//tant que l'ordi choisi une case où il y a déjà une pièce
            {
             ligne = R.Next(0, 3);
             col = R.Next(0, 3);
            }
            grille[ligne, col] = pièce; //l'ordi a mis la pièce dans une case vide
            

            //cas où le joueur joue
            //insérer un code pour faire choisi une pièce à l'ordi, un random sur chaque caractère?
            int car1 = R.Next(0, 1);
            int car2 = R.Next(0, 1);
            int car3 = R.Next(0, 1);
            int car4 = R.Next(0, 1);
            // faire un code pour être sur de ne pas retomber sur la même pièce qu'avant
            pièce = car1 + car2 + car3 + car4; //code faux ne pas mettre plus car on veut faire une chaine de caractère avec tous ces chiffres

            Console.WriteLine("Choisir une ligne (entre 0 et 3)");
            ligne = int.Parse(Console.ReadLine());
            Console.WriteLine("Choisir une ligne (entre 0 et 3)");
            col = int.Parse(Console.ReadLine());

            grille[ligne, col] = pièce;
             
            // for (int i=0; i<4; i++) Console.Write( " | " + )
        }
    }

}
