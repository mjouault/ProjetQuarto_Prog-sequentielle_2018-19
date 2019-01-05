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

        static int ligne; static int col; //lignes et colonnes que le joueur/l'ordi a choisi
        static string choixPiece; // variable globale désignant une pièce choisie pour être jouée par l'un des joueurs
        static int nbreLignes = 4; // constante donnant le nombre de lignes et de colonnes puisque le plateau est carré
        static int nbreCaractéristiques = 4; // constante donnant le nombre de caractéristiques des pièces

        static int[,] tablignes0 = new int[4, 4]; //tableau sommant, pour chaque caractéristique, le nombre de 0 sur chaque ligne 
        static int[,] tablignes1 = new int[4, 4]; //tableau sommant, pour chaque caractéristique, le nombre de 1 sur chaque ligne 
        static int[,] tabcol0 = new int[4, 4]; //tableau sommant, pour chaque caractéristique, le nombre de 0 sur chaque colonne
        static int[,] tabcol1 = new int[4, 4];  //tableau sommant, pour chaque caractéristique, le nombre de 1 sur chaque colonne
        static int[,] diago0 = new int[4, 4];   //tableau sommant, pour chaque caractéristique, le nombre de 0 sur chaque diagonale
        static int[,] diago1 = new int[4, 4];  //tableau sommant, pour chaque caractéristique, le nombre de 1 sur chaque diagonale


        static bool trace = false;  //  true si l'on veut afficher des messages pour débugueur notre code, false sinon
        static bool AGagne = false; //  true si un joueur a gagné, false sinon (lorsqu'elle est appelée dans les fonctions relative à l'IA, elle détermine si l'IA gagne ou non en plaçant ne pièce)
        static bool grilleRemplie = false;
        static bool modeIntell = false;

        // Main
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 40);

            AfficherEnTete();
            AfficherRegles();
            ChoisirMode();
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

                Console.Write("\nBUT DU JEU : Créer sur le plateau un alignement de 4 pièces ayant au moins un caractère commun(fig. 2).\nCet alignement peut-être horizontal, vertical ou diagonal." +
                    " \nDÉROULEMENT D’UNE PARTIE : Le premier joueur est tiré au sort.\nIl choisit une des 16 pièces et la donne à son adversaire." +
                    "\nCelui - ci doit la placer sur une des cases du plateau et choisir ensuite une des 15 pièces restantes pour la donner à son adversaire." +
                    "\nA son tour, celui-ci la place sur une case libre et ainsi de suite…." +
                    "\n\nGAIN DE LA PARTIE : La partie est gagnée par le premier joueur qui annonce “QUARTO !”" +
                    "\nUn joueur fait “QUARTO !” et gagne la partie lorsque, en plaçant la pièce donnée, il aligne 4 pièces ayant au moins un caractère en commun." +
                    "\nPlusieurs caractères peuvent se cumuler.\n\nDe plus, il n’est pas obligé d’avoir lui même déposé les trois autres pièces." +
                    "\nIl y a égalité: toutes les pièces ont été posées sans vainqueur.");
            }

            //Console.WriteLine("Que la partie commence et que le meilleur gagne !");
        }

        //Sous-programmes

        static bool ChoisirMode()
        {
            int choix;
            Console.WriteLine("Tu peux maintenant choisir le niveau de l'ordinateur!\n[1]: Ordinateur débutant\n[2]: Ordinateur intelligent");
            choix = int.Parse(Console.ReadLine());
            while (choix != 1 && choix != 2)
            {
                Console.WriteLine("Erreur de saisie, il faut répondre par 1 ou par 2:");
                choix = int.Parse(Console.ReadLine());
            }
            if (choix == 2)
                modeIntell = true;
            return modeIntell;
        }

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
            TabPieces = new string[,] { { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111",
                                          "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" },
                                        { "0", "0", "0", "0", "0", "0", "0", "0",
                                          "0", "0", "0", "0", "0", "0", "0", "0" } };
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
        /// choisir1erJoueur : désigne aléatoirement qui du joueur ou de l'ordi commence à jouer
        /// </summary>
        /// <returns></returns>
        static bool choisir1erJoueur()
        {
            // Random R = new Random();
            // int choix1er = R.Next(0, 2);
            int choix1er = 1;
            bool estHumain = true;

            if (choix1er == 0)
                estHumain = false;

            return estHumain;
        }

        /// <summary>
        /// Jouer() : Permet que le joueur et l'ordinateur jouent chacun leur tour
        /// </summary>
        static void Jouer()
        {
            bool joueurCourantHumain = choisir1erJoueur();
            AfficherTitre();
            AfficherGrille();

            while (!AGagne && !grilleRemplie)
            {
                //Console.Clear();
                // AfficherTitre();
                // AfficherGrille();

                if (trace)
                    Console.WriteLine("la partie commence");

                if (joueurCourantHumain)  // joueur etre humain
                {
                    if (trace)
                        Console.WriteLine("A toi de jouer");

                    JouerHumain();
                    if (trace)
                        Console.WriteLine("Le joueur joue");

                    Gagner();

                    // AfficherGrille();

                    if (trace)
                        Console.WriteLine("Le joueur a joué");

                    //vérification si le joueur a gagné à chaque fin de tour
                    if (AGagne) //cas s'il gagne
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

                    Gagner();
                    if (AGagne)
                        AfficherPerte();
                }

                if (!AGagne) // Cas où la grille est remplie mais personne ne gagne : c'est un match nul
                {
                    AvoirGrilleRemplie();
                    if (grilleRemplie)
                        Console.WriteLine("Match nul");
                }

                AfficherTitre();
                AfficherGrille();
                joueurCourantHumain = !joueurCourantHumain;
            }
        }




        /// <summary>
        /// JouerHumain : Fonciton permettant au joueur de jouer son tour soit de placer une pièce choisie par l'ordinateur
        /// </summary>
        static void JouerHumain()
        {
            //choix de la pièce dans le tableau par l'ordi

            if (!modeIntell)
            {
                int randomPiece;
                Random R = new Random();
                do
                {
                    randomPiece = R.Next(0, 16);
                    choixPiece = TabPieces[0, randomPiece];
                }
                while (TabPieces[1, randomPiece] == "1"); //Demander à l'ordi de choisir de nouveau la pièce s'il en a choisi une déjà jouée*/
            }
            else
            {
                ChoisirPieceIA();
            }

            UtiliserPiece();

            Console.WriteLine("L'ordinateur a choisi la pièce {0} pour vous\n" +
                "- 0000 correspond à petite, creuse, carrée, clair\n" +
                "- 1111 correspond à grande, pleine, ronde, foncee \n" +
                "Les caractères peuvent être mélangés entre eux", choixPiece);
            // améliorer notre présentation des pièces  Console.WriteLine("le 1er caractère correspond à [1]= ronde [0]=carrée, 2ème caractère [1]=creuse [0]=vide");


            //choix de la case par le joueur

            bool caseRemplie = false;

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

            Grille[ligne, col] = choixPiece;
            MettreAJourStrategies(false, 0);


            if (trace)
                Console.WriteLine("yolo2");
            //AfficherGrille();
            //ArreterPartie();
        }

        /// <summary>
        /// JouerOrdi : Fonction permettant à l'ordinateur de jouer son tour soit de placer une pièce choisie par le joueur
        /// </summary>
        static void JouerOrdi()
        {
            bool pieceUtilisee = false;
            //choix pièce par le joueur
            Console.WriteLine("Que choisissez-vous comme pièce pour l'ordinateur?\n" +
                "- 0000 correspond à petite, creuse, carrée, clair\n" +
                "- 1111 correspond à grande, pleine, ronde, foncee \n" +
                "vous pouvez mixer plusieurs caractères évidemment.");
            do
            {
                //Console.WriteLine("Pièce déjà utilisée, choisissez-en une autre");
                choixPiece = Console.ReadLine();//on récupère la pièce que le joueur choisi pour l'ordi

                if (choixPiece.Length != nbreCaractéristiques)
                {
                    Console.Beep(500, 300);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Erreur : une pièce doit avoir 4 caractères. Veuillez entrer un nom de pièce valide :");
                    Console.ResetColor();
                }
                else
                {
                    pieceUtilisee = VerifierSiPieceUtilisee();

                    if (pieceUtilisee)
                    {
                        Console.Beep(500, 300);
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Erreur : Pièce déjà utilisée, veuillez en choisir une autre :");
                        Console.ResetColor();
                    }
                }
            } while (pieceUtilisee || choixPiece.Length != nbreCaractéristiques ); //tant que la pièce n'est pas bonne on en rechoisit une autre

            UtiliserPiece();

            if (!modeIntell)
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
            else
            {
                GagnerIA();
                if (!AGagne)
                {
                    if (trace)
                        Console.WriteLine("if !AGagne");
                    ChoisirCaseIA();
                }
            }
                    UtiliserPiece();
                    MettreAJourStrategies(false, 0);
        }


        /// <summary>
        /// GagnerIA () : l'IA cherche si elle peut directement gagner avec la pièce qu'elle a. S'il l y a déjà 3 pièces d' "alignées", elle regarde si sa pièce est compatible
        /// </summary>
        static void GagnerIA()
        {
            if (trace)
                Console.WriteLine("entre ds gagnerIA");

            // l'IA recherche s'il y a déjà  sur une mm ligne, 3 pièces "alignées" et ayant 1 même caractéristique 
            int n = 0;

            while (n < nbreCaractéristiques && !AGagne)
            {
                int i = 0;
                if (choixPiece[n] == '0') // cherche dans le plateau de jeu s'il y a déjà 3 pièces alignées pour 1 caractéristique
                {

                    while (i < tablignes0.GetLength(0) && tablignes0[i, n] != 3)
                    {
                        if (trace)
                            Console.WriteLine("lablignes0" + i);
                        i++;
                    }
                }
                else
                {
                    while (i < tablignes1.GetLength(0) && tablignes1[i, n] != 3)
                    {
                        if (trace)
                            Console.WriteLine("lablignes1" + i);
                        i++;
                    }
                }
                if (trace)
                    Console.WriteLine("i ={0}, n={1}", i, n);
                if (i != tablignes0.GetLength(0) && TrouverCaseIALigne(i))
                {
                    Grille[i, col] = choixPiece;
                    AGagne = true;
                }
                else
                {
                    n++;
                }
            }

            n = 0;
            // l'IA recherche où il y a déjà sur une même colonne, 3 pièces "alignées" et ayant 1 même caractéristique 
            while (n < nbreCaractéristiques && !AGagne)
            {
                int j = 0;
                if (trace)
                    Console.WriteLine("piècesalignéescolonnes");
                if (choixPiece[n] == '0')
                {
                    while (j < tabcol0.GetLength(0) && tabcol0[j, n] != 3)
                    {
                        if (trace)
                            Console.WriteLine("tabcol0" + j);
                        j++;
                    }
                }
                else
                {
                    while (j < tabcol1.GetLength(0) && tabcol1[j, n] != 3)
                    {
                        if (trace)
                            Console.WriteLine("tabcol1" + j);
                        j++;
                    }
                }

                if (j != tabcol0.GetLength(0) && TrouverCaseIACol(j))
                {
                    Grille[ligne, j] = choixPiece;
                    AGagne = true;
                }
                else
                {
                    n++;
                }


                n = 0;

                while (n < nbreCaractéristiques && !AGagne)
                {
                    int k = 0;
                    if (choixPiece[n] == '0') // cherche dans le plateau de jeu s'il y a déjà 3 pièces alignées surl'une des 2 diagonales pour 1 caractéristique
                    {

                        while (k < diago0.GetLength(0) && diago0[0, n] != 3)
                        {
                            k++;
                            if (trace)
                            Console.WriteLine("ds while diago0DGHB, j={0}", k);
                        }
                    }
                    else
                    {
                        while (k < diago1.GetLength(0) && diago1[0, n] != 3)
                        {
                            k++;
                            if (trace)
                            Console.WriteLine("ds while diago1DGHB j={0}", k);
                        }
                    }
                    if (trace)
                        Console.WriteLine("avant if trouverCaseIAdiagoGDHB, j={0}, choixpiece={1}", k, choixPiece);

                    if (k != diago0.GetLength(0) && TrouverCaseIAdiago(k))
                    {
                        if (trace)
                            Console.WriteLine("ds if trouverCaseIAdiagoGDHB, j={0}, choixpiece={1}", k, choixPiece);
                        Grille[ligne, col] = choixPiece;
                        AGagne = true;
                    }
                    else
                    {
                        n++;
                    }
                }

               /* // cherche dans le plateau de jeu s'il y a déjà 3 pièces alignées sur la  diagonale de droite vers la gauche, du haut vers bas pour 1 caractéristique
                n = 0;
                while (n < nbreCaractéristiques && !AGagne)
                {
                    int k = 0;
                    if (choixPiece[n] == '0') // Cas si la caracctéristique considérée =0
                    {

                        while (k < diago0.GetLength(0) && diago0[k, n] != 3) // vérifie s'il y a 3 pièces alignées avec une caractéristique égale à 0 ne sur la diagonale 
                        {
                            k++;
                            if (trace)
                                Console.WriteLine("ds while diago0DGHB, j={0}", k);

                        }
                    }
                    else // cas si la caractéristique considérée = 1
                    {
                        while (k < diago1.GetLength(0) && diago1[k, n] != 3) // vérifie s'il y a 3 pièces alignées avec une caractéristique égale à 0 ne sur la diagonale 
                        {
                            k++;
                            if (trace)
                                Console.WriteLine("ds while diago0DGHB, j={0}", k);

                        }
                    }
                    if (trace)
                        Console.WriteLine("avant if trouverCaseIAdiago,k={0}, choixpiece={1}, n={2}", k, choixPiece, n);

                    if (k != diago0.GetLength(0) && TrouverCaseIAdiago(k)) // même nombre de lignes pour les tableaux diago0 et diago1
                    {
                        if (trace)
                            Console.WriteLine("ds if trouverCaseIAdiagoDGHB, j={0}, choixpiece={1}", k, choixPiece);
                        Grille[ligne, col] = choixPiece;
                        AGagne = true;
                    }
                    else
                    {
                        n++;
                    }
                }*/
            }
        }

        /// <summary>
        /// TrouverCaseIALigne(): permet à l'IA de recherche où est la case vide sur cette ligne où il y a déjà 3 pièces d'alignées
        /// </summary>
        static bool TrouverCaseIALigne(int i)
        {
            if (trace)
                Console.WriteLine("TrouveCaseLigne" + i);

            bool caseVide = false;
            int j = 0;
            while (j < nbreLignes && AvoirCaseRemplie(i, j))
            {
                j++;
            }
            if (j < nbreLignes)
            {
                caseVide = true;
                col = j;
            }

            if (trace)
                Console.WriteLine("TrouveCaseLigneFin col={0}, caseVide ={1}", col, caseVide);

            return caseVide;

        }

        /// <summary>
        /// /// TrouverCaseIACol(): permet à l'IA de recherche où est la case vide sur cette colonne où il y a déjà 3 pièces d'alignées
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        static bool TrouverCaseIACol(int j)
        {
            if (trace)
                Console.WriteLine("TrouveCasecol" + j);

            bool caseVide = false;
            int i = 0;
            while (i < nbreLignes && AvoirCaseRemplie(i, j))
            {
                i++;
            }
            if (i < nbreLignes)
            {
                caseVide = true;
            }
            ligne = i;
            return caseVide;
        }

        /// <summary>
        /// TrouverCaseIAdiago : permet à l'IA de recherche où est la case vide sur la diagonale où il y a déjà 3 pièces d'alignées
        /// </summary>
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        static bool TrouverCaseIAdiago(int k) //l'indice k indique sur quelle diagonale les 3pièces alignées se trouvent
        {
            bool caseVide = false;
            int i = 0;

            if (trace)
                Console.WriteLine("TrouveCasediago, k={0}", k);
            if (k == 0) // les 3 pièces aligénes sont sur la diagonale de la gauche vers la droite et haut vers bas (diagonale 1)
            {
                while (i < nbreLignes && AvoirCaseRemplie(i, i))
                {
                    i++;
                }
                if (i < nbreLignes)
                {
                    caseVide = true;
                }
                ligne = i;
                col = i;
            }

            if (k == 1)
            {
                if (trace)
                Console.WriteLine("début if k=1, on a k={0}", k);
                while (i < nbreLignes && AvoirCaseRemplie(i, (nbreLignes - 1) - i))
                {
                    i++;
                    if (trace)
                    Console.WriteLine("dans if k=1, ds while, on a k={0} et i={1}", k, i);
                }
                if (i < nbreLignes)
                {
                    caseVide = true;
                }
                ligne = i;
                col = (nbreLignes - 1) - i;
            }

            return caseVide;
        }



        /// <summary>
        /// ChoisirCaseIA: Si elle ne peut pas directement gagner, l'IA cherche à poser sa pièce dans les cases disponibles tout en s'assurant de ne pas permettre à l'humaind e gagner au prochain tour
        static void ChoisirCaseIA()
        {
            bool alignement3pieces = false; //booléen déterminant si  la simulation de placement de la pièce (donnée par l'humain) génère un alignement de 3 pièces avc une cractéristique commune (true) ou non. 
            if (trace)
            Console.WriteLine("entre ds trouverCaseIA");
           

            int indice = 0;
            int[][] casesPossiblesIA = new int [16][];

            ligne = 0; col = 0;
         

            bool trouveCasePossible = false;

            while (ligne < nbreLignes)
            {
                while(col < nbreLignes)
                {
                    if (trace)
                    Console.WriteLine("entre ds 2eme do col");
                    if (Grille[ligne, col] == caseVide)
                    {
                        // alignement3pieces = VerifierAlignementPieces(ligne, col, 2);
                        alignement3pieces =MettreAJourStrategies(true, 2);
                        Console.WriteLine("alignement3pieces = {0}, ligne ={1}, col={2}", alignement3pieces, ligne, col);
                        if (!alignement3pieces)
                        {
                            trouveCasePossible = true;
                            casesPossiblesIA[indice] = new int[2] { ligne, col }; // a trouvé une case telle que si il place sa pièce dedans, elle ne générera pas un alignement de 3 pièces en effet le prochain joueur est l'humain!
           
                            for (int m=0; m< 2;m++)
                            {
                                Console.WriteLine("casepossibleLigne = {0}", casesPossiblesIA[indice][m]);
                            }
                            indice++;
                        }
                    }
                    col++;
                }

                if (col == nbreLignes)
                {
                    col = 0;
                }
                ligne++;
            }

           /* int m = 0;
            while (m < casesPossiblesIA.Length -1)
            {
                Console.WriteLine("casepossibleLigne = {0}, casepossibleIACol=", casesPossiblesIA[m][0], casesPossiblesIA[m][1]);
                if (m < casesPossiblesIA.Length-2)
                    m++;
            }*/

            if ( trouveCasePossible) 
            {
                int randomCasePossible;
                Random R = new Random();

                do
                {
                    if (trace)
                        Console.WriteLine("pdt while random ds tableau possibilitées cases");
                    randomCasePossible = R.Next(0, casesPossiblesIA.Length); // [0,16] ou [0, 17] ?? car [0,17] provoque un outOfRange

                }
                while (casesPossiblesIA[randomCasePossible] == null);

                ligne = casesPossiblesIA[randomCasePossible] [0];
                col = casesPossiblesIA[randomCasePossible][1];
                 Grille[ligne, col] = choixPiece;
                Console.WriteLine("choisit au pif");
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

        static bool VerifierAlignementPieces(int ligne, int col, int nbPiecesAlignees) // true : l'IA a trouvé une case générant un alignement de 3 ou 4 pièces (selon nbrePiecesALignees) après simulation de placement d'une pièce 
                                                                                       // les tableaux en paramètres sont des tableaux d'indices (de lignes, colonnes particulières) --> Permet de parcourir tout ou partie de la grille de jeu
        {
            bool alignementPieces = false; //permet d'appeler MettreAJourStrategie en mode simulation
                                           //  bool trouveCaseAvantageuse = false; // une case est avantageuse pour l'IA si elle ne génère pas un alignement de 3


            if (trace)
                Console.WriteLine(" ds while fction VerifierAlignementPieces, ligne ={0}, col={1} ", ligne, col);
            // Console.WriteLine(" ds while j : i={0}, j={1}", i, j);

            if (Grille[ligne, col] == caseVide)
            {
                //  attention : était mis en commentaire
                alignementPieces = MettreAJourStrategies(true, nbPiecesAlignees); // il y a un alignement de pièces si la fonction MettreAJourStrategie retourne true.
                                                        // Le paramètre true dans MettrAJourStrategies signifie que l'on va simuler le placement d'une pièce dans la case Grille[ligne, col] considérée
            }
            //  Console.ReadLine();
            if (trace)
                Console.WriteLine("fin fction alignementPieces, alignementPiece ={0}", alignementPieces);
            return alignementPieces;
        }

        static void ChoisirPieceIA()
        {
            bool empecheVictoireHumain = false;
            bool alignement4Pieces = false;
            int k = 0;
            ligne = 0;  col = 0;

            
            string[] piecesPossiblesIA = new string[16]; // création d'un tableau qui recensera toutes les pièces que l'IA peut jouer sans risquer de faire gagner l'adversaire. Elle choisira alors aléatoirement entre ces pièces

            for (k = 0; k < TabPieces.GetLength(1); k++) // choix d'une pièce parmi les pièces dispos
            {
                choixPiece = TabPieces[0, k];
                if (TabPieces[1, k] == "0")// si pièce non utilisée
                {
                    //  l'IA parcourt tout le tableau pour s'assurer que cette pièce empêche l'humain de gagner.
                    //   Une pièce empêche l'humain de gagner si, pour n'importe quelle case vide de la grille, elle ne génère pas un alignement de 4 pièces avec 1 caractéristique commune
                    while (ligne < nbreLignes && !alignement4Pieces)
                    {
                        while (col < nbreLignes && !alignement4Pieces)
                        {

                            if (Grille[ligne, col] == caseVide)
                            {
                                alignement4Pieces = MettreAJourStrategies(true, 3); // l'IA vérifie pour la case considérée qu'elle ne génère pas un alignement de 4 pièces
                            }

                            col++;
                        } 
                        if (col == nbreLignes) // l'IA a parcouru toutes les colonnes de la ligne i considérée sans trouver de case générant un alignement de 4 pièces avec 1 caractéristique commune
                        {
                            col = 0; // Et remet l'indice des colonnes à 0 pour toutes les parcourir de nouveau lorsqu'elle changera de ligne
                        }
                        if (ligne <nbreLignes)
                        ligne++;
                    } 


                    if (alignement4Pieces) // l'IA trouve une case désavantageuse pour elle-même soit une case qui engendre l'alignement de 4 pièces pour l'adversaire
                    {
                       // k++; //  Elle cherche donc une autre pièce
                        ligne = 0;
                        alignement4Pieces = false;
                    }

                    else // L'IA a parcouru toutes les cases du tableau sans que le placement de la pièce considérée puisse génére un alignement de 4 pièces
                    {
                        empecheVictoireHumain = true; // cette pièce empêche donc l'humain de gagner avec cette pièce si on la lui donne
                        piecesPossiblesIA[k] = choixPiece; // remplissage du tableau recensant toutes les pièces que peut jouer l'IA sans faire gagner l'adversaire
                    }

                    // Console.WriteLine("fin des 2 whiles i={0}, j= {1}", i, j);
                    if (trace)
                    Console.WriteLine("alignemt4pieces ={0}", alignement4Pieces);

                   
                }
            }

            for (int m = 0; m < piecesPossiblesIA.Length; m++)
            {
                //if (trace)
                Console.WriteLine("piecepossible = {0}", piecesPossiblesIA[m]);
            }

            if (!empecheVictoireHumain) // si le tableau recensant les pièces que l'IA peut jouer est nul
            {
                if (trace)
                Console.WriteLine("tableau de possibilités nul, choix aléatoire");
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
                // l'IA va maintenant regarder si elle ne peut pas trouver une pièce qui empêche l'humain de gagner mais qui peuvent en plus permettre à l'IA de gagner au prochain tour
                bool alignement3Pieces = false;
                bool potentielleVictoireIA = false;
               
                string[] piecesGagnantesIA = new string[piecesPossiblesIA.Length]; // tableau recensant toutes les pièces disponibles qui peuvent permettre à l'IA de gagner au prochain tour. 
                                                                                   // sa taille maximale est égale à la taille du tableau des pièces qui empêchent l'humain de gagner

                for (k = 0; k < piecesPossiblesIA.Length; k++) // choix d'une pièce parmi les pièces dispos
                {
                    ligne = 0; col = 0;
                    choixPiece = piecesPossiblesIA[k];
                    alignement3Pieces = false;

                    if (piecesPossiblesIA[k] != null)
                    {
                        Console.WriteLine("entre ds le if piecepossible non null, alignement3Pieces ={0}", alignement3Pieces);
                        //  l'IA parcourt tout le tableau pour voir si, parmi les pièces qui empêchent l'humain de gagner, certaines peuvent lui permettre de gagner. 
                        // Une pièce qui permettrait à l'IA de gagner serait une pièce qui, placée dans l'une des cases, générerait un alignement de 3 pièces
                        while (ligne < nbreLignes && !alignement3Pieces)
                        {
                            while (col < nbreLignes && !alignement3Pieces)
                            {
                                Console.WriteLine("entre ds le 2eme while piecepossible non null, ");

                                if (Grille[ligne, col] == caseVide)
                                {
                                    alignement3Pieces = MettreAJourStrategies(true, 2); // l'IA vérifie pour la case considérée qu'elle ne génère pas un alignement de 4 pièces
                                    Console.WriteLine("alignement3pieces={0}, ligne={1}, col={2}", alignement3Pieces, ligne, col);
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

                if (!alignement3Pieces) // l'IA a parcouru tout le tableau sans que la pièce considérée puisse générer un alignement de 3 pièces
                {
                    //k++; //  Elle cherche donc une autre pièce
                    ligne = 0;
                }

                for (int m = 0; m < piecesGagnantesIA.Length; m++)
                {
                        Console.WriteLine("piecesGagnantesIA.Length= {0}, piecesGagnantesIA = {1}", piecesGagnantesIA.Length, piecesGagnantesIA[m]);
                }

                if (potentielleVictoireIA) 
                {
                       
                    // l'IA choisit au hasard dans les pièces encore disponibles celle qu'elle donnera puisque de toute façon, si le tableau est nul, c'est qu'elle ne peut éviter que le joueur adverse gagne
                    int randomPiecesGagnantes;
                    Random R2 = new Random();
                    do
                    {
                            Console.WriteLine("pdt while random ds tableau  pièces gagnantes");
                        randomPiecesGagnantes = R2.Next(0, piecesGagnantesIA.Length); // [0,16] ou [0, 17] ?? car [0,17] provoque un outOfRange

                    }
                    while (piecesGagnantesIA[randomPiecesGagnantes] == null);

                    choixPiece = piecesPossiblesIA[randomPiecesGagnantes];

                }
                else // si le tableau recensant les pièces gagnantes pr l'IA est nul*/
                {
                    // L'IA choit au hasard la pièce qu'elle va donner à l'adversaire entre les pièces qui empêchent l'adversaire de gagner (afin que son choix ne soit pas prévisible)
                    int randomPiece;
                    Random R = new Random();

                    do
                    {    
                            Console.WriteLine("tableau de pièces gagnantes nul, choix aléatoire"); 
                        randomPiece = R.Next(0, piecesPossiblesIA.Length); 

                    }
                    while (piecesPossiblesIA[randomPiece] == null);

                    choixPiece = piecesPossiblesIA[randomPiece];
                }

            }
        }

        /// <summary>
        /// UtiliserPiece : Fonction permettant de ne jouer qu'une seule fois chaque pièce
        /// </summary>
        /// <param name="choixPiece"></param>
        static void UtiliserPiece()
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
        /// Gagner () : Fonction donnant toutes les combinaisons gagnantes et terminant la partie
        /// </summary>
        /// <returns></returns>
        static void Gagner()
        {
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
                        AGagne = true;
                    }
                }
            }

            //verif colonnes
            if (!AGagne)
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
                            AGagne = true; // une ligne de 4 pièces avec au moins 1 caractéristique commune a été complétée
                        }
                    }
                }
            }

            //vérif diago de la gauche vers la droite, haut vers bas
            if (!AGagne)
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
                        AGagne = true; // la diagonale décrite a été complétée avec 4 pièces ayant au moins 1 caractéristique commune 
                    }
                }
            }

            //vérif diago de la droite vers la gauche, du haut vers le bas
            if (!AGagne)
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
                        AGagne = true; // la diagonale décrite a été complétée avec 4 pièces ayant au moins 1 caractéristique commune 
                    }
                }
            }
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

            if (i == nbreLignes && j == nbreLignes)
                grilleRemplie = true;
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
            string reponse = Console.ReadLine(); //le joueur choisit s'il veut refaire une partie
            while (reponse != "r" && reponse != "a")
            {
                Console.WriteLine("saisissez [r] ou [a]");
                reponse = Console.ReadLine();
            }
            if (reponse == "r")
                return true;

            else
                return false;
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
        /// MettreAJourStrategies : l'IA calcule le nombre de pièces ayant 1 caractéristique commune sur une même ligne/col/diago
        /// </summary>
        /// <returns></returns>
        static bool MettreAJourStrategies(bool simul, int nbPiecesAlignees)  /* La fonction MettreAJour est appelée dans TrouverCase afin de s'assurer qu'après simulation du placement de la pièce dans la grille,
                                                                                  il n'y aurait pas 3 pièces alignées car cela permettrait au joueur humain de gagner au tour d'après

                                                                                                                     dans TrouverPiece afin de s'assurer qu'après simulation du placement de la pièce dans la grille,
                                                                                 il n'y aurait pas 4 pièces alignées (car cela permettrait au joueur humain de gagner en plaçant la pièce comme dans la simulation)

                                                                                                                       après chaque tour des joueurs mais dans ce cas, le mode simulation n'est pas activé donc 
                                                                                                                       on ne cherche pas à vérifier un nombre de pièces alignées d'où nbPiecesAlignees = 0*/
        {
            bool alignementPieces = false;

          

            for (int n = 0; n < nbreCaractéristiques; n++)
            {

                //Mise à jour lignes, colonnes, diagos
                if (trace)
                Console.WriteLine("juste avant if simul nieme carac =0, on a ligne={0}, col={1}, grille[ligne, col] ={2}, n={3}", ligne, col, Grille[ligne, col], n);
                if (choixPiece[n] == '0') //compteur du nombre de 0 de la n ième caractéristique sur la ligne considérée
                {
                   // Console.WriteLine("Ds fction MettreAJour if simul nieme carac =0, on a ligne={0}, col={1}, grille[ligne, col] ={2}, n={3}", ligne, col, Grille[ligne, col], n);
                    if (simul)
                    {
                        if (trace)
                        Console.WriteLine("entre ds if simul");
                        if (tablignes0[ligne, n] == nbPiecesAlignees || tabcol0[col, n] == nbPiecesAlignees)
                        {
                            alignementPieces = true;
                        }
                       /*if (tabcol0[col, n] == nbPiecesAlignees)
                        {
                            alignementPieces = true;
                        }*/
                        if (ligne == col && diago0 [0,n] == nbPiecesAlignees)
                        {
                            alignementPieces = true;
                        }
                        if (ligne == ((nbreLignes-1) -col) && diago0[1, n] == nbPiecesAlignees || col == ((nbreLignes - 1) - ligne) && diago0[1, n] == nbPiecesAlignees)
                        {
                            alignementPieces = true;
                        }
                    }
                    else
                    {
                        if (trace)
                        Console.WriteLine("entre ds else ");
                        tablignes0[ligne, n] ++;
                        tabcol0[col, n] ++;

                        if (ligne == col)
                            diago0[0, n]++;

                        else if (ligne == ((nbreLignes - 1) - col) || col == (nbreLignes - 1) - ligne)
                            diago0[1, n]++;
                    }
                }
                else
                {
                    if (simul)
                    {
                        if (trace)
                        Console.WriteLine("Ds fction MettreAJour if simul, nieme carac =1, on a ligne={0}, col={1}, grille[ligne, col] ={2}, n={3}", ligne, col, Grille[ligne, col], n);
                        if (tablignes1[ligne, n] == nbPiecesAlignees)
                        {
                            alignementPieces = true;
                        }
                        /*else*/ if (tabcol1[col, n] == nbPiecesAlignees)
                        {
                            alignementPieces = true;
                        }
                    }
                    else
                    {
                        tablignes1[ligne, n] ++; //compteur du nombre de 1 de la n ième caractéristique sur la ligne considérée
                        tabcol1[col, n] ++;

                        if (ligne == col)
                            diago1[0, n]++;

                        if (ligne == ((nbreLignes - 1) - col) || col == ((nbreLignes - 1) - ligne))
                            diago1[1, n]++;

                    }
                }
            }

            if (trace)
            {
                Console.WriteLine("lignes0");
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
                }

                Console.WriteLine("diago0");
                for (int i = 0; i < diago0.GetLength(0); i++) // afiche tabdiago0
                {
                    Console.WriteLine("");
                    for (int j = 0; j < diago0.GetLength(1); j++)
                        Console.Write(diago0[i, j] + "\t");
                }

                Console.WriteLine("diago1");
                for (int i = 0; i < diago1.GetLength(0); i++) // afiche tabdiago1
                {
                    Console.WriteLine("");
                    for (int j = 0; j < diago0.GetLength(1); j++)
                        Console.Write(diago1[i, j] + "\t");
                }

            }

            return alignementPieces;
        }

    }
}



