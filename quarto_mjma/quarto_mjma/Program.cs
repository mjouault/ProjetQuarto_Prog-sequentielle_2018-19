using System;

namespace quarto_mjma
{
    class Program
    {
        // Variables globales
        static int nbPiecesTotales = 16;
        // tableau des pièces avec deuxième ligne servant à indiquer ou non la présence de la pièce sur la grille de jeu
        static string[,] TabPieces;
        static string[,] Grille;    // Grille de jeu
        static string caseVide = "    ";

        static string ChoixPiece;

        static int ligne; static int col;
        static int nbreLignes = 4;
        static int nbreCaractéristiques = 4;

        static int[,] tablignes1 = new int[4, 4];
        static int[,] tablignes0 = new int[4, 4];
        static int[,] tabcol1 = new int[4, 4];
        static int[,] tabcol0 = new int[4, 4];
        static int[,] diagos1 = new int[4, 4];
        static int[,] diago0 = new int[4, 4];

        static bool trace = false;

        // Main
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 50);
            AfficherEnTete();
            AfficherRegles();
            do
            {


                Grille = new string[nbreLignes, nbreLignes];

                InitialiserGrille();
                InitialiserPieces();
                Jouer();
            } while (RejouerPartie());
        }

        //Affichage introduction du jeu

        /// <summary>
        /// AfficherEnTete : Affiche l'en-tête
        /// </summary>
        static void AfficherEnTete()
        {
            string surnom;

            Console.Title = "Jeu de Quarto"; //la fenêtre d'exécution s'appellera jeu de quarto 
            AfficherTitre();

            Console.Write("C'est donc toi le nouveau joueur qui souhaite affronter la machine toute puissante au QUARTO !\nQuel est ton petit nom ? ");
            surnom = Console.ReadLine();
            Console.WriteLine("Sympa comme blaze!\nAvant de commencer {0}, veux-tu que je te rappelle les règles du jeu? [o]/[n]", surnom);

        }

        private static void AfficherTitre()
        {
            Console.WriteLine("==============================================");
            Console.WriteLine("                  QUARTO");
            Console.WriteLine("==============================================\n");
        }

        /// <summary>
        /// AfficherRegles : propose au joueur de rappeler les règles du jeu
        /// </summary>
        static void AfficherRegles()
        {
            string afficherRegles;
            do
            {
                afficherRegles = Console.ReadLine();
                Console.Clear();
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

            //Console.WriteLine("Que la partie commence et que le meilleur gagne !");
        }

        //Sous-programmes

        static void InitialiserGrille()
        {

            for (int i = 0; i < nbreLignes; i++) //indice ligne
            {
                for (int j = 0; j < nbreLignes; j++) // i = indice colonne
                {
                    Grille[i, j] = caseVide; // aucun caractère et pièce non présente
                }
            }
        }

        static void InitialiserPieces()
        {
            TabPieces = new string[,] { { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" },
                                    { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" } };
        }


        static bool choisir1erJoueur()
        {
            Random R = new Random();
            int choix1er = R.Next(0, 2);
            bool estHumain = true;

            if (choix1er == 0)
                estHumain = false;

            return estHumain;
        }

        static void Jouer()
        {

            bool joueurCourant = choisir1erJoueur();

            while (!Gagner() && !AvoirGrilleRemplie())
            {
                //Console.Clear();
                AfficherTitre();
                AfficherGrille();

                if (trace)
                    Console.WriteLine("la partie commence");

                if (joueurCourant)  // joueur etre humain
                {
                    if (trace)
                        Console.WriteLine("A toi de jouer");

                    JouerJoueur();
                    if (trace)
                        Console.WriteLine("Le joueur joue");

                    // AfficherGrille();

                    if (trace)
                        Console.WriteLine("Le joueur a joué");

                    //vérification si le joueur a gagné à chaque fin de tour
                    if (Gagner()) //cas s'il gagne
                        AfficherVictoire();
                }

                else   // joueur ordinateur
                {
                    JouerOrdi(); //si le joueur n'a pas gagné, l'ordinateur joue
                    //même vérification après chaque tour de jeu de l'ordinateur

                    if (trace)
                        Console.WriteLine("L'ordi joue");

                    //AfficherGrille();

                    if (trace)
                        Console.WriteLine("l'ordi a joué");

                    if (Gagner())
                        AfficherPerte();
                }

                if (AvoirGrilleRemplie() && !Gagner()) // Cas où la grille est remplie mais personne ne gagne : c'est un match nul
                    Console.WriteLine("Match nul");

                joueurCourant = !joueurCourant;
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

            /* Random R = new Random();
              // choisit aléatoirement la ligne et la colonne pour placer le pion
              do
              {
                  ligne = R.Next(0, nbreLignes);
                  col = R.Next(0, nbreLignes);
              } while (AvoirCaseRemplie(ligne, col)); // tant que la case qu'il a choisi est remplie, l'ordi doit replacer sa pièce 

              Grille[ligne, col] = ChoixPiece;
             AfficherGrille();*/

            ChoixIntell2();
            MettreAJourStrategies(ligne, col);

            //AfficherGrille();

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

            bool caseRemplie = false;
            //choix de la case par le joueur

            if (trace)
                Console.WriteLine("début while");

            do
            {
                do //vérification si 0<ligne choisie < 3
                {
                    Console.WriteLine("\nChoisir une ligne (entre 0 et 3) ");
                    ligne = int.Parse(Console.ReadLine());
                    if (ligne < 0 || ligne > 3)
                    {
                        Console.WriteLine("\nErreur  : Entre 0 et 3 on a dit !");
                    }

                } while (ligne < 0 || ligne > 3);


                do //vérification si 0<colonne choisie <3
                {
                    Console.WriteLine("\nChoisir une colonne (entre 0 et 3)");
                    col = int.Parse(Console.ReadLine());
                    if (col < 0 || col > 3)
                    {
                        Console.WriteLine("\nErreur  : Entre 0 et 3 on a dit !");
                    }
                } while (col < 0 || col > 3);

                if (trace)
                    Console.WriteLine("avant case remplie");

                caseRemplie = AvoirCaseRemplie(ligne, col);

                if (trace)
                    Console.WriteLine("après case remplie");

                if (caseRemplie)
                {
                    Console.WriteLine("\nErreur : case déjà remplie, veuillez en choisir une autre :");
                }
                if (trace)
                    Console.WriteLine("fin while");

            } while (caseRemplie); //tant que la case choisie est remplie, le joueur doit choisir une autre case. Préalablement, les conditions sur les lignes et les colonnes ont été vérifées pour ne pas tomber sur une case hors tableau.

            if (trace)
                Console.WriteLine("yolo1");

            Grille[ligne, col] = ChoixPiece;
            MettreAJourStrategies(ligne, col);


            if (trace)
                Console.WriteLine("yolo2");
            //AfficherGrille();
            //ArreterPartie();
        }

        /// <summary>
        /// JouerPiece : Fonction permettant de ne jouer qu'une seule fois chaque pièce
        /// </summary>
        /// <param name="choixPiece"></param>
        static void JouerPiece(string choixPiece)
        {
            int i = 0;

            // Recherche de l'indice
            while (i < nbPiecesTotales && choixPiece != TabPieces[0, i])
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

            while (choixPiece != TabPieces[0, i] && i < nbPiecesTotales)
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
            for (int i = 0; i < nbreLignes; i++) //indice ligne
            {

                Console.WriteLine("      +------+------+------+------+");
                Console.WriteLine("      |      |      |      |      |");
                Console.Write("{0}   ", i);
                Console.Write("  |");

                for (int j = 0; j < nbreLignes; j++) // i = indice colonne
                {
                    Console.Write(" " + Grille[i, j] + " |");
                }
                Console.Write("\n");// sauter une ligne pour mettre la barre entre chaque case
                Console.WriteLine("      |      |      |      |      |");
            }

            Console.WriteLine("      +------+------+------+------+");
            Console.WriteLine("         0      1      2      3");
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
            for (i = 0; i < nbreLignes; i++) //indice ligne
            {
                for (n = 0; n < nbreLignes; n++) //test pour chaque carcatéristique(x4)
                {
                    j = 0;
                    while (j < nbreLignes && Grille[i, 0] != caseVide && Grille[i, 0][n] == Grille[i, j][n]) //qd caractéristique commune, on compare la valeur de départ
                    {
                        j++;
                    }
                    if (j == nbreLignes)
                    {
                        gagner = true;
                    }
                }
            }

            //verif colonnes
            if (!gagner)
            {
                for (j = 0; j < nbreLignes; j++)
                {
                    for (n = 0; n < nbreLignes; n++)
                    {
                        i = 0;
                        while (i < nbreLignes && Grille[0, j] != caseVide && Grille[0, j][n] == Grille[i, j][n])
                        {
                            i++;
                        }
                        if (i == nbreLignes)
                        {
                            gagner = true; // une ligne de 4 pièces avec au moins 1 caractéristique commune a été complétée
                        }
                    }
                }
            }

            //vérif diago de la gauche vers la droite, haut vers bas
            if (!gagner)
            {
                for (n = 0; n < nbreCaractéristiques; n++)
                {
                    i = 1;
                    while (i < nbreLignes && Grille[0, 0] != caseVide && Grille[0, 0][n] == Grille[i, i][n])
                    {
                        i++;
                    }
                    if (i == nbreLignes)
                    {
                        gagner = true; // la diagonale décrite a été complétée avec 4 pièces ayant au moins 1 caractéristique commune 
                    }
                }
            }

            //vérif diago de la droite vers la gauche, du haut vers le bas
            if (!gagner)
            {
                for (n = 0; n < nbreCaractéristiques; n++)
                {
                    // Coordonnées (i, j) de la 1ere case que je compare
                    i = 1;
                    j = 2;
                    while (i < nbreLignes && j >= 0 && Grille[1, 3] != caseVide && Grille[1, 3][n] == Grille[i, j][n])
                    {
                        i++;
                        j--;
                    }
                    if (i == nbreLignes)
                    {
                        gagner = true; // la diagonale décrite a été complétée avec 4 pièces ayant au moins 1 caractéristique commune 
                    }
                }
            }
            return gagner;
        }

        /// <summary>
        /// AfficherVictoire : Affiche message de victoire si joueur humain gagne
        /// </summary>
        static void AfficherVictoire()
        {
            Console.ForegroundColor = ConsoleColor.Green;//affiche en vert si le joueur humain gagne
            Console.WriteLine("QUARTO! \nVous avez gagné, BRAVO !");
            Console.Beep(400, 100);//musique de victoire
            Console.Beep(550, 100);
            Console.Beep(450, 100);
            Console.Beep(600, 2000);
            Console.ResetColor();
        }

        /// <summary>
        /// AffichePerte : Affiche message si joueur humain perd
        /// </summary>
        static void AfficherPerte()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed; //affiche en rouge si le joueur humain perd
            Console.WriteLine(" QUARTO de l'ordinateur ! \nQuel dommage, votre adversaire a gagné... Ce sera pour une prochaine fois!");
            Console.Beep(500, 100);
            Console.Beep(400, 100);
            Console.Beep(350, 100);
            Console.Beep(300, 2000);//musique de défaite
            Console.ResetColor();
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
            while (i < nbreLignes && AvoirCaseRemplie(i, j))
            {
                while (j < nbreLignes && AvoirCaseRemplie(i, j))
                {
                    j++;
                }

                if (j == nbreLignes)
                {
                    j = 0;
                    i++;
                }
            }

            if (i == nbreLignes && j == nbreLignes)
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
        static bool ArreterPartie()
        {
            bool arret = false;
            Console.WriteLine("Voulez-vous arrêter la partie ? (o/n)");
            string stop = Console.ReadLine();
            if (stop == "n")
            {
                Console.WriteLine("On continue ...");
            }
            else
            {
                arret = true;
                Console.WriteLine("On s'arrête ...");
            }
            return arret;
        }

        /// <summary>
        /// ChoixIntell2 : l'ordi place la pièce qui lui est donnée autour d'une pièce qui a une caractéristique commune
        /// </summary>
        static void ChoixIntell2()
        {
            int i = 0;
            int j = 0;

            while (i < nbreLignes && !AvoirCaracCommuneIA(i, j) && !AvoirCaseJouableIA(i, j))
            {
                while (j < Grille.GetLength(1) && !AvoirCaracCommuneIA(i, j) && !AvoirCaseJouableIA(i, j) && Grille[i, j] == caseVide)
                {
                    j++;
                }

                if (!AvoirCaseJouableIA(i, j))
                    i++;
            }

            if (AvoirCaseJouableIA(i, j))
            {
                ligne = i;
                col = j;
                Grille[i, j] = ChoixPiece;
            }

            /*  else
              {
                  Random R = new Random();
                  // choisit aléatoirement la ligne et la colonne pour placer le pion
                  do
                  {
                      ligne = R.Next(0, nbreLignes);
                      col = R.Next(0, nbreLignes);
                  } while (AvoirCaseRemplie(ligne, col)); // tant que la case qu'il a choisi est remplie, l'ordi doit replacer sa pièce 

                  Grille[ligne, col] = ChoixPiece;
              }*/

            //AfficherGrille();

        }

        /// <summary>
        /// AvoirCaseJouableIA :  true s'il y a une case vide autour de la case de référence, false sinon. Permet de jouer autour de la case de référence.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        static bool AvoirCaseJouableIA(int i, int j)
        {
            bool caseJouable = false;

            // A Gauche
            if (j > 0 && !AvoirCaseRemplie(i, j - 1))
            {
                ligne = i;
                col = j - 1;
                caseJouable = true;
            }
            // A Droite
            else if (j < 3 && !AvoirCaseRemplie(i, j + 1))
            {
                ligne = i;
                col = j + 1;
                caseJouable = true;
            }
            // En Haut
            else if (j < 3 && !AvoirCaseRemplie(i, j + 1))
            {
                ligne = i;
                col = j + 1;
                caseJouable = true;
            }
            // En Bas
            else if (i > 0 && !AvoirCaseRemplie(i - 1, j))
            {
                ligne = i - 1;
                col = j;
                caseJouable = true;
            }
            //En haut
            else if (i > 0 && !AvoirCaseRemplie(i + 1, j))
            {
                ligne = i + 1;
                col = j;
                caseJouable = true;
            }
            //Diago Haut Droite
            else if (i > 0 && j < 3 && !AvoirCaseRemplie(i - 1, j + 1))
            {
                ligne = i - 1;
                col = j + 1;
                caseJouable = true;
            }
            //Diago Haut Gauche
            else if (i > 0 && j > 0 && !AvoirCaseRemplie(i - 1, j - 1))
            {
                ligne = i - 1;
                col = j - 1;
                caseJouable = true;
            }
            //Diago Bas Droite
            else if (i < 3 && j < 3 && !AvoirCaseRemplie(i + 1, j + 1))
            {
                ligne = i + 1;
                col = j + 1;
                caseJouable = true;
            }
            //Diago Bas Gauche
            else if (i < 3 && j > 0 && !AvoirCaseRemplie(i + 1, j - 1))
            {
                ligne = i + 1;
                col = j - 1;
                caseJouable = true;
            }

            return caseJouable;
        }

        /// <summary>
        /// AvoirCaracCommunIA : true si la pièce que l'IA doit jouer a au moins une caractéristique commune avec la 1ere pièce qu'il trouve sur le plateau de jeu
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        static bool AvoirCaracCommuneIA(int i, int j)
        {

            bool caracCommune = false;
            int n = 0;
            while (n < nbreCaractéristiques && Grille[i, j][n] != ChoixPiece[n])
            {
                n++;
            }

            if (n != 0 && n != 4)
                caracCommune = true;

            return caracCommune;
        }

        /// <summary>
        /// MettreAJourStratégies : l'IA calcule le nombre de pièces ayant 1 caractéristique commune sur une même ligne/col/diago
        /// </summary>
        /// <returns></returns>
        static void MettreAJourStrategies(int ligne, int col)
        {
            Console.WriteLine(ligne + "\t" + col + Grille [ligne, col]);
            for (int n = 0; n < nbreCaractéristiques; n++)
            {
                Console.WriteLine("case :" +Grille[ligne, col][n]);
                //Mise à jour lignes et colonnes
                if ( Grille[ligne,col][n] == '0') //compteur du nombre de 0 de la n ième caractéristique sur la ligne considérée
                {
                    tablignes0[ligne, n] += 1;
                    tabcol0[col, n] += 1;
                }
                else
                {
                    tablignes1[ligne, n] += 1; //compteur du nombre de 1 de la n ième caractéristique sur la ligne considérée
                     tabcol1[col, n] += 1;
                }
            }

           /* Console.WriteLine("lignes0");
            for (int i = 0; i < 4; i++) // affiche tabligne0
            {
                Console.WriteLine("");
                for (int j = 0; j < 4; j++)
                    Console.Write(tablignes0[i, j] + "\t");
            }

            Console.WriteLine("lignes1");
            for (int i = 0; i < 4; i++)  //affiche tabligne1
            {
                Console.WriteLine("");
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(tablignes1[i, j] + "\t");
                }
            }

            Console.WriteLine("col0");
            for (int i = 0; i < 4; i++) // afiche tabcol0
            {
                Console.WriteLine("");
                for (int j = 0; j < 4; j++)
                    Console.Write(tabcol0[i, j] + "\t");
            }

            Console.WriteLine("col1");
            for (int i = 0; i < 4; i++) // afiche tabcol1
            {
                Console.WriteLine("");
                for (int j = 0; j < 4; j++)
                    Console.Write(tabcol1[i, j] + "\t");
            }*/


        }

        static void ChoisirCaseIA()
        {
            // l'IA recherche où il y a déjà 3 pièces "alignées" et ayant 1 même caractéristique
            int n = 0;
            int i = 0;
            while (n < nbreCaractéristiques)
            {
                if (ChoixPiece[n] == '0')
                {
                    while (i < nbreLignes && tablignes0[i, n] != 3)
                    {
                        i++;
                    }
                }
                else
                {
                    while (i < nbreLignes && tablignes1[i, n] != 3)
                    {
                        i++;
                    }
                }

                if (tablignes0[i, n] == 3 || tablignes1[i, n] == 3)
                {
                    TrouverCaseIA(i);
                    Grille[i, j] = ChoixPiece;
                }
                else if ()
            }
        }

        /// <summary>
        /// TrouverCaseIA(): permet à l'IA de recherche où est la case vide sur cette ligne/col/diago où il y a déjà 3 pièces d'alignées
        /// </summary>
        static void TrouverCaseIA(int i)  // elle recherche où est la case vide sur cette ligne/col/diago et joue dedans
        {
            int j = 0;
            while (j < nbreLignes && AvoirCaseRemplie(i, j))
            {
                j++;
            }
            if ()
            
        }

    }
}


