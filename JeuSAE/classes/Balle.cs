using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


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
        private Vector2 vecteur;
        private Ellipse graphique;

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

        public Vector2 Vecteur
        {
            get { return vecteur; }
            set { vecteur = value; }
        }

        public Rect Rect { get => new Rect(PosX, PosY, Taille, Taille); }

        public Ellipse Graphique
        {
            get { return graphique; }
            set { graphique = value; }
        }

        public Balle(double vitesse, double taille, int type, string tireur, double acceleration, double posX, double posY, Vector2 vecteur)
        {
            Vitesse = vitesse;
            Taille = taille;
            Type = type;
            Tireur = tireur;
            Acceleration = acceleration;
            PosX = posX - taille / 2 + Vector2.Normalize(vecteur).X;
            PosY = posY - taille / 2 + Vector2.Normalize(vecteur).Y;
            Vecteur = vecteur;
            //Rect = new Rect(PosX, posY, Constantes.BALLE_WIDHT, Constantes.BALLE_HEIGHT);

            if (tireur == "joueur")
            {
                Graphique = new Ellipse
                {
                    Tag = "bulletPlayer",
                    Width = taille,
                    Height = taille,
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
                            Width = taille,
                            Height = taille,
                            Fill = Brushes.Red,
                            Stroke = Brushes.Black
                        };
                        break;
                    case 2:
                        Graphique = new Ellipse
                        {
                            Tag = "bulletEnnemySin",
                            Width = taille,
                            Height = taille,
                            Fill = Brushes.DarkBlue,
                            Stroke = Brushes.Black
                        };
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

            Vector2 vecteurNormalize = Vector2.Normalize(Vecteur);
            double newX = PosX + vecteurNormalize.X * Vitesse;
            double newY = PosY + vecteurNormalize.Y * Vitesse;

            PosX = newX;
            PosY = newY;


        }
    }
}
