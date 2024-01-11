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
        private int countTick = 0, vitesseJoueur = 50, tempsRechargeArme = 60, tempsRechargeActuel = 0, vitesseBalle = 10;
        private bool gauche = false, droite = false, haut = false, bas = false, tirer = false;

        

        private Rect player = new Rect(910, 490, 50, 50);



        public MainWindow()
        {
            InitializeComponent();
            

            MapGenerator.load(carte);

            Menu menu = new Menu();
            menu.ShowDialog();
            
            rectJoueur.Margin = new Thickness(fenetrePrincipale.Width/2 - rectJoueur.Width/2, fenetrePrincipale.Height/2 - rectJoueur.Height/2, 0, 0);
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
            
#if DEBUG
            Console.WriteLine(Canvas.GetLeft(carte));
            Console.WriteLine(Canvas.GetTop(carte));
#endif
            

            MouvementJoueur();
            TirJoueur();
        }

        private void MouvementJoueur()
        {
            if (gauche)
                if (Canvas.GetLeft(carte) + vitesseJoueur < fenetrePrincipale.Width / 2 - rectJoueur.Width / 2)
                {
                    Canvas.SetLeft(carte, Canvas.GetLeft(carte) + vitesseJoueur);
                }

                else
                {
                    Canvas.SetLeft(carte, fenetrePrincipale.Width / 2 - rectJoueur.Width / 2);
                }
            if (droite)
                if (Canvas.GetLeft(carte) - vitesseJoueur > -carte.Width + rectJoueur.Width/2 + fenetrePrincipale.Width/2)
                {
                    Canvas.SetLeft(carte, Canvas.GetLeft(carte) - vitesseJoueur);
                }
                else
                {
                    Canvas.SetLeft(carte, -carte.Width + rectJoueur.Width/2 + fenetrePrincipale.Width/2);
                }
            if (haut)
            {
                if (Canvas.GetTop(carte) + vitesseJoueur < fenetrePrincipale.Height / 2 - rectJoueur.Height / 2)
                {
                    Canvas.SetTop(carte, Canvas.GetTop(carte) + vitesseJoueur);
                }
                else
                {
                    Canvas.SetTop(carte, fenetrePrincipale.Height / 2 - rectJoueur.Height / 2);
                }
            }
            if (bas)
                if (Canvas.GetTop(carte) - vitesseJoueur > -carte.Height + rectJoueur.Height / 2 + fenetrePrincipale.Height/2)
                {
                    Canvas.SetTop(carte, Canvas.GetTop(carte) - vitesseJoueur);
                }
                else
                {
                    Canvas.SetTop(carte, -carte.Height + rectJoueur.Height/2 + fenetrePrincipale.Height/2);
                }
        }

        private void TirJoueur()
        {
            if (tempsRechargeActuel > 0)
                tempsRechargeActuel--;

            
            if (tirer && tempsRechargeActuel <= 0)
            {
                var posEcran = Mouse.GetPosition(Application.Current.MainWindow);
                var posCarte = Mouse.GetPosition(carte);
#if DEBUG
                Console.WriteLine(posCarte.X.ToString() + "  " + posCarte.Y.ToString());
#endif
                tempsRechargeActuel = tempsRechargeArme;
                Vector2 vecteurTir = new Vector2((float)posEcran.X - 910, (float)posEcran.Y - 490);
                Vector2 vecteurNormalise = Vector2.Normalize(vecteurTir);
                Balle balleJoueur = new Balle(vitesseBalle, 20, 0, "joueur", 0, 810, 390, vecteurNormalise);

                Canvas.SetTop(balleJoueur.Graphique, 390);
                Canvas.SetLeft(balleJoueur.Graphique, 810);
                monCanvas.Children.Add(balleJoueur.Graphique);
            }
            
        }
    }
}
