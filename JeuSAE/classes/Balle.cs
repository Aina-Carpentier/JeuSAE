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
        private double Vitesse;
        private double Taille;
        private int Type;
        public string Tireur;
        private double Acceleration;
        public double PosX;
        public double PosY;
        public double Degats;
        private Vector2 Vecteur;
        public Ellipse Graphique;
        private Vector2 VecteurSin;
        private float CoefSin = 0;
        private float CoefRotation = 0;
        private bool InverseSin;
        public List<Guid> ListeEnnemisPerces = new();
        
        public int NombrePerce
        {
            get { return Constantes.BALLE_NOMBRE_PERCE; }
            set { 
                if (value < 0) { throw new ArgumentException("Le nombre d'ennemis à percer ne peut pas être négatif."); }
                Constantes.BALLE_NOMBRE_PERCE = value; }
        }
        
        public Rect Rect { get => new(PosX, PosY, Taille, Taille); }

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
                        ImageBrush Image = new ImageBrush();
                        Image.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $"images\\boss\\arch_linux.png"));
                        Graphique = new Ellipse
                        {
                            Tag = "bulletBoss",
                            Width = Taille*3,
                            Height = Taille*3,
                            Fill = Image,
                            //Stroke = Brushes.Black
                        };
                        Degats = Constantes.DEGATS_BALLE_TROIS;
                        break;
                }
            }
        }


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
            MainWindow MainWindow = (MainWindow)App.Current.MainWindow;
            Vector2 VecteurNormalize = Vector2.Normalize(this.Vecteur);

            switch (Type)
            {
                case 0:
                {
                    double NewX = PosX + (VecteurNormalize.X * this.Vitesse);
                    double NewY = PosY + (VecteurNormalize.Y * this.Vitesse);

                    PosX = NewX;
                    PosY = NewY;
                    break;
                }
                case 1:
                {
                    double NewX = PosX + (VecteurNormalize.X * this.Vitesse);
                    double NewY = PosY + (VecteurNormalize.Y * this.Vitesse);

                    PosX = NewX;
                    PosY = NewY;
                    break;
                }
                case 2:
                {
                    double Sinus = Math.Sin(CoefSin) * 2;
                    double Cosinus = Math.Cos(CoefSin) *2;

                    Vector2 VecteurNormal = new Vector2(VecteurNormalize.Y, -VecteurNormalize.X);

                    double NouveauX = PosX + (VecteurNormal.X * Cosinus - VecteurNormal.Y * Sinus) * this.Vitesse;
                    double NouveauY = PosY + (VecteurNormal.X * Sinus + VecteurNormal.Y * Cosinus) * this.Vitesse;

                    PosX = NouveauX;
                    PosY = NouveauY;

                    
                    if (!InverseSin)
                    {
                        CoefSin += 0.1f;
                        if (CoefSin > Math.PI) { InverseSin = true; }
                    }
                    else
                    {
                        CoefSin -= 0.1f;
                        if (CoefSin <= 0) { InverseSin = false; }
                    }
                    break;
                }
                case 3:
                {

                        double PosJoueurX = MainWindow.fenetrePrincipale.Width / 2 - MainWindow.rectJoueur.Width * 0.5;
                        double PosJoueurY = MainWindow.fenetrePrincipale.Height / 2 - MainWindow.rectJoueur.Height * 0.5;



                        Vector2 VecteurBalleVersJoueur = new Vector2((float)this.PosX - (float)PosJoueurX, (float)this.PosY - (float)PosJoueurY);
                        Vector2 VecteurMoitie = Vector2.Normalize(new Vector2((float) PosJoueurX - (float)this.PosX, (float)PosJoueurY - (float) this.PosY));
                        Vector2.Multiply(0.5f, VecteurMoitie);
                        Vector2 NouveauVecteur = Vector2.Normalize(Vector2.Add(VecteurNormalize, VecteurMoitie));

                        if (Math.Abs(this.PosX - PosJoueurX) > 500 || Math.Abs(this.PosY - PosJoueurY) > 500)
                        {
                            NouveauVecteur = VecteurNormalize;
                        } else if (CoefRotation % 360 == 0)
                        {
                            this.Vecteur = NouveauVecteur;
                        }


                    double NewX = PosX + (NouveauVecteur.X * this.Vitesse);
                    double NewY = PosY + (NouveauVecteur.Y * this.Vitesse);

                        CoefRotation += 5;
                        if (CoefRotation >= 360)
                        {
                            CoefRotation = 0;
                        }

                        this.Graphique.RenderTransform = new RotateTransform(CoefRotation, this.Graphique.Width /2, this.Graphique.Height /2 );

                    this.PosX = NewX;
                    this.PosY = NewY;
                    break;
                }
            }
        }
    }
}
