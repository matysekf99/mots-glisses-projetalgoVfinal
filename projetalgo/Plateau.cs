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
        public Plateau(string filename, int lignes, int colonnes)
        {
            this.plateaumat = new char[lignes, colonnes];
            ReadFile(filename);
        }

        public Plateau(string filename)
        {
            ReadFile(filename);
        }

        //méthodes
        /// <summary>
        /// lire le fichier et le convertir en matrice de characteres
        /// ou creer une matrice aleatoire
        /// </summary>
        /// <param name="filename"></param>
        void ReadFile(string filename)
        {
            if (filename != "Lettre.txt")
            {
                // Détermine les dimensions de la matrice
                if (!File.Exists(filename))
                {
                    Console.WriteLine("Erreur: LE fichier '" + filename + "' n'existe pas.");
                    return;
                }
                int rows = 0;
                int columns = 0;
                using (StreamReader sr1 = new StreamReader(filename))
                {
                    while (sr1.Peek() >= 0)
                    {
                        string line1 = sr1.ReadLine();
                        string[] values = line1.Split(';');

                        if (values != null && values.Length > 0)
                        {
                            rows++;
                            if (columns == 0)
                                columns = values.Length;
                            if (values.Length != columns)
                            {
                                Console.WriteLine("Error: Inconsistent number of columns in the file.");
                                return;
                            }
                        }
                    }
                }
                this.plateaumat = new char[rows, columns];
                using (StreamReader sr = new StreamReader(filename))
                {
                    int i = 0;
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();
                        string[] values = line.Split(';');
                        if (values != null && values.Length == columns)
                        {
                            for (int j = 0; j < columns; j++)
                            {
                                this.plateaumat[i, j] = char.ToUpper(char.Parse(values[j]));
                            }
                        }
                        i++;
                    }
                }
            }
            else if (filename == "Lettre.txt")
            {
                int n = 0;
                string[] lines = File.ReadAllLines(filename);
                List<char> ListeLettres = new List<char>();

                foreach (string line in lines)
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

        public int[,] SearchWord(string word)
        {
            int[,] solution = new int[plateaumat.GetLength(0), plateaumat.GetLength(1)];
            for (int j = 0; j < plateaumat.GetLength(1); j++)
            {
                bool isFound = Search(solution, word, plateaumat.GetLength(0) - 1, j, 0);
                if (isFound)
                    return solution;
            }

            return solution;
        }
        public List<Coordonees> GetValidMoves(int m, int n, int row, int column)
        {
            List<Coordonees> possibleMoves = new List<Coordonees>();
            Coordonees point = new Coordonees();
            // peut aller en haut
            if (row - 1 >= 0)
            {
                point.X = row - 1;
                point.Y = column;
                possibleMoves.Add(point);
            }
            // peut aller en bas
            if (row + 1 < m)
            {
                point.X = row + 1;
                point.Y = column;
                possibleMoves.Add(point);
            }
            //peut aller a gauche
            if (column - 1 >= 0)
            {
                point.X = row;
                point.Y = column - 1;
                possibleMoves.Add(point);
            }
            // peut aller a droite
            if (column + 1 < n)
            {
                point.X = row;
                point.Y = column + 1;
                possibleMoves.Add(point);
            }

            //ajout des diagonales

            //en haut à droite
            if (column + 1 < n && row - 1 >= 0)
            {
                point.X = row - 1;
                point.Y = column + 1;
                possibleMoves.Add(point);
            }
            //en bas à droite
            if (column + 1 < n && row + 1 < m)
            {
                point.X = row + 1;
                point.Y = column + 1;
                possibleMoves.Add(point);
            }
            //en bas à gauche
            if (column - 1 >= 0 && row + 1 < m)
            {
                point.X = row + 1;
                point.Y = column - 1;
                possibleMoves.Add(point);
            }

            //en haut à gauche
            if (column - 1 >= 0 && row - 1 >= 0)
            {
                point.X = row - 1;
                point.Y = column - 1;
                possibleMoves.Add(point);
            }

            return possibleMoves;
        }

        public bool Search(int[,] solution, string word, int row, int column, int pathLength)
        {
            if (word.Length == pathLength)
            {
                return true;
            }

            if (solution[row, column] != 0 || plateaumat[row, column] != word[pathLength])
            {
                return false;
            }

            pathLength++;
            solution[row, column] = pathLength;

            List<Coordonees> validMoves = GetValidMoves(plateaumat.GetLength(0), plateaumat.GetLength(1), row, column);
            foreach (Coordonees point in validMoves)
            {
                if (Search(solution, word, point.X, point.Y, pathLength))
                    return true;
            }

            solution[row, column] = 0;
            return false;
        }

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

        public void Maj_Plateau(List<Coordonees> coordmot)
        {
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
            bool deplace = false;
            //se deplace comme dans un repere en maths donc origine en bas a gauche avec x=i et y=j. Pour chaque
            // colonne remonte la ligne et actualise les espaces vides avec la valeur non vide au dessus.
            for (int i = 0; i < plateaumat.GetLength(1); i++)
            {
                for (int j = plateaumat.GetLength(0) - 1; j > 0; j--)
                {
                    if (plateaumat[j, i] == ' ')
                    {
                        int t = j - 1;
                        while (!deplace && t >= 0)
                        {
                            if (plateaumat[t, i] != ' ')
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

        /// <summary>
        /// utile que pour les tests mais ne sert a rien dans le programme principal
        /// </summary>
        /// <param name="matrice"></param>
        public static void AfficherMatrice(char[,] matrice)
        {
            int lignes = matrice.GetLength(0);
            int colonnes = matrice.GetLength(1);

            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    Console.Write(matrice[i, j] + " ");
                }
                Console.WriteLine(); // Passer à la ligne après chaque ligne de la matrice
            }
        }


        /// <summary>
        /// affiche une liste de coordonnées
        /// </summary>
        /// <param name="l"></param>
        public static void afficheListecoordonne(List<Coordonees> l)
        {
            if (l == null || l.Count == 0)
            {
                Console.WriteLine("null");
            }
            else
            {
                foreach (Coordonees item in l)
                {
                    Console.WriteLine("X : " + item.X + " Y : " + item.Y);
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