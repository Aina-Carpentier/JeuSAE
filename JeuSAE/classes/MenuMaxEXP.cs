using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace JeuSAE.classes;

public class MenuMaxExp
{
    private static readonly MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;
    private static readonly Random Random = new();
    public static int NumAmeliorationBouttonUn, NumAmeliorationBouttonDeux, NumAmeliorationBouttonTrois;


    public static void AfficheMenu()
    {
        var ListeAmeliorationsAChoisir = new List<int>();
        var ListeAmeliorations = new List<int>();


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


        for (var I = 0; I < Constantes.NOMBRE_AMELIORATIONS; I++) ListeAmeliorationsAChoisir.Add(I);

        for (var I = 0; I < 3; I++)
        {
            var AmeliorationIndex = Random.Next(0, Constantes.NOMBRE_AMELIORATIONS - I);
            ListeAmeliorations.Add(ListeAmeliorationsAChoisir[AmeliorationIndex]);
            ListeAmeliorationsAChoisir.RemoveAt(AmeliorationIndex);
        }


#if DEBUG
        Console.WriteLine(ListeAmeliorations);
#endif

        var AmeliorationString = "";

        for (var I = 0; I < ListeAmeliorations.Count; I++)
        {
            var NumSwitch = 0;
            double ValeurActuelle = 0;
            switch (ListeAmeliorations[I])
            {
                case 0:
                    AmeliorationString = "Dégats + 1";
                    ValeurActuelle = MainWindow.Degat_Joueur;
                    NumSwitch = 0;
                    break;
                case 1:
                    AmeliorationString = "Vitesse Balles + 20 %";
                    ValeurActuelle = Math.Round(Constantes.VITESSE_BALLE_JOUEUR, 1);
                    NumSwitch = 1;
                    break;
                case 2:
                    AmeliorationString = "Vitesse + 20 %";
                    ValeurActuelle = Math.Round(Constantes.VITESSE_JOUEUR, 1);
                    NumSwitch = 2;
                    break;
                case 3:
                    AmeliorationString = "Taille des Balles + 10";
                    ValeurActuelle = MainWindow.Taille_Balle_Joueur;
                    NumSwitch = 3;
                    break;
                case 4:
                    AmeliorationString = "Vie + 20";
                    ValeurActuelle = Constantes.VIE_JOUEUR;
                    NumSwitch = 4;
                    break;
                case 5:
                    AmeliorationString = "Cadence de tir + 20 %";
                    ValeurActuelle = Math.Round(60 / MainWindow.Temps_Recharge_Arme, 1);
                    NumSwitch = 5;
                    break;
                case 6:
                    AmeliorationString = "Perçage + 1";
                    ValeurActuelle = MainWindow.Balle_Nombre_Perce;
                    NumSwitch = 6;
                    break;
                case 7:
                    AmeliorationString = "Vol de vie + 5 %";
                    ValeurActuelle = Constantes.POURCENTAGE_NOMBRE_DE_VIE;
                    NumSwitch = 7;
                    break;
            }

            AssigneAmeliorations(AmeliorationString, I, ValeurActuelle, NumSwitch);
        }


        MainWindow.tableauMaxEXP.Visibility = Visibility.Visible;
        MainWindow.bouttonUpgrade1.Visibility = Visibility.Visible;
        MainWindow.bouttonUpgrade2.Visibility = Visibility.Visible;
        MainWindow.bouttonUpgrade3.Visibility = Visibility.Visible;
        MainWindow.labAmelioration1.Visibility = Visibility.Visible;
        MainWindow.labAmelioration2.Visibility = Visibility.Visible;
        MainWindow.labAmelioration3.Visibility = Visibility.Visible;
        MainWindow.labActuelle1.Visibility = Visibility.Visible;
        MainWindow.labActuelle2.Visibility = Visibility.Visible;
        MainWindow.labActuelle3.Visibility = Visibility.Visible;
        MainWindow.labBonus.Visibility = Visibility.Visible;
        MainWindow.Cursor = Cursors.Arrow;

        MainWindow.OuvreMenuMaxExp = true;
    }


    public static void CacheMenu()
    {
        MainWindow.tableauMaxEXP.Visibility = Visibility.Hidden;
        MainWindow.bouttonUpgrade1.Visibility = Visibility.Hidden;
        MainWindow.bouttonUpgrade2.Visibility = Visibility.Hidden;
        MainWindow.bouttonUpgrade3.Visibility = Visibility.Hidden;
        MainWindow.labAmelioration1.Visibility = Visibility.Hidden;
        MainWindow.labAmelioration2.Visibility = Visibility.Hidden;
        MainWindow.labAmelioration3.Visibility = Visibility.Hidden;
        MainWindow.labActuelle1.Visibility = Visibility.Hidden;
        MainWindow.labActuelle2.Visibility = Visibility.Hidden;
        MainWindow.labActuelle3.Visibility = Visibility.Hidden;
        MainWindow.labBonus.Visibility = Visibility.Hidden;
        MainWindow.Cursor = Cursors.None;

        MainWindow.OuvreMenuMaxExp = false;
        Constantes.COEFFICIENT_EXPERIENCE *= 0.9;
    }

    private static void AssigneAmeliorations(string ameliorationString, int numLabel, double valeur, int numSwitch)
    {
        var StringActuelle = "Valeur actuelle : " + Environment.NewLine + valeur;

        if (numSwitch == 5) StringActuelle += " tir/s";


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
        var ValeurUtilise = 0;
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
                MainWindow.Degat_Joueur += 1;
                break;
            case 1:
                Constantes.VITESSE_BALLE_JOUEUR *= 1.2;
                break;
            case 2:
                Constantes.VITESSE_JOUEUR *= 1.2;
                break;
            case 3:
                MainWindow.Taille_Balle_Joueur += 10;
                break;
            case 4:
                Constantes.VIE_JOUEUR += 20;
                Hud.AjouteVie(20);
                break;
            case 5:
                MainWindow.Temps_Recharge_Arme*= 0.8;
                break;
            case 6:
                MainWindow.Balle_Nombre_Perce+= 1;
                break;
            case 7:
                Constantes.POURCENTAGE_NOMBRE_DE_VIE += 5;
                break;
        }


        CacheMenu();
    }
}