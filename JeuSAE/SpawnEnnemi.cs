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

        Random random = new Random();

        private void SpawnUnEnnemi (MainWindow mainWindow)
        {
            int x = random.Next(300, 1001);
            int y = random.Next(300, 1001);

            Ennemi ennemi = new Ennemi(random.Next(0, 8), x, y);
            //Canvas.SetLeft(ennemi.Graphique, )


            //mainWindow.monCanvas.Children.Add(ennemi.Graphique);





        }



    }
}
