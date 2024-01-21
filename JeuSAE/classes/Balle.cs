using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace JeuSAE.classes
{
    public class Balle
    {
        private double vitesse;
        private double taille;
        private int type;
        private string tireur;
        private double acceleration;
        private double posX;
        private double posY;
        private double degats;
        private Vector2 vecteur;
        private Ellipse graphique;
        private Vector2 vecteurSin;
        private float coefSin = 0;
        private bool inverseSin = false;
        private List<Guid> listeEnnemisPerces = new List<Guid>();

        public double Vitesse
        {
            get { return vitesse; }
            set { vitesse = value; }
        }

        public double Taille
        {
            get { return taille; }
            set
            {
                if (value <= 0) { throw new ArgumentException("La taille ne peut pas être inférieure ou égale à 0. "); }
                taille = value;
            }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Tireur
        {
            get { return tireur; }
            set { tireur = value; }
        }

        public double Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }

        public double PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        public double PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        public double Degats
        {
            get { return degats; }
            set { degats = value; }
        }


        public Vector2 Vecteur
        {
            get { return vecteur; }
            set { vecteur = value; }
        }

        public int NombrePerce
        {
            get { return Constantes.BALLE_NOMBRE_PERCE; }
            set { 
                if (value < 0) { throw new ArgumentException("Le nombre d'ennemis à percer ne peut pas être négatif."); }
                Constantes.BALLE_NOMBRE_PERCE = value; }
        }

        public List<Guid> ListeEnnemisPerces
        {
            get { return listeEnnemisPerces; }
            set { listeEnnemisPerces = value; }
        }



        public Rect Rect { get => new Rect(PosX, PosY, Taille, Taille); }

        public Ellipse Graphique
        {
            get { return graphique; }
            set { graphique = value; }
        }

        public Balle(double vitesse, double taille, int type, string tireur, double acceleration, double posX, double posY, Vector2 vecteur, double degats)
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
            {
                Graphique = new Ellipse
                {
                    Tag = "bulletPlayer",
                    Width = Taille,
                    Height = Taille,
                    Fill = Brushes.Red,
                    Stroke = Brushes.Black
                };
            }
            else
            {
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
                        ImageBrush image = new ImageBrush();
                        image.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"images\\boss\\arch_linux.png"));
                        Graphique = new Ellipse
                        {
                            Tag = "bulletBoss",
                            Width = Taille,
                            Height = Taille,
                            Fill = image,
                            Stroke = Brushes.Black
                        };
                        Degats = Constantes.DEGATS_BALLE_TROIS;
                        break;
                }
            }
        }


        public override bool Equals(object? obj)
        {
            return obj is Balle balle &&
                   Vitesse == balle.Vitesse &&
                   Taille == balle.Taille &&
                   Type == balle.Type &&
                   Tireur == balle.Tireur &&
                   Acceleration == balle.Acceleration &&
                   PosX == balle.PosX &&
                   PosY == balle.PosY &&
                   Vecteur.Equals(balle.Vecteur);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Vitesse, Taille, Type, Tireur, Acceleration, PosX, PosY, Vecteur);
        }

        public void Deplacement()
        {
            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            Vector2 vecteurNormalize = Vector2.Normalize(this.Vecteur);

            switch (this.Type)
            {
                case 0:
                {
                    double newX = PosX + (vecteurNormalize.X * this.Vitesse);
                    double newY = PosY + (vecteurNormalize.Y * this.Vitesse);

                    this.PosX = newX;
                    this.PosY = newY;
                    break;
                }
                case 1:
                {
                    double newX = PosX + (vecteurNormalize.X * this.Vitesse);
                    double newY = PosY + (vecteurNormalize.Y * this.Vitesse);

                    this.PosX = newX;
                    this.PosY = newY;
                    break;
                }
                case 2:
                {
                    double sinus = Math.Sin(coefSin) * 2;
                    double cosinus = Math.Cos(coefSin) *2;

                    Vector2 vecteurNormal = new Vector2(vecteurNormalize.Y, -vecteurNormalize.X);

                    double nouveauX = PosX + (vecteurNormal.X * cosinus - vecteurNormal.Y * sinus) * this.Vitesse;
                    double nouveauY = PosY + (vecteurNormal.X * sinus + vecteurNormal.Y * cosinus) * this.Vitesse;

                    this.PosX = nouveauX;
                    this.PosY = nouveauY;

                    
                    if (!inverseSin)
                    {
                        coefSin += 0.05f;
                        if (coefSin > Math.PI) { inverseSin = true; }
                    }
                    else
                    {
                        coefSin -= 0.05f;
                        if (coefSin <= 0) { inverseSin = false; }
                    }
                    break;
                }
                case 3:
                {
                    double newX = PosX + (vecteurNormalize.X * this.Vitesse);
                    double newY = PosY + (vecteurNormalize.Y * this.Vitesse);

                    this.PosX = newX;
                    this.PosY = newY;
                    break;
                }
            }
        }
    }
}
