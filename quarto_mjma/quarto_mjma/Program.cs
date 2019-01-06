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
        static int nbPiecesTotales = 16;
        // tableau des pièces avec deuxième ligne servant à indiquer ou non la présence de la pièce sur la grille de jeu
        static string[,] TabPieces = new string[,] { { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" },
                                                     { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" } };

        static int cursor = 9;
        static int longueurCase = 7; // nbre de lignes dans chaque case
        static int largeurCase = 16; //nbre de colonnes (carcatères) dans chaque case
       

        static string[,] Grille;    // Grille de jeu
        static string caseVide = "    ";
        static string ChoixPiece;
        static int ligne; static int col;
        static int nbreLignes = 4;
        static int nbreCaractéristiques = 4;

       static bool trace = false;

        static string blanc = "           ";
        static string[,] pieceVide =
           {
            { blanc},
            { blanc},
            { blanc},
            { blanc},
            { blanc},
            { blanc}
          };

        // Main
        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 40);
            AfficherEnTete();
            AfficherRegles();
            do
            {
                
                Grille = new string[nbreLignes, nbreLignes];

                InitialiserGrille();
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
            Console.WriteLine("                  ==============================================");
            Console.WriteLine("                                      QUARTO");
            Console.WriteLine("                  ==============================================\n");
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
               // Console.Clear();
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
            Console.Clear();
            AfficherTitre();
            if (trace)
            Console.ReadLine();
            AfficherGrille();
            AfficherPiecesRestantes();

            while (!Gagner() && !AvoirGrilleRemplie())
            {
         
                if (joueurCourant)  // joueur etre humain
                {
                    JouerJoueur();
                    //vérification si le joueur a gagné à chaque fin de tour
                    if (Gagner()) //cas s'il gagne
                        AfficherVictoire();
                }

                else   // joueur ordinateur
                {
                    JouerOrdi(); //si le joueur n'a pas gagné, l'ordinateur joue
                    //même vérification après chaque tour de jeu de l'ordinateur
                    if (Gagner())
                        AfficherPerte();
                }

                if (AvoirGrilleRemplie() && !Gagner()) // Cas où la grille est remplie mais personne ne gagne : c'est un match nul
                    Console.WriteLine("Match nul");

                Console.Clear();
                AfficherTitre();
                if (trace)
                Console.ReadLine();
                AfficherGrille();
                AfficherPiece();
                AfficherPiecesRestantes();
                //Console.ReadLine();

                joueurCourant = !joueurCourant;
            }
        }

        /// <summary>
        /// JouerOrdi : Fonction permettant à l'ordinateur de jouer son tour soit de placer une pièce choisie par le joueur
        /// </summary>
        static void JouerOrdi()
        {
            bool pieceUtilisee = false;
            //choix pièce par le joueur
            Console.SetCursorPosition(0, longueurCase * nbreLignes+7);
            Console.WriteLine("Que choisissez-vous comme pièce pour l'ordinateur?\n" +
                "- 0000 correspond à ronde, petite, creuse, rouge\n" +
                "- 1111 correspond à carrée, grande, pleine, bleue \n" +
                "vous pouvez mixer plusieurs caractères évidemment.");

            do
            {
                //Console.WriteLine("Pièce déjà utilisée, choisissez-en une autre");
                ChoixPiece = Console.ReadLine();//on récupère la pièce que le joueur choisi pour l'ordi

                if (ChoixPiece.Length != nbreCaractéristiques)
                {
                    Console.Beep(500, 300);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Erreur : une pièce doit avoir 4 caractères. Veuillez entrer un nom de pièce valide :");
                    Console.ResetColor();
                }
                else 
                {
                    pieceUtilisee = VerifierSiPieceUtilisee(ChoixPiece);

                    if (pieceUtilisee)
                    {
                        Console.Beep(500, 300);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Erreur : Pièce déjà utilisée, veuillez en choisir une autre :");
                        Console.ResetColor();
                    }
                }

            } while ( ChoixPiece.Length != nbreCaractéristiques || pieceUtilisee); //tant que la pièce n'est pas bonne on en rechoisit une autre

           

            JouerPiece(ChoixPiece);

            //choix case par l'ordi

            Random R = new Random();
            // choisit aléatoirement la ligne et la colonne pour placer le pion
            do
            {
                ligne = R.Next(0, nbreLignes);
                col = R.Next(0, nbreLignes);
            } while (AvoirCaseRemplie(ligne, col)); // tant que la case qu'il a choisi est remplie, l'ordi doit replacer sa pièce 

            Grille[ligne, col] = ChoixPiece;
        
           // AfficherGrille();

            // ChoixIntell2();

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
            Console.SetCursorPosition(0, longueurCase * nbreLignes + 7);
            Console.WriteLine("L'ordinateur a choisi la pièce {0} pour vous\n" +
                "- 0000 correspond à petite, creuse, carrée, clair\n" +
                "- 1111 correspond à grande, pleine, ronde, foncee \n" +
                "Les caractères peuvent être mélangés entre eux", ChoixPiece);
            // améliorer notre présentation des pièces  Console.WriteLine("le 1er caractère correspond à [1]= ronde [0]=carrée, 2ème caractère [1]=creuse [0]=vide");

            bool caseRemplie = false;
            //choix de la case par le joueur
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

                caseRemplie = AvoirCaseRemplie(ligne, col);
                if (caseRemplie)
                {
                    Console.WriteLine("\nErreur : case déjà remplie, veuillez en choisir une autre :");
                }
            } while (caseRemplie); //tant que la case choisie est remplie, le joueur doit choisir une autre case. Préalablement, les conditions sur les lignes et les colonnes ont été vérifées pour ne pas tomber sur une case hors tableau.

            Grille[ligne, col] = ChoixPiece;
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
                if (i<nbPiecesTotales-1)
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

                Console.WriteLine("      +---------------+---------------+---------------+---------------+");
                Console.WriteLine("      |               |               |               |               |");
                Console.WriteLine("      |               |               |               |               |");
                Console.WriteLine("      |               |               |               |               |");
                Console.WriteLine("      |               |               |               |               |");
                Console.WriteLine("      |               |               |               |               |");
                Console.WriteLine("      |               |               |               |               |");
               // Console.WriteLine("      |               |               |               |               |");
                // Console.WriteLine("{0}   ", i);
                // Console.Write("                    |");

                //Console.Write("\n");// sauter une ligne pour mettre la barre entre chaque case
            }

                Console.WriteLine("      +---------------+---------------+---------------+---------------+");
                Console.WriteLine("              0              1              2                3         ");

            //permet d'afficher les pièces restantes à la droite de la grille
            
            Console.SetCursorPosition(75, 4);
            Console.WriteLine("Pièces restantes:");
                

            
              
               /* for (int n = 0; n < nbPiecesTotales; n++)
                {
                
                    Console.WriteLine("{0}",n);
                    if (TabPieces[1, n] == "0"&& n<5)

                    {
                        Console.SetCursorPosition(75, longueurCase* n);
                        string[,] dessin = TrouverDessin(TabPieces[0, n]);
                        for(int k = 0; k < longueurCase - 1; k++)
                        { Console.WriteLine("{0}", dessin[k, 0]);}
                        
                        Console.SetCursorPosition(75, longueurCase * (n+1));
                        Console.ResetColor();
                        Console.WriteLine("Pièce {0}", TabPieces[0, n]);
                    }

                if (TabPieces[1, n] == "0" && 5< n && n< 10)
                {
                    Console.SetCursorPosition(82, longueurCase * n);
                    string[,] dessin = TrouverDessin(TabPieces[0, n]);
                    for (int k = 0; k < longueurCase - 1; k++)
                    { Console.WriteLine("{0}", dessin[k, 0]); }

                    Console.SetCursorPosition(82, longueurCase * (n + 1));
                    Console.ResetColor();
                    Console.WriteLine("Pièce {0}", TabPieces[0, n]);
                }
                if (TabPieces[1, n] == "0" && 10 < n && n < nbPiecesTotales)
                {
                    Console.SetCursorPosition(89, longueurCase * n);
                    string[,] dessin = TrouverDessin(TabPieces[0, n]);
                    for (int k = 0; k < longueurCase - 1; k++)
                    { Console.WriteLine("{0}", dessin[k, 0]); }

                    Console.SetCursorPosition(89, longueurCase * (n + 1));
                    Console.ResetColor();
                    Console.WriteLine("Pièce {0}", TabPieces[0, n]);
                }
            }*/
                
                        //affiche pièces restantes
                  
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
        /// ChoixIntell2 : l"ordi place la pièce qui lui est donnée autour d'une pièce qui a une caractéristique commune
        /// </summary>
        static void ChoixIntell2()
        {
            int i = 0;
            int j = 0;

            while (i < Grille.GetLength(1) && !AvoirCaracCommuneIA(i, j) && !AvoirCaseJouableIA(i, j))
            {
                while (j < Grille.GetLength(1) && !AvoirCaracCommuneIA(i, j) && !AvoirCaseJouableIA(i, j) && Grille[i, j] == caseVide)
                {

                    j++;
                }

                if (!AvoirCaseJouableIA(i, j))
                    i++;
            }

            if (AvoirCaseJouableIA(i, j))
                Grille[ligne, col] = ChoixPiece;
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

        static void AfficherPiece()
        {
            string[,] dessin;

            int i = 0;
            int j = 0;

            for (i = 0; i < nbreLignes; i++)
            {
                for (j = 0; j < nbreLignes; j++)
                {
                   // Console.WriteLine("début for parcours de la grille avant trouverdessin, i={0}, j={1}", i, j);
                  dessin = TrouverDessin(Grille[i, j]);
                  //  Console.WriteLine("début for parcours de la grille après trouverdessin, i={0}, j={1}", i, j);

                    for (int k = 0; k < longueurCase-1; k++)
                    {//là où se trouve le curseur + largeur de la case *(le nombre de colonne+1) (déplace vers la droite)
                        //ligne=i, 5 (la grille est à 5 du haut de l'écran) + longueur de la case*(nbre ligne+1) + k (?)
                        Console.SetCursorPosition (cursor+ largeurCase *j , 5+ longueurCase*i + k);
                        Console.WriteLine(dessin[k, 0]);
                    }
                    Console.ResetColor();
                }


            }
        }

        static void AfficherPiecesRestantes()
        {
            int i = 0; int j = 0; int m = 0;
            string[,] dessin;

            while (i < 4) //i= nombre de lignes
            {
                while (j < 4 && m < 16)//j= nbre de colonnes
                {
                   // if (TabPieces[1, m] == "0")
                    //{
                        // Console.WriteLine("début for parcours de la grille avant trouverdessin, i={0}, j={1}", i, j);
                        dessin = TrouverDessin(TabPieces[0, m]);
                        //  Console.WriteLine("début for parcours de la grille après trouverdessin, i={0}, j={1}", i, j);

                        for (int k = 0; k < 8; k++)//+une case à chaque fois
                        {//là où se trouve le curseur + largeur de la case *(le nombre de colonne+1) (déplace vers la droite)
                         //ligne=i, 5 (la grille est à 5 du haut de l'écran) + longueur de la case*(nbre ligne+1) + k (?)

                            Console.SetCursorPosition(75 + largeurCase * j, 5 + 7 * i + k);

                        if (k < 6)
                        {
                            if (TabPieces[1, m] == "0")
                                Console.WriteLine(dessin[k, 0]);
                        }
                       
                        if (k == 7)
                            {
                            if (TabPieces[1, m] == "0")
                            { 
                                Console.ResetColor();
                                //Console.WriteLine("m={0}, Tab[0,m]={1}", m, TabPieces[0, m]);
                                //  Console.WriteLine();
                                Console.WriteLine("{0}", TabPieces[0, m]);}
                                //  Console.WriteLine();

                                m++;
                            }
                        }
                  /*  }
                    else if T
                   {
                        for (int k = 0; k < 6; k++)
                        {//là où se trouve le curseur + largeur de la case *(le nombre de colonne+1) (déplace vers la droite)
                         //ligne=i, 5 (la grille est à 5 du haut de l'écran) + longueur de la case*(nbre ligne+1) + k (?)

                            Console.SetCursorPosition(75 + largeurCase * j, 5 + 7 * i + k);
                            if (k < 6)
                                Console.WriteLine( pieceVide[0,k]);
                        }*/
                        j++;
                        if (j == 4 || j == 8 || j == 12)
                        {
                            i++;
                            j = 0;
                        }
                    }
                }
                Console.ResetColor();
            }

        static string [,] TrouverDessin(string piece) // Trouver le dessin qui correspond à la pièce voulue
        {

         string largeurGrandCarre = "*         *";
         string largeurGrandCarrePlein = "***********";
         //int hauteurGrandCarre = 6;
         string largeurPetitCarre = "  *     *  ";
         string largeurPetitCarrePlein = "  *******  ";
        // int hauteurPetitCarre = 4;
   

         string[,] pieceCarreePetite =
        {
        { blanc},
        { largeurPetitCarrePlein },
        { largeurPetitCarre },
        { largeurPetitCarre },
        { largeurPetitCarrePlein },
         { blanc},
        };

         string[,] pieceCarrePetitePleine =
        {
        { blanc},
        { largeurPetitCarrePlein},
        { largeurPetitCarrePlein},
        { largeurPetitCarrePlein},
        { largeurPetitCarrePlein},
        { blanc},
        };

         string[,] pieceCarreeGrande =
        {
        { largeurGrandCarrePlein },
        { largeurGrandCarre },
        { largeurGrandCarre },
        { largeurGrandCarre },
        { largeurGrandCarre },
        { largeurGrandCarrePlein },
        };

        string[,] pieceCarreeGrandePleine =
        {
        { largeurGrandCarrePlein },
        { largeurGrandCarrePlein },
        { largeurGrandCarrePlein },
        { largeurGrandCarrePlein },
        { largeurGrandCarrePlein },
        { largeurGrandCarrePlein },
        };

        string[,] pieceRondePetite = 
        {
        { blanc},
        {"    * *    "},
        {"  *     *  "},
        {"  *     *  "},
        {"    * *    "},
        { blanc}
         };


            string[,] pieceRondePetitePleine =
         {
         { blanc},
         {"    ***    "},
         {"  *******  "},
         {"  *******  "},
         {"    ***    "},
          { blanc}
        };

            string[,] pieceRondeGrande =
            {
           { "    * *    "},
           { " *       * "},
           { "*         *"},
           { "*         *"},
           { " *       * "},
           { "    * *    "}
        };

            string[,] pieceRondeGrandePleine =
            {
           {"   ****   "},
           {" ********* "},
           {"***********"},
           {"***********"},
           {" ********* "},
           {"   ****    "}
        };

        

        string[,] dessin = new string [6,1];

            // 0000 = ronde, petite creuse, rouge
            if (piece[0] == ' ')
            {
                dessin = pieceVide;
            }
            else
            {
                //identifie d'abord la couleur
                if (piece[3] == '0') 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                //identifie la forme
                if (piece[0] == '0') // pièce ronde
                {
                    //identifie la grandeur
                    if (piece[1] == '0') //petite
                    {
                        dessin = pieceRondePetite;

                        //identifie le caractère creux/plein
                        if (piece[2] == '1')
                        {
                            dessin = pieceRondePetitePleine;
                        }
                    }
                    else //grande
                    {
                        dessin = pieceRondeGrande;

                        if (piece[2] == '1')
                        {
                            dessin = pieceRondeGrandePleine;
                        }
                    }
                }
                else if (piece[0] == '1') // piece carrée
                {
                    if (piece[1] == '0') //petite
                    {
                        dessin = pieceCarreePetite;

                        if (piece[2] == '1')
                        {
                            dessin = pieceCarrePetitePleine;
                        }

                    }
                    else //grande
                    {
                        dessin = pieceCarreeGrande;

                        if (piece[2] == '1')
                        {
                            dessin = pieceCarreeGrandePleine;
                        }
                    }
                }
            }
           // Console.ResetColor();
            return dessin;
        }

       
    }     
}


