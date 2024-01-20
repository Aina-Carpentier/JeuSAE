using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace JeuSAE.classes
{
    public class MenuMaxEXP
    {
        static MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
        static Random random = new Random();


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
                int valeurActuelle;
                switch (listeAmeliorations[i])
                {
                    case 0:
                        ameliorationString = "Dégats + 1";
                        
                        numSwitch = 0;
                        break;
                    case 1:
                        ameliorationString = "Vitesse Balles + 20 %";
                        numSwitch = 1;
                        break;
                    case 2:
                        ameliorationString = "Vitesse + 20 %";
                        numSwitch = 2;
                        break;
                    case 3:
                        ameliorationString = "Taille des Balles + 10 px";
                        numSwitch = 3;
                        break;
                    case 4:
                        ameliorationString = "Vie + 20";
                        numSwitch = 4;
                        break;
                    case 5:
                        ameliorationString = "Cadence de tir + 20 %";
                        numSwitch = 5;
                        break;
                }

                AssigneAmeliorations(ameliorationString, i, numSwitch);


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



        }

        private static void AssigneAmeliorations(String ameliorationString, int numLabel, int numSwitch)
        {
            String valeurActuelle = "";



            if (numLabel == 0) { mainWindow.labAmelioration1.Content = ameliorationString; }
            else if (numLabel == 1) { mainWindow.labAmelioration2.Content = ameliorationString; }
            else if (numLabel == 2) { mainWindow.labAmelioration3.Content = ameliorationString; }


        }




    }
}
