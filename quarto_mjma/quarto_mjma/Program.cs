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
        static string[,] pieces = new string[,] { { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" } };
        static string[,] grille;    // Grille de jeu
   // Liste des pièces la deuxième colonne sert à indiquer ou non la présence (?)

        // Main
        static void Main(string[] args)
        {
            Console.Title = "Jeu de Quarto"; //la fenêtre d'exécution s'appellera jeu de quarto 
            Console.WriteLine("==============================================");
            Console.WriteLine("            VOUS JOUEZ AU QUARTO");
            Console.WriteLine("==============================================\n");

            grille = new string[4, 4];
            //pieces = new string[2, NB_PIECES_TOTALE];
            
            InitialiserGrille();
          //  InitialiserPieces();
            Choix1erJoueur(grille);
            Console.ReadKey();
        }

        // Methods
        static void InitialiserPieces()
        {
            string [,]pieces= new string[,]{ { "0000","0001","0010","0011","0100","0101","0110","0111","1000","1001","1010","1011","1100","1101","1110","1111"},{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0"} };
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
                    //Console.WriteLine("Pièce déjà utilisée, choisissez-en une autre");
                    choixPiece = Console.ReadLine();//on récupère la pièce que le joueur choisi pour l'ordi
                } while (!IsGoodPiece(choixPiece)); //tant que la pièce n'est pas bon on en rechoisi une autre
                Random R = new Random();// choisit aléatoirement la ligne et la colonne pour placer le pion

                int ligne = R.Next(0, 3);
                int col = R.Next(0, 3);
                     
                grille[ligne, col] = choixPiece;
                JouerPiece(choixPiece);

                AfficherGrille();
            }
            while (!Gagner() && !AvoirGrilleRemplie());

        }

        static void JouerPiece(string choixPiece) //permet de marquer lorsqu'une pièce est jouée
        {
            int i = 0;

            // Recherche de l'indice
            while (i<NB_PIECES_TOTALE && choixPiece != pieces[0, i])  
            {
                // Incrémentation
                i++;
            }

            // Pièce utilisée 
            pieces[1, i] = "1";
        }

        /// <summary>
        /// IsGoodPiece() : True si la pièce n'a pas été joué, False sinon
        /// </summary>
        /// <returns></returns>
        static bool IsGoodPiece(string choixPiece) //vérifier si la pièce a été utilisé ou non
        {
            bool good = true;
            int i = 0;  // Compteur

            // Vérification

                while (choixPiece != pieces[0, i]&&i<NB_PIECES_TOTALE)
                    i++;
                if (pieces[1, i] == "1")
                        good = false;
            
                return good;
        }

        static void JouerJoueur()
        {
            int ligne, col;
            Random R = new Random();
            //cas où le joueur joue
            //insérer un code pour faire choisi une pièce à l'ordi, un random sur chaque caractère?
            int car1 = R.Next(0, 1);
            string carun=car1.ToString();
            int car2 = R.Next(0, 1);
            string cardeux=car2.ToString();
            
            int car3 = R.Next(0, 1);
            string cartrois=car3.ToString();
            int car4 = R.Next(0, 1);
            string carquatre=car4.ToString();

            string piece = carun+cardeux+cartrois+carquatre; 

            Console.WriteLine("Choisir une ligne (entre 0 et 3)");
            ligne = int.Parse(Console.ReadLine());
            Console.WriteLine("Choisir une colonne (entre 0 et 3)");
            col = int.Parse(Console.ReadLine());

            JouerPiece(piece);
            grille[ligne, col] = piece;
            AfficherGrille();

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
            bool grilleRemplie = true;
            int i = 0; //indice ligne
            while (i < 4 && AvoirLigneRemplie(i))
                i++;
            
            int j = 0; //indice colonne
            while (j < 4 && AvoirColRemplie(j))
                j++;
                
             grilleRemplie = false;

            return grilleRemplie;
        }

        static bool AvoirLigneRemplie(int ligne)
        {
            bool ligneRemplie = false; // ligne non remplie, on peut donc jouer dessus
            int j = 0; //compteur colonnes
           
            while (j<4 && grille [ligne, j] != "   ")
                j++;
            if (j==4)
            return false; // ligne remplie, impossible de jouer sur cette ligne

            return ligneRemplie;
        }

        static bool AvoirColRemplie (int col)
        {
            bool colRemplie = false; // colonne non remplie, on peut donc jouer dessus
            int i = 0; //compteur lignes

            while (i < 4 && grille[i, col] != "   ")
                i++;
            if (i == 4)
                return false; // colonne remplie, impossible de jouer sur cette colonne

            return colRemplie;
        }
      
    }

}


