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
        static int nbPiecesTotales = 16;
        // tableau des pièces avec deuxième ligne servant à indiquer ou non la présence de la pièce sur la grille de jeu
        static string[,] TabPieces = new string[,] { { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" }, { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" } };
        static string[,] Grille;    // Grille de jeu
        static string caseVide = "    "; //pourquoi ça nous souligne les 2 premières lettres?
        static string ChoixPiece;
        static int ligne; static int col;
        static int nbreLignes = 4;
        static int nbreCaractéristiques = 4;
        static void ChoixIntelligentpiece()
        {
            //faire cas où il y en a 3 pareil dans une colonne ou ligne ou diagonale et quil reste une case vide donc pas alignés mais ne pas choisir pièce avec ce caractère quand même
            //choix pièce:
            bool aligne =false; //bool qui nous dit si il a trouvé 3 pièces alignés de caractère identique
            int i; //indice lignes
            int j; //indice colonnes
            int n; //indice des 4 caractéristiques de la pièce

            //verif lignes
            for (i = 0; i < 4; i++) //indice ligne
            {
                for (n = 0; n < 4; n++) //test pour chaque caractéristique(x4)
                {
                    j = 0;
                    while (j < 4 && Grille[i, 0] != caseVide && Grille[i, 0][n] == Grille[i, j][n]) //qd caractéristique commune, on compare la valeur de départ
                    {
                        j++;
                    }
                    if (j == 3)
                    {

                        //choisir pièce avec[n] != [n] de j = 3
                        //  VerifierSiPieceUtilisee()
                        //if (verifiersipieceutilisee) alignee=true;
                        //else alignee=false
                    }
                }
            }

            //verif colonnes
            if (!aligne)
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
                        if (i == 3)
                        {

                            //choisir pièce avec[n] != [n] de i = 3
                            //  VerifierSiPieceUtilisee()
                            //if (verifiersipieceutilisee) alignee=true;
                            //else alignee=false
                        }
                    }
                }
            }

            //vérif diago de la gauche vers la droite, haut vers bas
            if (!aligne)
            {
                for (n = 0; n < 4; n++)
                {
                    i = 1;
                    while (i < 4 && Grille[0, 0] != caseVide && Grille[0, 0][n] == Grille[i, i][n])
                    {
                        i++;
                    }
                    if (i == 3)
                    {
                        //choisir pièce avec[n] != [n] de i = 3
                        //  VerifierSiPieceUtilisee()
                        //if (verifiersipieceutilisee) alignee=true;
                        //else alignee=false
                    }
                }
            }

            //vérif diago de la droite vers la gauche, du haut vers le bas
            if (!aligne)
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
                    if (i == 3)
                    {
                        //choisir pièce avec[n] != [n] de i = 3
                        //  VerifierSiPieceUtilisee()
                        //if (verifiersipieceutilisee) alignee=true;
                        //else alignee=false
                    }
                }
            }

        }

        static void ChoixIntelligentCase()
        {
            //faire cas où il y en a 3 pareil dans une colonne ou ligne ou diagonale et quil reste une case vide donc les 3 ne sont pas alignés à la suite (mettre dedans)

            bool aligne = false; //bool qui nous dit si il a trouvé 3 pièces alignés de caractère identique
            int i; //indice lignes
            int j; //indice colonnes
            int n; //indice des 4 caractéristiques de la pièce

            //verif lignes
            for (i = 0; i < 4; i++) //indice ligne
            {
                for (n = 0; n < 4; n++) //test pour chaque carcatéristique(x4)
                {
                    j = 0;
                    while (j < 3 && Grille[i, 0] != caseVide && Grille[i, 0][n] == Grille[i, j][n]) //qd caractéristique commune, on compare la valeur de départ
                    {
                        j++;
                    }
                    if (j == 3)
                    {
                        //Grille[i,mettre dans la case vide de la colonne j)]=ChoixPiece;
                    }
                }
            }

            //verif colonnes
            if (!aligne)
            {
                for (j = 0; j < 4; j++)
                {
                    for (n = 0; n < 4; n++)
                    {
                        i = 0;
                        while (i < 3 && Grille[0, j] != caseVide && Grille[0, j][n] == Grille[i, j][n])
                        {
                            i++;
                        }
                        if (i == 3)
                        {
                            //Grille[mettre dans la case vide de la ligne i,j)]=ChoixPiece;
                        }
                    }
                }
            }

            //vérif diago de la gauche vers la droite, haut vers bas
            if (!aligne)
            {
                for (n = 0; n < 4; n++)
                {
                    i = 1;
                    while (i < 3 && Grille[0, 0] != caseVide && Grille[0, 0][n] == Grille[i, i][n])
                    {
                        i++;
                    }
                    if (i == 3)
                    {
                        //Grille[mettre dans la case vide de la ligne i,j)]=ChoixPiece;
                    }
                }
            }

            //vérif diago de la droite vers la gauche, du haut vers le bas
            if (!aligne)
            {
                for (n = 0; n < 4; n++)
                {
                    // Coordonnées (i, j) de la 1ere case que je compare
                    i = 1;
                    j = 2;
                    while (i < 3 && j >= 0 && Grille[1, 3] != caseVide && Grille[1, 3][n] == Grille[i, j][n])
                    {
                        i++;
                        j--;
                    }
                    if (i == 3)
                    {
                        //Grille[mettre dans la case vide de la ligne i,j)]=ChoixPiece;
                    }
                }
            }


        }

    
        static bool Continuer()
        {
            Console.WriteLine("Tapez [r] pour rejour ou [a] pour arrêter");
            string rejouer = Console.ReadLine();
            bool continuer=true;
            while (rejouer != "r" && rejouer != "a")
            {
                Console.WriteLine("saisissez [r] ou [a]");
                rejouer = Console.ReadLine();
            }
            if (rejouer == "r")
                continuer = true;
            if (rejouer == "a")
                continuer = false;
            return continuer;
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
                    Console.Write(" "+ Grille[i, j] + " |");
                }
                Console.Write("\n");// sauter une ligne pour mettre la barre entre chaque case
                Console.WriteLine("      |      |      |      |      |");
            }

            Console.WriteLine("      +------+------+------+------+");
            Console.WriteLine("         0      1      2      3");
        }
        static void piece1()
        {

            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine("**************");
            }
            Console.ResetColor();
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

        static void ChoixIntell2 ()
        {
            int i = 0;
            int j = 0;
            int n = 0;
            //trouver une case non vide
            while (i < Grille.GetLength(1) && !AvoirCaracCommuneIA(i,j) && !AvoirCaseJouableIA(i,j))
            {
                while (j < Grille.GetLength(1) && !AvoirCaracCommuneIA(i,j) && !AvoirCaseJouableIA (i,j) && Grille [i,j]==caseVide)
                {

                    j++;
                }

                if (!AvoirCaseJouableIA(i, j))
                    i++;
            }

            if (AvoirCaseJouableIA(i, j))
            {
                Grille[ligne, col] = ChoixPiece;

            }

        }

        static bool AvoirCaracCommuneIA(int i, int j)
        {

            bool caracCommune = false;
            int n = 0;
            while (n < nbreCaractéristiques && Grille[i, j][n] != ChoixPiece[n])
            {
                n++;
            }

            if (n != 0 && n!=4)
                caracCommune = true;

            return caracCommune;
        }

        static bool AvoirCaseJouableIA (int i, int j)
        {
            bool caseJouable = false;

            // A Gauche
            if (!AvoirCaseRemplie(i, j - 1))
            {
                ligne = i;
                col = j - 1;
                caseJouable = true;
            }
            // A Droite
            else if (!AvoirCaseRemplie(i, j + 1))
            {
                ligne = i;
                col = j + 1;
                caseJouable = true;
            }
            // En Haut
            else if (!AvoirCaseRemplie(i, j + 1))
            {
                ligne = i;
                col = j + 1;
                caseJouable = true;
            }
            // En Bas
            else if (!AvoirCaseRemplie(i - 1, j))
            {
                ligne = i - 1;
                col = j;
                caseJouable = true;
            }
            //En haut
            else if (!AvoirCaseRemplie(i + 1, j))
            {
                ligne = i + 1;
                col = j;
                caseJouable = true;
            }
            //Diago Haut Droite
            else if (!AvoirCaseRemplie(i - 1, j + 1))
            {
                ligne = i - 1;
                col = j + 1;
                caseJouable = true;
            }
            //Diago Haut Gauche
            else if (!AvoirCaseRemplie(i - 1, j - 1))
            {
                ligne = i - 1;
                col = j - 1;
                caseJouable = true;
            }
            //Diago Bas Droite
            else if (!AvoirCaseRemplie(i + 1, j + 1))
            {
                ligne = i + 1;
                col = j + 1;
                caseJouable = true;
            }
            //Diago Bas Gauche
            else if (!AvoirCaseRemplie(i + 1, j - 1))
            {
                ligne = i + 1;
                col = j - 1;
                caseJouable = true;
            }

            return caseJouable;
        }

        static void Main(string[] args)
        {
            string[,] grille = new string[4, 4];
        }


    }
}
 