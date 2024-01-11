using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JeuSAE
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public Menu()
        {
            InitializeComponent();
            canvasMenu.Height = windowMenu.Height;
            canvasMenu.Width = windowMenu.Width;

            double coteImage = (canvasMenu.Width) + 50;

            background1.Width = coteImage;
            background1.Height = coteImage;
            background2.Width = coteImage;
            background2.Height = coteImage;

            Canvas.SetLeft(background1, 0);//TODO fix the scaling and do the scroll logic
            Canvas.SetLeft(background2, background1.Width);
            //background2.Visibility = Visibility.Hidden;



            dispatcherTimer.Tick += GameEngine;
            // rafraissement toutes les 16 milliseconds
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            // lancement du timer
            dispatcherTimer.Start();


        }


        private void GameEngine(object sender, EventArgs e)
        {
            Canvas.SetLeft(background1, Canvas.GetLeft(background1) - 2);
            Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 2);
            
            if (Canvas.GetLeft(background1) + background1.Width < 0)
            {
                Canvas.SetLeft(background1, background1.Width);
            }
            if (Canvas.GetLeft(background2) + background2.Width < 0)
            {
                Canvas.SetLeft(background2, background2.Width);
            }
        }




        }
}
