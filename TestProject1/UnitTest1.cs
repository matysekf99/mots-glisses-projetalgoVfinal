using projetalgo;
using System.Diagnostics;

namespace Projet
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodAddMot()
        {
            Joueur joueur1 = new Joueur("test");
            joueur1.Add_Mot("salut");
            bool motDansLaListe = joueur1.Contient("salut");
            Assert.AreEqual(true, motDansLaListe);
        }

        [TestMethod]
        public void TestMethodRecherche()
        {
            Dictionnaire d = new Dictionnaire("Mots_Français.txt");
            d.Tri_XXX();
            bool motTrouvé = d.RechDichoRecursif("AVION", 0, d.Dico.Count);
            Assert.AreEqual(true,motTrouvé);
        }

        [TestMethod]
        public void TestMethodCherche()
        {
            Plateau plateau = new Plateau("Test1.csv");
            int[,] solution = new int[8, 8];
            bool motDansLaMatrice = plateau.Cherche(solution, "MAISON", 7, 3, 0);
            Assert.AreEqual(true, motDansLaMatrice);
        }
        [TestMethod]
        public void TestMethodQuickSort()
        {
            Dictionnaire d = new Dictionnaire("Mots_Français.txt");
            d.QuickSort(d.Dico, 0, d.Dico.Count - 1);
            bool triee=d.EstTriee(d.Dico);
            Assert.AreEqual(true, triee);
        }
        [TestMethod]
        public void TestMethodePointMot()
        {
            Dictionnaire d = new Dictionnaire("Mots_Français.txt");
            Plateau plateau = new Plateau("Test1.csv");
            Joueur joueur1 = new Joueur("Joueur1");
            Joueur joueur2 = new Joueur("Joueur2");
            Jeu partie=new Jeu(d,plateau,joueur1,joueur2,1);
            int point = partie.pointMot("AVION");
            Assert.AreEqual(15, point);//le mot "avion" rapporte 15 points
        }
    }
}