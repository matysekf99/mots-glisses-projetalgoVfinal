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
        public Dictionnaire(string nomfichier)
        {
            dico = new List<string>();
            ReadFile(nomfichier);
        }

        /// <summary>
        /// methode qui convertit le fichier dictionnaire en liste
        /// </summary>
        /// <param name="file"></param>
        void ReadFile(string nomFichier)
        {
            try
            {
                using (StreamReader sr = new StreamReader(nomFichier))
                {
                    string ligne;
                    while ((ligne = sr.ReadLine()) != null)
                    {
                        string[] mots = ligne.Split(' ');
                        foreach (string mot in mots)
                        {
                            if (!string.IsNullOrWhiteSpace(mot))//vérifie si la chaîne de caractères mot n'est pas nulle, vide ou composée uniquement d'espaces
                            {
                                dico.Add(mot);
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Erreur : Fichier non trouvé - {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erreur d'entrée/sortie - {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite - {ex.Message}");
            }
        }



        /// <summary>
        /// methode qui affiche le nombre de mots par lettre
        /// </summary>
        /// <returns></returns>
        public string toString()
        {
            Dictionary<char, int> compteurLettres = new Dictionary<char, int>();
            foreach (string mot in dico)
            {
                if (mot.Length > 0)
                {
                    char firstLetter = char.ToUpper(mot[0]);
                    // Vérification si la lettre est déjà présente dans le dictionnaire
                    if (compteurLettres.ContainsKey(firstLetter))
                    {
                        // Si la lettre est présente, on incrémente son compteur
                        compteurLettres[firstLetter]++;
                    }
                    else
                    {
                        // Si la lettre n'est pas présente, on l'ajoute au dictionnaire avec un compteur initialisé à 1
                        compteurLettres[firstLetter] = 1;
                    }
                }
            }

            string result = "C'est un dictionnaire français. Nombre de mots par lettre : \n";
            // Parcours du dictionnaire trié par clé (lettre)
            foreach (var kvp in compteurLettres.OrderBy(x => x.Key))
            {
                // Construction de la partie résultat avec la lettre et son compteur
                result += kvp.Key +" : "+ kvp.Value+"\n";
            }

            return result;
        }


        /// <summary>
        /// autre maniere de faire sans dicttionaryy
        /// </summary>
        /// <returns></returns>
        //public string toString()
        //{
        //    string retour = "";
        //    string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        //    int j = 0;
        //    for (int i = 0; i < alphabet.Length; i++)
        //    {
        //        int compteur = 0;
        //        string lettre = alphabet[i];
        //        while (dico[j][0] == Convert.ToChar(lettre) && j < dico.Count - 1)
        //        {
        //            compteur++;
        //            j++;
        //        }
        //        retour += lettre + " : " + compteur + "\n";
        //        compteur = 0;
        //    }
        //    return "C'est un dictionnaire francais. Nombre de mots par lettre : \n" + retour;
        //}


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
            if (debut > fin || dico == null|| dico.Count==0)
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
            // Vérification si la sous-liste a plus d'un élément
            if (gauche < droite)
            {
                // Sélection du pivot et partitionnement de la liste
                int pivot = Partition(liste, gauche, droite);

                // Appel récursif pour la partie gauche de la liste
                if (pivot > gauche)
                {
                    QuickSort(liste, gauche, pivot - 1);
                }

                // Appel récursif pour la partie droite de la liste
                if (pivot < droite)
                {
                    QuickSort(liste, pivot + 1, droite);
                }
            }
        }

        public int Partition(List<string> liste, int gauche, int droite)
        {
            // Choix du pivot comme élément le plus à droite de la liste
            string pivot = liste[droite];
            int i = gauche - 1;

            // Parcours de la sous-liste de gauche à droite (sauf le pivot)
            for (int j = gauche; j < droite; j++)
            {
                // Comparaison des éléments avec le pivot
                if (liste[j].CompareTo(pivot) <= 0)
                {
                    // Si l'élément est inférieur ou égal au pivot, on l'échange avec l'élément à la position i+1
                    i++;
                    string temp = liste[i];
                    liste[i] = liste[j];
                    liste[j] = temp;
                }
            }

            // Échange du pivot avec l'élément à la position i+1 pour le placer à sa position finale
            string temp1 = liste[i + 1];
            liste[i + 1] = liste[droite];
            liste[droite] = temp1;

            // Retourne la position finale du pivot
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