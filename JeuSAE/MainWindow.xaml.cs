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
using System.Security.Cryptography.X509Certificates;

namespace JeuSAE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int countTick = 0, vitesseJoueur = 150, tempsRechargeArme = 60, tempsRechargeActuel = 0, vitesseBalle = 3;
        private bool gauche = false, droite = false, haut = false, bas = false, tirer = false;
        private List<Balle> balleList = new List<Balle>();
        private Rect player = new Rect(910, 490, 50, 50);

        double posJoueurX = 0, posJoueurY = 0;

        public MainWindow()
        {
            InitializeComponent();
            posJoueurX = fenetrePrincipale.Width / 2;
            posJoueurY = fenetrePrincipale.Height / 2;


            MapGenerator.load(carte);

            Menu menu = new Menu();
            menu.ShowDialog();
            while (menu.choix != "jouer")
            {

                switch (menu.choix)
                {
                    case "quitter":
                        Application.Current.Shutdown();
                        break;


                }
            }
            rectJoueur.Margin = new Thickness(posJoueurX - rectJoueur.Width / 2, posJoueurY - rectJoueur.Height / 2, 0, 0);
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
                if (Canvas.GetLeft(carte) + vitesseJoueur < posJoueurX - rectJoueur.Width / 2)
                {
                    Canvas.SetLeft(carte, Canvas.GetLeft(carte) + vitesseJoueur);
                    foreach (Balle balle in balleList) { 
                        Canvas.SetLeft(balle.Graphique, Canvas.GetLeft(balle.Graphique) + vitesseJoueur);
                        balle.PosX = Canvas.GetLeft(balle.Graphique);
                        balle.PosY = Canvas.GetTop(balle.Graphique);
                    }
                }

                else
                {
                    Canvas.SetLeft(carte, posJoueurX - rectJoueur.Width / 2);
                    //foreach (Balle balle in balleList) { Canvas.SetLeft(balle.Graphique, posJoueurX - rectJoueur.Width / 2);
                    //    balle.PosX = Canvas.GetLeft(balle.Graphique);
                    //    balle.PosY = Canvas.GetTop(balle.Graphique);
                    
                    //}
                    }
            if (droite)
                if (Canvas.GetLeft(carte) - vitesseJoueur > -carte.Width + rectJoueur.Width/2 + posJoueurX)
                {
                    Canvas.SetLeft(carte, Canvas.GetLeft(carte) - vitesseJoueur);
                    foreach (Balle balle in balleList) { Canvas.SetLeft(balle.Graphique, Canvas.GetLeft(balle.Graphique) - vitesseJoueur); 
                        balle.PosX = Canvas.GetLeft(balle.Graphique);
                        balle.PosY = Canvas.GetTop(balle.Graphique);
                    }
                }
                else
                {
                    Canvas.SetLeft(carte, -carte.Width + rectJoueur.Width/2 + posJoueurX);
                    //foreach (Balle balle in balleList) { Canvas.SetLeft(balle.Graphique, -carte.Width + rectJoueur.Width / 2 + posJoueurX); 
                    //                        balle.PosX = Canvas.GetLeft(balle.Graphique);
                    //    balle.PosY = Canvas.GetTop(balle.Graphique);
                    //}
                }
            if (haut)
            {
                if (Canvas.GetTop(carte) + vitesseJoueur < posJoueurY - rectJoueur.Height / 2)
                {
                    Canvas.SetTop(carte, Canvas.GetTop(carte) + vitesseJoueur);
                    foreach (Balle balle in balleList) { Canvas.SetTop(balle.Graphique, Canvas.GetTop(balle.Graphique) + vitesseJoueur); 
                                            balle.PosX = Canvas.GetLeft(balle.Graphique);
                        balle.PosY = Canvas.GetTop(balle.Graphique);
                    }
                }
                else
                {
                    Canvas.SetTop(carte, posJoueurY - rectJoueur.Height / 2);
                    //foreach (Balle balle in balleList) { Canvas.SetTop(balle.Graphique, posJoueurY - rectJoueur.Height / 2); 
                    //                        balle.PosX = Canvas.GetLeft(balle.Graphique);
                    //    balle.PosY = Canvas.GetTop(balle.Graphique);
                    //}
                }
            }
            if (bas)
                if (Canvas.GetTop(carte) - vitesseJoueur > -carte.Height + rectJoueur.Height / 2 + posJoueurY)
                {
                    Canvas.SetTop(carte, Canvas.GetTop(carte) - vitesseJoueur);
                    foreach (Balle balle in balleList) { Canvas.SetTop(balle.Graphique, Canvas.GetTop(balle.Graphique) - vitesseJoueur); 
                                            balle.PosX = Canvas.GetLeft(balle.Graphique);
                        balle.PosY = Canvas.GetTop(balle.Graphique);
                    }
                }
                else
                {
                    Canvas.SetTop(carte, -carte.Height + rectJoueur.Height/2 + posJoueurY);
                    //foreach (Balle balle in balleList) { Canvas.SetTop(balle.Graphique, -carte.Height + rectJoueur.Height / 2 + posJoueurY); 
                    //                        balle.PosX = Canvas.GetLeft(balle.Graphique);
                    //    balle.PosY = Canvas.GetTop(balle.Graphique);
                    //}
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
                /*
                Vector2 vecteurTir = new Vector2((float)posEcran.X - 910, (float)posEcran.Y - 490);
                Vector2 vecteurNormalise = Vector2.Normalize(vecteurTir);
                Balle balleJoueur = new Balle(vitesseBalle, 20, 0, "joueur", 0, 810, 390, vecteurNormalise);

                Canvas.SetTop(balleJoueur.Graphique, 390);
                Canvas.SetLeft(balleJoueur.Graphique, 810);
                */
                Vector2 vecteurTir = new Vector2((float)posEcran.X - (float)posJoueurX, (float)posEcran.Y - (float)posJoueurY);

                Balle balleJoueur = new Balle(vitesseBalle, 20, 0, "joueur", 0, posJoueurX-10, posJoueurY-10, vecteurTir);
                Canvas.SetLeft(balleJoueur.Graphique, balleJoueur.PosX);
                Canvas.SetTop(balleJoueur.Graphique, balleJoueur.PosY);


                monCanvas.Children.Add(balleJoueur.Graphique);
                balleList.Add(balleJoueur);
                
            }



            if (balleList != null)
            {

                foreach (Balle balle in balleList)
                {
                    balle.Deplacement();
#if DEBUG
                    Console.WriteLine("Balle PosX : " + balle.PosX);
                    Console.WriteLine("Balle PosY : " + balle.PosY);

#endif
                    Canvas.SetLeft(balle.Graphique, balle.PosX);
                    Canvas.SetTop(balle.Graphique, balle.PosY);
                }

            }


            
            
        }

        

        


    }
}
