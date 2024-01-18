﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuSAE.classes
{
    internal class Constantes
    {
        public const int BALLE_WIDHT = 25;
        public const int BALLE_HEIGHT = 10;
        public const double VIE_JOUEUR = 100d;
        public const int DEGATS_BALLE_UN = 10;
        public const int DEGATS_BALLE_DEUX = 5;


        //-------------------------------------------ENNEMI-------------------------------------------
        public const int ENNEMI_RECT_LARGEUR = 75;
        public const int ENNEMI_RECT_HAUTEUR = 75;


        // VIE

        public const double VIE_TRIANGLE_EQ = 3;
        public const double VIE_CARRE = 4;
        public const double VIE_PENTAGONE = 10;
        public const double VIE_HEXAGONE = 12;
        public const double VIE_HEPTAGONE = 14;
        public const double VIE_OCTOGONE = 20;
        public const double VIE_CERCLE = Math.PI;


        // VITESSE

        public const double VITESSE_TRIANGLE_EQ = 3;
        public const double VITESSE_CARRE = 4;
        public const double VITESSE_PENTAGONE = 3;
        public const double VITESSE_HEXAGONE = 3;
        public const double VITESSE_HEPTAGONE = 2;
        public const double VITESSE_OCTOGONE = 6;
        public const double VITESSE_CERCLE = Math.PI;


        // CADENCE DE TIR

        public const double CADENCE_TRIANGLE_EQ = 3 * 60;
        public const double CADENCE_CARRE = 3 * 60;
        public const double CADENCE_PENTAGONE = 1.5 * 60d;
        public const double CADENCE_HEXAGONE = 1 * 60;
        public const double CADENCE_HEPTAGONE = 0.5 * 60d;
        public const double CADENCE_OCTOGONE = 0.4 * 60d;
        public const double CADENCE_CERCLE = Math.PI * 60d;


        // NOM

        public const string NOM_TRIANGLE_EQ = "Triangle équilatéral";
        public const string NOM_CARRE = "Carré";
        public const string NOM_PENTAGONE = "Pentagone";
        public const string NOM_HEXAGONE = "Hexagone";
        public const string NOM_HEPTAGONE = "Heptagone";
        public const string NOM_OCTOGONE = "Octogone";
        public const string NOM_CERCLE = "Cercle";


    }
}