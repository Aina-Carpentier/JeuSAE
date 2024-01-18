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
using Microsoft.Win32;
using System.Media;
using JeuSAE.classes;

namespace JeuSAE
{
    public partial class MainWindow : Window
    {
        private SoundPlayer lecteurMusiqueMenu = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "audio\\musiques\\musique_menu.wav");
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public List<Balle> listeBalle = new List<Balle>();
        public static List<Ennemi> listeEnnemi = new List<Ennemi>();
        public static List<Ennemi> listeEnnemiAEnlever = new List<Ennemi>();
        public List<Balle> listeBalleAEnlever = new List<Balle>();
        private int vitesseJoueur = 20, tempsRechargeArme = 15, tempsRechargeActuel = 0, vitesseBalle = 25, compteurSpawn = 0, compteurAAtteindre = 150, tickAnimation = 0;
        private bool gauche = false, droite = false, haut = false, bas = false, regardeADroite = true, tirer = false, numPadUn = false, numPadQuatre = false, toucheX = false, toucheC = false, toucheR = false;
        private Rect player = new Rect(910, 490, 50, 50); // Hitbox player
        private double posJoueurX = 0, posJoueurY = 0;
        public String choix, cheminSprite;
        private ImageBrush apparenceJoueur = new ImageBrush(), apparenceArme = new ImageBrush();
        public int coefEXP = 1;




        public MainWindow()
        {
            InitializeComponent();
            lecteurMusiqueMenu.Load();
            posJoueurX = fenetrePrincipale.Width / 2;
            posJoueurY = fenetrePrincipale.Height / 2;

            Menu menu = new Menu();
            Parametres parametres = new Parametres();
            Magasin magasin = new Magasin();
            lecteurMusiqueMenu.PlayLooping();
            menu.ShowDialog();
            choix = menu.choix;
            
            
            while (choix != "jouer")
            {
                switch (choix)
                {
                    case "quitter":
                        Application.Current.Shutdown();
                        break;

                    case "parametre":
                        parametres.ShowDialog();
                        choix = parametres.choix;
                        break;

                    case "menu":
                        menu.ShowDialog();
                        choix = menu.choix;
                        break;

                    case "magasin":
                        magasin.ShowDialog();
                        choix = magasin.choix;
                        break;

                }
            }

            MapGenerator.load(this);
            lecteurMusiqueMenu.Stop();
            rectJoueur.Margin = new Thickness(posJoueurX - rectJoueur.Width / 2, posJoueurY - rectJoueur.Height / 2, 0, 0);
            rectArme.Margin = new Thickness(posJoueurX - rectJoueur.Width / 2, posJoueurY - rectJoueur.Height / 2, 0, 0);
            HUDResolution1920x1080();
            HUD.ChangeBarreEliminations(0);
            HUD.ChangeBarreExp(0);
            dispatcherTimer.Tick += GameEngine;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            dispatcherTimer.Start();

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
            LogiqueEnnemis();
            CollisionEnnemi();
            CollisionBalle();
            AnimationJoueur();
            SupprimerEnnemis();


        }

        private void monCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point curseur = e.GetPosition(monCanvas);
            Canvas.SetTop(curseurPerso, curseur.Y - curseurPerso.Height / 2);
            Canvas.SetLeft(curseurPerso, curseur.X - curseurPerso.Width / 2);
            if (curseur.X > fenetrePrincipale.Width / 2)
                regardeADroite = true;
            else
                regardeADroite = false;
            Cursor = Cursors.None;
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

            //------------------------------------------- CODES DE TRICHE -------------------------------------------

            //Super vitesse
            if (e.Key == Key.NumPad1)
                numPadUn = true;
            if (e.Key == Key.NumPad4)
                numPadQuatre = true;
            if (numPadUn && numPadQuatre) { vitesseJoueur = 200; } else { vitesseJoueur = 10; }

            //Clear ennemis
            if (e.Key == Key.X) { toucheX = true; }
                
            if (e.Key == Key.C) { 
                toucheC = true; 
            }
                
            if (toucheC && toucheX) { 
                toucheX = false;
                toucheC = false;
                EnleverTousLesEnnemis();
                Ennemi.SpawnUnEnnemi(this);
            }


            //Spawn cercle

            if (e.Key == Key.R) { toucheR = true; }

            if (toucheC && toucheR)
            {
                toucheC = false;
                toucheR = false;
                Ennemi.SpawnUnEnnemi(this, 6);
            }


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

            //------------------------------------------- CODES DE TRICHE -------------------------------------------

            //Super vitesse
            if (e.Key == Key.NumPad1)
                numPadUn = false;
            if (e.Key == Key.NumPad4)
                numPadQuatre = false;

            //Clear ennemis
            if (e.Key == Key.X) { toucheX = false; }
                
            if (e.Key == Key.C) { toucheC = false; }

            //Spawn cercle
            if (e.Key == Key.R) { toucheR = false; }
                
        }

        private void MouvementJoueur()
        {
            DeplacerEnDirection(gauche, vitesseJoueur, 0, posJoueurX - rectJoueur.Width / 2);
            DeplacerEnDirection(droite, -vitesseJoueur, 0, -carte.Width + rectJoueur.Width / 2 + posJoueurX); // non
            DeplacerEnDirection(haut, 0, vitesseJoueur, posJoueurY - rectJoueur.Height / 2);
            DeplacerEnDirection(bas, 0, -vitesseJoueur, -carte.Height + rectJoueur.Height / 2 + posJoueurY); // non
        }

        private void DeplacerEnDirection(bool direction, double deplacementX, double deplacementY, double positionLimite)
        {

            if (direction)
            {
                if (EstDansLesLimites(deplacementX, deplacementY, positionLimite))
               {

                    if (deplacementX != 0) Canvas.SetLeft(carte, Canvas.GetLeft(carte) + deplacementX);
                    else Canvas.SetTop(carte, Canvas.GetTop(carte) + deplacementY);
                    DeplacerObjets(listeBalle, deplacementX, deplacementY);
                    DeplacerObjets(listeEnnemi, deplacementX, deplacementY);
                }
                else
                {
                    if (deplacementX != 0) Canvas.SetLeft(carte, positionLimite);
                    else Canvas.SetTop(carte, positionLimite);
                }

                foreach(Ennemi ennemi in listeEnnemi)
                {
                    ennemi.Tir();// Aucune idée de pourquoi les ennemis tirent pas quand on bouge ducoup j'ai mis ça là
                }

            }
        }

        private void DeplacerObjets(List<Balle> objets, double deplacementX, double deplacementY)
        {
            foreach (Balle objet in objets)
            {
                Canvas.SetLeft(objet.Graphique, Canvas.GetLeft(objet.Graphique) + deplacementX);
                Canvas.SetTop(objet.Graphique, Canvas.GetTop(objet.Graphique) + deplacementY);

                objet.PosX = Canvas.GetLeft(objet.Graphique);
                objet.PosY = Canvas.GetTop(objet.Graphique);
            }
        }

        private void DeplacerObjets(List<Ennemi> objets, double deplacementX, double deplacementY)
        {
            foreach (Ennemi objet in objets)
            {
                Canvas.SetLeft(objet.Graphique, Canvas.GetLeft(objet.Graphique) + deplacementX);
                Canvas.SetTop(objet.Graphique, Canvas.GetTop(objet.Graphique) + deplacementY);

                objet.PosX = Canvas.GetLeft(objet.Graphique);
                objet.PosY = Canvas.GetTop(objet.Graphique);
            }
        }

        private bool EstDansLesLimites(double deplacementX, double deplacementY, double positionLimite)
        {
            if (deplacementX != 0)
            {
                return deplacementX < 0 ? Canvas.GetLeft(carte) + deplacementX > positionLimite : Canvas.GetLeft(carte) + deplacementX < positionLimite;
            }
            else if (deplacementY != 0)
            {
                return deplacementY < 0 ? Canvas.GetTop(carte) + deplacementY > positionLimite : Canvas.GetTop(carte) + deplacementY < positionLimite;
            }

            return false;
        }

        private void TirJoueur()
        {
            GestionTempsRecharge();

            if (tirer && tempsRechargeActuel <= 0)
            {
                CreerBalleJoueur();
            }

            GestionDeplacementBalles();
        }

        private void GestionTempsRecharge()
        {
            if (tempsRechargeActuel > 0)
                tempsRechargeActuel--;
        }

        private void CreerBalleJoueur()
        {
            tempsRechargeActuel = tempsRechargeArme;

            Point posEcran = Mouse.GetPosition(Application.Current.MainWindow);
            Point posCarte = Mouse.GetPosition(carte);

#if DEBUG
            Console.WriteLine(posCarte.X.ToString() + "  " + posCarte.Y.ToString());
#endif

            Vector2 vecteurTir = new Vector2((float)posEcran.X - (float)posJoueurX, (float)posEcran.Y - (float)posJoueurY);

            Balle balleJoueur = new Balle(vitesseBalle, 500, 0, "joueur", 0, posJoueurX, posJoueurY, vecteurTir);
            PositionnerBalle(balleJoueur);

            monCanvas.Children.Add(balleJoueur.Graphique);
            listeBalle.Add(balleJoueur);
        }

        private void PositionnerBalle(Balle balle)
        {
            Canvas.SetLeft(balle.Graphique, balle.PosX);
            Canvas.SetTop(balle.Graphique, balle.PosY);
        }

        private void GestionDeplacementBalles()
        {
            if (listeBalle != null)
            {
                foreach (Balle balle in listeBalle)
                {
                    balle.Deplacement();

                    if (BalleHorsLimite(balle))
                    {
                        listeBalleAEnlever.Add(balle);
                    }

                    PositionnerBalle(balle);

                }

                foreach (Balle balle in listeBalleAEnlever)
                {
                    listeBalle.Remove(balle);
                    monCanvas.Children.Remove(balle.Graphique);
                }

                listeBalleAEnlever.Clear();
            }
        }

        private void CollisionBalle()
        {
            foreach(Balle balle in listeBalle)
            {
                if (balle.Rect.IntersectsWith(player) && balle.Tireur != "joueur")
                {
                    //Application.Current.Shutdown();
                    listeBalleAEnlever.Add(balle);
                    HUD.AjouteVie((int) -balle.Degats);


                }
                foreach(Ennemi ennemi in listeEnnemi)
                {
                    if (balle.Rect.IntersectsWith(ennemi.Rect) && balle.Tireur == "joueur")
                    {
                        listeEnnemiAEnlever.Add(ennemi);
                        HUD.AjouteElimination(1);
                        HUD.AjouteExp(coefEXP);
                    }
                    
                }
            }
        }

        private void CollisionEnnemi()
        {
            foreach (Ennemi ennemi in listeEnnemi)
            {
                if (player.IntersectsWith(ennemi.Rect))
                {
                    HUD.AjouteVie(-Constantes.DEGATS_COLLISION);
                    //rajouter frames d'invicibilité + knockback
                }
            }
        }

        private bool BalleHorsLimite(Balle balle)
        {
            return Canvas.GetLeft(balle.Graphique) <= Canvas.GetLeft(carte) - 400 ||
                   Canvas.GetTop(balle.Graphique) <= Canvas.GetTop(carte) - 400 ||
                   Canvas.GetLeft(balle.Graphique) >= Canvas.GetLeft(carte) + carte.Width + 400 ||
                   Canvas.GetTop(balle.Graphique) >= Canvas.GetTop(carte) + carte.Height + 400;
        }

        private void gereLeSpawn()
        {
            if (compteurSpawn >= compteurAAtteindre)
            {
                Ennemi.SpawnUnEnnemi(this);
                compteurSpawn = 0;
            }
            compteurSpawn++;
        }

        private void HUDResolution1920x1080()
        {
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

        private void EnleverTousLesEnnemis()
        {
            foreach (Ennemi ennemi in listeEnnemi)
            {
                listeEnnemiAEnlever.Add(ennemi);
            }

            foreach (Ennemi ennemi in listeEnnemiAEnlever)
            {
                listeEnnemi.Remove(ennemi);
                monCanvas.Children.Remove(ennemi.Graphique);
            }
            listeEnnemiAEnlever.Clear();
        }

        private void LogiqueEnnemis()
        {
            double posJoueurX = fenetrePrincipale.Width / 2 - rectJoueur.Width*0.75;
            double posJoueurY = fenetrePrincipale.Height / 2 - rectJoueur.Height*0.75;

            foreach (Ennemi ennemi in listeEnnemi)
            {
                ennemi.PosX = Canvas.GetLeft(ennemi.Graphique);
                ennemi.PosY = Canvas.GetTop(ennemi.Graphique);
                Vector2 vecteurDeplace = new Vector2((float)ennemi.PosX - (float)posJoueurX, (float)ennemi.PosY - (float)posJoueurY);
                Vector2 vecteurDeplaceNormalise = Vector2.Normalize(vecteurDeplace);

                double newPosEnnemiX = ennemi.PosX - (vecteurDeplaceNormalise.X * ennemi.Vitesse);
                double newPosEnnemiY = ennemi.PosY - (vecteurDeplaceNormalise.Y * ennemi.Vitesse);

                ennemi.PosX = newPosEnnemiX;
                ennemi.PosY = newPosEnnemiY;

                Canvas.SetLeft(ennemi.Graphique, newPosEnnemiX);
                Canvas.SetTop(ennemi.Graphique, newPosEnnemiY);
                ennemi.Tir();
            }


        }

        private void AnimationJoueur()
        {
            Point posEcran = Mouse.GetPosition(Application.Current.MainWindow);
            Vector2 vecteurTir = new Vector2((float)posEcran.X - (float)posJoueurX, (float)posEcran.Y - (float)posJoueurY);
            float normalVecteurX = Vector2.Normalize(vecteurTir).X, normalVecteurY = Vector2.Normalize(vecteurTir).Y;
#if DEBUG
            Console.WriteLine("vecteur x " + normalVecteurX.ToString());
            Console.WriteLine("vecteur y " + normalVecteurY.ToString());
#endif

            cheminSprite = AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\";
            tickAnimation++;
            rectJoueur.Fill = apparenceJoueur;
            rectArme.Fill = apparenceArme;
            Console.WriteLine(tickAnimation);
            if (regardeADroite)
                cheminSprite += "droite\\";
            else
                cheminSprite += "gauche\\";



            //marche
            if (bas || haut || droite || gauche)
            {
                if (tickAnimation >= 30)
                    tickAnimation = 0;
                apparenceJoueur.ImageSource = new BitmapImage(new Uri(cheminSprite + $"\\marche\\marche{tickAnimation / 5 + 1}.png"));
            }
            //idle
            else
            {
                if (tickAnimation >= 20)
                    tickAnimation = 0;
                apparenceJoueur.ImageSource = new BitmapImage(new Uri(cheminSprite + $"\\idle\\idle{tickAnimation / 5 + 1}.png"));
            }
            //faire varier en fonction de la position du curseur
            if (Math.Abs(normalVecteurX) < 0.2 && normalVecteurY > 0.8)
                apparenceArme.ImageSource = new BitmapImage(new Uri(cheminSprite + $"\\arme\\arme1_5.png"));
            else if (Math.Abs(normalVecteurX) < 0.2 && normalVecteurY < -0.8)
                apparenceArme.ImageSource = new BitmapImage(new Uri(cheminSprite + $"\\arme\\arme1_1.png"));
            else if (Math.Abs(normalVecteurX) < 0.96 && normalVecteurY > 0.25)
                apparenceArme.ImageSource = new BitmapImage(new Uri(cheminSprite + $"\\arme\\arme1_4.png"));
            else if (Math.Abs(normalVecteurX) < 0.96 && normalVecteurY < -0.25)
                apparenceArme.ImageSource = new BitmapImage(new Uri(cheminSprite + $"\\arme\\arme1_2.png"));
            else
                apparenceArme.ImageSource = new BitmapImage(new Uri(cheminSprite + $"\\arme\\arme1_3.png"));



        }

        private void SupprimerEnnemis()
        {
            foreach (Ennemi ennemi in listeEnnemiAEnlever)
            {
                listeEnnemi.Remove(ennemi);
                monCanvas.Children.Remove(ennemi.Graphique);
            }
            listeEnnemiAEnlever.Clear();
        }
    }
}
