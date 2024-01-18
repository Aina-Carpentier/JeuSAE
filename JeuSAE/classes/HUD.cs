using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JeuSAE.classes
{
    public class HUD
    {
        static MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
        static double largeurBarVieMax = mainWindow.rectanglePV.Width;
        static double largeurBarExpMax = mainWindow.rectangleEXP.Width;
        static double largeurBarEliminationsMax = mainWindow.rectangleElimination.Width;


        public static void ChangeBarreDeVie(double pourcentage)
        {

            mainWindow.rectanglePV.Width = largeurBarVieMax * pourcentage;
            

        }


        public static void AjouteVie(int nombrePV)
        {

            
            
                double nouvelleLargeur = ((nombrePV) / 100d) * largeurBarVieMax;
                if (nouvelleLargeur + mainWindow.rectanglePV.Width >= largeurBarVieMax)
                {// Si la vie ajoutée fait dépasser la vie max, on la met juste au max
                    mainWindow.rectanglePV.Width = largeurBarVieMax;
                }
                else if (nouvelleLargeur + mainWindow.rectanglePV.Width <= 0)
                {// Si la vie ajoutée (enlevée dans ce cas) rends la vie plus petite que 0, on la met à 0
                    mainWindow.rectanglePV.Width = 0;
                } else
                {// Sinon on ajoute la vie
                    mainWindow.rectanglePV.Width += nouvelleLargeur;
                }

            

        }


        public static void ChangeBarreExp(double pourcentage)
        {

            mainWindow.rectangleEXP.Width = largeurBarExpMax * pourcentage;

        }

        public static void AjouteExp(int nombreEXP)
        {

            if (mainWindow.rectangleEXP.Width != largeurBarExpMax)
            {
                double nouvelleLargeur = ((nombreEXP) / 100d) * largeurBarExpMax;
                if (nouvelleLargeur + mainWindow.rectangleEXP.Width >= largeurBarExpMax)
                {
                    mainWindow.rectangleEXP.Width = largeurBarExpMax;
                }
                else
                {
                    mainWindow.rectangleEXP.Width += nouvelleLargeur;
                }

            }

        }



        public static void ChangeBarreEliminations(double pourcentage)
        {

            mainWindow.rectangleElimination.Width = largeurBarEliminationsMax * pourcentage;

        }

        public static void AjouteElimination(int nombreEliminations)
        {

            if (mainWindow.rectangleElimination.Width != largeurBarEliminationsMax)
            {
                double nouvelleLargeur = ((nombreEliminations) / 100d) * largeurBarEliminationsMax;
                if (nouvelleLargeur + mainWindow.rectangleElimination.Width >= largeurBarEliminationsMax)
                {
                    mainWindow.rectangleElimination.Width = largeurBarEliminationsMax;
                }
                else
                {
                    mainWindow.rectangleElimination.Width += nouvelleLargeur;
                }

            }

        }



        public static void FixeNombreDiamants(int nombreDiamants)
        {

            mainWindow.labDiamant.Content = nombreDiamants.ToString();

        }

        public static void AjouteNombreDiamants(int nombreDiamants)
        {

            int nombreDiamantDansLabel = int.Parse(mainWindow.labDiamant.Content.ToString());

            mainWindow.labDiamant.Content = (nombreDiamants + nombreDiamantDansLabel).ToString();

        }






    }
}
