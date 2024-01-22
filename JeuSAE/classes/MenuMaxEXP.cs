using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace JeuSAE.classes
{
    public class MenuMaxExp
    {
        static MainWindow MainWindow = (MainWindow)App.Current.MainWindow;
        static Random Random = new Random();
        public static int NumAmeliorationBouttonUn = 0, NumAmeliorationBouttonDeux = 0, NumAmeliorationBouttonTrois = 0;


        public static void AfficheMenu()
        {

            List<int> ListeAmeliorationsAChoisir = new List<int>();
            List<int> ListeAmeliorations = new List<int>();





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


            for (int I = 0; I < Constantes.NOMBRE_AMELIORATIONS; I++)
            {
                ListeAmeliorationsAChoisir.Add(I);
            }

            for (int I = 0; I < 3; I++)
            {
                int AmeliorationIndex = Random.Next(0, Constantes.NOMBRE_AMELIORATIONS - I);
                ListeAmeliorations.Add(ListeAmeliorationsAChoisir[AmeliorationIndex]);
                ListeAmeliorationsAChoisir.RemoveAt(AmeliorationIndex);
            }


#if DEBUG
            Console.WriteLine(ListeAmeliorations);
#endif

            String AmeliorationString = "";

            for (int I = 0; I < ListeAmeliorations.Count; I++)
            {
                int NumSwitch = 0;
                double ValeurActuelle = 0;
                switch (ListeAmeliorations[I])
                {
                    case 0:
                        AmeliorationString = "Dégats + 1";
                        ValeurActuelle = Constantes.DEGATS_JOUEUR;
                        NumSwitch = 0;
                        break;
                    case 1:
                        AmeliorationString = "Vitesse Balles + 20 %";
                        ValeurActuelle = Math.Round( Constantes.VITESSE_BALLE_JOUEUR, 1);
                        NumSwitch = 1;
                        break;
                    case 2:
                        AmeliorationString = "Vitesse + 20 %";
                        ValeurActuelle = Math.Round( Constantes.VITESSE_JOUEUR, 1);
                        NumSwitch = 2;
                        break;
                    case 3:
                        AmeliorationString = "Taille des Balles + 10";
                        ValeurActuelle = Constantes.TAILLE_BALLE_JOUEUR;
                        NumSwitch = 3;
                        break;
                    case 4:
                        AmeliorationString = "Vie + 20";
                        ValeurActuelle = Constantes.VIE_JOUEUR;
                        NumSwitch = 4;
                        break;
                    case 5:
                        AmeliorationString = "Cadence de tir + 20 %";
                        ValeurActuelle = Math.Round( 60 / Constantes.TEMPS_RECHARGE_ARME , 1);
                        NumSwitch = 5;
                        break;
                    case 6:
                        AmeliorationString = "Perçage + 1";
                        ValeurActuelle = Constantes.BALLE_NOMBRE_PERCE;
                        NumSwitch = 6;
                        break;
                        case 7:
                        AmeliorationString = "Vol de vie + 1 %";
                        ValeurActuelle = Constantes.POURCENTAGE_NOMBRE_DE_VIE;
                        NumSwitch = 7;
                        break;
                }

                AssigneAmeliorations(AmeliorationString, I, ValeurActuelle, NumSwitch);


            }







            MainWindow.tableauMaxEXP.Visibility = System.Windows.Visibility.Visible;
            MainWindow.bouttonUpgrade1.Visibility = System.Windows.Visibility.Visible;
            MainWindow.bouttonUpgrade2.Visibility = System.Windows.Visibility.Visible;
            MainWindow.bouttonUpgrade3.Visibility = System.Windows.Visibility.Visible;
            MainWindow.labAmelioration1.Visibility = System.Windows.Visibility.Visible;
            MainWindow.labAmelioration2.Visibility = System.Windows.Visibility.Visible;
            MainWindow.labAmelioration3.Visibility = System.Windows.Visibility.Visible;
            MainWindow.labActuelle1.Visibility = System.Windows.Visibility.Visible;
            MainWindow.labActuelle2.Visibility = System.Windows.Visibility.Visible;
            MainWindow.labActuelle3.Visibility = System.Windows.Visibility.Visible;
            MainWindow.labBonus.Visibility = System.Windows.Visibility.Visible;
            MainWindow.Cursor = Cursors.Arrow;

            MainWindow.OuvreMenuMaxExp = true;


        }


        public static void CacheMenu()
        {

            MainWindow.tableauMaxEXP.Visibility = System.Windows.Visibility.Hidden;
            MainWindow.bouttonUpgrade1.Visibility = System.Windows.Visibility.Hidden;
            MainWindow.bouttonUpgrade2.Visibility = System.Windows.Visibility.Hidden;
            MainWindow.bouttonUpgrade3.Visibility = System.Windows.Visibility.Hidden;
            MainWindow.labAmelioration1.Visibility = System.Windows.Visibility.Hidden;
            MainWindow.labAmelioration2.Visibility = System.Windows.Visibility.Hidden;
            MainWindow.labAmelioration3.Visibility = System.Windows.Visibility.Hidden;
            MainWindow.labActuelle1.Visibility = System.Windows.Visibility.Hidden;
            MainWindow.labActuelle2.Visibility = System.Windows.Visibility.Hidden;
            MainWindow.labActuelle3.Visibility = System.Windows.Visibility.Hidden;
            MainWindow.labBonus.Visibility = System.Windows.Visibility.Hidden;
            MainWindow.Cursor = Cursors.None;

            MainWindow.OuvreMenuMaxExp = false;
            Constantes.COEFFICIENT_EXPERIENCE *= 0.9;


        }

        private static void AssigneAmeliorations(String ameliorationString, int numLabel, double valeur, int numSwitch)
        {
            String StringActuelle = "Valeur actuelle : " + Environment.NewLine + valeur.ToString();

            if (numSwitch == 5)
            {
                StringActuelle += " tir/s";
            }



            if (numLabel == 0)
            {
                MainWindow.labAmelioration1.Content = ameliorationString;
                MainWindow.labActuelle1.Content = StringActuelle;
                NumAmeliorationBouttonUn = numSwitch;
            }
            else if (numLabel == 1)
            {
                MainWindow.labAmelioration2.Content = ameliorationString;
                MainWindow.labActuelle2.Content = StringActuelle;
                NumAmeliorationBouttonDeux = numSwitch;
            }
            else if (numLabel == 2)
            {
                MainWindow.labAmelioration3.Content = ameliorationString;
                MainWindow.labActuelle3.Content = StringActuelle;
                NumAmeliorationBouttonTrois = numSwitch;
            }




        }

        public static void AppliqueBonusAuJoueur(int numBoutton)
        {
            int ValeurUtilise = 0;
            switch (numBoutton)
            {
                case 1:
                    ValeurUtilise = NumAmeliorationBouttonUn;
                    break;
                case 2:
                    ValeurUtilise = NumAmeliorationBouttonDeux;
                    break;
                case 3:
                    ValeurUtilise = NumAmeliorationBouttonTrois;
                    break;
            }


            switch (ValeurUtilise)
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
                    case 7:
                    Constantes.POURCENTAGE_NOMBRE_DE_VIE += 1;
                    break;
            }


            CacheMenu();



        }




    }
}
