using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO.Enumeration;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static projetalgo.Plateau;


namespace projetalgo
{
    public class Plateau
    {
        //attributs
        char[,] plateaumat;

        //constructeur
        public Plateau(string nomfichier, int lignes, int colonnes)
        {
            this.plateaumat = new char[lignes, colonnes];
            ReadFile(nomfichier);
        }

        public Plateau(string nomfichier)
        {
            ReadFile(nomfichier);
        }

        //méthodes
        /// <summary>
        /// lire le fichier et le convertir en matrice de characteres
        /// ou creer une matrice aleatoire
        /// </summary>
        /// <param name="nomfichier"></param>
        void ReadFile(string nomfichier)
        {
            if (nomfichier != "Lettre.txt")
            {
                // Détermine les dimensions de la matrice
                if (!File.Exists(nomfichier))
                {
                    Console.WriteLine("Erreur: Le fichier '" + nomfichier + "' n'existe pas.");
                    return;
                }
                int lignes = 0;
                int colonnes = 0;
                using (StreamReader sr1 = new StreamReader(nomfichier))
                {
                    while (sr1.Peek() >= 0)
                    {
                        string line1 = sr1.ReadLine();
                        string[] values = line1.Split(';');

                        if (values != null && values.Length > 0)
                        {
                            lignes++;
                            if (colonnes == 0)
                            {
                                colonnes = values.Length;
                            }
                        }
                    }
                }
                this.plateaumat = new char[lignes, colonnes];
                using (StreamReader sr = new StreamReader(nomfichier))
                {
                    int i = 0;
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        string[] values = line.Split(';');
                        if (values != null && values.Length == colonnes)
                        {
                            for (int j = 0; j < colonnes; j++)
                            {
                                this.plateaumat[i, j] = char.ToUpper(char.Parse(values[j]));
                            }
                        }
                        i++;
                    }
                }
            }
            else if (nomfichier == "Lettre.txt")
            {
                int n = 0;
                string[] lignesfichier = File.ReadAllLines(nomfichier);
                List<char> ListeLettres = new List<char>();

                foreach (string line in lignesfichier)
                {
                    string[] values = line.Split(',');
                    if (values != null && values.Length == 3 && values[1] != null)
                    {
                        for (int j = 0; j < Convert.ToInt32(values[1]); j++)
                        {
                            ListeLettres.Add(char.Parse(values[0]));
                        }
                    }
                }
                Melanger(ListeLettres);
                for (int i = 0; i < plateaumat.GetLength(0); i++)
                {
                    for (int j = 0; j < plateaumat.GetLength(1); j++)
                    {
                        this.plateaumat[i, j] = ListeLettres[i + j + n];
                    }
                    n += plateaumat.GetLength(1);
                }
            }
        }

        /// <summary>
        /// melange la liste pour creer une matrice
        /// </summary>
        /// <param name="li"></param>
        static void Melanger(List<char> li)
        {
            char temp;
            Random random = new Random();
            int n = 0;
            for (int i = 0; i < li.Count; i++)
            {
                for (int j = i; j < li.Count; j++)
                {
                    n = random.Next(0, li.Count);
                    temp = li[n];
                    li[n] = li[i];
                    li[i] = temp;
                }
            }
        }

        /// <summary>
        /// affiche la liste pour tester le code
        /// </summary>
        /// <param name="liste"></param>
        static void AfficherListe(List<char> liste)
        {
            Console.WriteLine("Contenu de la liste :");
            foreach (var element in liste)
            {
                Console.WriteLine(element);
            }
        }

        /// <summary>
        /// affiche le plateau
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            string retour = "";
            if (plateaumat == null)
            {
                return null;
            }
            retour += "┌";
            for (int r = 0; r < plateaumat.GetLength(1) - 1; r++)
            {
                retour += "───┬";
            }
            retour += "───┐\n";
            for (int i = 0; i < plateaumat.GetLength(0); i++)
            {
                retour += "│";
                for (int j = 0; j < plateaumat.GetLength(1); j++)
                {
                    retour += " " + plateaumat[i, j] + " │";
                }
                retour += "\n";


                if (i == plateaumat.GetLength(0) - 1)
                {
                    retour += "└";
                    for (int r = 0; r < plateaumat.GetLength(1) - 1; r++)
                    {
                        retour += "───┴";
                    }
                }
                else if (i != plateaumat.GetLength(0) - 1)
                {
                    retour += "├";
                    for (int r = 0; r < plateaumat.GetLength(1) - 1; r++)
                    {
                        retour += "───┼";
                    }
                }
                if (i == plateaumat.GetLength(0) - 1)
                {
                    retour += "───┘\n";
                }
                else
                {

                    retour += "───┤\n";
                }
            }
            return "\n" + retour;
        }

        /// <summary>
        /// creer un fichier d'un plateau
        /// </summary>
        /// <param name="filename"></param>
        public void WriteFile(string filename)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    for (int i = 0; i < plateaumat.GetLength(0); i++)
                    {
                        for (int j = 0; j < plateaumat.GetLength(1); j++)
                        {
                            sw.Write(plateaumat[i, j]);
                            if (j < plateaumat.GetLength(1) - 1)
                            {
                                sw.Write(",");
                            }
                        }
                        sw.WriteLine();
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Le fichier CSV a été créé avec succès.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erreur lors de la création du fichier CSV : " + ex.Message);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// parcourt la base de la matrice pour chercher le mot
        /// Renvoie une matrice d'entier avec le chemin de mot
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public int[,] ChercheMot(string word)
        {
            int[,] solution = new int[plateaumat.GetLength(0), plateaumat.GetLength(1)];
            for (int j = 0; j < plateaumat.GetLength(1); j++)
            {
                bool isFound = Cherche(solution, word, plateaumat.GetLength(0) - 1, j, 0);
                if (isFound)
                    return solution;
            }

            return solution;
        }

        /// <summary>
        /// retourne une liste de tous les mouvement possibles autour d'une case
        /// </summary>
        /// <param name="m">dimension verticale de la matrice</param>
        /// <param name="n">dimension horizontale de la matrice</param>
        /// <param name="ligne">coordonnée verticale de la matrice</param>
        /// <param name="colonne">coordonnée horizontale de la matrice</param>
        /// <returns></returns>
        public List<Coordonees> Mouvementspossibles(int m, int n, int ligne, int colonne)
        {
            List<Coordonees> listeMouvements = new List<Coordonees>();
            Coordonees point = new Coordonees();
            // peut aller en haut
            if (ligne - 1 >= 0)
            {
                point.X = ligne - 1;
                point.Y = colonne;
                listeMouvements.Add(point);
            }
            // peut aller en bas
            if (ligne + 1 < m)
            {
                point.X = ligne + 1;
                point.Y = colonne;
                listeMouvements.Add(point);
            }
            //peut aller a gauche
            if (colonne - 1 >= 0)
            {
                point.X = ligne;
                point.Y = colonne - 1;
                listeMouvements.Add(point);
            }
            // peut aller a droite
            if (colonne + 1 < n)
            {
                point.X = ligne;
                point.Y = colonne + 1;
                listeMouvements.Add(point);
            }

            //diagonales

            //en haut à droite
            if (colonne + 1 < n && ligne - 1 >= 0)
            {
                point.X = ligne - 1;
                point.Y = colonne + 1;
                listeMouvements.Add(point);
            }
            //en bas à droite
            if (colonne + 1 < n && ligne + 1 < m)
            {
                point.X = ligne + 1;
                point.Y = colonne + 1;
                listeMouvements.Add(point);
            }
            //en bas à gauche
            if (colonne - 1 >= 0 && ligne + 1 < m)
            {
                point.X = ligne + 1;
                point.Y = colonne - 1;
                listeMouvements.Add(point);
            }

            //en haut à gauche
            if (colonne - 1 >= 0 && ligne - 1 >= 0)
            {
                point.X = ligne - 1;
                point.Y = colonne - 1;
                listeMouvements.Add(point);
            }

            return listeMouvements;
        }

        /// <summary>
        /// methode recursive qui retourne un booleen;
        /// </summary>
        /// <param name="solution"></param>
        /// <param name="mot"></param>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool Cherche(int[,] solution, string mot, int ligne, int colonne, int index)
        {
            //condition de sortie si le mot est dans la matrice
            if (mot.Length == index)
            {
                return true;
            }
            //condition 2 de sortie si le mot n'est pas dans la matrice
            // solution[ligne, colonne] != 0 --> sert à ne pas retourner sur une case deja utilisé pour completer le mot
            if (solution[ligne, colonne] != 0 || plateaumat[ligne, colonne] != mot[index])
            {
                return false;
            }
            //si on est arrivé ici, la lettre dans case actuelle = mot[index]
            //commence le chemin dans la matrice solution
            index++;
            solution[ligne, colonne] = index;

            //recupere une liste de tous les mouvements possible
            List<Coordonees> listePointsPossible = Mouvementspossibles(plateaumat.GetLength(0), plateaumat.GetLength(1), ligne, colonne);
            foreach (Coordonees point in listePointsPossible)
            {
                //appel de la fonction pour tester chaque case autour
                if (Cherche(solution, mot, point.X, point.Y, index))
                    return true;
            }
            //si la lettre autour ne correspond pas a celle du mot alors remettre la case à 0.
            solution[ligne, colonne] = 0;
            return false;
        }


        /// <summary>
        /// convertit la matricce solution en liste de coordonnées
        /// methode utilisée dans jeu pour creer la liste passée en parametre de la methode maj_plateau
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        public static List<Coordonees> ConvertirSolutionenListe(int[,] solution)
        {
            List<Coordonees> retour = new List<Coordonees>();
            Coordonees point = new Coordonees();
            for (int i = 0; i < solution.GetLength(0); i++)
            {
                for (int j = 0; j < solution.GetLength(1); j++)
                {
                    if (solution[i, j] != 0)
                    {
                        point.X = i;
                        point.Y = j;
                        retour.Add(point);
                    }
                }
            }
            return retour;
        }

        /// <summary>
        /// actualise le plateau en fonction du mot trouvé
        /// </summary>
        /// <param name="coordmot"></param>
        public void Maj_Plateau(List<Coordonees> coordmot)
        {
            //1ere partie : remplace les cases du mot trouvé par des espaces
            int index = 0;
            for (int i = 0; i < plateaumat.GetLength(0); i++)
            {
                for (int j = 0; j < plateaumat.GetLength(1); j++)
                {
                    if (index == coordmot.Count)
                    {
                        break;
                    }
                    if (i == coordmot[index].X && j == coordmot[index].Y)
                    {
                        index++;
                        plateaumat[i, j] = ' ';
                    }
                }
                if (index == coordmot.Count)
                {
                    break;
                }
            }
            //se deplace comme dans un repere en maths donc origine en bas a gauche avec x=i et y=j. Pour chaque
            // colonne remonte la ligne et actualise les espaces vides avec la valeur non vide au dessus.
            bool deplace = false;
            for (int i = 0; i < plateaumat.GetLength(1); i++)//de gauche à droite
            {
                for (int j = plateaumat.GetLength(0) - 1; j > 0; j--)//de bas en haut
                {
                    if (plateaumat[j, i] == ' ')
                    {
                        int t = j - 1;//commence une case au dessus d'ou le j-1. Ex: si la case vide est 7;1, alors on s'interesse à 6;1 puis 5;1 ,...
                        while (!deplace && t >= 0)//echange la case vide avec la case non vide en hauteur la plus proche
                        {
                            if (plateaumat[t, i] != ' ')//detecte la case vide en hauteur la plus proche
                            {
                                plateaumat[j, i] = plateaumat[t, i];
                                plateaumat[t, i] = ' ';
                                deplace = true;
                            }
                            t--;
                        }
                        deplace = false;
                    }

                }
            }
        }


        //proprites
        public char[,] Plateaumat
        {
            get { return plateaumat; }
        }


        //structures
        /// <summary>
        /// structure de coordonées pour ne pas avoir à utiliser des tableaux de tableaux
        /// </summary>
        public struct Coordonees
        {
            public int X;
            public int Y;
            public Coordonees(int X, int Y)
            {
                this.X = X;
                this.Y = Y;
            }
        }
    }
}