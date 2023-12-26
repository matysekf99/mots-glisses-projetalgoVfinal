using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using static projetalgo.Plateau;
namespace projetalgo
{
    public class Jeu
    {
        // attributs
        Dictionnaire dictionnaire;
        Plateau plateau;
        Joueur joueur1;
        Joueur joueur2;
        Minuteur minuteurJoueur1;
        Minuteur minuteurJoueur2; 

        // constructeur
        public Jeu(Dictionnaire dictionnaire, Plateau plateau, Joueur joueur1, Joueur joueur2, int TempsMinutes)
        {
            this.dictionnaire = dictionnaire;
            this.plateau = plateau;
            this.joueur1 = joueur1;
            this.joueur2 = joueur2;
            this.minuteurJoueur1 = new Minuteur(TimeSpan.FromMinutes(TempsMinutes));
            this.minuteurJoueur2 = new Minuteur(TimeSpan.FromMinutes(TempsMinutes));
        }

        /// <summary>
        /// fonction qui permet de faire une partie de mots glissés
        /// </summary>

        public void play()
        {
            dictionnaire.Tri_XXX();
            minuteurJoueur1.Demarrer();
            minuteurJoueur2.Demarrer();

            while (!minuteurJoueur1.EstTermine() || !minuteurJoueur2.EstTermine())
            {
                minuteurJoueur1.MettreEnPause();
                minuteurJoueur2.MettreEnPause();
                if (!minuteurJoueur1.EstTermine())
                {
                    minuteurJoueur1.Reprendre();
                    TourDeJeu(joueur1, minuteurJoueur1);
                    minuteurJoueur1.MettreAJour();
                    minuteurJoueur1.MettreEnPause();
                }
                if (minuteurJoueur1.EstTermine())
                {
                    Console.Clear();
                }
                if (!minuteurJoueur2.EstTermine())
                {
                    minuteurJoueur2.Reprendre();
                    TourDeJeu(joueur2, minuteurJoueur2);
                    minuteurJoueur2.MettreAJour();
                    minuteurJoueur2.MettreEnPause();
                }
                if (minuteurJoueur2.EstTermine())
                {
                    Console.Clear();
                }

            }
        }

        /// <summary>
        /// Fonction qui permet de faire un tour de jeu
        /// </summary>
        /// <param name="joueur"></param>
        /// <param name="minuteur"></param>

        public void TourDeJeu(Joueur joueur, Minuteur minuteur)
        {
            Console.WriteLine(joueur1.toString());
            Console.WriteLine($"Temps restant : {minuteurJoueur1.TempsRestant().ToString(@"mm\:ss")}");
            Console.WriteLine(joueur2.toString());
            Console.WriteLine($"Temps restant : {minuteurJoueur2.TempsRestant().ToString(@"mm\:ss")}");
            if (joueur.Nom == joueur1.Nom)
            {
                Console.ForegroundColor = ConsoleColor.Blue;  // affiche les prochains textes en bleu
            }
            else if (joueur.Nom == joueur2.Nom)
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // affiche les prochains textes en jaune
            }
            Console.WriteLine("Tour de " + joueur.Nom);
            Console.ResetColor(); // Met les prochains textes en couleur d'origine
            Console.WriteLine(plateau.toString());
            bool existe = false;
            string mot = null;
            do
            {
                mot = Console.ReadLine();
                existe = dictionnaire.RechDichoRecursif(mot, 0, dictionnaire.Dico.Count);
                if (!existe)
                {
                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine("Le mot n'est pas dans le dictionnaire");
                    Console.ResetColor();
                }
            }
            while (!existe);

            int[,] solution = plateau.ChercheMot(mot.ToUpper());
            bool motdanslamtrice = false;
            for (int i = 0; i < solution.GetLength(0); i++)
            {
                for (int j = 0; j < solution.GetLength(1); j++)
                {
                    if (solution[i, j] != 0)
                    {
                        motdanslamtrice = true;
                    }
                }
            }
            if (!motdanslamtrice)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Le mot n'est pas dans la matrice");
                Thread.Sleep(750);
                Console.ResetColor();
                Console.Clear();

            }
            else if (motdanslamtrice && !minuteur.EstTermine() && !joueur.Contient(mot))
            {
                joueur.Add_Mot(mot);
                joueur.Add_Score(pointMot(mot));
                List<Coordonees> listesoltion = Plateau.ConvertirSolutionenListe(solution);
                Console.Clear();
                Console.WriteLine(joueur1.toString());
                Console.WriteLine($"Temps restant : {minuteurJoueur1.TempsRestant().ToString(@"mm\:ss")}");
                Console.WriteLine(joueur2.toString());
                Console.WriteLine($"Temps restant : {minuteurJoueur2.TempsRestant().ToString(@"mm\:ss")}");
                if (joueur.Nom == joueur1.Nom)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                else if (joueur.Nom == joueur2.Nom)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.WriteLine("Tour de " + joueur.Nom + "\n");
                Console.ResetColor();
                AppliquerCouleur(joueur, solution);
                plateau.Maj_Plateau(listesoltion);
            }
        }

        /// <summary>
        /// Calcul les points du mot entré
        /// </summary>
        /// <param name="mot"></param>
        /// <returns></returns>

        public int pointMot(string mot)
        {
            Dictionary<char, int> lettresPoints = new Dictionary<char, int>();
            string[] lines = File.ReadAllLines("lettre.txt");

            foreach (string line in lines)
            {
                string[] values = line.Split(',');

                if (values != null && values.Length == 3 && values[2] != null)
                {
                    char lettre = char.Parse(values[0]);
                    int nombrePoints = int.Parse(values[2]);
                    lettresPoints[lettre] = nombrePoints;
                }
            }
            int scoreTotal = 0;

            foreach (char lettre in mot.ToUpper())
            {
                if (lettresPoints.ContainsKey(lettre))
                {
                    scoreTotal += lettresPoints[lettre];
                }
            }

            return scoreTotal + mot.Length;
        }

        /// <summary>
        /// Affiche le mot entré en vert dans la matrice
        /// </summary>
        /// <param name="joueur"></param>
        /// <param name="solution"></param>

        public void AppliquerCouleur(Joueur joueur, int[,] solution)
        {
            Console.Clear();
            Console.WriteLine(joueur1.toString());
            Console.WriteLine($"Temps restant : {minuteurJoueur1.TempsRestant().ToString(@"mm\:ss")}");
            Console.WriteLine(joueur2.toString());
            Console.WriteLine($"Temps restant : {minuteurJoueur2.TempsRestant().ToString(@"mm\:ss")}");
            if (joueur.Nom == joueur1.Nom)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (joueur.Nom == joueur2.Nom)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine("Tour de " + joueur.Nom + "\n");
            Console.ResetColor();

            Console.Write("┌");
            for (int r = 0; r < solution.GetLength(1) - 1; r++)
            {
                Console.Write("───┬");
            }
            Console.WriteLine("───┐");
            for (int i = 0; i < solution.GetLength(0); i++)
            {



                for (int j = 0; j < solution.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        Console.Write("│");
                    }
                    if (solution[i, j] > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.Write(" " + plateau.Plateaumat[i, j]);
                    Console.ResetColor();
                    Console.Write(" │");
                }

                Console.WriteLine();
                if (i < solution.GetLength(0) - 1)
                {
                    for (int r = 0; r < plateau.Plateaumat.GetLength(1) - 1; r++)
                    {
                        if (r == 0)
                        {
                            Console.Write("├");
                        }
                        Console.Write("───┼");
                    }

                    Console.Write("───┤\n");
                }
                else
                {
                    for (int r = 0; r < plateau.Plateaumat.GetLength(1) - 1; r++)
                    {
                        if (r == 0)
                        {
                            Console.Write("└");
                        }
                        Console.Write("───┴");
                    }

                    Console.Write("───┘\n");
                }


            }
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}