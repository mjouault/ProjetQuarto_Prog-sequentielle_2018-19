using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quarto_mjma
{
    class Program
    {
        // Variables globales
        static int NB_PIECES_TOTALE = 16;

        static string[,] grille;    // Grille de jeu
        static string[,] pieces;    // Liste des pièces

        // Main
        static void Main(string[] args)
        {
            Console.Title = "Jeu de Quarto"; //la fenêtre d'exécution s'appellera jeu de quarto 
            Console.WriteLine("==============================================");
            Console.WriteLine("            VOUS JOUEZ AU QUARTO");
            Console.WriteLine("==============================================\n");

            grille = new string[4, 4];
            pieces = new string[NB_PIECES_TOTALE, 2];

            InitialiserGrille();
            InitialiserPieces();
            JouerOrdi();
            Console.ReadKey();
        }

        // Methods
        static void InitialiserPieces()
        {
            // Initialiser les pieces
            // 0000 0001 0010 0011 0100 0101 0110 0111 1000 1001 1010 1011 1100 1101 1110 1111
        }

        static void InitialiserGrille()
        {

            for (int i = 0; i < 4; i++) //indice ligne
            {
                Console.WriteLine("  +----+----+----+----+");
                Console.Write(i);
                Console.Write(" |");

                for (int j = 0; j < 4; j++) // i = indice colonne
                {
                    grille[i, j] = "    "; // aucun caractère et pièce non présente
                    Console.Write(grille[i, j] + "|");
                }
                Console.Write("\n");// sauter une ligne pour mettre la barre entre chaque case
            }
            Console.WriteLine("  +----+----+----+----+");
            Console.WriteLine("    0    1    2    3");
        }

        static void Choix1erJoueur(string[,] grille)
        {
            int choix1er = 0;
            do
            {
                Console.WriteLine("Si vous voulez jouer en 1er tapez [1] sinon tapez [2]");
                choix1er = int.Parse(Console.ReadLine());
                if (choix1er == 2) // l'ordi joue en premier
                    JouerOrdi();
                if (choix1er == 1)
                    JouerJoueur();
                else //on a pas saisi 1 ou 2 
                {
                    Console.WriteLine("vous n'avez pas choisi [1] ou [2], recommencez");
                    Console.Write("Si vous voulez jouer en 1er tapez [1] sinon tapez [2]");
                    choix1er = int.Parse(Console.ReadLine());
                }
            }
            while (choix1er != 2 | choix1er != 1);
        }

        static void JouerOrdi()
        {
            string choixPiece = "";

            do
            {
                Console.WriteLine("Que choisissez-vous comme pièce pour l'ordinateur?\n 0000= petite, creuse, carrée, clair et 1111=grande, pleine, ronde, foncee\n vous pouvez mixer plusieurs caractères évidemment.");
                do
                {
                    choixPiece = Console.ReadLine();//on récupère la pièce que le joueur choisi pour l'ordi
                } while (!IsGoodPiece(choixPiece));
                Random R = new Random();// choisit aléatoirement la ligne et la colonne pour placer le pion

                int ligne = R.Next(0, 3);
                int col = R.Next(0, 3);

                grille[ligne, col] = choixPiece;
                JouerPiece(choixPiece);

                AfficherGrille();
            }
            while (!Gagner() && !AvoirGrilleRemplie());

        }

        static void JouerPiece(string choixPiece)
        {
            int i = 0;

            // Recherche de l'indice
            while (choixPiece != pieces[i, 0])
            {
                // Incrémentation
                i++;
            }

            // Pièce utilisée
            pieces[i, 1] = "1";
        }

        /// <summary>
        /// IsGoodPiece() : True si la pièce n'a pas été joué, False sinon
        /// </summary>
        /// <returns></returns>
        static bool IsGoodPiece(string choixPiece)
        {
            bool good = true;
            int i = 0;  // Compteur

            // Vérification
            while ((i < NB_PIECES_TOTALE) && good)
            {
                if (choixPiece == pieces[i, 0] && pieces[i, 1] == "1")
                {
                    // 1 si déjà jouée, 0 sinon
                    good = false;
                }

                // Incrémentation
                i++;
            }

            return good;
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

        static void AfficherGrille()
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
        }

        static bool Gagner()
        {
            return false;
        }

        static bool AvoirGrilleRemplie()
        {
            return false;
        }

      
    }

}


