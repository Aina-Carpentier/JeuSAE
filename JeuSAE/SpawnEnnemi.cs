using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JeuSAE
{
    public class SpawnEnnemi
    {

        static Random random = new Random();

        public static void SpawnUnEnnemi (MainWindow mainWindow)
        {
            double posJoueurX = mainWindow.fenetrePrincipale.Width / 2;
            double posJoueurY = mainWindow.fenetrePrincipale.Height / 2;

            int y = random.Next(0, 2000);
            int x = random.Next(500, 2000) - y;



            if (random.Next(0,2) == 0)
            {
                x = x * (-1);
            }
            if (random.Next(0, 2) == 0)
            {
                y = y * (-1);
            }


            Ennemi ennemi = new Ennemi(random.Next(0, 8), x, y);
            Canvas.SetLeft(ennemi.Graphique, posJoueurX + x);
            Canvas.SetTop(ennemi.Graphique, posJoueurY + y);
            Canvas.SetZIndex(ennemi.Graphique, 1); 

            mainWindow.monCanvas.Children.Add(ennemi.Graphique);

        }



    }
}
