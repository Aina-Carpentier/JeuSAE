using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JeuSAE.classes;

public class Balle
{
    private readonly double Acceleration;
    private readonly double Taille;
    private readonly int Type;
    private readonly double Vitesse;
    private float CoefRotation;
    private float CoefSin;
    public double Degats;
    public Ellipse Graphique;
    private bool InverseSin;
    public List<Guid> ListeEnnemisPerces = new();
    public double PosX;
    public double PosY;
    public string Tireur;
    private Vector2 Vecteur;
    private Vector2 VecteurSin;

    public Balle(double vitesse, double taille, int type, string tireur, double acceleration, double posX, double posY,
        Vector2 vecteur, double degats)
    {
        Vitesse = vitesse;
        Taille = taille;
        Type = type;
        Tireur = tireur;
        Acceleration = acceleration;
        PosX = posX - taille / 2 + Vector2.Normalize(vecteur).X;
        PosY = posY - taille / 2 + Vector2.Normalize(vecteur).Y;
        Vecteur = vecteur;
        Degats = degats;

        if (tireur == "joueur")
            Graphique = new Ellipse
            {
                Tag = "bulletPlayer",
                Width = Taille,
                Height = Taille,
                Fill = Brushes.Red,
                Stroke = Brushes.Black
            };
        else
            switch (Type)
            {
                case 1:
                    Graphique = new Ellipse
                    {
                        Tag = "bulletEnnemy",
                        Width = Taille,
                        Height = Taille,
                        Fill = Brushes.Red,
                        Stroke = Brushes.Black
                    };
                    Degats = Constantes.DEGATS_BALLE_UN;
                    break;
                case 2:
                    Graphique = new Ellipse
                    {
                        Tag = "bulletEnnemySin",
                        Width = Taille,
                        Height = Taille,
                        Fill = Brushes.DarkBlue,
                        Stroke = Brushes.Black
                    };
                    Degats = Constantes.DEGATS_BALLE_DEUX;
                    break;
                case 3:
                    var Image = new ImageBrush();
                    Image.ImageSource =
                        new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory +
                                                "images\\boss\\arch_linux.png"));
                    Graphique = new Ellipse
                    {
                        Tag = "bulletBoss",
                        Width = Taille * 3,
                        Height = Taille * 3,
                        Fill = Image
                        //Stroke = Brushes.Black
                    };
                    Degats = Constantes.DEGATS_BALLE_TROIS;
                    break;
            }
    }

    public int NombrePerce
    {
        get => Constantes.BALLE_NOMBRE_PERCE;
        set
        {
            if (value < 0) throw new ArgumentException("Le nombre d'ennemis à percer ne peut pas être négatif.");
            Constantes.BALLE_NOMBRE_PERCE = value;
        }
    }

    public Rect Rect => new(PosX, PosY, Taille, Taille);


    public override bool Equals(object? obj)
    {
        return obj is Balle Balle &&
               Vitesse == Balle.Vitesse &&
               Taille == Balle.Taille &&
               Type == Balle.Type &&
               Tireur == Balle.Tireur &&
               Acceleration == Balle.Acceleration &&
               PosX == Balle.PosX &&
               PosY == Balle.PosY &&
               Vecteur.Equals(Balle.Vecteur);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Vitesse, Taille, Type, Tireur, Acceleration, PosX, PosY, Vecteur);
    }

    public void Deplacement()
    {
        var MainWindow = (MainWindow)Application.Current.MainWindow;
        var VecteurNormalize = Vector2.Normalize(Vecteur);

        switch (Type)
        {
            case 0:
            {
                var NewX = PosX + VecteurNormalize.X * Vitesse;
                var NewY = PosY + VecteurNormalize.Y * Vitesse;

                PosX = NewX;
                PosY = NewY;
                break;
            }
            case 1:
            {
                var NewX = PosX + VecteurNormalize.X * Vitesse;
                var NewY = PosY + VecteurNormalize.Y * Vitesse;

                PosX = NewX;
                PosY = NewY;
                break;
            }
            case 2:
            {
                var Sinus = Math.Sin(CoefSin) * 2;
                var Cosinus = Math.Cos(CoefSin) * 2;

                var VecteurNormal = new Vector2(VecteurNormalize.Y, -VecteurNormalize.X);

                var NouveauX = PosX + (VecteurNormal.X * Cosinus - VecteurNormal.Y * Sinus) * Vitesse;
                var NouveauY = PosY + (VecteurNormal.X * Sinus + VecteurNormal.Y * Cosinus) * Vitesse;

                PosX = NouveauX;
                PosY = NouveauY;


                if (!InverseSin)
                {
                    CoefSin += 0.1f;
                    if (CoefSin > Math.PI) InverseSin = true;
                }
                else
                {
                    CoefSin -= 0.1f;
                    if (CoefSin <= 0) InverseSin = false;
                }

                break;
            }
            case 3:
            {
                var PosJoueurX = MainWindow.fenetrePrincipale.Width / 2 - MainWindow.rectJoueur.Width * 0.5;
                var PosJoueurY = MainWindow.fenetrePrincipale.Height / 2 - MainWindow.rectJoueur.Height * 0.5;


                var VecteurBalleVersJoueur =
                    new Vector2((float)PosX - (float)PosJoueurX, (float)PosY - (float)PosJoueurY);
                var VecteurMoitie =
                    Vector2.Normalize(new Vector2((float)PosJoueurX - (float)PosX, (float)PosJoueurY - (float)PosY));
                Vector2.Multiply(0.5f, VecteurMoitie);
                var NouveauVecteur = Vector2.Normalize(Vector2.Add(VecteurNormalize, VecteurMoitie));

                if (Math.Abs(PosX - PosJoueurX) > 500 || Math.Abs(PosY - PosJoueurY) > 500)
                    NouveauVecteur = VecteurNormalize;
                else if (CoefRotation % 360 == 0) Vecteur = NouveauVecteur;


                var NewX = PosX + NouveauVecteur.X * Vitesse;
                var NewY = PosY + NouveauVecteur.Y * Vitesse;

                CoefRotation += 5;
                if (CoefRotation >= 360) CoefRotation = 0;

                Graphique.RenderTransform =
                    new RotateTransform(CoefRotation, Graphique.Width / 2, Graphique.Height / 2);

                PosX = NewX;
                PosY = NewY;
                break;
            }
        }
    }
}