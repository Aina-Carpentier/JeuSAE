using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuSAE.classes
{
    public class MenuMaxEXP
    {
         static MainWindow mainWindow = (MainWindow) App.Current.MainWindow;

        public static void afficheMenu()
        {





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

        }


        public static void cacheMenu()
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



        }




    }
}
