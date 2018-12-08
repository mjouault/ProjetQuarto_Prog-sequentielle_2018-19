using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test_de_chaque_fonction
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 440; i <= 800; i += 200)
            {
                Console.Beep(i, 500);
            }
        }
        static void InitialiserGrille(string[,] grille)
        {

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    grille[i, j] = "    ";
                }
            }
        }

       static void Choisir1erJoueur(string [,]grille)
        {
            int choix = 0;
            do
            { 
                Console.WriteLine("Si vous voulez jouer en 1er tapez [1] sinon tapez [2]");
                choix = int.Parse(Console.ReadLine());
                if (choix == 2) // l'ordi joue en premier
                {
                    JouerOrdi(grille);
                }
                if (choix == 1)
                {
                    JouerJoueur();
                }
                else //on n'a pas saisi 1 ou 2 
                {
                    Console.WriteLine("vous n'avez pas choisi [1] ou [2], recommencez");
                    Console.Write("Si vous voulez jouer en 1er tapez [1] sinon tapez [2]");
                    choix = int.Parse(Console.ReadLine());
                }
        } while (choix != 2 | choix != 1);
        }

        static void JouerOrdi(string[,] grille)
        {
            do
            {
                Console.WriteLine("Que choisissez-vous comme pièce pour l'ordinateur?\n 0000= petite, creuse, carrée, clair et 1111=grande, pleine, ronde, foncee\n vous pouvez mixer plusieurs caractères évidemment.");
               string piece = Console.ReadLine();//on récupère la pièce que le joueur choisi pour l'ordi.
                Random R = new Random();// choisit aléatoirement la ligne et la colonne pour placer le pion


                int ligne = R.Next(0, 3);
                int col = R.Next(0, 3);

                grille [ligne, col]= piece;

                AfficherGrille(grille, ligne, col, piece);
            }
            while (!Gagner() && !AvoirGrilleRemplie());

        }
       
        static void AfficherGrille(string[,] grille, int ligne, int col, string piece)
        {
            for (int i = 0; i < 4; i++) //indice ligne
            {

                Console.WriteLine("      +----+----+----+----+");
                Console.Write("   " + i);
                Console.Write("  |");

                for (int j = 0; j < 4; j++) // i = indice colonne
                {
                Console.Write(grille[i, j] + "|");
                }
                Console.Write("\n");// sauter une ligne pour mettre la barre entre chaque case
            }

            Console.WriteLine("      +----+----+----+----+");
            Console.WriteLine("         0    1    2    3");
            Console.ReadLine();

        }

        //pb d'affichage pour 0000
        static bool Gagner ()
        {
            return false;
        }

        static bool AvoirGrilleRemplie ()
        {
            return false;
        }

        static void JouerJoueur()
        {
            int ligne, col;
            string[,] grille = new string[4, 4];
            Random R = new Random();
            //cas où le joueur joue
            //insérer un code pour faire choisi une pièce à l'ordi, un random sur chaque caractère?
            int car1 = R.Next(0, 1);
            int car2 = R.Next(0, 1);
            int car3 = R.Next(0, 1);
            int car4 = R.Next(0, 1);
            // faire un code pour être sur de ne pas retomber sur la même pièce qu'avant
            string piece = (car1 + car2 + car3 + car4).ToString();

            Console.WriteLine("Choisir une ligne (entre 0 et 3)");
            ligne = int.Parse(Console.ReadLine());
            Console.WriteLine("Choisir une ligne (entre 0 et 3)");
            col = int.Parse(Console.ReadLine());

            grille[ligne, col] = piece;

            // for (int i=0; i<4; i++) Console.Write( " | " + )
        }


    }
}