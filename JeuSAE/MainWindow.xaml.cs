using System;
using System.Numerics;
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
        private int countTick = 0, vitesseJoueur = 150, tempsRechargeArme = 60, tempsRechargeActuel = 0, vitesseBalle = 10;
        private bool gauche = false, droite = false, haut = false, bas = false, tirer = false;

        

        private Rect player = new Rect(910, 490, 50, 50);



        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += GameEngine;
            // rafraissement toutes les 16 milliseconds
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            // lancement du timer
            dispatcherTimer.Start();    
        }

        private void monCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tirer = true;
        }

        private void monCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tirer = false;
        }

        private void CanvasKeyIsDown(object sender, KeyEventArgs e)
        {
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
            /*
#if DEBUG
            Console.WriteLine(Canvas.GetLeft(rectCarte));
            Console.WriteLine(Canvas.GetTop(rectCarte));
#endif
            */

            MouvementJoueur();
            TirJoueur();
        }

        private void MouvementJoueur()
        {
            if (gauche)
                if (Canvas.GetLeft(rectCarte) + vitesseJoueur < 910)
                {
                    Canvas.SetLeft(rectCarte, Canvas.GetLeft(rectCarte) + vitesseJoueur);
                }

                else
                {
                    Canvas.SetLeft(rectCarte, 910);
                }

            if (droite)
                if (Canvas.GetLeft(rectCarte) - vitesseJoueur > -18240)
                {
                    Canvas.SetLeft(rectCarte, Canvas.GetLeft(rectCarte) - vitesseJoueur);
                }
                else
                {
                    Canvas.SetLeft(rectCarte, -18240);
                }
            if (haut)
            {
                if (Canvas.GetTop(rectCarte) + vitesseJoueur < 490)
                {
                    Canvas.SetTop(rectCarte, Canvas.GetTop(rectCarte) + vitesseJoueur);
                }
                else
                {
                    Canvas.SetTop(rectCarte, 490);
                }
            }
            if (bas)
                if (Canvas.GetTop(rectCarte) - vitesseJoueur > -10360)
                {
                    Canvas.SetTop(rectCarte, Canvas.GetTop(rectCarte) - vitesseJoueur);
                }
                else
                {
                    Canvas.SetTop(rectCarte, -10260);
                }
        }

        private void TirJoueur()
        {
            if (tempsRechargeActuel > 0)
                tempsRechargeActuel--;

            
            if (tirer && tempsRechargeActuel <= 0)
            {
                var posEcran = Mouse.GetPosition(Application.Current.MainWindow);
                var posCarte = Mouse.GetPosition(rectCarte);
#if DEBUG
                Console.WriteLine(posCarte.X.ToString() + "  " + posCarte.Y.ToString());
#endif
                tempsRechargeActuel = tempsRechargeArme;
                Vector2 vecteurTir = new Vector2((float)posEcran.X - 910, (float)posEcran.Y - 490);
                Balle balleJoueur = new Balle(vitesseBalle, 5, 0, "joueur", 0, (int)posCarte.X, (int)posCarte.Y, vecteurTir);

                Canvas.SetTop(balleJoueur.Graphique, posEcran.Y);
                Canvas.SetLeft(balleJoueur.Graphique, posEcran.X);
                monCanvas.Children.Add(balleJoueur.Graphique);
            }
            
        }
    }
}
