using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace projetalgo
{
    public class Dictionnaire
    {
        //attribut
        List<string> dico;


        //constructeur
        public Dictionnaire(string filename)
        {
            dico = new List<string>();
            ReadFile(filename);
        }

        /// <summary>
        /// methode qui convertit le fichier dictionnaire en liste
        /// </summary>
        /// <param name="file"></param>
        void ReadFile(string file)
        {
            StreamReader sr = new StreamReader(file);
            string line;
            while (sr.Peek() >= 0)
            {
                line = sr.ReadLine();
                String[] mots = line.Split(' ');
                for (int i = 0; i < mots.Length; i++)
                {
                    dico.Add(mots[i]);
                }
            }
            sr.Close();
        }


        /// <summary>
        /// methode qui affiche le nombre de mots par lettre
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            string retour = "";
            string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            int j = 0;
            for (int i = 0; i < alphabet.Length; i++)
            {
                int compteur = 0;
                string lettre = alphabet[i];
                while (dico[j][0] == Convert.ToChar(lettre) && j < dico.Count - 1)
                {
                    compteur++;
                    j++;
                }
                retour += lettre + " : " + compteur + "\n";
                compteur = 0;
            }
            return "C'est un dictionnaire francais. Nombre de mots par lettre : \n" + retour;
        }

        /// <summary>
        /// methode qui affiche le dictionnaire
        /// cette methode sert juste à verfier les autres methode. Elle n'apporte rien au programme
        /// </summary>
        /// <param name="liste"></param>
        public static void AfficherListe(List<string> liste)
        {
            Console.WriteLine("Contenu du dictionnaire :");
            foreach (string element in liste)
            {
                Console.WriteLine(element.ToLower());
            }
        }

        /// <summary>
        /// methode recursive qui recherche un mot avec une approche dichotomique
        /// la methode divise le tableau en 2 et cherche dans le tableau ou l'element pourrait se trouver
        /// LA LISTE DOIT ETRE TRIEE POUR QUE LA METHODE PUISSE FONCTIONNER
        /// </summary>
        /// <param name="mot"></param>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        /// <returns></returns>
        public bool RechDichoRecursif(string mot, int debut, int fin)
        {
            int millieu = (debut + fin) / 2;
            if (debut > fin || dico == null)
            {
                return false;
            }
            else if (mot.ToLower() == dico[millieu].ToLower())
            {
                return true;
            }
            else if (string.Compare(mot, dico[millieu]) > 0)
            {
                return RechDichoRecursif(mot, millieu + 1, fin);
            }

            else
            {
                return RechDichoRecursif(mot, debut, millieu - 1);
            }
        }

        /// <summary>
        /// methode d'instance qui trie le tbleau avec la methode qui le tableau que s'il n'est pas trié
        /// </summary>
        public void Tri_XXX()
        {
            if (!EstTriee(dico))
            {
                QuickSort(dico, 0, dico.Count - 1);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="liste"></param>
        /// <param name="gauche"></param>
        /// <param name="droite"></param>
        public void QuickSort(List<string> liste, int gauche, int droite)
        {
            if (gauche < droite)
            {
                int pivot = Partition(liste, gauche, droite);
                if (pivot > gauche)
                {
                    QuickSort(liste, gauche, pivot - 1);
                }
                if (pivot < droite)
                {
                    QuickSort(liste, pivot + 1, droite);
                }
            }
        }


        public int Partition(List<string> liste, int gauche, int droite)
        {
            string pivot = liste[droite];
            int i = gauche - 1;

            for (int j = gauche; j < droite; j++)
            {
                if (liste[j].CompareTo(pivot) <= 0)
                {
                    i++;
                    string temp = liste[i];
                    liste[i] = liste[j];
                    liste[j] = temp;
                }
            }

            string temp1 = liste[i + 1];
            liste[i + 1] = liste[droite];
            liste[droite] = temp1;

            return i + 1;
        }

        /// <summary>
        /// verifie si la liste est deja triée
        /// </summary>
        /// <param name="liste"></param>
        /// <returns></returns>
        public bool EstTriee(List<string> liste)
        {
            for (int i = 1; i < liste.Count; i++)
            {
                if (string.Compare(liste[i - 1], liste[i]) > 0)
                {
                    return false;
                }
            }
            return true;
        }


        //proprieté
        public List<string> Dico
        {
            get { return dico; }
        }
    }
}