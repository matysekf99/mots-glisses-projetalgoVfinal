using projetalgo;
using System.Collections.Generic;
using System.Reflection;
using static projetalgo.Plateau;
static void main()
{
    string filename;
    do
    {
        Console.WriteLine("Saisir 'sortir'  pour sortir ou 'aleatoire' pour joueur avec une matrice aleatoire ou taper le nom du fichier avec lequel vous voulez jouer");
        filename = Console.ReadLine();
        if (filename != "sortir")
        {

            if (!File.Exists(filename) && filename != "aleatoire")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erreur: Le fichier '" + filename + "' n'existe pas.");
                Console.ResetColor();
            }
            else if (filename == "aleatoire")
            {
                Console.Clear();
                Plateau p = new Plateau("Lettre.txt", 8, 8);
                Dictionnaire d = new Dictionnaire("Mots_Français.txt");
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
            else
            {
                Console.Clear();
                Plateau p = new Plateau(filename);
                Dictionnaire d = new Dictionnaire("Mots_Français.txt");


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
    } while (filename != "sortir");

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
}

main();