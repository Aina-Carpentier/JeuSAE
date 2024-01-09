using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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

        public Balle(double vitesse, double taille, int type, string tireur, double acceleration, double posX, double posY, Vector2 vecteur)
        {
            Vitesse = vitesse;
            Taille = taille;
            Type = type;
            Tireur = tireur;
            Acceleration = acceleration;
            PosX = posX;
            PosY = posY;
            Vecteur = vecteur;
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
    }
}
