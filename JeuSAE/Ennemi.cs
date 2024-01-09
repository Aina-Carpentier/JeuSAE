using System;
using System.Drawing;
using System.Windows;

namespace JeuSAE
{
    internal class Ennemi
    {
        private double vie;
        private double vitesse; // En pixel/tick
        private double cadenceTir; // En seconde/tir donc si = 3 alors l'ennemi tir une fois toutes les 3 secondes, donc pour 3 fois par seconde c'est approx 0.33
        private int type;
        private String nom;
        private int posX;
        private int posY;
        private Guid id = Guid.NewGuid();
        private Rect rect;
        private Rectangle graphique;


        public double Vie
        {
            get { return vie; }
            set { vie = value; }
        }


        public double Vitesse
        {
            get { return vitesse; }
            set { vitesse = value; }
        }

        public double CadenceTir
        {
            get { return cadenceTir; }
            set { cadenceTir = value; }
        }

        public int Type
        {
            get { return type; }
            set { type = value; }
        }


        public String Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public int PosX
        {
            get { return posX; }
            set { posX = value; }
        }

        public int PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        public Guid Id
        {
            get { return id; }
        }

        public Rect Rect { get => rect; set => rect = value; }

        public Rectangle Graphique { get => graphique; set => graphique = value; }

        public Ennemi(int type, int posX, int posY)
        {
            Type = type;

            switch (type)
            {
                case 0:
                    this.Vie = 3;
                    this.Vitesse = 1;
                    this.CadenceTir = 3;
                    this.Nom = "Carré";
                    break;
                case 1:
                    this.Vie = 5;
                    this.Vitesse = 1;
                    this.CadenceTir = 2;
                    this.Nom = "Rectangle";
                    break;
                case 2:
                    this.Vie = 3;
                    this.Vitesse = 3;
                    this.CadenceTir = 3;
                    this.Nom = "Triangle équilatéral";
                    break;
                case 3:
                    this.Vie = 10;
                    this.Vitesse = 3;
                    this.CadenceTir = 1.5;
                    this.Nom = "Pentagone";
                    break;
                case 4:
                    this.Vie = 12;
                    this.Vitesse = 3;
                    this.CadenceTir = 1;
                    this.Nom = "Hexagone";
                    break;
                case 5:
                    this.Vie = 14;
                    this.Vitesse = 2;
                    this.CadenceTir = 0.5;
                    this.Nom = "Heptagone";
                    break;
                case 6:
                    this.Vie = 20;
                    this.Vitesse = 8;
                    this.CadenceTir = 0.4;
                    this.Nom = "Octogone";
                    break;
                case 7:
                    this.Vie = Math.PI;
                    this.Vitesse = Math.PI;
                    this.CadenceTir = Math.PI;
                    this.Nom = "Cercle";
                    break;
                case 8:
                    this.Vie = 10;
                    this.Vitesse = 3;
                    this.CadenceTir = 4;
                    this.Nom = "Triangle rectangle";
                    break; //TODO ajouter plus d'ennemis si on a des idées
            }
            PosX = posX;
            PosY = posY;
            Rect = new Rect(PosX, PosY, Constantes.ENNEMI_RECT_LARGEUR, Constantes.ENNEMI_RECT_HAUTEUR);
            Graphique = new Rectangle(PosX, PosY, Constantes.ENNEMI_RECT_LARGEUR, Constantes.ENNEMI_RECT_HAUTEUR);
        }

        public override bool Equals(object? obj)
        {
            return obj is Ennemi ennemi &&
                   Id.Equals(ennemi.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }

        public override string? ToString()
        {
            return this.Nom;
        }
    }
}
