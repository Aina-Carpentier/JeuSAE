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


namespace JeuSAE
{
    public class Balle
    {
        private double vitesse;
        private double taille;
        private int type;
        private String tireur;
        private double acceleration;
        private double posX;
        private double posY;
        private Vector2 vecteur;
        private Rect rect;
        private Ellipse graphique;

        public double Vitesse
        {
            get { return vitesse; }
            set { vitesse = value; }
        }

        public double Taille
        {
            get { return taille; }
            set { 
                if (value <= 0) { throw new ArgumentException("La taille ne peut pas être inférieure ou égale à 0. "); }
                taille = value; 
            }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        public String Tireur
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

        public Rect Rect { get => rect; set => rect = value; }

        public Ellipse Graphique 
        {
            get { return graphique; }
            set { graphique = value;}
        }

        public Balle(double vitesse, double taille, int type, string tireur, double acceleration, double posX, double posY, Vector2 vecteur)
        {
            Vitesse = vitesse;
            Taille = taille;
            Type = type;
            Tireur = tireur;
            Acceleration = acceleration;
            PosX = posX-taille/2;
            PosY = posY-taille/2;
            Vecteur = vecteur;
            Rect = new Rect(PosX, posY, Constantes.BALLE_WIDHT, Constantes.BALLE_HEIGHT);

            Graphique = new Ellipse{
                Tag = "bulletPlayer",
                Width = taille,
                Height = taille,
                Fill = Brushes.Red,
                Stroke = Brushes.Black
            };
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

            Vector2 vecteurNormalize = Vector2.Normalize(vecteur);
            double newX = PosX + (vecteurNormalize.X * this.Vitesse);
            double newY = PosY + (vecteurNormalize.Y * this.Vitesse);

            this.PosX = newX;
            this.PosY = newY;


        }
    }
}
