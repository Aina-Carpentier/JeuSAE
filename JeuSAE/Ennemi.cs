using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuSAE
{
    internal class Ennemi
    {
        private double vie;
        private double vitesse;
        private double cadenceTir;
        private int type;
        private String nom;
        private double posX;
        private double posY;
        private Guid id = Guid.NewGuid();


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




        public Guid Id
        {
            get { return id; }
        }


        public Ennemi(int type, double posX, double posY)
        {
            Type = type;

            switch (type)
            {
                case 0://TODO faire les différents types d'ennemis
                    break;
            }
            PosX = posX;
            PosY = posY;
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
