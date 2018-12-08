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
        // tableau des pièces avec deuxième ligne servant à indiquer ou non la présence de la pièce sur la grille de jeu
        static string[,] tabPieces = new string[,] { { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" } };
        static string[,] grille;    // Grille de jeu
        static string CaseVide = "    ";
        static string choixPiece;


        // Main
        static void Main(string[] args)
        {
            Console.Title = "Jeu de Quarto"; //la fenêtre d'exécution s'appellera jeu de quarto 
            Console.WriteLine("==============================================");
            Console.WriteLine("            VOUS JOUEZ AU QUARTO");
            Console.WriteLine("==============================================\n");

            grille = new string[4, 4];
            
            InitialiserGrille();
            Jouer();
            Console.ReadKey();
        }

       

        // Methods
        static void InitialiserPieces()
        {
            string [,]tabPieces= new string[,]{ { "0000","0001","0010","0011","0100","0101","0110","0111","1000","1001","1010","1011","1100","1101","1110","1111"},{"0","0","0","0","0","0","0","0","0","0","0","0","0","0","0","0"} };
        }

        static void InitialiserGrille()
        {

            for (int i = 0; i < 4; i++) //indice ligne
            {
                for (int j = 0; j < 4; j++) // i = indice colonne
                {
                    grille[i, j] = CaseVide; // aucun caractère et pièce non présente
                }
                Console.Write("\n");// sauter une ligne pour mettre la barre entre chaque case
            }
        }

        static string Choisir_1er_joueur()
        {
            string choix1er = "";

            Console.WriteLine("Tapez [1] pour jouer en premier\n" +
                "Tapez [0] si l'IA joue en premier");
            choix1er = Console.ReadLine();

            while (choix1er != "0" && choix1er != "1")
            {
                Console.Beep(500, 300);
                Console.ForegroundColor = ConsoleColor.DarkRed;//afficher le message d'erreur en rouge
                Console.WriteLine("vous n'avez pas choisi [1] ou [0], recommencez");
                choix1er = Console.ReadLine();
                Console.ResetColor();
            }

            return choix1er;
        }

        static void Jouer()
        {
            if (Choisir_1er_joueur() == "1")
            {
                while (!Gagner() && !AvoirGrilleRemplie())
                {
                    JouerJoueur();

                    if (Gagner())
                    {
                        Console.Beep(440, 500);
                        Console.ForegroundColor = ConsoleColor.Green;//affiche en vert si gagne
                        Console.WriteLine("Vous avez gagné, BRAVO !");
                        Console.ResetColor();
                    }
                    else
                    {
                        JouerOrdi();

                        if (Gagner())
                        {
                            Console.Beep(440,500);
                            Console.ForegroundColor = ConsoleColor.DarkRed; //affiche en rouge si perds
                            Console.WriteLine("Quel dommage, votre adversaire a gagné... Ce sera pour une prochaine fois!");
                            Console.ResetColor();
                        }
                    }
                }
            }
            else
            {
                while (!Gagner() && !AvoirGrilleRemplie())
                {
                    JouerOrdi();

                    if (Gagner())
                    {
                        Console.Beep(440,500);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Quel dommage, votre adversaire a gagné... Ce sera pour une prochaine fois!");
                        Console.ResetColor();
                    }
                    else
                    {
                        JouerJoueur();

                        if (Gagner())
                        {
                            Console.Beep(440, 500);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Vous avez gagné, BRAVO !");
                            Console.ResetColor();
                        }
                    }
                }
            }
        }
        static void JouerOrdi()
        {
            //choix pièce par le joueur
            Console.WriteLine("Que choisissez-vous comme pièce pour l'ordinateur?\n" +
                "- 0000 correspond à petite, creuse, carrée, clair\n" +
                "- 1111 correspond à grande, pleine, ronde, foncee \n" +
                "vous pouvez mixer plusieurs caractères évidemment.");
            do
            {
                //Console.WriteLine("Pièce déjà utilisée, choisissez-en une autre");
                choixPiece = Console.ReadLine();//on récupère la pièce que le joueur choisi pour l'ordi
                if (!Verifier_si_pièce_non_utilisée(choixPiece))
                {
                    Console.Beep(500, 300);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Erreur : Pièce déjà utilisée, veuillez en choisir une autre :");
                    Console.ResetColor();
                }
            } while (!Verifier_si_pièce_non_utilisée(choixPiece)); //tant que la pièce n'est pas bonne on en rechoisit une autre

            JouerPiece(choixPiece);

            //choix case par l'ordi
            Random R = new Random();

            // choisit aléatoirement la ligne et la colonne pour placer le pion

            int ligne; int col;

            do
            {
                ligne = R.Next(0, 3);
                col = R.Next(0, 3);
            } while (AvoirCaseRemplie(ligne, col)); // tant que la case qu'il a choisi est remplie, l'ordi doit replacer sa pièce 

            grille[ligne, col] = choixPiece;
            AfficherGrille();
        }

        static void JouerJoueur()
        {

            //choix de la pièce dans le tableau par l'ordi
            int randomPiece;
            Random R = new Random();
            do
            {
                randomPiece = R.Next(0, 16);
                choixPiece = tabPieces[0, randomPiece];
            }
            while (tabPieces[1, randomPiece] == "1"); //Demander à l'ordi de choisir de nouveau la pièce s'il en a choisi une déjà jouée

            JouerPiece(choixPiece);
            Console.WriteLine("L'ordinateur a choisi la pièce {0} pour vous\n" +
                "- 0000 correspond à petite, creuse, carrée, clair\n" +
                "- 1111 correspond à grande, pleine, ronde, foncee \n" +
                "Les caractères peuvent être mélangés entre eux", choixPiece);
          // améliorer notre présentation des pièces  Console.WriteLine("le 1er caractère correspond à [1]= ronde [0]=carrée, 2ème caractère [1]=creuse [0]=vide");
            //choix de la case par le joueur
            int ligne, col;

            do
            {
                Console.WriteLine("Choisir une ligne (entre 0 et 3)");
                ligne = int.Parse(Console.ReadLine());
                Console.WriteLine("Choisir une colonne (entre 0 et 3)");
                col = int.Parse(Console.ReadLine());

                if (AvoirCaseRemplie(ligne, col))
                {
                    Console.WriteLine("Erreur : case déjà remplie, veuillez en choisir une autre :");
                }
            } while (AvoirCaseRemplie(ligne, col)); //tant que la case choisie est remplie, le joueur doit choisir une autre case

            grille[ligne, col] = choixPiece;
            AfficherGrille();
        }

        static void JouerPiece(string choixPiece) //permet de marquer lorsqu'une pièce est jouée
        {
            int i = 0;

            // Recherche de l'indice
            while (i < NB_PIECES_TOTALE && choixPiece != tabPieces[0, i])
            {
                // Incrémentation
                i++;
            }

            // Pièce utilisée 
            tabPieces[1, i] = "1";
        }

        /// <summary>
        /// verifPieceJouee() : True si la pièce n'a pas été joué, False sinon
        /// </summary>
        /// <returns></returns>
        static bool Verifier_si_pièce_non_utilisée(string choixPiece) //vérifier si la pièce a été utilisée (true) ou non (false)
        {
            bool Piecelibre = true;
            int i = 0;  // Compteur

            // Vérification

            while (choixPiece != tabPieces[0, i] && i < NB_PIECES_TOTALE)
                i++;
            if (tabPieces[1, i] == "1")
                Piecelibre = false;

            return Piecelibre;
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
            bool gagner = false;
            int i; //indice lignes
            int j; //indice colonnes
            int n; //indice des 4 caractéristiques de la pièce

            //verif lignes
            for (i = 0; i < 4; i++) //indice ligne
            {
                for (n = 0; n < 4; n++) //test pour chaque carcatéristique(x4)
                {
                    j = 0;
                    while (j < 4 && grille[i, 0] != CaseVide && grille[i, 0][n] == grille[i, j][n]) //qd caractéristique commune, on compare la valeur de départ
                    {
                        j++;
                    }
                    if (j == 4)
                    {
                        gagner = true;
                    }
                }
            }

            //verif colonnes
            if (!gagner)
            {
                for (j = 0; j < 4; j++)
                {
                    for (n = 0; n < 4; n++)
                    {
                        i = 0;
                        while (i < 4 && grille[0, j] != CaseVide && grille[0, j][n] == grille[i, j][n])
                        {
                            i++;
                        }
                        if (i == 4)
                        {
                            gagner = true; // une ligne de 4 pièces avec au moins 1 caractéristique commune a été complétée
                        }
                    }
                }
            }

            //vérif diago de la gauche vers la droite, haut vers bas
            if (!gagner)
            {
                for (n = 0; n < 4; n++)
                {
                    i = 1;
                    while (i < 4 && grille[0, 0] != CaseVide && grille[0, 0][n] == grille[i, i][n])
                    {
                        i++;
                    }
                    if (i == 4)
                    {
                        gagner = true; // la diagonale décrite a été complétée avec 4 pièces ayant au moins 1 caractéristique commune 
                    }
                }
            }

            //vérif diago de la droite vers la gauche, du haut vers le bas
            if (!gagner)
            {
                for (n = 0; n < 4; n++)
                {
                    // Coordonnées (i, j) de la 1ere case que je compare
                    i = 1;
                    j = 2;
                    while (i < 4 && j >= 0 && grille[1, 3] != CaseVide && grille[1, 3][n] == grille[i, j][n])
                    {
                        i++;
                        j--;
                    }
                    if (i == 4)
                    {
                        gagner = true; // la diagonale décrite a été complétée avec 4 pièces ayant au moins 1 caractéristique commune 
                    }
                }
            }

            return gagner;
        }



        //static bool AvoirGrilleRemplie()
        //{
        //    bool grilleRemplie = false;

        //    int i = 0; //indice ligne
        //    while (i < 4 && AvoirLigneRemplie(i))
        //        i++;

        //    if (i == 4)
        //        grilleRemplie = true;

        //    return grilleRemplie;
        //}
        static bool AvoirGrilleRemplie()
        {
            bool grilleRemplie = false;

            int i = 0; //indice ligne
            int j = 0;  // Indice colonne
            while (i < 4 && AvoirCaseRemplie(i, j))
            {
                while (j < 4 && AvoirCaseRemplie(i, j))
                {
                    j++;
                }

                if (j == 4)
                {
                    j = 0;
                    i++;
                }
            }

            if (i == 4 && j == 4)
                grilleRemplie = true;

            return grilleRemplie;
        }

        static bool AvoirCaseRemplie(int i, int j)
        {
            return grille[i, j] != CaseVide; // retourne true si la case considérée n'est pas vide, false sinon
        }

        //static bool AvoirLigneRemplie(int ligne)
        //{
        //    bool ligneRemplie = false; // ligne non remplie, on peut donc jouer dessus
        //    int j = 0; //compteur colonnes
           
        //    // Parcours
        //    while (j < 4 && grille[ligne, j] != caseVide)
        //        j++;

        //    // Vérif
        //    if (j == 4)
        //        ligneRemplie = true; // ligne remplie, impossible de jouer sur cette ligne

        //    return ligneRemplie;
        //}
      
    }

}


