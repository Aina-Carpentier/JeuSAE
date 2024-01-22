using System;
using System.Media;
using System.Windows.Input;

namespace JeuSAE.classes;

internal class Constantes
{
    // Constantes relatives aux balles
    public const int BALLE_WIDHT = 25;
    public const int BALLE_HEIGHT = 10;
    public const int DEGATS_BALLE_UN = 10;
    public const int DEGATS_BALLE_DEUX = 5;
    public const int DEGATS_BALLE_TROIS = 17;

    public const int DEGATS_COLLISION = 10;
    public const int NOMBRE_AMELIORATIONS = 8;

    //-------------------------------------------ENNEMI-------------------------------------------
    // Dimensions des ennemis et boss
    public const int ENNEMI_RECT_LARGEUR = 75;
    public const int ENNEMI_RECT_HAUTEUR = 75;

    public const int BOSS_RECT_LARGEUR = 180;
    public const int BOSS_RECT_HAUTEUR = 180;

    // VIE des ennemis et boss
    public const double VIE_TRIANGLE_EQ = 3;
    public const double VIE_CARRE = 4;
    public const double VIE_PENTAGONE = 10;
    public const double VIE_HEXAGONE = 12;
    public const double VIE_HEPTAGONE = 14;
    public const double VIE_OCTOGONE = 20;
    public const double VIE_BOSS = 45;
    public const double VIE_CERCLE = Math.PI;

    // VITESSE des ennemis et boss
    public const double VITESSE_TRIANGLE_EQ = 3;
    public const double VITESSE_CARRE = 4;
    public const double VITESSE_PENTAGONE = 3;
    public const double VITESSE_HEXAGONE = 3;
    public const double VITESSE_HEPTAGONE = 2;
    public const double VITESSE_OCTOGONE = 4;
    public const double VITESSE_BOSS = 7;
    public const double VITESSE_CERCLE = Math.PI;

    // CADENCE DE TIR des ennemis et boss
    public const double CADENCE_TRIANGLE_EQ = 3 * 60;
    public const double CADENCE_CARRE = 3 * 60;
    public const double CADENCE_PENTAGONE = 1.5 * 60d;
    public const double CADENCE_HEXAGONE = 1 * 60;
    public const double CADENCE_HEPTAGONE = 0.5 * 60d;
    public const double CADENCE_OCTOGONE = 0.4 * 60d;
    public const double CADENCE_BOSS = 2.5 * 60d;
    public const double CADENCE_CERCLE = Math.PI * 60d;

    // NOM des ennemis et boss
    public const string NOM_TRIANGLE_EQ = "Triangle équilatéral";
    public const string NOM_CARRE = "Carré";
    public const string NOM_PENTAGONE = "Pentagone";
    public const string NOM_HEXAGONE = "Hexagone";
    public const string NOM_HEPTAGONE = "Heptagone";
    public const string NOM_OCTOGONE = "Octogone";
    public const string NOM_BOSS = "Boss";
    public const string NOM_CERCLE = "Cercle";

    // Coûts et prix
    public const int PRIX_ARME = 200;
    public const int PRIX_BASE_AMELIORATION = 10;
    public static double VIE_JOUEUR = 100d;

    // Lecteur de musique
    public static readonly SoundPlayer LECTEUR_MUSIQUE_MENU =
        new(AppDomain.CurrentDomain.BaseDirectory + "audio\\musiques\\musique_menu.wav");
    public static readonly SoundPlayer LECTEUR_MUSIQUE_JEU =
    new(AppDomain.CurrentDomain.BaseDirectory + "audio\\musiques\\musique_jeu.wav");

    // Chemins et touches
    public static string CHEMIN_BDD = AppDomain.CurrentDomain.BaseDirectory + "data\\database.json";
    public static double COEFFICIENT_EXPERIENCE = 20;
    public static Key TOUCHE_HAUT = Key.Z, TOUCHE_BAS = Key.S, TOUCHE_DROITE = Key.D, TOUCHE_GAUCHE = Key.Q;

    // Caractéristiques des balles du joueur
    public static int BALLE_NOMBRE_PERCE_1 = 1;
    public static int BALLE_NOMBRE_PERCE_2 = 3;
    public static int BALLE_NOMBRE_PERCE_3 = 1;
    public static int POURCENTAGE_NOMBRE_DE_VIE = 0;
    // Caractéristiques du joueur
    public static int TEMPS_RECHARGE_ACTUEL = 0;
    public static double VITESSE_JOUEUR = 10;

    public static double TEMPS_RECHARGE_ARME_1 = 25;
    public static double TEMPS_RECHARGE_ARME_2 = 50;
    public static double TEMPS_RECHARGE_ARME_3 = 10;
    
    public static int TAILLE_BALLE_JOUEUR_1 = 30;
    public static int TAILLE_BALLE_JOUEUR_2 = 60;
    public static int TAILLE_BALLE_JOUEUR_3 = 15;
    
    public static int DEGATS_JOUEUR_1 = 2;
    public static int DEGATS_JOUEUR_2 = 5;
    public static int DEGATS_JOUEUR_3 = 1;

    public static double VITESSE_BALLE_JOUEUR = 25;
    public static int VITESSE_BALLE = 15;
    public static int COMPTEUR_SPAWN = 0;
    public static int TICK_REQUIS_POUR_SPAWN_ENNEMI = 150;
    public static int TICK_ANIMATION = 0;
}