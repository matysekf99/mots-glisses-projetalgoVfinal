using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace projetalgo
{
    public class Minuteur
    {
        //attributs
        private DateTime debut;
        private TimeSpan duree;
        private TimeSpan tempsEcoule;
        private bool estTermine;
        private bool estEnPause;
        private DateTime pauseDebut;


        //constructeurs
        public Minuteur(TimeSpan duree)
        {
            this.duree = duree;
            this.estTermine = false;
            this.estEnPause = false;
        }

        //méthodes

        /// <summary>
        /// demarre le minuteur
        /// </summary>
        public void Demarrer()
        {
            if (!estEnPause)
                debut = DateTime.Now;
            else
            {
                TimeSpan tempsPause = DateTime.Now - pauseDebut;
                debut += tempsPause; // Ajouter la durée de pause au début pour maintenir la cohérence du temps
                estEnPause = false;
            }

            estTermine = false;
        }

        /// <summary>
        /// met en pause le minuteur
        /// </summary>
        public void MettreEnPause()
        {
            if (!estTermine && !estEnPause)
            {
                tempsEcoule = DateTime.Now - debut;
                estEnPause = true;
                pauseDebut = DateTime.Now;
            }
        }

        /// <summary>
        /// redemarre le minuteur
        /// </summary>
        public void Reprendre()
        {
            if (estEnPause)
            {
                estEnPause = false;
                debut += DateTime.Now - pauseDebut; // Ajouter la durée de pause au début pour maintenir la cohérence du temps
            }
        }

        /// <summary>
        /// met à jour le chrono
        /// </summary>
        public void MettreAJour()
        {
            if (!estTermine && !estEnPause)
            {
                tempsEcoule = DateTime.Now - debut;
                estTermine = tempsEcoule >= duree;
            }
        }

        public bool EstTermine()
        {
            MettreAJour();
            return estTermine;
        }

        /// <summary>
        /// retourne le temps qui reste
        /// </summary>
        /// <returns></returns>
        public TimeSpan TempsRestant()
        {
            MettreAJour();
            TimeSpan tempsRestant = duree - tempsEcoule;

            if (tempsRestant.TotalSeconds > 0)
            {
                return tempsRestant;
            }
            else
            {
                return TimeSpan.Zero;
            }
        }
    }
}