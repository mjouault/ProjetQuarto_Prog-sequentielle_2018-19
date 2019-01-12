using System;

namespace quarto_mjma
{
    class Program
    {
        // --------Variables globales------

         // variables relatives aux caractéristiques du jeu
        static int nbPiecesTotales = 16;
        static int nbreLignes = 4; // constante donnant le nombre de lignes et de colonnes puisque le plateau est carré
        static int nbreCaractéristiques = 4; // constante donnant le nombre de caractéristiques des pièces


        //variables relatives à l'affichage
        static int largeurCase = 16;
        static int longueurCase = 7;
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
        static string[,] dessinPiece = new string [6,1]; //visuel d'une pièce : tableau de 6 lignes composées chacune de 11 caractères

        // varibales mises à jour durant le jeu 
        static string[,] TabPieces; //tableau recensant le nom ds pièces (ligne 0) et leur indice de présence (0 : absent) dans la grille de jeu (ligne1)
        static string[,] Grille;    // Grille de jeu
        static string caseVide = "    ";
        static int ligne; static int col; //lignes et colonnes que le joueur/l'ordi choisit pour placer la pièce donnée
        static string choixPiece; // désigne une pièce choisie pour être jouée par l'un des joueurs



        // tableux de sommes pour que l'IA mène sa stratégie
        static int[,] tabLignes0 = new int[4, 4]; //tableau sommant, pour chaque caractéristique, le nombre de 0 sur chaque ligne 
        static int[,] tabLignes1 = new int[4, 4]; //tableau sommant, pour chaque caractéristique, le nombre de 1 sur chaque ligne 
        static int[,] tabCol0 = new int[4, 4]; //tableau sommant, pour chaque caractéristique, le nombre de 0 sur chaque colonne
        static int[,] tabCol1 = new int[4, 4];  //tableau sommant, pour chaque caractéristique, le nombre de 1 sur chaque colonne
        static int[,] tabDiago0 = new int[4, 4];   //tableau sommant, pour chaque caractéristique, le nombre de 0 sur chaque diagonale
        static int[,] tabDiago1 = new int[4, 4];  //tableau sommant, pour chaque caractéristique, le nombre de 1 sur chaque diagonale


        //booléen utilisés durant tout le jeu
        static bool AGagne = false; //  true si un joueur a gagné, false sinon (lorsqu'elle est appelée dans les fonctions relative à l'IA, elle détermine si l'IA gagne ou non en plaçant ne pièce)
        static bool grilleRemplie = false;
        static bool modeIntell = false; // true : mode intelligent activé / false : mode noviced de l'ordinateur

       


        //----------------------------------- Main----------------------------------------------
        static void Main(string[] args)
        {
            Console.SetWindowSize(150, 40);

            AfficherEnTete();
            AfficherRegles();
            ChoisirMode();
            do
            {
               Grille = new string[nbreLignes, nbreLignes];
                
                InitialiserGrille();
                InitialiserPieces();
                InitialiserStrategiesIA();
                AGagne = false;
                Jouer();
            } while (RejouerPartie());
        }

        //-----------------------------Fonctions de démarrage du jeu-----------------------------

        /// <summary>
        /// AfficherEnTete : Affiche l'en-tête
        /// </summary>
        static void AfficherEnTete()
        {
            string pseudo;

            Console.Title = "Jeu de Quarto"; //la fenêtre d'exécution s'appellera jeu de quarto 
            AfficherTitre();

            Console.Write("C'est donc toi le nouveau joueur qui souhaite affronter la machine toute puissante au QUARTO !\nQuel est ton petit nom ? ");
            pseudo = Console.ReadLine();
            Console.WriteLine("Sympa comme pseudo!\nAvant de commencer {0}, veux-tu que je te rappelle les règles du jeu? [o]/[n]", pseudo);

        }

        /// <summary>
        /// AfficherTitre : affichage du titre durant toute la partie de jeu
        /// </summary>
         static void AfficherTitre()
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
                afficherRegles = Console.ReadLine(); // le joueur human choisit si oui ou non il veut qu'on lui rappelle les règles du jeu
                Console.Clear();
                if (afficherRegles != "o" && afficherRegles != "n") // message d'erreur si l'humain ne répond pas par "" ou "n"
                {
                    Console.Beep(400, 300);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Il faut répondre par [o] ou [n] on a dit !");
                    Console.ResetColor();
                }
            } while (afficherRegles != "o" && afficherRegles != "n");

            if (afficherRegles == "o") // s'il répond oui, affichage des règles
            {
                Console.WriteLine("\nSage décision, un petit rappel ne fait jamais de mal !\n==============================================");
                Console.WriteLine("            REGLES DU JEU");
                Console.WriteLine("==============================================\n");

                Console.Write("\nBUT DU JEU : \nCréer sur le plateau un alignement de 4 pièces ayant au moins un caractère commun. Cet alignement peut-être horizontal, vertical ou diagonal." +
                    " \n\nDÉROULEMENT D’UNE PARTIE : \nLe premier joueur est tiré au sort.\nIl choisit une des 16 pièces et la donne à son adversaire." +
                    "\nCelui - ci doit la placer sur une des cases du plateau et choisir ensuite une des 15 pièces restantes pour la donner à son adversaire." +
                    "\nA son tour, celui-ci la place sur une case libre et ainsi de suite…." +
                    "\n\nGAIN DE LA PARTIE : \nLa partie est gagnée par le premier joueur qui annonce “QUARTO !”" +
                    "\nUn joueur fait “QUARTO !” et gagne la partie lorsque, en plaçant la pièce donnée, il aligne 4 pièces ayant au moins un caractère en commun." +
                    "\nPlusieurs caractères peuvent se cumuler.\nDe plus, il n’est pas obligé d’avoir lui même déposé les trois autres pièces." +
                    "\nIl y a égalité: toutes les pièces ont été posées sans vainqueur.");
            }
        }


            /// <summary>
            /// ChoisirMode : l'humain choisit le niveau de l'ordinateur : débutant (en tapant 1) ou intelligent (en tapant 2)
            /// </summary>
            /// <returns></returns>
        static void ChoisirMode()
        {
            int choix;
            Console.WriteLine("\n\nTu peux maintenant choisir le niveau de l'ordinateur!\n[1]: Ordinateur débutant (jeu aléatoire)\n[2]: Ordinateur intelligent");
            choix = int.Parse(Console.ReadLine());
            while (choix != 1 && choix != 2) // message d'erreur s'il ne répond pas par "1" ou "2"
            {
                Console.WriteLine("Erreur de saisie, il faut répondre par 1 ou par 2:");
                choix = int.Parse(Console.ReadLine());
            }
            if (choix == 2)
                modeIntell = true;
        }

        /// <summary>
        /// InitialiserGrille : met le contenu caseVide dans toutes les cases de la grille avant chaque début de partie
        /// </summary>
        static void InitialiserGrille()
        {

            for (int i = 0; i < nbreLignes; i++) //indice ligne
            {
                for (int j = 0; j < nbreLignes; j++) // i = indice colonne
                {
                    Grille[i, j] = caseVide; // met le contenu caseVide ("   ") dans toutes les cases du tableau
                }
            }
        }

        /// <summary>
        /// InitialiserPieces : remise à 0 des indices de présence dans la grille de jeu des 16 pièces
        /// </summary>
        static void InitialiserPieces()
        {
            TabPieces = new string[,] { { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111",     //la 1ere ligne du tableau recense le nom des pièces du jeu
                                          "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" },
                                        { "0", "0", "0", "0", "0", "0", "0", "0",                             // la 2eme ligne recense l'indice de présence de chaque pièce dans la grille (1= présence, 0 sinon)
                                          "0", "0", "0", "0", "0", "0", "0", "0" } };
        }

        /// <summary>
        /// InitialiserStrategiesIA : réinitialise les tableaux de sommes dont l'IA se sert pour jouer intelligemment
        /// </summary>
        static void InitialiserStrategiesIA()
        {
            Array.Clear(tabLignes0, 0, tabLignes0.Length);
            Array.Clear(tabLignes1, 0, tabLignes1.Length);
            Array.Clear(tabCol0, 0, tabCol0.Length);
            Array.Clear(tabCol1, 0, tabCol1.Length);
            Array.Clear(tabDiago0, 0, tabDiago0.Length);
            Array.Clear(tabDiago1, 0, tabDiago1.Length);
        }


        /// <summary>
        /// choisir1erJoueur : désigne aléatoirement qui du joueur ou de l'ordi commence à jouer (si 1 est tiré, l'humain commance, si le 0 est tiré, l'est l'ordi).
        /// </summary>
        /// <returns></returns>
        static bool choisir1erJoueur()
        {
            bool estHumain = false;

            Random R = new Random();
            int choix1er = R.Next(0, 2); ;

            if (choix1er == 1)
                estHumain = true;

            return estHumain;
        }


        //---------------------------Fonctions d'affichage du jeu----------------------------

        /// <summary>
        /// AfficherGrille : affiche la grille avec le dessin des pièces à chaque tour de jeu 
        /// </summary>
        static void AfficherGrille()
        {
            for (int i = 0; i < nbreLignes; i++) //indice ligne
            {

                Console.WriteLine("      +---------------+---------------+---------------+---------------+");
                Console.WriteLine("      |               |               |               |               |");
                Console.WriteLine("      |               |               |               |               |");
                Console.WriteLine("      |               |               |               |               |");
              Console.WriteLine(i+"     |               |               |               |               |");
                Console.WriteLine("      |               |               |               |               |");
                Console.WriteLine("      |               |               |               |               |");
            }

            Console.WriteLine("      +---------------+---------------+---------------+---------------+");
            Console.WriteLine("              0              1              2                3         ");



            Console.SetCursorPosition(75, 4);
            Console.WriteLine("Pièces restantes:");

        }

        /// <summary>
        /// TrouverDessinPiece : permet d'associer au nom de pièce textuel, le visuel correspondant
        /// </summary>
        /// <param name="piece"></param>
        /// <returns></returns>
        static string[,] TrouverDessinPiece(string piece) // Trouver le dessin qui correspond à la pièce voulue
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


            // 0000 = ronde, petite creuse, rouge
            if (piece[0] == ' ')
            {
                dessinPiece = pieceVide;
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
                        dessinPiece = pieceRondePetite;

                        //identifie le caractère creux/plein
                        if (piece[2] == '1')
                        {
                            dessinPiece = pieceRondePetitePleine;
                        }
                    }
                    else //grande
                    {
                        dessinPiece = pieceRondeGrande;

                        if (piece[2] == '1')
                        {
                            dessinPiece = pieceRondeGrandePleine;
                        }
                    }
                }
                else if (piece[0] == '1') // piece carrée
                {
                    if (piece[1] == '0') //petite
                    {
                        dessinPiece = pieceCarreePetite;

                        if (piece[2] == '1')
                        {
                            dessinPiece = pieceCarrePetitePleine;
                        }

                    }
                    else //grande
                    {
                        dessinPiece = pieceCarreeGrande;

                        if (piece[2] == '1')
                        {
                            dessinPiece = pieceCarreeGrandePleine;
                        }
                    }
                }
            }
            // Console.ResetColor();
            return dessinPiece;
        }

        static void AfficherPiece()
        {
            int i = 0;
            int j = 0;

            for (i = 0; i < nbreLignes; i++)
            {
                for (j = 0; j < nbreLignes; j++)
                {
                    // Console.WriteLine("début for parcours de la grille avant trouverdessin, i={0}, j={1}", i, j);
                    dessinPiece = TrouverDessinPiece(Grille[i, j]);
                    //  Console.WriteLine("début for parcours de la grille après trouverdessin, i={0}, j={1}", i, j);

                    for (int k = 0; k < longueurCase - 1; k++)
                    {//là où se trouve le curseur + largeur de la case *(le nombre de colonne+1) (déplace vers la droite)
                        //ligne=i, 5 (la grille est à 5 du haut de l'écran) + longueur de la case*(nbre ligne+1) + k (?)
                        Console.SetCursorPosition(9 + largeurCase * j, 5 + longueurCase * i + k);
                        Console.WriteLine(dessinPiece[k, 0]);
                    }
                    Console.ResetColor();
                }


            }
        }

        /// <summary>
        /// AfficherPiecesRestantes : Affiche les pièces disponibles, réactualisées à chaque tour
        /// </summary>
        static void AfficherPiecesRestantes()
        {
            int i = 0; int j = 0; int m = 0;

            while (i < 4) //i= nombre de lignes
            {
                while (j < 4 && m < 16)//j= nbre de colonnes
                {
                    // if (TabPieces[1, m] == "0")
                    //{
                    // Console.WriteLine("début for parcours de la grille avant trouverdessin, i={0}, j={1}", i, j);
                    dessinPiece = TrouverDessinPiece(TabPieces[0, m]);
                    //  Console.WriteLine("début for parcours de la grille après trouverdessin, i={0}, j={1}", i, j);

                    for (int k = 0; k < 8; k++)//+une case à chaque fois
                    {//là où se trouve le curseur + largeur de la case *(le nombre de colonne+1) (déplace vers la droite)
                     //ligne=i, 5 (la grille est à 5 du haut de l'écran) + longueur de la case*(nbre ligne+1) + k (?)

                        Console.SetCursorPosition(75 + largeurCase * j, 5 + 9 * i + k);

                        if (k < 6)
                        {
                            if (TabPieces[1, m] == "0")
                                Console.WriteLine(dessinPiece[k, 0]);
                        }

                        if (k == 7)
                        {
                            if (TabPieces[1, m] == "0")
                            {
                                Console.ResetColor();
                                //Console.WriteLine("m={0}, Tab[0,m]={1}", m, TabPieces[0, m]);
                                //  Console.WriteLine();
                                Console.WriteLine("{0}", TabPieces[0, m]);
                            }
                            //  Console.WriteLine();

                            m++;
                        }
                    }
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

       

        //----------Fonctions permettant de jouer------------
       
        /// <summary>
        /// Jouer() : Fait en sorte que le joueur et l'ordinateur jouent chacun leur tour
        /// </summary>
        static void Jouer()
        {
            bool joueurCourantHumain = choisir1erJoueur(); // appel de la fonction booléenne choisir1erJoueur pour déterminer l'ordre d'alternance
            Console.Clear();
            AfficherTitre();
            AfficherGrille();
            AfficherPiecesRestantes();

            while (!AGagne && !grilleRemplie)
            {
                if (joueurCourantHumain)  // joueur être humain commence
                {
                    JouerHumain();
                    Gagner();  //vérification si le joueur a gagné à chaque fin de tour
                    if (AGagne) //cas s'il gagne
                        AfficherVictoireHumain();
                }

                else  //si le joueur n'a pas gagné
                {
                    JouerOrdi(); // l'ordinateur joue
                    //même vérification après chaque tour de jeu de l'ordinateur
                    Gagner();
                    if (AGagne) // cas si l'ordi gagne
                        AfficherPerteHumain();
                }

                if (!AGagne) // Cas où la grille est remplie mais personne n'a gagné : c'est un match nul
                {
                    AvoirGrilleRemplie();
                    if (grilleRemplie)
                        Console.WriteLine("Match nul");
                }

                Console.Clear();
                AfficherTitre();
                AfficherGrille();
                AfficherPiece();
                AfficherPiecesRestantes();
                joueurCourantHumain = !joueurCourantHumain; // le joueur courant n'est devient l'autre joueur
            }
        }

        /// <summary>
        /// UtiliserPiece : Fonction permettant de ne jouer qu'une seule fois chaque pièce
        /// </summary>
        /// <param name="choixPiece"></param>
        static void UtiliserPiece()
        {
            int i = 0;

            // Recherche de l'indice de colonne de la pièce "choixPiece" dans le tableau recensant les pièces
            while (i < nbPiecesTotales && choixPiece != TabPieces[0, i])
            {
                i++;
            }

            // une fois cet indice trouvé, l'indice de présence de la pièce passe à 1
            TabPieces[1, i] = "1";
        }

        /// <summary>
        /// verifierSiPieceUtilisee : True si la pièce n'a pas été joué, False sinon
        /// </summary>
        /// <returns></returns>
        static bool VerifierSiPieceUtilisee() //vérifier si la pièce a été utilisée (true) ou non (false)
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
        /// JouerHumain : Fonciton permettant au joueur de jouer son tour soit de placer une pièce choisie par l'ordinateur
        /// </summary>
        static void JouerHumain()
        {
            //L'ordinateur choisit d'abord la pièce qu'il va donner à l'humain

            if (!modeIntell) // si mode débutant
            {
                int randomPiece;
                Random R = new Random();
                do
                {
                    randomPiece = R.Next(0, 16);
                    choixPiece = TabPieces[0, randomPiece]; // l'ordi choisit aléatoirement parmi les pièces disponibles
                }
                while (TabPieces[1, randomPiece] == "1"); //Demander à l'ordi de choisir de nouveau la pièce s'il en a choisi une déjà jouée*/
            }
            else // si mode intelligent
            {
                ChoisirPieceIA();
            }

            UtiliserPiece(); // l'indice de présence de la pièce choisie passe de 0 à 1

            Console.SetCursorPosition(0, longueurCase * nbreLignes + 7);
            Console.WriteLine("L'ordinateur a choisi cette pièce pour vous : ");
            dessinPiece = TrouverDessinPiece(choixPiece);
            for (int k = 0; k < 6; k++)
            {
                Console.SetCursorPosition(45, longueurCase * nbreLignes + 8 + k);
                Console.WriteLine(dessinPiece[k, 0]);

            }
            Console.ResetColor();


            //choix de la case par le joueur

            bool caseRemplie = false;
            do
            {
                do // l'humain saisit de nouveau une ligne tant que la ligne n'est pas comprise entre 0 et 3
                {
                    Console.WriteLine("\nChoisir une ligne (entre 0 et 3) ");
                    ligne = int.Parse(Console.ReadLine());
                    if (ligne < 0 || ligne > 3)
                    {
                        Console.WriteLine("\nErreur  : Entre 0 et 3 on a dit !");
                    }

                } while (ligne < 0 || ligne > 3); 

                do //de^même pour la saisie de la colonne
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
                    Console.WriteLine("\nErreur, case déjà remplie, veuillez en choisir une autre :"); //message d'erreur si case déjà remplie
                }
             

            } while (caseRemplie); //tant que la case choisie est remplie, le joueur doit choisir une autre case. 
                                   //Préalablement, les conditions sur les lignes et les colonnes ont été vérifées pour ne pas tomber sur une case hors tableau.

            Grille[ligne, col] = choixPiece; // la grille est actualisée
            MettreAJourStrategies(false, 0); // Les tableaux concernés par l'ajout de cette pièce dans la grille sont aussi actualisés
        }

        /// <summary>
        /// JouerOrdi : Fonction permettant à l'ordinateur de jouer son tour soit de placer une pièce choisie par le joueur
        /// </summary>
        static void JouerOrdi()
        {
            bool pieceUtilisee = false;

            //choix pièce par le joueur
            Console.SetCursorPosition(0, longueurCase * nbreLignes + 7);
            Console.WriteLine("Que choisissez-vous comme pièce pour l'ordinateur?\n" +
                "(Indiquez le nom de la pièce)");
            do
            {
                choixPiece = Console.ReadLine(); //on récupère la pièce que le joueur choisi pour l'ordi

                if (choixPiece.Length != nbreCaractéristiques) // message d'erreur si la pièce saisie ne comporte pas 4 caractéristiques (l'humain peut avoir coulu rentrer un numéro de ligne par mégarde)
                {
                    Console.Beep(500, 300);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Erreur, une pièce doit avoir 4 caractères. Veuillez entrer un nom de pièce valide :"); 
                    Console.ResetColor();
                }
                else // si la pièce est correcte
                {
                    pieceUtilisee = VerifierSiPieceUtilisee(); 

                    if (pieceUtilisee) // message d'erreur si pièce utilisée
                    {
                        Console.Beep(500, 300);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Erreur, Pièce déjà utilisée, veuillez en choisir une autre :");
                        Console.ResetColor();
                    }
                }
            } while (pieceUtilisee || choixPiece.Length != nbreCaractéristiques ); //tant que la pièce n'a pas été saisie correctement ou qu'elle est déjà utilisée, l'huamin en choisit une autre

            UtiliserPiece(); // l'indice de présence de la pièce passe à 1

            if (!modeIntell) // en mode débutant
            {
                Random R = new Random();
                // choisit aléatoirement la ligne et la colonne pour placer le pion
                do
                {
                    ligne = R.Next(0, nbreLignes);
                    col = R.Next(0, nbreLignes);
                } while (AvoirCaseRemplie(ligne, col)); // tant que la case qu'il a choisi est remplie, l'ordi doit replacer sa pièce 

                Grille[ligne, col] = choixPiece;
            }
            else // en mode intelligent
            {
                GagnerIA(); // l'IA vérifie si elle peut gagner en plaçant la pièce donnée
                if (!AGagne) // si elle ne peut pas
                {
                    ChoisirCaseIA(); // elle cherche une case telle que le placement de la pièce dans cette case ne permet pas à l'humain de gagner ensuite
                }
            }
                    MettreAJourStrategies(false, 0); // mise à jour des tableaux de sommes correspondant
        }


        // --------------------sous- fonctions relatives à l'intelligence artificielle------------------------------ 

        /// <summary>
        /// MettreAJourStrategies : l'IA calcule après chaque placement de pièce le nombre de pièces ayant 1 caractéristique commune sur une même ligne/col/diago
        /// </summary>
        /// <returns></returns>
        static bool MettreAJourStrategies(bool simul, int nbPiecesAlignees)  /* La fonction MettreAJourStrategies est appelée :
                                                                                - A chaque fin de tour pour que l'IA recalcule ses stratégies
                                                                               -  Dans TrouverCase afin de s'assurer qu'après simulation du placement de la pièce (d'où vient notre bool simul),
                                                                                il n'y aurait pas 3 pièces alignées afin de ne pas permettre au joueur de gagner au tour suivant.
                                                                               - Dans TrouverPiece afin de s'assurer qu'après simulation du placement de la pièce dans la grille,(d'où vient nbPiecesAlignees) 
                                                                                l'humain ne pourrait pas former un alignement de 4caractéristiques avec cette pièce*/

        {
            bool alignementPieces = false;


            for (int n = 0; n < nbreCaractéristiques; n++) //parcours chaque caractéristique
            {
                if (choixPiece[n] == '0') //compteur du nombre de 0 de la n ième caractéristique sur la ligne/colonne/diagonale considérée
                {
                    if (simul) // On simule le placement d'une pièce dans la grille sans que les tableaux de sommes soient mis à jour
                    {

                        if (tabLignes0[ligne, n] == nbPiecesAlignees || tabCol0[col, n] == nbPiecesAlignees) // cherche un certain nombre de pièces alignées (2 ou 3) dans les tableaux de sommes des lignes et des colonnes concernant les caractéristiques codée par "0"
                        {
                            alignementPieces = true;
                        }
                        else if (ligne == col && tabDiago0[0, n] == nbPiecesAlignees) // idem pour le tableau de sommes de la diagonale de la gauche vers la droite, du haut vers le bas (il s'agit de la première ligne du tableau tabDiago0 d'où  tabDiago[0,n])
                        {
                            alignementPieces = true;
                        }
                        else if (ligne == ((nbreLignes - 1) - col) && tabDiago0[1, n] == nbPiecesAlignees || col == ((nbreLignes - 1) - ligne) && tabDiago0[1, n] == nbPiecesAlignees) // idem pour le tableau de sommes de la diagonale de la droite vers la gauche, du haut vers le bas (il s'agit de la première ligne du tableau tabDiago0 d'où  tabDiago[1,n])
                        {
                            alignementPieces = true;
                        }
                    }
                    else // Une pièce vient d'être placée dans la grille de jeu, les tableaux de sommes sont actualisés (incrémentation des tableaux concernés selon les caractéristiques de la pièce)
                    {
                        tabLignes0[ligne, n]++;
                        tabCol0[col, n]++;

                        if (ligne == col)
                            tabDiago0[0, n]++;

                        else if (ligne == ((nbreLignes - 1) - col) || col == (nbreLignes - 1) - ligne)
                            tabDiago0[1, n]++;
                    }
                }
                else //compteur du nombre de 1 de la n ième caractéristique sur la ligne, colonne, diagonale considérée. Mêmes manipulations mais dans les tableaux de sommes des caractéristiques codée par "1"
                {
                    if (simul)
                    {
                        if (tabLignes1[ligne, n] == nbPiecesAlignees || tabCol1[col, n] == nbPiecesAlignees) // cherche un certain nombre de pièces alignées (2 ou 3) dans les tableaux de sommes des lignes et des colonnes concernant les caractéristiques codée par "1"
                        {
                            alignementPieces = true;
                        }
                        else if (ligne == col && tabDiago1[0, n] == nbPiecesAlignees) // idem pour le tableau de sommes de la diagonale de la gauche vers la droite, du haut vers le bas (il s'agit de la première ligne du tableau tabDiago0 d'où  tabDiago1[0,n])
                        {

                            alignementPieces = true;
                        }
                        else if (ligne == ((nbreLignes - 1) - col) && tabDiago1[1, n] == nbPiecesAlignees || col == ((nbreLignes - 1) - ligne) && tabDiago1[1, n] == nbPiecesAlignees) // idem pour le tableau de sommes de la diagonale de la droite vers la gauche, du haut vers le bas (il s'agit de la première ligne du tableau tabDiago0 d'où tabDiago1[1,n])
                        {
                            alignementPieces = true;
                        }
                    }
                    else
                    {
                        tabLignes1[ligne, n]++;
                        tabCol1[col, n]++;

                        if (ligne == col)
                            tabDiago1[0, n]++;

                        if (ligne == ((nbreLignes - 1) - col) || col == ((nbreLignes - 1) - ligne))
                            tabDiago1[1, n]++;
                    }
                }
            }
            return alignementPieces;
        }

        /// <summary>
        /// ChoisirPieceIA : l'IA choisit intelligemment la pièce qu'elle donnera à l'humain
        /// </summary>
        static void ChoisirPieceIA()
        {
            bool empecheVictoireHumain = false; // true : l'IA a trouvé une pièce qui empêche l'humain de gagner, false sinon
            bool alignement4Pieces = false; // true : après simulation du placement de la pièce considérée dans une case, il y a alignement de 4 pièces ayant au moins une caractéristique commune (soit, il y a un quarto)
            int k = 0; // indice de parcours du tableau recensant toutes les pièces du jeu
            ligne = 0; col = 0;


            string[] piecesPossiblesIA = new string[16]; // création d'un tableau qui recensera toutes les pièces que l'IA peut jouer sans risquer de faire gagner l'adversaire (Elle choisira alors aléatoirement entre ces pièces)
                                                         // taille maximale du tableau = nombre de pièces soit 16
            for (k = 0; k < TabPieces.GetLength(1); k++) // parcours du tableau recensant les pièces 
            {
                choixPiece = TabPieces[0, k]; // Permet d'appeler la fonction MettreAJourStrategies qui dépend de choixPiece
                if (TabPieces[1, k] == "0")//choix d'une pièce parmi les pièces disponibles
                {
                    ligne = 0; col = 0; // remise à 0 des indices lorsque l'on passe à une autre pièce
                    alignement4Pieces = false;
                    //  l'IA parcourt tout le tableau pour s'assurer que cette pièce empêche l'humain de gagner.
                    //   Une pièce empêche l'humain de gagner si, pour n'importe quelle case vide de la grille, elle ne génère pas un alignement de 4 pièces avec 1 caractéristique commune

                    while (ligne < nbreLignes && !alignement4Pieces) // parcours des cases de la grille de jeu
                    {
                        while (col < nbreLignes && !alignement4Pieces)
                        {

                            if (Grille[ligne, col] == caseVide) // si la case considérée est vide
                            {
                                alignement4Pieces = MettreAJourStrategies(true, 3); // l'IA vérifie si, pour la case considérée, la simulation de placement de la pièce considérée dans chaque case Grille[ligne, col]  génère ou non un alignement de 4 pièces 
                            }

                            col++;
                        }
                        if (col == nbreLignes) // l'IA a parcouru toutes les colonnes de la ligne i considérée sans trouver de case générant un alignement de 4 pièces avec 1 caractéristique commune
                        {
                            col = 0; // Et remet l'indice des colonnes à 0 pour toutes les parcourir de nouveau lorsqu'elle changera de ligne
                        }
                        if (ligne < nbreLignes)
                            ligne++;
                    }

                    if (!alignement4Pieces) // L'IA a parcouru toutes les cases du tableau sans que le placement de la pièce considérée puisse générer un alignement de 4 pièces
                    {
                        empecheVictoireHumain = true; // cette pièce empêche donc l'humain de gagner avec cette pièce si on la lui donne
                        piecesPossiblesIA[k] = choixPiece; // remplissage du tableau recensant toutes les pièces que peut jouer l'IA sans faire gagner l'adversaire
                    }
                }
            }

            if (!empecheVictoireHumain) // si le tableau recensant les pièces que l'IA peut jouer est nul
            {
                // l'IA choisit au hasard dans les pièces encore disponibles celle qu'elle donnera puisque de toute façon, si le tableau est nul, c'est qu'elle ne peut éviter que le joueur adverse gagne
                int randomPiece;
                Random R = new Random();
                do
                {
                    randomPiece = R.Next(0, 16);
                    choixPiece = TabPieces[0, randomPiece];
                }
                while (TabPieces[1, randomPiece] == "1"); //l'IA choisit de nouveau la pièce s'il en a choisi une déjà jouée
            }

            else  // si le tableau recensant les pièces que l'IA peut jouer n'est pas nul
            {
                
             ChoisirPieceGagnante(piecesPossiblesIA);  // Elle procède de la même façon que précédemment mais cette fois, en cherchant une pièce dont la simulation de placement générerait un alignement de 3 pièces (ainsi, si l'humain place la pièce au bon endroit et ensuite, lui donne une pièce adéquate, il pourra faire un quarto)

            }
        }

        /// <summary>
        /// ChoisirPieceGagnanteIA : l'IA cherche une pièce qui empêche l'humain de gagner et qui peut permettre à l'IA de gagner au prochain tour
        /// </summary>
        /// <param name="piecesPossiblesIA"></param>
        static void ChoisirPieceGagnante(string[] piecesPossiblesIA)
        {
            int k = 0;
            bool alignement3Pieces = false; //true : La simulation de placement de la pièce considérée dans une case de la grille génère un alignement de 3 pièces (avec une caractéristique commune), false sinon
            bool potentielleVictoireIA = false; // true : l'IA a trouvé une pièce telle que, si elle la donne, elle peut potentiellement gagner lorsque viendra son tour, false sinon

            string[] piecesGagnantesIA = new string[piecesPossiblesIA.Length]; // tableau recensant toutes les pièces disponibles qui peuvent permettre à l'IA de gagner au prochain tour. 
                                                                               // sa taille maximale est égale à la taille du tableau des pièces qui empêchent l'humain de gagner

            for (k = 0; k < piecesPossiblesIA.Length; k++) // choix d'une pièce parmi les pièces dispos
            {
                choixPiece = piecesPossiblesIA[k];
                ligne = 0; col = 0;
                alignement3Pieces = false;

                if (piecesPossiblesIA[k] != null)
                {
                    //  l'IA parcourt tout le tableau pour voir si, parmi les pièces qui empêchent l'humain de gagner, certaines peuvent lui permettre de gagner. 
                    // Une pièce qui permettrait à l'IA de gagner serait une pièce qui, placée dans l'une des cases, générerait un alignement de 3 pièces
                    while (ligne < nbreLignes && !alignement3Pieces)
                    {
                        while (col < nbreLignes && !alignement3Pieces)
                        {
                            if (Grille[ligne, col] == caseVide)
                            {
                                alignement3Pieces = MettreAJourStrategies(true, 2); // l'IA vérifie pour la case considérée qu'elle ne génère pas un alignement de 4 pièces
                                if (alignement3Pieces) // L'IA a trouvé une pièce qui générait un alignement de 3 pièces si elle était placée dans une case
                                {
                                    potentielleVictoireIA = true; // cette pièce empêche donc l'humain de gagner avec cette pièce si on la lui donne
                                    piecesGagnantesIA[k] = choixPiece; // remplissage du tableau recensant toutes les pièces que peut jouer l'IA sans faire gagner l'adversaire
                                }
                            }

                            col++;
                        }
                        if (col == nbreLignes) // l'IA a parcouru toutes les colonnes de la ligne i considérée sans trouver de case générant un alignement de 3 pièces avec 1 caractéristique commune
                        {
                            col = 0; // Et remet l'indice des colonnes à 0 pour toutes les parcourir de nouveau lorsqu'elle changera de ligne
                        }
                        if (ligne < nbreLignes)
                            ligne++;
                    }
                }
            }

            if (potentielleVictoireIA) // l'IA a trouvé au moins une pièce potentiellement gagnante pour elle, le tableau piecesGagnantesIA n'est donc pas nul
            {
                // l'IA choisit au hasard une pièce dans le tableau piecesGagnantesIA (pour ne pas être prévisible)
                int randomPiecesGagnantes;
                Random R2 = new Random();
                do
                {
                    randomPiecesGagnantes = R2.Next(0, piecesGagnantesIA.Length); // choix aléatoire d'un indice (entre 0 et 15) de colonne du tableau piecesGagnantes

                }
                while (piecesGagnantesIA[randomPiecesGagnantes] == null); // il se peut que des valeurs du tableau soient nulles (puisqu'il comporte 16 cases)

                choixPiece = piecesPossiblesIA[randomPiecesGagnantes];

            }
            else // si le tableau recensant les pièces gagnantes pr l'IA est nul
            {
                // L'IA choit au hasard la pièce qu'elle va donner à l'adversaire entre les pièces qui empêchent l'adversaire de gagner (afin que son choix ne soit pas prévisible)
                int randomPiece;
                Random R = new Random();

                do
                {
                    randomPiece = R.Next(0, piecesPossiblesIA.Length);

                }
                while (piecesPossiblesIA[randomPiece] == null);

                choixPiece = piecesPossiblesIA[randomPiece];
            }
        }

        //  sous-fonctions de choix intelligent de la case

        /// <summary>
        /// GagnerIA () : l'IA cherche si elle peut directement gagner avec la pièce qu'elle a : S'il l y a déjà 3 pièces d' "alignées", elle regarde si sa pièce est compatible et si elle peut la placer dans la case restante
        /// </summary>
        static void GagnerIA()
        {
           // l'IA recherche s'il y a déjà  sur une mm ligne, 3 pièces "alignées" et ayant 1 même caractéristique 
            int n = 0;

            while (n < nbreCaractéristiques && !AGagne)
            {
                int i = 0;
                if (choixPiece[n] == '0') // cherche dans les tableaux de sommes relatives aux lignes de jeu s'il y a déjà 3 pièces alignées ayant la caractéristique 0 en n ième position
                {

                    while (i < tabLignes0.GetLength(0) && tabLignes0[i, n] != 3) // continue à chercher tant qu'elle n'a pas parcouru tous les tableaux et qu'elle ne trouve pas un "3" dans ces tableaux 
                    {
                        i++;
                    }
                }
                else // cherche dans le plateau de jeu s'il y a déjà 3 pièces alignées ayant la caractéristique 1 en n ième position (procède de même)
                {
                    while (i < tabLignes1.GetLength(0) && tabLignes1[i, n] != 3)
                    {
                        i++;
                    }
                }

                if (i != tabLignes0.GetLength(0) && TrouverCaseIALigne(i)) // l'IA a trouvé un alignement de 3 pièces et une  case vide pour poser la pièce qui lui permettra de faire un quarto
                {
                    Grille[i, col] = choixPiece; // actualisation de la grille
                    AGagne = true;
                }
                else
                {
                    n++;
                }
            }

            // l'IA recherche de la même manière  où il y a déjà sur une même colonne, 3 pièces "alignées" et ayant 1 même caractéristique 
            n = 0; // l'IA parcourt de nouveau les 4 caractéristiques de la pièce
            while (n < nbreCaractéristiques && !AGagne)
            {
                int j = 0;
                if (choixPiece[n] == '0')
                {
                    while (j < tabCol0.GetLength(0) && tabCol0[j, n] != 3)
                    {
                        j++;
                    }
                }
                else
                {
                    while (j < tabCol1.GetLength(0) && tabCol1[j, n] != 3)
                    {
                        j++;
                    }
                }

                if (j != tabCol0.GetLength(0) && TrouverCaseIACol(j))
                {
                    Grille[ligne, j] = choixPiece;
                    AGagne = true;
                }
                else
                {
                    n++;
                }
            }

                // si elle n'a toujours pas gagné, cherche de la même manière dans le plateau de jeu s'il y a déjà 3 pièces alignées surl'une des 2 diagonales pour 1 caractéristique
                n = 0;
                while (n < nbreCaractéristiques && !AGagne)
                {
                    int k = 0;
                    if (choixPiece[n] == '0') 
                    {

                        while (k < tabDiago0.GetLength(0) && tabDiago0[0, n] != 3)
                        {
                            k++;
                        }
                    }
                    else
                    {
                        while (k < tabDiago1.GetLength(0) && tabDiago1[0, n] != 3)
                        {
                            k++;
                        }
                    }

                    if (k != tabDiago0.GetLength(0) && TrouverCaseIADiago(k)) //l'indice k permet d'identifier dans laquelle des 2 diagonales se trouve l'alignement de 3 pièces identiques
                    {
                        Grille[ligne, col] = choixPiece;
                        AGagne = true;
                    }
                    else
                    {
                        n++;
                    }
                }
        }

        /// <summary>
        ///  TrouverCaseIALigne(): permet à l'IA de recherche où est la case restante sur cette ligne où il y a déjà 3 pièces d'alignées et si elle est vide
        /// </summary>
        /// <param name="i"></param> 
        /// <returns></returns>
        static bool TrouverCaseIALigne(int i) //  int k indique sur laquelle des 4 colonnes de la grille les 3pièces alignées se trouvent
        {
            bool caseJouable = false;
            int j = 0;
            while (j < nbreLignes && AvoirCaseRemplie(i, j)) // tant que la case est remplie, l'IA continue de chercher sur la ligne en question
            {
                j++;
            }
            if (j < nbreLignes) // si l'IA a trouvé une case vide sur la ligne considérée
            {
                caseJouable = true;
                col = j;   // l'indice de colonne j trouvé est enregistré dans la variable gglobale col (permettra d'écrire ensuite Grille [ligne, col] = choixPiece) 
            }
            return caseJouable; // retourne true si elle a trouvé une case vide qui lui permettra de faire un Quarto, false si elle n'a pas trouvé de case vide sur la ligne où déjà 3 pièces étaient alignées

        }

        /// <summary>
        /// /// TrouverCaseIACol(): permet à l'IA de recherche où est la case vide sur cette colonne où il y a déjà 3 pièces d'alignées
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        static bool TrouverCaseIACol(int j) // int j indique sur laquelle des 4 colonnes de la grille les 3pièces alignées se trouvent
        {
            // fonctionne de la même façon que la précédente
            bool caseJouable = false;
            int i = 0;
            while (i < nbreLignes && AvoirCaseRemplie(i, j))
            {
                i++;
            }
            if (i < nbreLignes)
            {
                caseJouable = true;
            }
            ligne = i; // l'indice de ligne i trouvé est enregistré dans la variable globale ligne (permettra d'écrire ensuite Grille[ligne, col] = choixPiece) 
            return caseJouable;
        }

        /// <summary>
        /// TrouverCaseIAdiago : permet à l'IA de recherche où est la case vide sur la diagonale où il y a déjà 3 pièces d'alignées
        /// </summary>
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        static bool TrouverCaseIADiago(int k) // int k indique sur laquelle des 2 diagonales de la grille les 3pièces alignées se trouvent
        {
            bool caseJouable = false;
            int i = 0;

            if (k == 0) // les 3 pièces alignées sont sur la diagonale de la gauche vers la droite et haut vers bas (diagonale 0)
            {
                while (i < nbreLignes && AvoirCaseRemplie(i, i)) //sur cette diagonale, les cases sont du type Grille[i, i]
                {
                    i++;
                }
                if (i < nbreLignes)
                {
                    caseJouable = true;
                }
                ligne = i; //l'indice de ligne i trouvé est enregistré dans la variable globale ligne 
                col = i;  //Le même indice i (cette fois de colonne) est enregistré dans la variable globale ligne (permettra d'écrire ensuite Grille[ligne, col] = choixPiece) 
            }

            if (k == 1) // les 3 pièces aligénes sont sur la diagonale de la droite vers la gauche et haut vers bas (diagonale 1)
            {
                while (i < nbreLignes && AvoirCaseRemplie(i, (nbreLignes - 1) - i)) // Cherche sur la diagonale, une case vide (sur cette diagonale, les cases sont du type Grille[i, 3-i])
                {
                    i++;
                }
                if (i < nbreLignes)
                {
                    caseJouable = true;
                }
                ligne = i; //l'indice de ligne i trouvé est enregistré dans la variable globale ligne  (permettra d'écrire ensuite Grille[ligne, col] = choixPiece) 
                col = (nbreLignes - 1) - i; // la variable globale col prend la valeur 3-i (sur cette diagonale, les cases sont du type Grille[i, 3-i])
            }

            return caseJouable;
        }


        /// <summary>
        /// ChoisirCaseIA: Si elle ne peut pas directement gagner, l'IA cherche à poser sa pièce dans les cases disponibles tout en s'assurant de ne pas permettre à l'humain de gagner au prochain tour
        /// </summary>
        static void ChoisirCaseIA()
        {
            bool alignement3pieces = false; //booléen déterminant si  la simulation de placement de la pièce (donnée par l'humain) génère un alignement de 3 pièces avc une cractéristique commune (true) ou non. 

            int[][] casesPossiblesIA = new int[16][]; // tableau recensant toutes les cases dans lesuelles l'IA peut jouer sa pièce sans risque de faire gagner l'humain. Taille maximale du tableau = nombre de pièces soit 16
                                                      // le tableau imbriqué comportera 2 éléments : l'indice de ligne et l'indice de colonne
            int indice = 0; // indice permettant de remplir le tableau ci-dessus
            
            ligne = 0; col = 0;
            bool trouveCasePossible = false;

            while (ligne < nbreLignes) // parcourt de la grille de jeu
            {
                while(col < nbreLignes)
                {
                    if (Grille[ligne, col] == caseVide) // si case vide
                    {
                        alignement3pieces =MettreAJourStrategies(true, 2); //simulation de placement de la pièce dans cette case et on vérifie si elle génère un alignement de 3 pièces identiques (en effet, le prochain joueur est l'humain!)
                        if (!alignement3pieces) /// a trouvé une case telle que si il place sa pièce dedans, elle ne générera pas un alignement de 3 pièces 
                        {
                            trouveCasePossible = true;
                            casesPossiblesIA[indice] = new int[2] { ligne, col }; // cette case est recensée dans le tableau des cases dans lesquelles l'IA peut jouer sans risque de faire gagner l'humain sous forme d'un tableau imbriqué
                            indice++;
                        }
                    }
                    col++;
                }

                if (col == nbreLignes) // si toutes les colonnes d'une ligne ont étéb parcourues, remise à 0 de l'indice de colonne pour passer à une autre ligne
                {
                    col = 0;
                }
                ligne++;
            }

            if ( trouveCasePossible)  // si le tableau recensant les cases jouables par l'IA n'est pas nul ('IA a trouvé au moins une case possible)
            {
                // choisit aléatoirement la case dans laquelle elle va jouer (pour ne pas être prévisible)
                int randomCasePossible;
                Random R = new Random();
                do
                {
                    randomCasePossible = R.Next(0, casesPossiblesIA.Length); 

                }
                while (casesPossiblesIA[randomCasePossible] == null); // il se peut que le tableau de 16 éléments ne soit pas entièrement rempli, l'IA tire au sort jusqu'à tomber sur une case du tableau non nulle

                ligne = casesPossiblesIA[randomCasePossible] [0]; // le 1er élément du tableau imbriqué donne la ligne de la grille dans laquelle sera jouée la pièce
                col = casesPossiblesIA[randomCasePossible][1];    // le 2er élément du tableau imbriqué donne la colonne de la grille dans laquelle sera jouée la pièce
                Grille[ligne, col] = choixPiece;
            }
            else // toutes les cases disponibles génèrent un alignement de 3 pièces ayant une caractéristique commune
            {
                // choisit aléatoirement la ligne et la colonne pour placer le pion puisque dans tous les cas il a perdu
                Random R = new Random();

                do
                {
                    ligne = R.Next(0, nbreLignes);
                    col = R.Next(0, nbreLignes);
                } while (AvoirCaseRemplie(ligne, col)); // tant que la case qu'il a choisi est remplie, l'ordi doit replacer sa pièce 

                Grille[ligne, col] = choixPiece;
            }
        }
      

        //-------------------------------------------Fonctions relatives à la fin du jeu -------------------------------------------------

        /// <summary>
        /// Gagner () : Fonction donnant toutes les combinaisons gagnantes et terminant la partie
        /// </summary>
        /// <returns></returns>
        static void Gagner()
        {
            int i; //indice lignes
            int j; //indice colonnes
            int n; //indice des 4 caractéristiques de la pièce

            //verification si quarto sur chaque ligne 
            for (i = 0; i < nbreLignes; i++) //indice ligne
            {
                for (n = 0; n < nbreLignes; n++) //test pour chaque carcatéristique(x4)
                {
                    j = 0;
                    while (j < nbreLignes && AvoirCaseRemplie (i, 0) && Grille[i, 0][n] == Grille[i, j][n]) //qd caractéristique commune, on compare la valeur de départ à chacune des autres cases remplies de la ligne considérée
                    {
                        j++;
                    }
                    if (j == nbreLignes) // Toute une ligne a été parcourue sans sortie de la boucle donc, une ligne de 4 pièces avec au moins 1 caractéristique commune a été complétée
                    {
                        AGagne = true; // le dernier joueur ayant placé la pièce a donc gagné
                    }
                }
            }

            //verification si quarto sur chaque colonne. Même procédé mais parcours des colonnes
            if (!AGagne)
            {
                for (j = 0; j < nbreLignes; j++)
                {
                    for (n = 0; n < nbreLignes; n++)
                    {
                        i = 0;
                        while (i < nbreLignes && AvoirCaseRemplie (0, j) && Grille[0, j][n] == Grille[i, j][n])
                        {
                            i++;
                        }
                        if (i == nbreLignes) // une colonne de 4 pièces avec au moins 1 caractéristique commune a été complétée
                        {
                            AGagne = true;
                        }
                    }
                }
            }

            //vérification si quarto sur la diagonale de la gauche vers la droite, haut vers bas
            if (!AGagne)
            {
                for (n = 0; n < nbreCaractéristiques; n++)
                {
                    // Coordonnées (i, i) de la 1ere case que je compare à la 1ere case de la diagonale (soit Grille[0,0])
                    i = 1;
                    while (i < nbreLignes && AvoirCaseRemplie (0, 0) && Grille[0, 0][n] == Grille[i, i][n])
                    {
                        i++;
                    }
                    if (i == nbreLignes)
                    {
                        AGagne = true; // la diagonale en question a été complétée avec 4 pièces ayant au moins 1 caractéristique commune 
                    }
                }
            }

            //vérification si quarto sur la diagonale de la droite vers la gauche, du haut vers le bas
            if (!AGagne)
            {
                for (n = 0; n < nbreCaractéristiques; n++)
                {
                    // Coordonnées (i, j) de la 1ere case que je compare à la 1ere case de la diagonale (soit Grille[0,3])
                    i = 1;
                    j = 2;
                    while (i < nbreLignes && j >= 0 && AvoirCaseRemplie (0, 3) && Grille[0, 3][n] == Grille[i, j][n])
                    {
                        i++;
                        j--;
                    }
                    if (i == nbreLignes)
                    {
                        AGagne = true; // la diagonale en question a été complétée avec 4 pièces ayant au moins 1 caractéristique commune 
                    }
                }
            }
        }

        /// <summary>
        /// AfficherVictoire : Affiche message de victoire si joueur humain gagne
        /// </summary>
        static void AfficherVictoireHumain()
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
        static void AfficherPerteHumain()
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
        /// AvoirGrilleRemplie : Permet de vérifier si la grille de jeu est remplie. Condition de fin de jeu + permet de définir quand il y a match nul
        /// </summary>
        /// <returns></returns>
        static void AvoirGrilleRemplie()
        {

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

            if (i == nbreLignes && j == nbreLignes) // toute la grille a été parcourue sans sortie de boucle
                grilleRemplie = true; //donc toutes les cases sont remplies
        }

        /// <summary>
        /// RejouerPartie : Le joueur décide, à chaque fin de partie, s'il souhaite en refaire une ou non
        /// </summary>
        /// <returns></returns>
         static bool RejouerPartie()
        {
            Console.WriteLine("\n\n\nTapez [r] pour rejouer ou [a] pour arrêter");
            string reponse = Console.ReadLine(); //le joueur choisit s'il veut refaire une partie
            while (reponse != "r" && reponse != "a")
            {
                Console.WriteLine("Erreur, saisissez [r] ou [a] on a dit !"); //Message d'erreur, demande à l'humain de saisir à nouveau tant qu'il ne saisit pas [r] ou [a]
                reponse = Console.ReadLine();
            }
            if (reponse == "r")
                return true;
            else
                return false;
        }

        // fonction que l'on voulait utiliser mais nous n'avons pas trouvé comment arrêter à tout moment sans demander à l'utilisateur s'il veut quitter la partie.

        /// <summary>
        /// ArreterPartie : à tout moment, le joueur peut décider d'arrêter la partie
        /// </summary>
       /* static bool ArreterPartie()
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
        }*/

    }
}



