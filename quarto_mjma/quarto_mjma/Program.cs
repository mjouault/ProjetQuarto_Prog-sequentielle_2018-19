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
        static string[,] TabPieces = new string[,] { { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" } };
        static string[,] Grille;    // Grille de jeu
        static string caseVide = "    "; //pourquoi ça nous souligne les 2 premières lettres?
        static string ChoixPiece;
        static int ligne; static int col;

        // Main
        static void Main(string[] args)
        {
            string surnom; string afficherRegles;
            do
            {
                Console.Title = "Jeu de Quarto"; //la fenêtre d'exécution s'appellera jeu de quarto 
                Console.WriteLine("==============================================");
                Console.WriteLine("            VOUS JOUEZ AU QUARTO");
                Console.WriteLine("==============================================\n");

                Console.Write("C'est donc toi le nouveau joueur qui souhaite affronter la machine toute puissante au QUARTO !\nQuel est ton petit nom ? ");
                surnom = Console.ReadLine();
                Console.WriteLine("Sympa comme blaze!\nAvant de commencer {0}, veux-tu que je te rappelle les règles du jeu? [o]/[n]", surnom);

                //Afficher règles du jeu
                do
                {
                    afficherRegles = Console.ReadLine();
                    if (afficherRegles != "o" && afficherRegles != "n")
                    {
                        Console.Beep(400, 300);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Il faut répondre par [o] ou [n] on a dit !");
                        Console.ResetColor();
                    }
                } while (afficherRegles != "o" && afficherRegles != "n");

                if (afficherRegles == "o")
                {
                    Console.WriteLine("\nSage décision, un petit rappel ne fait jamais de mal !\n==============================================");
                    Console.WriteLine("            REGLES DU JEU");
                    Console.WriteLine("==============================================\n");

                    Console.Write("\nBUT DU JEU : Créer sur le plateau un alignement de 4 pièces ayant au moins un caractère commun(fig. 2).\nCet alignement peut-être horizontal, vertical ou diagonal. \nDÉROULEMENT D’UNE PARTIE : Le premier joueur est tiré au sort.\nIl choisit une des 16 pièces et la donne à son adversaire.\nCelui - ci doit la placer sur une des cases du plateau et choisir ensuite une des 15 pièces restantes pour la donner à son adversaire.\nA son tour, celui-ci la place sur une case libre et ainsi de suite…." +
                        "\n\nGAIN DE LA PARTIE : La partie est gagnée par le premier joueur qui annonce “QUARTO !”\nUn joueur fait “QUARTO !” et gagne la partie lorsque, en plaçant la pièce donnée, il aligne 4 pièces ayant au moins un caractère en commun.\nPlusieurs caractères peuvent se cumuler.\n\nDe plus, il n’est pas obligé d’avoir lui même déposé les trois autres pièces." +
                        "\nIl y a égalité: toutes les pièces ont été posées sans vainqueur.");
                }

                Console.WriteLine("Que la partie commence et que le meilleur gagne !");

                Grille = new string[4, 4];

                InitialiserGrille();
                Jouer();
            } while (RejouerPartie());
        }



        //Sous-programmes


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


        static string Choisir_1er_joueur()
        {
            string choix1er = "";

            Console.WriteLine("Tapez [1] pour jouer en premier\n" +
                "Tapez [0] si l'IA joue en premier");
            choix1er = Console.ReadLine();

            while (choix1er != "0" && choix1er != "1")
            {
                Console.Beep(400, 300);
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

                    //vérification si le joueur a gagné à chaque fin de tour
                    if (Gagner()) //cas s'il gagne
                    {
                        Console.ForegroundColor = ConsoleColor.Green;//affiche en vert si gagne
                        Console.WriteLine("Vous avez gagné, BRAVO !");
                        Console.Beep(400, 100);//musique de victoire
                        Console.Beep(550, 100);
                        Console.Beep(450, 100);
                        Console.Beep(600, 2000);
                        Console.ResetColor();
                    }

                    else
                    {
                        JouerOrdi(); //si le joueur n'a pas gagné, l'ordinateur joue

                        //même vérification après chaque tour de jeu de l'ordinateur
                        if (Gagner())
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed; //affiche en rouge si perds
                            Console.WriteLine("Quel dommage, votre adversaire a gagné... Ce sera pour une prochaine fois!");
                            Console.Beep(500, 100);
                            Console.Beep(400, 100);
                            Console.Beep(350, 100);
                            Console.Beep(300, 2000);//musique de défaite
                            Console.ResetColor();
                        }
                    }
                }

                if (AvoirGrilleRemplie() && !Gagner()) // Cas où la grille est remplie mais personne ne gagne : c'est un match nul
                    Console.WriteLine("Match nul");
            }

            else // si l'ordinateur commence, l'alternance est inversée
            {
                while (!Gagner() && !AvoirGrilleRemplie())
                {
                    JouerOrdi();

                    if (Gagner())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Quel dommage, votre adversaire a gagné... Ce sera pour une prochaine fois!");
                        Console.Beep(500, 100);
                        Console.Beep(400, 100);
                        Console.Beep(350, 100);
                        Console.Beep(300, 2000);//musique de défaite
                        Console.ResetColor();
                    }

                    else
                    {
                        JouerJoueur();

                        if (Gagner())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;//affiche en vert si gagne
                            Console.WriteLine("Vous avez gagné, BRAVO !");
                            Console.Beep(400, 100);//musique de victoire
                            Console.Beep(550, 100);
                            Console.Beep(450, 100);
                            Console.Beep(600, 2000);
                            Console.ResetColor();
                        }
                    }
                }
                if (AvoirGrilleRemplie() && !Gagner())
                    Console.WriteLine("Match nul");
            }
        }

        /// <summary>
        /// JouerOrdi : Fonction permettant à l'ordinateur de jouer son tour soit de placer une pièce choisie par le joueur
        /// </summary>
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
                ChoixPiece = Console.ReadLine();//on récupère la pièce que le joueur choisi pour l'ordi
                if (VerifierSiPieceUtilisee(ChoixPiece))
                {
                    Console.Beep(500, 300);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Erreur : Pièce déjà utilisée, veuillez en choisir une autre :");
                    Console.ResetColor();
                }
            } while (VerifierSiPieceUtilisee(ChoixPiece)); //tant que la pièce n'est pas bonne on en rechoisit une autre

            JouerPiece(ChoixPiece);

            //choix case par l'ordi
            Random R = new Random();

            // choisit aléatoirement la ligne et la colonne pour placer le pion

            //int ligne; int col;

            do
            {
                ligne = R.Next(0, 4);
                col = R.Next(0, 4);
            } while (AvoirCaseRemplie(ligne, col)); // tant que la case qu'il a choisi est remplie, l'ordi doit replacer sa pièce 

            Grille[ligne, col] = ChoixPiece;
            AfficherGrille();
        }

        /// <summary>
        /// JouerJoueur : Fonciton permettant au joueur de jouer son tour soit de placer une pièce choisie par l'ordinateur
        /// </summary>
        static void JouerJoueur()
        {

            //choix de la pièce dans le tableau par l'ordi
            int randomPiece;
            Random R = new Random();
            do
            {
                randomPiece = R.Next(0, 16);
                ChoixPiece = TabPieces[0, randomPiece];
            }
            while (TabPieces[1, randomPiece] == "1"); //Demander à l'ordi de choisir de nouveau la pièce s'il en a choisi une déjà jouée

            JouerPiece(ChoixPiece);
            Console.WriteLine("L'ordinateur a choisi la pièce {0} pour vous\n" +
                "- 0000 correspond à petite, creuse, carrée, clair\n" +
                "- 1111 correspond à grande, pleine, ronde, foncee \n" +
                "Les caractères peuvent être mélangés entre eux", ChoixPiece);
            // améliorer notre présentation des pièces  Console.WriteLine("le 1er caractère correspond à [1]= ronde [0]=carrée, 2ème caractère [1]=creuse [0]=vide");

            //choix de la case par le joueur

           // int ligne, col;

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

            Grille[ligne, col] = ChoixPiece;
            AfficherGrille();
        }

        /// <summary>
        /// JouerPiece : Fonction permettant de ne jouer qu'une seule fois chaque pièce
        /// </summary>
        /// <param name="choixPiece"></param>
        static void JouerPiece(string choixPiece)
        {
            int i = 0;

            // Recherche de l'indice
            while (i < NB_PIECES_TOTALE && choixPiece != TabPieces[0, i])
            {
                // Incrémentation
                i++;
            }

            // Pièce utilisée 
            TabPieces[1, i] = "1";
        }

        /// <summary>
        /// verifierSiPieceUtilisee : True si la pièce n'a pas été joué, False sinon
        /// </summary>
        /// <returns></returns>
        static bool VerifierSiPieceUtilisee(string choixPiece) //vérifier si la pièce a été utilisée (true) ou non (false)
        {
            bool pieceUtilisee = false;
            int i = 0;  // Compteur

            // Vérification

            while (choixPiece != TabPieces[0, i] && i < NB_PIECES_TOTALE)
                i++;
            if (TabPieces[1, i] == "1")
                pieceUtilisee = true;

            return pieceUtilisee;
        }


        /// <summary>
        /// AfficherGrille : réactualise la grille à chaque tour de jeu
        /// </summary>
        static void AfficherGrille()
        {
            for (int i = 0; i < 4; i++) //indice ligne
            {

                Console.WriteLine("      +----+----+----+----+");
                Console.Write("   " + i);
                Console.Write("  |");

                for (int j = 0; j < 4; j++) // i = indice colonne
                {
                    Console.Write(Grille[i, j] + "|");
                }
                Console.Write("\n");// sauter une ligne pour mettre la barre entre chaque case
            }

            Console.WriteLine("      +----+----+----+----+");
            Console.WriteLine("         0    1    2    3");
        }

        /// <summary>
        /// Gagner () : Fonction donnant toutes les combinaisons gagnantes et terminant la partie
        /// </summary>
        /// <returns></returns>
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
                    while (j < 4 && Grille[i, 0] != caseVide && Grille[i, 0][n] == Grille[i, j][n]) //qd caractéristique commune, on compare la valeur de départ
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
                        while (i < 4 && Grille[0, j] != caseVide && Grille[0, j][n] == Grille[i, j][n])
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
                    while (i < 4 && Grille[0, 0] != caseVide && Grille[0, 0][n] == Grille[i, i][n])
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
                    while (i < 4 && j >= 0 && Grille[1, 3] != caseVide && Grille[1, 3][n] == Grille[i, j][n])
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

            if (gagner)
            {
                Console.WriteLine("QUARTO!"); //pb : dit 2x quarto !
            }

            return gagner;

        }

        /// <summary>
        /// AvoirGrilleRemplie : Condition de fin de jeu et permet de définir quand il y a match nul
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// AvoirCaseRemplie : Permet de savoir si le joueur ou l'ordinateur peut jouer dans la case ou si elle est déjà remplie
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        static bool AvoirCaseRemplie(int i, int j)
        {
            return Grille[i, j] != caseVide; // retourne true si la case considérée n'est pas vide, false sinon
        }

        /// <summary>
        /// RejouerPartie : Le joueur décide, à chaque fin de partie, s'il souhaite en refaire une ou non
        /// </summary>
        /// <returns></returns>
        static bool RejouerPartie()
        {
            Console.WriteLine("Tapez [r] pour rejouer ou [a] pour arrêter");
            string rejouer = Console.ReadLine(); //le joueur choisit s'il veut refaire une partie
            bool continuer = true;
            while (rejouer != "r" && rejouer != "a")
            {
                Console.WriteLine("saisissez [r] ou [a]");
                rejouer = Console.ReadLine();
            }
            if (rejouer == "a")
            {
                continuer = false;
            }
            return continuer;
        }

        /// <summary>
        /// ArreterPartie : à tout moment, le joueur peut décider d'arrêter la partie
        /// </summary>
        static bool ArreterPartie( string stop)
        {
            bool arret = false;
            if (stop.ToString() == "s")
            {
                Console.WriteLine("Voulez-vous arrêter la partie ? (o/n)");
                string arreter = Console.ReadLine();
                if (stop.ToString() == "n")
                {
                    Console.WriteLine("On continue ...");
                }
                else
                {
                    arret = true;
                    Console.WriteLine("On s'arrête ...");
                }
            }
            return arret;
        }
  
    }
}


