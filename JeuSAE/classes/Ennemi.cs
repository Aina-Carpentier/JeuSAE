using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using JeuSAE.classes;

namespace JeuSAE;

public class Ennemi
{
    private static readonly Random Random = new();
    private readonly Uri DossierSprites = new(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\");
    private readonly ImageBrush EnnemiImage = new();

    public double
        CadenceTir; // En tick/tir donc si = 3 alors l'ennemi tir une fois tous les 3 tick, donc pour 3 fois par seconde c'est approx 20

    private double CooldownTir = 180; // La valeur utiliser pour calculer quand l'ennemi tire en fonction de la cadence
    public Rectangle Graphique;
    public Guid Id = Guid.NewGuid();
    public string Nom;
    public double PosX;
    public double PosY;
    public int Type;
    public double Vie;
    public double Vitesse; // En pixel/tick

    public Ennemi(int type, double posX, double posY)
    {
        Type = type;

        Graphique = new Rectangle() /*(PosX, PosY, Constantes.ENNEMI_RECT_LARGEUR, Constantes.ENNEMI_RECT_HAUTEUR)*/;
        Graphique.Width = Constantes.ENNEMI_RECT_LARGEUR;
        Graphique.Height = Constantes.ENNEMI_RECT_LARGEUR;

        switch (type)
        {
            case 0: // Triangle équilatéral
                Vie = Constantes.VIE_TRIANGLE_EQ;
                Vitesse = Constantes.VITESSE_TRIANGLE_EQ;
                CadenceTir = Constantes.CADENCE_TRIANGLE_EQ;
                Nom = Constantes.NOM_TRIANGLE_EQ;
                EnnemiImage.ImageSource =
                    new BitmapImage(new Uri(DossierSprites +
                                            "triangle.png")); // dossierImage c'est un Uri donc ça vas peut-être bugger
                break;
            case 1: // Carré
                Vie = Constantes.VIE_CARRE;
                Vitesse = Constantes.VITESSE_CARRE;
                CadenceTir = Constantes.CADENCE_CARRE;
                Nom = Constantes.NOM_CARRE;
                EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "carre.png"));
                break;
            case 2: // Pentagone
                Vie = Constantes.VIE_PENTAGONE;
                Vitesse = Constantes.VITESSE_PENTAGONE;
                CadenceTir = Constantes.CADENCE_PENTAGONE;
                Nom = Constantes.NOM_PENTAGONE;
                EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "pentagone.png"));
                break;
            case 3: // Hexagone
                Vie = Constantes.VIE_HEXAGONE;
                Vitesse = Constantes.VITESSE_HEXAGONE;
                CadenceTir = Constantes.CADENCE_HEXAGONE;
                Nom = Constantes.NOM_HEXAGONE;
                EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "hexagone.png"));
                break;
            case 4: // Heptagone
                Vie = Constantes.VIE_HEPTAGONE;
                Vitesse = Constantes.VITESSE_HEPTAGONE;
                CadenceTir = Constantes.CADENCE_HEPTAGONE;
                Nom = Constantes.NOM_HEPTAGONE;
                EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "heptagone.png"));
                break;
            case 5: // Octogone
                Vie = Constantes.VIE_OCTOGONE;
                Vitesse = Constantes.VITESSE_OCTOGONE;
                CadenceTir = Constantes.CADENCE_OCTOGONE;
                Nom = Constantes.NOM_OCTOGONE;
                EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "octogone.png"));
                break;
            case 6: // Cercle
                Vie = Constantes.VIE_CERCLE;
                Vitesse = Constantes.VITESSE_CERCLE;
                CadenceTir = Constantes.CADENCE_CERCLE;
                Nom = Constantes.NOM_CERCLE;
                EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "cercle.png"));
                break; //TODO ajouter plus d'ennemis si on a des idées
        }

        PosX = posX;
        PosY = posY;
        Graphique.Fill = EnnemiImage;
    }

    public Rect Rect => new(PosX, PosY, Constantes.ENNEMI_RECT_LARGEUR, Constantes.ENNEMI_RECT_HAUTEUR);

    public override bool Equals(object? obj)
    {
        return obj is Ennemi Ennemi &&
               Id.Equals(Ennemi.Id);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public override string? ToString()
    {
        return Nom;
    }

    public static void SpawnUnBoss(MainWindow mainWindow)
    {
        var posJoueur = CalculerPositionJoueur(mainWindow);

        var x = Random.Next(500, 2000) - Random.Next(0, 2000);
        var y = Random.Next(0, 2) == 0 ? -Random.Next(0, 2000) : Random.Next(0, 2000);

        var ennemi = new Boss(0, x, y);
        PositionnerEnnemi(mainWindow, ennemi, posJoueur.X, posJoueur.Y, x, y);
    }

    public static void SpawnUnEnnemi(MainWindow mainWindow)
    {
        var posJoueur = CalculerPositionJoueur(mainWindow);

        var x = Random.Next(500, 2000) - Random.Next(0, 2000);
        var y = Random.Next(0, 2) == 0 ? -Random.Next(0, 2000) : Random.Next(0, 2000);

        var ennemi = new Ennemi(Random.Next(0, 7), x, y);
        PositionnerEnnemi(mainWindow, ennemi, posJoueur.X, posJoueur.Y, x, y);
    }

    public static void SpawnUnEnnemi(MainWindow mainWindow, int type)
    {
        var posJoueur = CalculerPositionJoueur(mainWindow);

        var x = Random.Next(500, 2000) - Random.Next(0, 2000);
        var y = Random.Next(0, 2) == 0 ? -Random.Next(0, 2000) : Random.Next(0, 2000);

        var ennemi = new Ennemi(type, x, y);
        PositionnerEnnemi(mainWindow, ennemi, posJoueur.X, posJoueur.Y, x, y);
    }

    private static void PositionnerEnnemi(MainWindow mainWindow, Ennemi ennemi, double posJoueurX, double posJoueurY,
        int x, int y)
    {
        var canvas = mainWindow.monCanvas;
        var carte = mainWindow.carte;

        ennemi.Graphique.SetValue(Canvas.LeftProperty, posJoueurX + x);
        ennemi.Graphique.SetValue(Canvas.TopProperty, posJoueurY + y);

        if (!EstDansLaCarte(ennemi.Graphique, carte)) ennemi.Graphique.SetValue(Canvas.LeftProperty, posJoueurX - x);
        if (!EstDansLaCarte(ennemi.Graphique, carte, false))
            ennemi.Graphique.SetValue(Canvas.TopProperty, posJoueurY - y);

        ennemi.PosX = (double)ennemi.Graphique.GetValue(Canvas.LeftProperty);
        ennemi.PosY = (double)ennemi.Graphique.GetValue(Canvas.TopProperty);

        canvas.Children.Add(ennemi.Graphique);
        MainWindow.Ennemis.Add(ennemi);
    }

    private static bool EstDansLaCarte(FrameworkElement element, FrameworkElement carte, bool estGaucheDroite = true)
    {
        var position = estGaucheDroite ? Canvas.GetLeft(element) : Canvas.GetTop(element);
        var tailleElement = estGaucheDroite ? element.ActualWidth : element.ActualHeight;
        var tailleCarte = estGaucheDroite ? carte.Width : carte.Height;

        return position + tailleElement >= 0 && position <= tailleCarte;
    }

    private static Point CalculerPositionJoueur(MainWindow mainWindow)
    {
        return new Point(mainWindow.fenetrePrincipale.Width / 2, mainWindow.fenetrePrincipale.Height / 2);
    }


    public void Tir()
    {
        if (CooldownTir > 0)
        {
            CooldownTir--;
            return;
        }

        var mainWindow = (MainWindow)Application.Current.MainWindow;
        var joueur = mainWindow.rectJoueur;

        var posJoueurX = mainWindow.fenetrePrincipale.Width / 2 - joueur.Width * 0.75;
        var posJoueurY = mainWindow.fenetrePrincipale.Height / 2 - joueur.Height * 0.75;

        var deplacementVecteur = new Vector2((float)PosX - (float)posJoueurX, (float)PosY - (float)posJoueurY);

        Balle nouvelleBalle;

        if (Type == 6)
            nouvelleBalle = new Balle(Constantes.VITESSE_BALLE, 20, 2, Id.ToString(), 0,
                PosX + (float)Graphique.Width / 2, PosY + (float)Graphique.Height / 2, -deplacementVecteur, 1);
        else if (Type == 9) //Boss
            nouvelleBalle = new Balle(12, 45, 3, Id.ToString(), 0,
                PosX + (float)Graphique.Width / 2, PosY + (float)Graphique.Height / 2, -deplacementVecteur, 1);
        else
            nouvelleBalle = new Balle(Constantes.VITESSE_BALLE, 20, 1, Id.ToString(), 0,
                PosX + (float)Graphique.Width / 2, PosY + (float)Graphique.Height / 2, -deplacementVecteur, 1);

        mainWindow.monCanvas.Children.Add(nouvelleBalle.Graphique);
        MainWindow.Balles.Add(nouvelleBalle);

        CooldownTir = CadenceTir;
    }
}