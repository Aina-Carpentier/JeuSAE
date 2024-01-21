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
using JeuSAE.data;
using System.Windows.Media.Effects;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;


namespace JeuSAE
{
    public partial class MainWindow : Window
    {
        private SoundPlayer lecteurMusiqueMenu = new SoundPlayer(AppDomain.CurrentDomain.BaseDirectory + "audio\\musiques\\musique_menu.wav");
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public bool mort = false, mortDroite = true, regardeADroite = true;
        public static bool ouvreMenuMaxEXP = false;
        public Key toucheHaut = Key.Z, toucheBas = Key.S, toucheDroite = Key.D, toucheGauche = Key.Q;
        public List<Balle> listeBalle = new List<Balle>();
        public static List<Ennemi> listeEnnemi = new List<Ennemi>();
        public static List<Ennemi> listeEnnemiAEnlever = new List<Ennemi>();
        public List<Balle> listeBalleAEnlever = new List<Balle>();
        private Rect hitboxJoueur = new Rect(940, 500, 40, 80); // Hitbox player
        public int coefEXP = 1, tickAnimation = 0;


        private static bool gauche = false, droite = false, haut = false, bas = false,  tirer = false, numPadUn = false, numPadQuatre = false, toucheX = false, toucheC = false, toucheR = false;
        public static string choix, cheminSprite;

        private static ImageBrush apparenceJoueur = new ImageBrush(), apparenceArme = new ImageBrush();
        private static double posJoueurX = 0, posJoueurY = 0;

        private static BaseDeDonnee baseDeDonnee = JsonUtilitaire.LireFichier(Constantes.CHEMIN_BDD);
        private static BlurBitmapEffect myBlurEffect = new BlurBitmapEffect();
        private static BlurBitmapEffect nonFloue = new BlurBitmapEffect();
        private BitmapImage bouttonAmelioration = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\rectangle_upgrade.png"));
        private BitmapImage bouttonAmeliorationPresse = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\rectangle_upgrade_presse.png"));

        public MainWindow()
        {
            InitializeComponent();
            Constantes.LECTEUR_MUSIQUE_MENU.Load();
            Constantes.LECTEUR_MUSIQUE_MENU.PlayLooping();

            posJoueurX = fenetrePrincipale.Width / 2;
            posJoueurY = fenetrePrincipale.Height / 2;
            nonFloue.Radius = 0;

            Menu menu = new Menu();
            Parametres parametres = new Parametres();
            Magasin magasin = new Magasin();
            Touche touche = new Touche(Constantes.TOUCHE_HAUT, Constantes.TOUCHE_BAS, Constantes.TOUCHE_DROITE, Constantes.TOUCHE_GAUCHE);
            PreJeu preJeu = new PreJeu();

            menu.ShowDialog();
            choix = menu.choix;
            
            
            while (choix != "jouer")
            {
                switch (choix)
                {
                    case "quitter":
                        Environment.Exit(0);
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

                    case "touche":
                        touche.ShowDialog();
                        choix = touche.choix;
                        Constantes.TOUCHE_HAUT = touche.tHaut; Constantes.TOUCHE_BAS = touche.tBas; Constantes.TOUCHE_DROITE = touche.tDroite; Constantes.TOUCHE_GAUCHE = touche.tGauche;
                        break;

                    case "difficulte":
                        preJeu.ShowDialog();
                        choix = preJeu.choix;
                        break;

                }
            }
            MapGenerator.load(this);
            Constantes.LECTEUR_MUSIQUE_MENU.Stop();
            Cursor = Cursors.None;
            rectJoueur.Margin = new Thickness(posJoueurX - rectJoueur.Width / 2, posJoueurY - rectJoueur.Height / 2, 0, 0);
            rectArme.Margin = new Thickness(posJoueurX - rectJoueur.Width / 2, posJoueurY - rectJoueur.Height / 2, 0, 0);
            labQuitter.Margin = new Thickness(fenetrePrincipale.Width / 2 - labQuitter.Width / 2, fenetrePrincipale.Height*0.35, 0, 0);
            labRetour.Margin = new Thickness(fenetrePrincipale.Width / 2 - labRetour.Width / 2, fenetrePrincipale.Height * 0.2, 0, 0);
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

            if (mort)
            {
                AnimationMort();
            } else if (ouvreMenuMaxEXP)
            {

            }
            else
            {
                MouvementJoueur();
                TirJoueur();
                gereLeSpawn();
                LogiqueEnnemis();
                CollisionEnnemi();
                CollisionBalle();
                AnimationJoueur();
                SupprimerEnnemis();
                MettreAJourBdd();
            }
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
            if (e.Key == Constantes.TOUCHE_GAUCHE)
                gauche = true;
            if (e.Key == Constantes.TOUCHE_DROITE) 
                droite = true; 
            if (e.Key == Constantes.TOUCHE_HAUT)
                haut = true;
            if (e.Key == Constantes.TOUCHE_BAS)
                bas = true;

            //------------------------------------------- CODES DE TRICHE -------------------------------------------

            //Super vitesse
            if (e.Key == Key.NumPad1)
                numPadUn = true;
            if (e.Key == Key.NumPad4)
                numPadQuatre = true;
            if (numPadUn && numPadQuatre) { Constantes.VITESSE_JOUEUR = 200; }

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
            if (e.Key == Constantes.TOUCHE_GAUCHE)
                gauche = false;
            if (e.Key == Constantes.TOUCHE_DROITE)
                droite = false;
            if (e.Key == Constantes.TOUCHE_HAUT)
                haut = false;
            if (e.Key == Constantes.TOUCHE_BAS)
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
            DeplacerEnDirection(gauche, Constantes.VITESSE_JOUEUR, 0, posJoueurX - rectJoueur.Width / 2);
            DeplacerEnDirection(droite, -Constantes.VITESSE_JOUEUR, 0, -carte.Width + rectJoueur.Width / 2 + posJoueurX); // non
            DeplacerEnDirection(haut, 0, Constantes.VITESSE_JOUEUR, posJoueurY - rectJoueur.Height / 2);
            DeplacerEnDirection(bas, 0, -Constantes.VITESSE_JOUEUR, -carte.Height + rectJoueur.Height / 2 + posJoueurY); // non
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

            if (tirer && Constantes.TEMPS_RECHARGE_ACTUEL <= 0)
            {
                CreerBalleJoueur();
            }

            GestionDeplacementBalles();
        }

        private void GestionTempsRecharge()
        {
            if (Constantes.TEMPS_RECHARGE_ACTUEL > 0)
                Constantes.TEMPS_RECHARGE_ACTUEL--;
        }

        private void CreerBalleJoueur()
        {
            Constantes.TEMPS_RECHARGE_ACTUEL = (int) Constantes.TEMPS_RECHARGE_ARME;

            Point posEcran = Mouse.GetPosition(Application.Current.MainWindow);
            Point posCarte = Mouse.GetPosition(carte);

#if DEBUG
            Console.WriteLine(posCarte.X.ToString() + "  " + posCarte.Y.ToString());
#endif

            Vector2 vecteurTir = new Vector2((float)posEcran.X - (float)posJoueurX, (float)posEcran.Y - (float)posJoueurY);

            Balle balleJoueur = new Balle(Constantes.VITESSE_BALLE_JOUEUR, Constantes.TAILLE_BALLE_JOUEUR, 0, "joueur", 0, posJoueurX, posJoueurY, vecteurTir, Constantes.DEGATS_JOUEUR);
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
                if (balle.Rect.IntersectsWith(hitboxJoueur) && balle.Tireur != "joueur")
                {
                    //Application.Current.Shutdown();
                    listeBalleAEnlever.Add(balle);
                    HUD.AjouteVie((int) -balle.Degats);


                }
                List<Ennemi> listeEnnemiTemporaire = new List<Ennemi>(listeEnnemi);
                foreach(Ennemi ennemi in listeEnnemiTemporaire)
                {
                    if (balle.Rect.IntersectsWith(ennemi.Rect) && balle.Tireur == "joueur")
                    {


                        if (!balle.ListeEnnemisPerces.Contains(ennemi.Id))
                        {
                            ennemi.Vie -= balle.Degats;
                            balle.ListeEnnemisPerces.Add(ennemi.Id);
                        }

                        if (balle.ListeEnnemisPerces.Count >= balle.NombrePerce)
                        {
                            listeBalleAEnlever.Add(balle);
                        }

                        if (ennemi.Vie <= 0)
                        {
                            listeEnnemiAEnlever.Add(ennemi);
                            HUD.AjouteElimination(50);
                            HUD.AjouteExp((int) Constantes.COEFFICIENT_EXPERIENCE);
                        }
                    }
                    
                }
            }
        }

        private void bouttonUpgrade1_MouseEnter(object sender, MouseEventArgs e)
        {
            bouttonUpgrade1.Source = bouttonAmeliorationPresse;
        }

        private void bouttonUpgrade1_MouseLeave(object sender, MouseEventArgs e)
        {
            bouttonUpgrade1.Source = bouttonAmelioration;
        }

        private void bouttonUpgrade2_MouseEnter(object sender, MouseEventArgs e)
        {
            bouttonUpgrade2.Source = bouttonAmeliorationPresse;
        }

        private void bouttonUpgrade2_MouseLeave(object sender, MouseEventArgs e)
        {
            bouttonUpgrade2.Source = bouttonAmelioration;
        }

        private void bouttonUpgrade3_MouseEnter(object sender, MouseEventArgs e)
        {
            bouttonUpgrade3.Source = bouttonAmeliorationPresse;
        }

        private void bouttonUpgrade3_MouseLeave(object sender, MouseEventArgs e)
        {
            bouttonUpgrade3.Source = bouttonAmelioration;
        }

        private void bouttonUpgrade1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MenuMaxEXP.AppliqueBonusAuJoueur(1);
        }

        private void bouttonUpgrade2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MenuMaxEXP.AppliqueBonusAuJoueur(2);
        }

        private void bouttonUpgrade3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MenuMaxEXP.AppliqueBonusAuJoueur(3);
        }

        private void CollisionEnnemi()
        {
            foreach (Ennemi ennemi in listeEnnemi)
            {
                if (hitboxJoueur.IntersectsWith(ennemi.Rect))
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
            if (Constantes.COMPTEUR_SPAWN >= Constantes.TICK_REQUIS_POUR_SPAWN_ENNEMI)
            {
                Ennemi.SpawnUnEnnemi(this);
                Constantes.COMPTEUR_SPAWN = 0;
            }
            Constantes.COMPTEUR_SPAWN++;

            // Boss
            if (HUD.BossDoitSpawn())
            {
                Ennemi.SpawnUnBoss(this);
                HUD.ChangeBarreExp(0);
            }
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
            Constantes.TICK_ANIMATION++;
            rectJoueur.Fill = apparenceJoueur;
            rectArme.Fill = apparenceArme;
            Console.WriteLine(Constantes.TICK_ANIMATION);
            if (regardeADroite)
                cheminSprite += "droite\\";
            else
                cheminSprite += "gauche\\";


            //marche
            if (bas || haut || droite || gauche)
            {
                if (Constantes.TICK_ANIMATION >= 30)
                    Constantes.TICK_ANIMATION = 0;
                apparenceJoueur.ImageSource = new BitmapImage(new Uri(cheminSprite + $"\\marche\\marche{Constantes.TICK_ANIMATION / 5 + 1}.png"));
            }
            //idle
            else
            {
                if (Constantes.TICK_ANIMATION >= 20)
                    Constantes.TICK_ANIMATION = 0;
                apparenceJoueur.ImageSource = new BitmapImage(new Uri(cheminSprite + $"\\idle\\idle{Constantes.TICK_ANIMATION / 5 + 1}.png"));
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
                NouvelleElimination();
                listeEnnemi.Remove(ennemi);
                monCanvas.Children.Remove(ennemi.Graphique);
            }
            listeEnnemiAEnlever.Clear();
        }
        private void NouvelleElimination()
        {
            baseDeDonnee.eliminations += 1;
            MettreAJourBdd();
        }

        private void MettreAJourBdd()
        {
            JsonUtilitaire.Ecriture(baseDeDonnee, Constantes.CHEMIN_BDD);
            labDiamant.Content = baseDeDonnee.argent;
            labEliminations.Content = baseDeDonnee.eliminations;
        }

        private void AnimationMort()
        {
            myBlurEffect.Radius = 10;
            
            foreach (UIElement children in monCanvas.Children)
            {
                    children.BitmapEffect = myBlurEffect;
            }

            labQuitter.BitmapEffect = nonFloue;
            labRetour.BitmapEffect = nonFloue;
            rectJoueur.BitmapEffect = nonFloue;

            //carte.BitmapEffect = myBlurEffect;

            cheminSprite = AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\";
            if (tickAnimation < 40)
                tickAnimation++;
            rectArme.Visibility = Visibility.Hidden;
            Console.WriteLine(tickAnimation);
            if (mortDroite)
                cheminSprite += "droite\\";
            else
                cheminSprite += "gauche\\";
            
            apparenceJoueur.ImageSource = new BitmapImage(new Uri(cheminSprite + $"\\mort\\mort{tickAnimation / 10 + 1}.png"));
        }


        private void labRetour_MouseEnter(object sender, MouseEventArgs e)
        {
            labRetour.Foreground = Brushes.LightSlateGray;
        }

        private void labRetour_MouseLeave(object sender, MouseEventArgs e)
        {
            labRetour.Foreground = Brushes.Black;
        }

        private void labRetour_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Application.Current.Shutdown();

        }

        private void labQuitter_MouseEnter(object sender, MouseEventArgs e)
        {
            labQuitter.Foreground = Brushes.LightSlateGray;
        }

        private void labQuitter_MouseLeave(object sender, MouseEventArgs e)
        {
            labQuitter.Foreground = Brushes.Black;
        }

        private void labQuitter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void RemetValeursAZero()
        {
            mort = false;
            mortDroite = true; 
            regardeADroite = true;
            listeBalle.Clear();
            listeEnnemi.Clear();
            listeEnnemiAEnlever.Clear();
            listeBalleAEnlever.Clear();
            coefEXP = 1;
            tickAnimation = 0;
            gauche = false;
            droite = false;
            haut = false;
            bas = false;
            tirer = false;
            numPadUn = false;
            numPadQuatre = false;
            toucheX = false;
            toucheC = false; 
            toucheR = false;


    }

    }
}
