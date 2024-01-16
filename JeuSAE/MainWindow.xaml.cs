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
        private int countTick = 0, vitesseJoueur = 10, tempsRechargeArme = 15, tempsRechargeActuel = 0, vitesseBalle = 25;
        private bool gauche = false, droite = false, haut = false, bas = false, tirer = false, numPadUn = false, numPadQuatre = false;
        private List<Balle> listeBalle = new List<Balle>();
        private List<Balle> listeBalleAEnlever = new List<Balle>();
        private int compteurSpawn = 0;
        private int compteurAAtteindre = 1;

        private void monCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point curseur = e.GetPosition(monCanvas);
            Canvas.SetTop(curseurPerso, curseur.Y - curseurPerso.Height/2);
            Canvas.SetLeft(curseurPerso, curseur.X - curseurPerso.Width/2);
            Cursor = Cursors.None;
        }

        private Rect player = new Rect(910, 490, 50, 50);
        double posJoueurX = 0, posJoueurY = 0;

        public MainWindow()
        {
            InitializeComponent();
            posJoueurX = fenetrePrincipale.Width / 2;
            posJoueurY = fenetrePrincipale.Height / 2;





            Menu menu = new Menu();
            Parametres parametres = new Parametres();
            menu.ShowDialog();
            
            while (menu.choix != "jouer")
            {

                switch (menu.choix)
                {
                    case "quitter":
                        Application.Current.Shutdown();
                        break;

                    case "parametre":
                        parametres.ShowDialog();
                        break;

                }
            }
            MapGenerator.load(this);
            rectJoueur.Margin = new Thickness(posJoueurX - rectJoueur.Width / 2, posJoueurY - rectJoueur.Height / 2, 0, 0);
            HUDResolution1920x1080();
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





            //-------------------------------------------CODES DE TRICHE-------------------------------------------

            //Super vitesse
            if (e.Key == Key.NumPad1)
                numPadUn = true;
            if (e.Key == Key.NumPad4)
                numPadQuatre = true;
            if (numPadUn && numPadQuatre) { vitesseJoueur = 200; } else { vitesseJoueur = 10; }

//#if DEBUG
//            vitesseJoueur = 300;
//#endif

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





            //-------------------------------------------CODES DE TRICHE-------------------------------------------

            //Super vitesse
            if (e.Key == Key.NumPad1)
                numPadUn = false;
            if (e.Key == Key.NumPad4)
                numPadQuatre = false;


        }


        private void GameEngine(object sender, EventArgs e)
        {
            
#if DEBUG
            Console.WriteLine(Canvas.GetLeft(carte));
            Console.WriteLine(Canvas.GetTop(carte));
#endif

            MouvementJoueur();
            TirJoueur();
            gereLeSpawn();

        }

        private void MouvementJoueur()
        {
            if (gauche)
                if (Canvas.GetLeft(carte) + vitesseJoueur < posJoueurX - rectJoueur.Width / 2)
                {
                    Canvas.SetLeft(carte, Canvas.GetLeft(carte) + vitesseJoueur);
                    foreach (Balle balle in listeBalle) { 
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
                    foreach (Balle balle in listeBalle) { Canvas.SetLeft(balle.Graphique, Canvas.GetLeft(balle.Graphique) - vitesseJoueur); 
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
                    foreach (Balle balle in listeBalle) { Canvas.SetTop(balle.Graphique, Canvas.GetTop(balle.Graphique) + vitesseJoueur); 
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
                    foreach (Balle balle in listeBalle) { Canvas.SetTop(balle.Graphique, Canvas.GetTop(balle.Graphique) - vitesseJoueur); 
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

                Balle balleJoueur = new Balle(vitesseBalle, 500, 0, "joueur", 0, posJoueurX, posJoueurY, vecteurTir);
                Canvas.SetLeft(balleJoueur.Graphique, balleJoueur.PosX);
                Canvas.SetTop(balleJoueur.Graphique, balleJoueur.PosY);


                monCanvas.Children.Add(balleJoueur.Graphique);
                listeBalle.Add(balleJoueur);
                
            }

            if (listeBalle != null)
            {

                foreach (Balle balle in listeBalle)
                {
                    balle.Deplacement();
#if DEBUG
                    //Console.WriteLine("Balle PosX : " + balle.PosX);
                    //Console.WriteLine("Balle PosY : " + balle.PosY);

#endif
                    
                    if (Canvas.GetLeft(balle.Graphique) <= Canvas.GetLeft(carte) - 400 || 
                        Canvas.GetTop(balle.Graphique) <= Canvas.GetTop(carte) - 400 ||
                        Canvas.GetLeft(balle.Graphique) >= Canvas.GetLeft(carte) + carte.Width + 400 ||
                        Canvas.GetTop(balle.Graphique) >= Canvas.GetTop(carte) + carte.Height + 400) { listeBalleAEnlever.Add(balle); }

                    Canvas.SetLeft(balle.Graphique, balle.PosX);
                    Canvas.SetTop(balle.Graphique, balle.PosY);
                }

                foreach (Balle balle in listeBalleAEnlever)
                {
                    listeBalle.Remove(balle);
                    monCanvas.Children.Remove(balle.Graphique);
                }
                listeBalleAEnlever.Clear();

            }
            
        }

        
        private void gereLeSpawn()
        {

            if (compteurSpawn >= compteurAAtteindre)
            {
                SpawnEnnemi.SpawnUnEnnemi(this);
                compteurSpawn = 0;
            }
            compteurSpawn++;
        }

        private void HUDResolution1920x1080()
        {

            //A Verifier
            if (fenetrePrincipale.Width == 1920 && fenetrePrincipale.Height == 1080)
            {
                Canvas.SetLeft(labDiamant, 98);
                Canvas.SetTop(labDiamant,165);

                Canvas.SetLeft(rectanglePV, 150);
                Canvas.SetTop(rectanglePV, 19);
                rectanglePV.Height = 32;
                rectanglePV.Width = 349;

                Canvas.SetLeft(rectangleEXP, 150);
                Canvas.SetTop(rectangleEXP, 110);
                rectangleEXP.Height = 30;
                rectangleEXP.Width = 345;

                Canvas.SetLeft(rectangleElimination, 32);
                Canvas.SetTop(rectangleElimination, 991);
                rectangleElimination.Height = 63;
                rectangleElimination.Width = 405;

            }
        }
    }
}
