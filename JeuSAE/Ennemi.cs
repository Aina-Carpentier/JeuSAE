using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JeuSAE
{
    internal class Ennemi
    {
        private double vie;
        private double vitesse;
        private double cadenceTir;
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
                case 0://TODO faire les différents types d'ennemis
                    break;
            }
            PosX = posX;
            PosY = posY;
            Graphique = new Rectangle(PosX, PosY, 75, 75); // TODO: Editer en fonction du type d'ennemi la taille
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
