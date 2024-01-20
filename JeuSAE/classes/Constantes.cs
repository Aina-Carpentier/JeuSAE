using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JeuSAE.classes
{
    internal class Constantes
    {
        public const int BALLE_WIDHT = 25;
        public const int BALLE_HEIGHT = 10;
        public static double VIE_JOUEUR = 100d;
        public const int DEGATS_BALLE_UN = 10;
        public const int DEGATS_BALLE_DEUX = 5;
        //public const int DEGAT_BALLE_JOUEUR = 1;
        public const int DEGATS_COLLISION = 10;
        public const int NOMBRE_AMELIORATIONS = 7;

        public static readonly SoundPlayer LECTEUR_MUSIQUE_MENU = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "audio\\musiques\\musique_menu.wav");


        public static string CHEMIN_BDD = AppDomain.CurrentDomain.BaseDirectory + "data\\database.json";
        public static double COEFFICIENT_EXPERIENCE = 20;
        public static Key TOUCHE_HAUT = Key.Z, TOUCHE_BAS = Key.S, TOUCHE_DROITE = Key.D, TOUCHE_GAUCHE = Key.Q;
        public static int BALLE_NOMBRE_PERCE = 1;

        public static double VITESSE_JOUEUR = 10;
        public static double TEMPS_RECHARGE_ARME = 15;
        public static int TEMPS_RECHARGE_ACTUEL = 0;
        public static int TAILLE_BALLE_JOUEUR = 50;
        public static double VITESSE_BALLE_JOUEUR = 25;
        public static int DEGATS_JOUEUR = 1;
        public static int VITESSE_BALLE = 25;
        public static int COMPTEUR_SPAWN = 0;
        public static int TICK_REQUIS_POUR_SPAWN_ENNEMI = 150;
        public static int TICK_ANIMATION = 0;

        //-------------------------------------------ENNEMI-------------------------------------------
        public const int ENNEMI_RECT_LARGEUR = 75;
        public const int ENNEMI_RECT_HAUTEUR = 75;

        public const int BOSS_RECT_LARGEUR = 130;
        public const int BOSS_RECT_HAUTEUR = 130;


        // VIE

        public const double VIE_TRIANGLE_EQ = 3;
        public const double VIE_CARRE = 4;
        public const double VIE_PENTAGONE = 10;
        public const double VIE_HEXAGONE = 12;
        public const double VIE_HEPTAGONE = 14;
        public const double VIE_OCTOGONE = 20;
        public const double VIE_BOSS = 45;
        public const double VIE_CERCLE = Math.PI;


        // VITESSE

        public const double VITESSE_TRIANGLE_EQ = 3;
        public const double VITESSE_CARRE = 4;
        public const double VITESSE_PENTAGONE = 3;
        public const double VITESSE_HEXAGONE = 3;
        public const double VITESSE_HEPTAGONE = 2;
        public const double VITESSE_OCTOGONE = 6;
        public const double VITESSE_BOSS = 9;
        public const double VITESSE_CERCLE = Math.PI;


        // CADENCE DE TIR

        public const double CADENCE_TRIANGLE_EQ = 3 * 60;
        public const double CADENCE_CARRE = 3 * 60;
        public const double CADENCE_PENTAGONE = 1.5 * 60d;
        public const double CADENCE_HEXAGONE = 1 * 60;
        public const double CADENCE_HEPTAGONE = 0.5 * 60d;
        public const double CADENCE_OCTOGONE = 0.4 * 60d;
        public const double CADENCE_BOSS = 2.5 * 60d;
        public const double CADENCE_CERCLE = Math.PI * 60d;


        // NOM

        public const string NOM_TRIANGLE_EQ = "Triangle équilatéral";
        public const string NOM_CARRE = "Carré";
        public const string NOM_PENTAGONE = "Pentagone";
        public const string NOM_HEXAGONE = "Hexagone";
        public const string NOM_HEPTAGONE = "Heptagone";
        public const string NOM_OCTOGONE = "Octogone";
        public const string NOM_BOSS = "Boss";
        public const string NOM_CERCLE = "Cercle";

    }
}
