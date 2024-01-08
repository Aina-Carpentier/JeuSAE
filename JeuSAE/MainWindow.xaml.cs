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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace JeuSAE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int countTick = 0;
        private bool gauche = false, droite = false, haut = false, bas = false;

        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += GameEngine;
            // rafraissement toutes les 16 milliseconds
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            // lancement du timer
            dispatcherTimer.Start();    
        }

        private void CanvasKeyIsDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("ytes");
            if (e.Key == Key.Left)
                gauche = true;
            if (e.Key == Key.Right)
                droite = true;
            if (e.Key == Key.Up)
                haut = true;
            if (e.Key == Key.Down)
                bas = true;
        }

        private void CanvasKeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
                gauche = false;
            if (e.Key == Key.Right)
                droite = false;
            if (e.Key == Key.Up)
                haut = false;
            if (e.Key == Key.Down)
                bas = false;
        }


        private void GameEngine(object sender, EventArgs e)
        {

            Rect player = new Rect(Canvas.GetLeft(rectJoueur), Canvas.GetTop(rectJoueur), rectJoueur.Width, rectJoueur.Height);
            if (gauche)
            {
                Canvas.SetLeft(rectCarte, Canvas.GetLeft(rectCarte) + 100);
            }
            if (droite)
            {
                Canvas.SetLeft(rectCarte, Canvas.GetLeft(rectCarte) - 100);
            }
            if (haut)
            {
                Canvas.SetTop(rectCarte, Canvas.GetTop(rectCarte) + 100);
            }
            if (bas)
            {
                Canvas.SetTop(rectCarte, Canvas.GetTop(rectCarte) - 100);
            }

        }

    }
}
