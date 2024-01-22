using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace JeuSAE.classes
{
    public class Hud
    {
        private static readonly MainWindow MainWindow = (MainWindow)App.Current.MainWindow;
        private static readonly double LargeurBarVieMax = MainWindow.rectanglePV.Width;
        private static readonly double LargeurBarExpMax = MainWindow.rectangleEXP.Width;
        private static readonly double LargeurBarEliminationsMax = MainWindow.rectangleElimination.Width;


        public static void ChangeBarreDeVie(double pourcentage)
        {
            MainWindow.rectanglePV.Width = LargeurBarVieMax * pourcentage;
        }


        public static void AjouteVie(double nombrePv)
        {
            double NouvelleLargeur = ((nombrePv) / Constantes.VIE_JOUEUR) * LargeurBarVieMax;

            if (NouvelleLargeur + MainWindow.rectanglePV.Width >= LargeurBarVieMax)
            {// Si la vie ajoutée fait dépasser la vie max, on la met juste au max
                MainWindow.rectanglePV.Width = LargeurBarVieMax;
            }
            else if (NouvelleLargeur + MainWindow.rectanglePV.Width <= 0)
            {// Si la vie ajoutée (enlevée dans ce cas) rends la vie plus petite que 0, on la met à 0
                MainWindow.rectanglePV.Width = 0; //TODO activer le killscreen ici
                MainWindow.Mort = true;
                //pour l'animation de mort
                MainWindow.TickAnimation = 0;
                MainWindow.curseurPerso.Visibility = Visibility.Hidden;
                MainWindow.Cursor = Cursors.Arrow;
                MainWindow.labQuitter.Visibility = Visibility.Visible;
                MainWindow.labRetour.Visibility = Visibility.Visible;
                MainWindow.MortDroite = MainWindow.RegardeADroite;
            } 
            else
            {// Sinon on ajoute la vie
                MainWindow.rectanglePV.Width += NouvelleLargeur;
            }
        }

        public static void ChangeBarreExp(double pourcentage)
        {
            MainWindow.rectangleEXP.Width = LargeurBarExpMax * pourcentage;
        }

        public static void AjouteExp(double nombreExp)
        {

            if (MainWindow.rectangleEXP.Width != LargeurBarExpMax)
            {
                double NouvelleLargeur = ((nombreExp) / 100d) * LargeurBarExpMax;
                if (NouvelleLargeur + MainWindow.rectangleEXP.Width >= LargeurBarExpMax)
                {
                    MainWindow.rectangleEXP.Width = 0;
                    MenuMaxExp.AfficheMenu();
                }
                else
                {
                    MainWindow.rectangleEXP.Width += NouvelleLargeur;
                }

            }

        }

        public static void ChangeBarreEliminations(double pourcentage)
        {

            MainWindow.rectangleElimination.Width = LargeurBarEliminationsMax * pourcentage;

        }
        
        public static bool BossDoitSpawn()
        {
            return (MainWindow.rectangleElimination.Width / LargeurBarEliminationsMax) == 1;
        }

        public static void AjouteElimination(int nombreEliminations)
        {

            if (MainWindow.rectangleElimination.Width != LargeurBarEliminationsMax)
            {
                double NouvelleLargeur = ((nombreEliminations) / 100d) * LargeurBarEliminationsMax;
                if (NouvelleLargeur + MainWindow.rectangleElimination.Width >= LargeurBarEliminationsMax)
                {
                    MainWindow.rectangleElimination.Width = 0;
                    Boss.SpawnUnBoss(MainWindow);
                }
                else
                {
                    MainWindow.rectangleElimination.Width += NouvelleLargeur;
                }
            }
        }

        public static void FixeNombreDiamants(int nombreDiamants)
        {

            MainWindow.labDiamant.Content = nombreDiamants.ToString();

        }

        public static void AjouteNombreDiamants(int nombreDiamants)
        {
            int NombreDiamantDansLabel = int.Parse(MainWindow.labDiamant.Content.ToString());
            MainWindow.labDiamant.Content = (nombreDiamants + NombreDiamantDansLabel).ToString();
        }
    }
}
