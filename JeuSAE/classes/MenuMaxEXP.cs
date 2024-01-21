﻿using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace JeuSAE.classes
{
    public class MenuMaxEXP
    {
        static MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
        static Random random = new Random();
        public static int numAmeliorationBouttonUn = 0, numAmeliorationBouttonDeux = 0, numAmeliorationBouttonTrois = 0;


        public static void AfficheMenu()
        {

            List<int> listeAmeliorationsAChoisir = new List<int>();
            List<int> listeAmeliorations = new List<int>();





            /*
             * dégats
             * rapidité des balles
             * speed du joueur
             * (multishot?)
             * taille des balles
             * vie
             * cadence de tir améliorée
             * piercing des balles
             * life leech
             */


            for (int i = 0; i < Constantes.NOMBRE_AMELIORATIONS; i++)
            {
                listeAmeliorationsAChoisir.Add(i);
            }

            for (int i = 0; i < 3; i++)
            {
                int ameliorationIndex = random.Next(0, Constantes.NOMBRE_AMELIORATIONS - i);
                listeAmeliorations.Add(listeAmeliorationsAChoisir[ameliorationIndex]);
                listeAmeliorationsAChoisir.RemoveAt(ameliorationIndex);
            }


#if DEBUG
            Console.WriteLine(listeAmeliorations);
#endif

            String ameliorationString = "";

            for (int i = 0; i < listeAmeliorations.Count; i++)
            {
                int numSwitch = 0;
                double valeurActuelle = 0;
                switch (listeAmeliorations[i])
                {
                    case 0:
                        ameliorationString = "Dégats + 1";
                        valeurActuelle = Constantes.DEGATS_JOUEUR;
                        numSwitch = 0;
                        break;
                    case 1:
                        ameliorationString = "Vitesse Balles + 20 %";
                        valeurActuelle = Math.Round( Constantes.VITESSE_BALLE_JOUEUR, 1);
                        numSwitch = 1;
                        break;
                    case 2:
                        ameliorationString = "Vitesse + 20 %";
                        valeurActuelle = Math.Round( Constantes.VITESSE_JOUEUR, 1);
                        numSwitch = 2;
                        break;
                    case 3:
                        ameliorationString = "Taille des Balles + 10";
                        valeurActuelle = Constantes.TAILLE_BALLE_JOUEUR;
                        numSwitch = 3;
                        break;
                    case 4:
                        ameliorationString = "Vie + 20";
                        valeurActuelle = Constantes.VIE_JOUEUR;
                        numSwitch = 4;
                        break;
                    case 5:
                        ameliorationString = "Cadence de tir + 20 %";
                        valeurActuelle = Math.Round( 60 / Constantes.TEMPS_RECHARGE_ARME , 1);
                        numSwitch = 5;
                        break;
                    case 6:
                        ameliorationString = "Perçage +1";
                        valeurActuelle = Constantes.BALLE_NOMBRE_PERCE;
                        numSwitch = 6;
                        break;
                }

                AssigneAmeliorations(ameliorationString, i, valeurActuelle, numSwitch);


            }







            mainWindow.tableauMaxEXP.Visibility = System.Windows.Visibility.Visible;
            mainWindow.bouttonUpgrade1.Visibility = System.Windows.Visibility.Visible;
            mainWindow.bouttonUpgrade2.Visibility = System.Windows.Visibility.Visible;
            mainWindow.bouttonUpgrade3.Visibility = System.Windows.Visibility.Visible;
            mainWindow.labAmelioration1.Visibility = System.Windows.Visibility.Visible;
            mainWindow.labAmelioration2.Visibility = System.Windows.Visibility.Visible;
            mainWindow.labAmelioration3.Visibility = System.Windows.Visibility.Visible;
            mainWindow.labActuelle1.Visibility = System.Windows.Visibility.Visible;
            mainWindow.labActuelle2.Visibility = System.Windows.Visibility.Visible;
            mainWindow.labActuelle3.Visibility = System.Windows.Visibility.Visible;
            mainWindow.labBonus.Visibility = System.Windows.Visibility.Visible;
            mainWindow.Cursor = Cursors.Arrow;

            MainWindow.ouvreMenuMaxEXP = true;


        }


        public static void CacheMenu()
        {

            mainWindow.tableauMaxEXP.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.bouttonUpgrade1.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.bouttonUpgrade2.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.bouttonUpgrade3.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.labAmelioration1.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.labAmelioration2.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.labAmelioration3.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.labActuelle1.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.labActuelle2.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.labActuelle3.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.labBonus.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.Cursor = Cursors.None;

            MainWindow.ouvreMenuMaxEXP = false;
            Constantes.COEFFICIENT_EXPERIENCE *= 0.9;


        }

        private static void AssigneAmeliorations(String ameliorationString, int numLabel, double valeur, int numSwitch)
        {
            String stringActuelle = "Valeur actuelle : " + Environment.NewLine + valeur.ToString();

            if (numSwitch == 5)
            {
                stringActuelle += " tir/s";
            }



            if (numLabel == 0)
            {
                mainWindow.labAmelioration1.Content = ameliorationString;
                mainWindow.labActuelle1.Content = stringActuelle;
                numAmeliorationBouttonUn = numSwitch;
            }
            else if (numLabel == 1)
            {
                mainWindow.labAmelioration2.Content = ameliorationString;
                mainWindow.labActuelle2.Content = stringActuelle;
                numAmeliorationBouttonDeux = numSwitch;
            }
            else if (numLabel == 2)
            {
                mainWindow.labAmelioration3.Content = ameliorationString;
                mainWindow.labActuelle3.Content = stringActuelle;
                numAmeliorationBouttonTrois = numSwitch;
            }




        }

        public static void AppliqueBonusAuJoueur(int numBoutton)
        {
            int valeurUtilise = 0;
            switch (numBoutton)
            {
                case 1:
                    valeurUtilise = numAmeliorationBouttonUn;
                    break;
                case 2:
                    valeurUtilise = numAmeliorationBouttonDeux;
                    break;
                case 3:
                    valeurUtilise = numAmeliorationBouttonTrois;
                    break;
            }


            switch (valeurUtilise)
            {
                case 0:
                    Constantes.DEGATS_JOUEUR += 1;
                    break;
                case 1:
                    Constantes.VITESSE_BALLE_JOUEUR *= 1.2;
                    break;
                case 2:
                    Constantes.VITESSE_JOUEUR *= 1.2;
                    break;
                case 3:
                    Constantes.TAILLE_BALLE_JOUEUR += 10;
                    break;
                case 4:
                    Constantes.VIE_JOUEUR += 20;
                    break;
                case 5:
                    Constantes.TEMPS_RECHARGE_ARME *= 0.8;
                    break;
                case 6:
                    Constantes.BALLE_NOMBRE_PERCE += 1;
                    break;
            }


            CacheMenu();



        }




    }
}