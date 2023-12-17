using projetalgo;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using static projetalgo.Plateau;
static void main()
{
    string nomfichier;
    do
    {
        Console.WriteLine("Saisir 'sortir'  pour sortir ou 'aleatoire' pour joueur avec une matrice aleatoire ou taper le nom du fichier avec lequel vous voulez jouer");
        nomfichier = Console.ReadLine();
        if (nomfichier != "sortir")
        {

            if (!File.Exists(nomfichier) && nomfichier != "aleatoire")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erreur: Le fichier '" + nomfichier + "' n'existe pas.");
                Console.ResetColor();
            }
            else if (nomfichier == "aleatoire")
            {
                Console.Clear();
                Plateau p = new Plateau("Lettre.txt", 8, 8);
                Dictionnaire d = new Dictionnaire("Mots_Français.txt");
                Console.WriteLine(d.toString());
                int temps;
                bool tempsValide = false;

                do
                {
                    Console.WriteLine("Determinez le temps des joueurs");

                    // On utilise int.TryParse pour vérifier si l'entrée est un entier
                    if (int.TryParse(Console.ReadLine(), out temps))
                    {
                        tempsValide = true;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("L'entrée n'est pas un entier valide. Veuillez réessayer.");
                        Console.ResetColor();
                    }

                } while (!tempsValide);


                Console.Clear();
                Joueur joueur1 = new Joueur(Début(1));
                Console.Clear();
                Joueur joueur2 = new Joueur(Début(2));
                Console.Clear();
                Jeu partie = new Jeu(d, p, joueur1, joueur2, temps);
                partie.play();
            }
            else if(MemeNbColonne(nomfichier))
            {
                Console.Clear();
                Plateau p = new Plateau(nomfichier);
                Dictionnaire d = new Dictionnaire("Mots_Français.txt");
                Console.WriteLine(d.toString());

                int temps;
                bool tempsValide = false;
                do
                {
                    Console.WriteLine("Determinez le temps des joueurs");

                    // On utilise int.TryParse pour vérifier si l'entrée est un entier
                    if (int.TryParse(Console.ReadLine(), out temps))
                    {
                        tempsValide = true;
                    }
                    else
                    {
                        Console.WriteLine("L'entrée n'est pas un entier valide. Veuillez réessayer.");
                    }
                } while (!tempsValide);


                Console.Clear();
                Joueur joueur1 = new Joueur(Début(1));
                Console.Clear();
                Joueur joueur2 = new Joueur(Début(2));
                Console.Clear();
                Jeu partie = new Jeu(d, p, joueur1, joueur2, temps);
                partie.play();
            }

        }
    } while (nomfichier != "sortir");

    string Début(int n)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; // Permet l'affichage de certains caractères spéciaux
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("███┐   ███┐ █████┐ ████████┐ █████┐     █████┐  ██│      ██│ █████┐ █████┐ █████┐ █████┐  ™");
        Console.WriteLine("████┐ ████│██┌──██┐   ██┌──┘ ██┌──┘    ██┌───┘  ██│      ██│ ██┌──┘ ██┌──┘ ██┌──┘ ██┌──┘");
        Console.WriteLine("██┌████┌██│██│  ██│   ██│    █████┐    ██│      ██│      ██│ █████┐ █████┐ █████┐ █████┐");
        Console.WriteLine("██│└██┌┘██│██│  ██│   ██│    └──██│    ██│  ██┐ ██│      ██│ └──██│ └──██│ ██┌──┘ └──██│");
        Console.WriteLine("██│ └─┘ ██│└█████┌┘   ██│    █████│    └█████┌┘ ███████┐ ██│ █████│ █████│ █████│ █████│");
        Console.WriteLine("└─┘     └─┘ └────┘    └─┘    └────┘     └────┘  └──────┘ └─┘ └────┘ └────┘ └────┘ └────┘");
        Console.ResetColor();
        Console.WriteLine("Joueur " + n + ", entrez votre nom :");
        return Console.ReadLine();
    }


    bool MemeNbColonne(string nom_fichier)
    {
        int lignes = 0;
        int colonnes = 0;
        using (StreamReader sr1 = new StreamReader(nom_fichier))
        {
            while (sr1.Peek() >= 0)
            {
                string line1 = sr1.ReadLine();
                string[] values = line1.Split(';');

                if (values != null && values.Length > 0)
                {
                    lignes++;
                    if (colonnes == 0)
                        colonnes = values.Length;
                    if (values.Length != colonnes)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("erreur : le nombre de colonnes n'est pas le meme sur chaque ligne");
                        Console.ResetColor();
                        return false;
                    }
                }
            }
        }
        return true;
    }
}

main();