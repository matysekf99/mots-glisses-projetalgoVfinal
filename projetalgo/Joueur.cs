using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetalgo
{
    public class Joueur
    {
        //attributs
        string nom;
        List<string> motsTrouves;
        int score;

        //constructeur
        public Joueur(string nom)
        {
            this.nom = nom;
            motsTrouves = new List<string>();
            score = 0;
        }

        /// <summary>
        /// Ajoute un mot trouvé par la joueur a la liste des mots trouvés
        /// </summary>
        /// <param name="mot"></param>
        public void Add_Mot(string mot)
        {
            motsTrouves.Add(mot);
        }

        /// <summary>
        /// affiche les attributs de la classe joueur
        /// </summary>
        /// <returns>string</returns>
        public string toString()
        {
            string retour = "";
            if (motsTrouves != null)
            {
                foreach (string mot in motsTrouves)
                {
                    if (mot == motsTrouves.Last())
                    {
                        retour += mot;
                    }
                    else
                    {
                        retour += mot + ";";
                    }
                }
                return "nom: " + nom + "\nMots trouvés : " + retour + "\nScore : " + score;
            }
            else
            {
                return "nom : " + nom + "\nMots trouvés : null" + "\nScore : " + score;
            }
        }

        /// <summary>
        /// ajoute "val" au score du joueur
        /// </summary>
        /// <param name="val"></param>
        public void Add_Score(int val)
        {
            score += val;
        }

        /// <summary>
        /// test si "mot" est présent dans la liste de mots
        /// </summary>
        /// <param name="mot"></param>
        /// <returns>bool</returns>
        public bool Contient(string mot)
        {
            if (motsTrouves.Contains(mot))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        //proprités
        public List<string> MotsTrouves
        {
            get { return motsTrouves; }
            set { motsTrouves = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public string Nom
        {
            get { return nom; }
        }

    }
}