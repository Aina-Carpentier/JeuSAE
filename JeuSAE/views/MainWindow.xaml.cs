using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using JeuSAE.classes;
using JeuSAE.data;

namespace JeuSAE;

public partial class MainWindow : Window
{
    public static bool OuvreMenuMaxExp = false;
    
    // État du joueur
    private static bool Gauche, Droite, Haut, Bas, Tirer;
    private static bool NumPadUn, NumPadQuatre;
    private static bool ToucheX, ToucheC, ToucheR, ToucheI, ToucheO, ToucheP;
    
    // Chemin et apparence
    public static string? CheminSprite;
    private static readonly ImageBrush ApparenceJoueur = new();
    private static readonly ImageBrush ApparenceArme = new();
    
    // Position du joueur
    private static double PosJoueurX, PosJoueurY;
    
    // Base de données et effets visuels
    private static readonly BaseDeDonnee BaseDeDonnee = JsonUtilitaire.LireFichier(Constantes.CHEMIN_BDD);
    private static readonly BlurBitmapEffect EffetFlou = new();
    private static readonly BlurBitmapEffect NonFloue = new();
    
    // Images
    private readonly BitmapImage ImgBtnAmelioration = new(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\rectangle_upgrade.png"));
    private readonly BitmapImage ImgBtnPresse = new(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\rectangle_upgrade_presse.png"));
    
    // Timer et Hitbox
    private readonly DispatcherTimer Timer = new();
    private Rect HitboxJoueur = new(940, 500, 40, 80); // Hitbox player
    
    // Balles
    public static readonly List<Balle> Balles = new();
    public static readonly List<Balle> BallesMortes = new();
    public static List<Ennemi> Ennemis = new();
    public static List<Ennemi> EnnemisMorts = new();
    
    // État du joueur (mort, direction)
    public bool Mort, MortDroite = true, RegardeADroite = true;
    
    // Animation
    public int TickAnimation;

    
    public MainWindow()
    {
        InitializeComponent();
        InitialisationMenu();
        InitialisationHud();
        InitialisationElementsGraphiques();
        
        // Moteur de jeu rafraîchit le jeu toutes les 16ms
        Timer.Tick += GameEngine;
        Timer.Interval = TimeSpan.FromMilliseconds(16);
        Timer.Start();
    }

    private void InitialisationElementsGraphiques()
    {
        NonFloue.Radius = 0;
        
        // Joueur
        PosJoueurX = fenetrePrincipale.Width / 2;
        PosJoueurY = fenetrePrincipale.Height / 2;
        rectJoueur.Margin = new Thickness(PosJoueurX - rectJoueur.Width / 2, PosJoueurY - rectJoueur.Height / 2, 0, 0);
        rectArme.Margin = new Thickness(PosJoueurX - rectJoueur.Width / 2, PosJoueurY - rectJoueur.Height / 2, 0, 0); 
        
        // Boutons de l'écran de mort
        labQuitter.Margin = new Thickness(fenetrePrincipale.Width / 2 - labQuitter.Width / 2,
            fenetrePrincipale.Height * 0.35, 0, 0);
        labRetour.Margin = new Thickness(fenetrePrincipale.Width / 2 - labRetour.Width / 2,
            fenetrePrincipale.Height * 0.2, 0, 0);
        
        // Map
        MapGenerator.load(this);
    }

    private void InitialisationHud()
    {
        HudResolution1920X1080();
        Hud.ChangeBarreEliminations(0);
        Hud.ChangeBarreExp(0);
    }

    private void InitialisationMenu()
    {
        Constantes.LECTEUR_MUSIQUE_MENU.Load();
        Constantes.LECTEUR_MUSIQUE_MENU.PlayLooping();
        
        var Menu = new Menu();
        var Parametres = new Parametres();
        var Magasin = new Magasin();
        var Touche = new Touche(Constantes.TOUCHE_HAUT, Constantes.TOUCHE_BAS, Constantes.TOUCHE_DROITE,
            Constantes.TOUCHE_GAUCHE);
        var PreJeu = new PreJeu();

        Menu.ShowDialog();
        string Choix = Menu.Choix;


        while (Choix != "jouer")
            switch (Choix)
            {
                case "quitter":
                    Environment.Exit(0);
                    break;

                case "parametre":
                    Parametres.ShowDialog();
                    Choix = Parametres.Choix;
                    break;

                case "menu":
                    Menu.ShowDialog();
                    Choix = Menu.Choix;
                    break;

                case "magasin":
                    Magasin.ShowDialog();
                    Choix = Magasin.Choix;
                    break;

                case "touche":
                    Touche.ShowDialog();
                    Choix = Touche.Choix;
                    Constantes.TOUCHE_HAUT = Touche.Haut;
                    Constantes.TOUCHE_BAS = Touche.Bas;
                    Constantes.TOUCHE_DROITE = Touche.Droite;
                    Constantes.TOUCHE_GAUCHE = Touche.Gauche;
                    break;

                case "difficulte":
                    PreJeu.ShowDialog();
                    Choix = PreJeu.Choix;
                    break;
            }
        Constantes.LECTEUR_MUSIQUE_MENU.Stop();
    }

    private void GameEngine(object sender, EventArgs e)
    {
#if DEBUG
        Console.WriteLine(Canvas.GetLeft(carte));
        Console.WriteLine(Canvas.GetTop(carte));
#endif

        if (Mort)
        {
            AnimationMort();
        }
        else if (OuvreMenuMaxExp)
        {
        }
        else
        {
            MouvementJoueur();
            TirJoueur();
            SpawnEnnemis();
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
        var Curseur = e.GetPosition(monCanvas);
        Canvas.SetTop(curseurPerso, Curseur.Y - curseurPerso.Height / 2);
        Canvas.SetLeft(curseurPerso, Curseur.X - curseurPerso.Width / 2);
        if (Curseur.X > fenetrePrincipale.Width / 2)
            RegardeADroite = true;
        else
            RegardeADroite = false;
    }

    private void monCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Tirer = true;
    }

    private void monCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        Tirer = false;
    }

    private void CanvasKeyIsDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Constantes.TOUCHE_GAUCHE)
            Gauche = true;
        if (e.Key == Constantes.TOUCHE_DROITE)
            Droite = true;
        if (e.Key == Constantes.TOUCHE_HAUT)
            Haut = true;
        if (e.Key == Constantes.TOUCHE_BAS)
            Bas = true;

        //------------------------------------------- CODES DE TRICHE -------------------------------------------

        //Super vitesse
        if (e.Key == Key.NumPad1)
            NumPadUn = true;
        if (e.Key == Key.NumPad4)
            NumPadQuatre = true;
        if (NumPadUn && NumPadQuatre) Constantes.VITESSE_JOUEUR = 200;

        //Clear ennemis
        if (e.Key == Key.X) ToucheX = true;

        if (e.Key == Key.C) ToucheC = true;

        if (ToucheC && ToucheX)
        {
            ToucheX = false;
            ToucheC = false;
            EnleverTousLesEnnemis();
            Ennemi.SpawnUnEnnemi(this);
        }


        //Spawn cercle

        if (e.Key == Key.R) ToucheR = true;

        if (ToucheC && ToucheR)
        {
            ToucheC = false;
            ToucheR = false;
            Ennemi.SpawnUnEnnemi(this, 6);
        }

        //God mode
        if (e.Key == Key.I) ToucheI = true;
        if (e.Key == Key.O) ToucheO = true;
        if (e.Key == Key.P) ToucheP = true;

        if (ToucheI && ToucheO && ToucheP)
        {
            ToucheI = false;
            ToucheO = false;
            ToucheP = false;
            Constantes.VIE_JOUEUR = double.MaxValue;
            Constantes.DEGATS_JOUEUR = int.MaxValue;
        }
    }

    private void CanvasKeyIsUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Constantes.TOUCHE_GAUCHE)
            Gauche = false;
        if (e.Key == Constantes.TOUCHE_DROITE)
            Droite = false;
        if (e.Key == Constantes.TOUCHE_HAUT)
            Haut = false;
        if (e.Key == Constantes.TOUCHE_BAS)
            Bas = false;

        //------------------------------------------- CODES DE TRICHE -------------------------------------------

        //Super vitesse
        if (e.Key == Key.NumPad1)
            NumPadUn = false;
        if (e.Key == Key.NumPad4)
            NumPadQuatre = false;

        //Clear ennemis
        if (e.Key == Key.X) ToucheX = false;

        if (e.Key == Key.C) ToucheC = false;

        //Spawn cercle
        if (e.Key == Key.R) ToucheR = false;


        //God mode
        if (e.Key == Key.I) ToucheI = false;
        if (e.Key == Key.O) ToucheO = false;
        if (e.Key == Key.P) ToucheP = false;
    }

    private void MouvementJoueur()
    {
        DeplacerEnDirection(Gauche, Constantes.VITESSE_JOUEUR, 0, PosJoueurX - rectJoueur.Width / 2);
        DeplacerEnDirection(Droite, -Constantes.VITESSE_JOUEUR, 0,
            -carte.Width + rectJoueur.Width / 2 + PosJoueurX);
        DeplacerEnDirection(Haut, 0, Constantes.VITESSE_JOUEUR, PosJoueurY - rectJoueur.Height / 2);
        DeplacerEnDirection(Bas, 0, -Constantes.VITESSE_JOUEUR,
            -carte.Height + rectJoueur.Height / 2 + PosJoueurY);
    }

    private void DeplacerEnDirection(bool direction, double deplacementX, double deplacementY, double positionLimite)
    {
        if (direction)
        {
            if (EstDansLesLimites(deplacementX, deplacementY, positionLimite))
            {
                if (deplacementX != 0) Canvas.SetLeft(carte, Canvas.GetLeft(carte) + deplacementX);
                else Canvas.SetTop(carte, Canvas.GetTop(carte) + deplacementY);
                DeplacerObjets(Balles, deplacementX, deplacementY);
                DeplacerObjets(Ennemis, deplacementX, deplacementY);
            }
            else
            {
                if (deplacementX != 0) Canvas.SetLeft(carte, positionLimite);
                else Canvas.SetTop(carte, positionLimite);
            }

            foreach (var Ennemi in
                     Ennemis)
                Ennemi.Tir();
        }
    }

    private void DeplacerObjets(List<Balle> objets, double deplacementX, double deplacementY)
    {
        foreach (var Objet in objets)
        {
            Canvas.SetLeft(Objet.Graphique, Canvas.GetLeft(Objet.Graphique) + deplacementX);
            Canvas.SetTop(Objet.Graphique, Canvas.GetTop(Objet.Graphique) + deplacementY);

            Objet.PosX = Canvas.GetLeft(Objet.Graphique);
            Objet.PosY = Canvas.GetTop(Objet.Graphique);
        }
    }

    private void DeplacerObjets(List<Ennemi> objets, double deplacementX, double deplacementY)
    {
        foreach (var Objet in objets)
        {
            Canvas.SetLeft(Objet.Graphique, Canvas.GetLeft(Objet.Graphique) + deplacementX);
            Canvas.SetTop(Objet.Graphique, Canvas.GetTop(Objet.Graphique) + deplacementY);

            Objet.PosX = Canvas.GetLeft(Objet.Graphique);
            Objet.PosY = Canvas.GetTop(Objet.Graphique);
        }
    }

    private bool EstDansLesLimites(double deplacementX, double deplacementY, double positionLimite)
    {
        if (deplacementX != 0)
            return deplacementX < 0
                ? Canvas.GetLeft(carte) + deplacementX > positionLimite
                : Canvas.GetLeft(carte) + deplacementX < positionLimite;
        if (deplacementY != 0)
            return deplacementY < 0
                ? Canvas.GetTop(carte) + deplacementY > positionLimite
                : Canvas.GetTop(carte) + deplacementY < positionLimite;

        return false;
    }

    private void TirJoueur()
    {
        GestionTempsRecharge();

        if (Tirer && Constantes.TEMPS_RECHARGE_ACTUEL <= 0) CreerBalleJoueur();

        GestionDeplacementBalles();
    }

    private void GestionTempsRecharge()
    {
        if (Constantes.TEMPS_RECHARGE_ACTUEL > 0)
            Constantes.TEMPS_RECHARGE_ACTUEL--;
    }

    private void CreerBalleJoueur()
    {
        Constantes.TEMPS_RECHARGE_ACTUEL = (int)Constantes.TEMPS_RECHARGE_ARME;

        var PosEcran = Mouse.GetPosition(Application.Current.MainWindow);
        var PosCarte = Mouse.GetPosition(carte);

#if DEBUG
        Console.WriteLine(PosCarte.X + "  " + PosCarte.Y);
#endif

        var VecteurTir = new Vector2((float)PosEcran.X - (float)PosJoueurX, (float)PosEcran.Y - (float)PosJoueurY);

        var BalleJoueur = new Balle(Constantes.VITESSE_BALLE_JOUEUR, Constantes.TAILLE_BALLE_JOUEUR, 0, "joueur", 0,
            PosJoueurX, PosJoueurY, VecteurTir, Constantes.DEGATS_JOUEUR);
        PositionnerBalle(BalleJoueur);

        monCanvas.Children.Add(BalleJoueur.Graphique);
        Balles.Add(BalleJoueur);
    }

    private void PositionnerBalle(Balle balle)
    {
        Canvas.SetLeft(balle.Graphique, balle.PosX);
        Canvas.SetTop(balle.Graphique, balle.PosY);
    }

    private void GestionDeplacementBalles()
    {
        if (Balles != null)
        {
            foreach (var Balle in Balles)
            {
                Balle.Deplacement();

                if (BalleHorsLimite(Balle)) BallesMortes.Add(Balle);

                PositionnerBalle(Balle);
            }

            foreach (var Balle in BallesMortes)
            {
                Balles.Remove(Balle);
                monCanvas.Children.Remove(Balle.Graphique);
            }

            BallesMortes.Clear();
        }
    }

    private void CollisionBalle()
    {
        foreach (var Balle in Balles)
        {
            if (Balle.Rect.IntersectsWith(HitboxJoueur) && Balle.Tireur != "joueur")
            {
                //Application.Current.Shutdown();
                BallesMortes.Add(Balle);
                Hud.AjouteVie((int)-Balle.Degats);
            }

            var ListeEnnemiTemporaire = new List<Ennemi>(Ennemis);
            foreach (var Ennemi in ListeEnnemiTemporaire)
                if (Balle.Rect.IntersectsWith(Ennemi.Rect) && Balle.Tireur == "joueur")
                {
                    if (!Balle.ListeEnnemisPerces.Contains(Ennemi.Id))
                    {
                        Ennemi.Vie -= Balle.Degats;
                        Balle.ListeEnnemisPerces.Add(Ennemi.Id);
                    }

                    if (Balle.ListeEnnemisPerces.Count >= Balle.NombrePerce) BallesMortes.Add(Balle);

                    if (Ennemi.Vie <= 0)
                    {
                        EnnemisMorts.Add(Ennemi);
                        Hud.AjouteElimination(50);
                        if (Ennemi.Nom == "Boss")
                            Hud.AjouteExp(Constantes.COEFFICIENT_EXPERIENCE * 200d);
                        else
                            Hud.AjouteExp(Constantes.COEFFICIENT_EXPERIENCE);
                    }
                }
        }
    }

    private void bouttonUpgrade1_MouseEnter(object sender, MouseEventArgs e)
    {
        bouttonUpgrade1.Source = ImgBtnPresse;
    }

    private void bouttonUpgrade1_MouseLeave(object sender, MouseEventArgs e)
    {
        bouttonUpgrade1.Source = ImgBtnAmelioration;
    }

    private void bouttonUpgrade2_MouseEnter(object sender, MouseEventArgs e)
    {
        bouttonUpgrade2.Source = ImgBtnPresse;
    }

    private void bouttonUpgrade2_MouseLeave(object sender, MouseEventArgs e)
    {
        bouttonUpgrade2.Source = ImgBtnAmelioration;
    }

    private void bouttonUpgrade3_MouseEnter(object sender, MouseEventArgs e)
    {
        bouttonUpgrade3.Source = ImgBtnPresse;
    }

    private void bouttonUpgrade3_MouseLeave(object sender, MouseEventArgs e)
    {
        bouttonUpgrade3.Source = ImgBtnAmelioration;
    }

    private void bouttonUpgrade1_MouseDown(object sender, MouseButtonEventArgs e)
    {
        MenuMaxExp.AppliqueBonusAuJoueur(1);
    }

    private void bouttonUpgrade2_MouseDown(object sender, MouseButtonEventArgs e)
    {
        MenuMaxExp.AppliqueBonusAuJoueur(2);
    }

    private void bouttonUpgrade3_MouseDown(object sender, MouseButtonEventArgs e)
    {
        MenuMaxExp.AppliqueBonusAuJoueur(3);
    }

    private void CollisionEnnemi()
    {
        foreach (var Ennemi in Ennemis)
            if (HitboxJoueur.IntersectsWith(Ennemi.Rect))
                Hud.AjouteVie(-Constantes.DEGATS_COLLISION);
    }

    private bool BalleHorsLimite(Balle balle)
    {
        return Canvas.GetLeft(balle.Graphique) <= Canvas.GetLeft(carte) - 400 ||
               Canvas.GetTop(balle.Graphique) <= Canvas.GetTop(carte) - 400 ||
               Canvas.GetLeft(balle.Graphique) >= Canvas.GetLeft(carte) + carte.Width + 400 ||
               Canvas.GetTop(balle.Graphique) >= Canvas.GetTop(carte) + carte.Height + 400;
    }

    private void SpawnEnnemis()
    {
        if (Constantes.COMPTEUR_SPAWN >= Constantes.TICK_REQUIS_POUR_SPAWN_ENNEMI)
        {
            Ennemi.SpawnUnEnnemi(this);
            Constantes.COMPTEUR_SPAWN = 0;
        }

        Constantes.COMPTEUR_SPAWN++;

        // Boss
        if (Hud.BossDoitSpawn())
        {
            Ennemi.SpawnUnBoss(this);
            Hud.ChangeBarreExp(0);
        }
    }

    private void HudResolution1920X1080()
    {
        if (fenetrePrincipale.Width == 1920 && fenetrePrincipale.Height == 1080)
        {
            Canvas.SetLeft(labDiamant, 98);
            Canvas.SetTop(labDiamant, 165);

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
        foreach (var Ennemi in Ennemis) EnnemisMorts.Add(Ennemi);

        foreach (var Ennemi in EnnemisMorts)
        {
            Ennemis.Remove(Ennemi);
            monCanvas.Children.Remove(Ennemi.Graphique);
        }

        EnnemisMorts.Clear();
    }

        private void LogiqueEnnemis()
        {
            double PosJoueurX = HitboxJoueur.Left + HitboxJoueur.Width /2;
            double PosJoueurY = HitboxJoueur.Top + HitboxJoueur.Height/2;

        foreach (var Ennemi in Ennemis)
        {
            Ennemi.PosX = Canvas.GetLeft(Ennemi.Graphique);
            Ennemi.PosY = Canvas.GetTop(Ennemi.Graphique);
            var VecteurDeplace = new Vector2((float)Ennemi.PosX - (float)PosJoueurX,
                (float)Ennemi.PosY - (float)PosJoueurY);
            var VecteurDeplaceNormalise = Vector2.Normalize(VecteurDeplace);

            var NewPosEnnemiX = Ennemi.PosX - VecteurDeplaceNormalise.X * Ennemi.Vitesse;
            var NewPosEnnemiY = Ennemi.PosY - VecteurDeplaceNormalise.Y * Ennemi.Vitesse;

            Ennemi.PosX = NewPosEnnemiX;
            Ennemi.PosY = NewPosEnnemiY;

            Canvas.SetLeft(Ennemi.Graphique, NewPosEnnemiX);
            Canvas.SetTop(Ennemi.Graphique, NewPosEnnemiY);
            Ennemi.Tir();
        }
    }

    private void AnimationJoueur()
    {
        var PosEcran = Mouse.GetPosition(Application.Current.MainWindow);
        var VecteurTir = new Vector2((float)PosEcran.X - (float)PosJoueurX, (float)PosEcran.Y - (float)PosJoueurY);
        float NormalVecteurX = Vector2.Normalize(VecteurTir).X, NormalVecteurY = Vector2.Normalize(VecteurTir).Y;
#if DEBUG
        Console.WriteLine("vecteur x " + NormalVecteurX);
        Console.WriteLine("vecteur y " + NormalVecteurY);
#endif

        CheminSprite = AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\";
        Constantes.TICK_ANIMATION++;
        rectJoueur.Fill = ApparenceJoueur;
        rectArme.Fill = ApparenceArme;
        Console.WriteLine(Constantes.TICK_ANIMATION);
        if (RegardeADroite)
            CheminSprite += "droite\\";
        else
            CheminSprite += "gauche\\";


        //marche
        if (Bas || Haut || Droite || Gauche)
        {
            if (Constantes.TICK_ANIMATION >= 30)
                Constantes.TICK_ANIMATION = 0;
            ApparenceJoueur.ImageSource =
                new BitmapImage(new Uri(CheminSprite + $"\\marche\\marche{Constantes.TICK_ANIMATION / 5 + 1}.png"));
        }
        //idle
        else
        {
            if (Constantes.TICK_ANIMATION >= 20)
                Constantes.TICK_ANIMATION = 0;
            ApparenceJoueur.ImageSource =
                new BitmapImage(new Uri(CheminSprite + $"\\idle\\idle{Constantes.TICK_ANIMATION / 5 + 1}.png"));
        }

        //faire varier en fonction de la position du curseur
        if (Math.Abs(NormalVecteurX) < 0.2 && NormalVecteurY > 0.8)
            ApparenceArme.ImageSource = new BitmapImage(new Uri(CheminSprite + "\\arme\\arme1_5.png"));
        else if (Math.Abs(NormalVecteurX) < 0.2 && NormalVecteurY < -0.8)
            ApparenceArme.ImageSource = new BitmapImage(new Uri(CheminSprite + "\\arme\\arme1_1.png"));
        else if (Math.Abs(NormalVecteurX) < 0.96 && NormalVecteurY > 0.25)
            ApparenceArme.ImageSource = new BitmapImage(new Uri(CheminSprite + "\\arme\\arme1_4.png"));
        else if (Math.Abs(NormalVecteurX) < 0.96 && NormalVecteurY < -0.25)
            ApparenceArme.ImageSource = new BitmapImage(new Uri(CheminSprite + "\\arme\\arme1_2.png"));
        else
            ApparenceArme.ImageSource = new BitmapImage(new Uri(CheminSprite + "\\arme\\arme1_3.png"));
    }

    private void SupprimerEnnemis()
    {
        foreach (var Ennemi in EnnemisMorts)
        {
            NouvelleElimination();
            Ennemis.Remove(Ennemi);
            monCanvas.Children.Remove(Ennemi.Graphique);
        }

        EnnemisMorts.Clear();
    }

    private void NouvelleElimination()
    {
        BaseDeDonnee.Eliminations += 1;
        MettreAJourBdd();
    }

    private void MettreAJourBdd()
    {
        JsonUtilitaire.Ecriture(BaseDeDonnee, Constantes.CHEMIN_BDD);
        labDiamant.Content = BaseDeDonnee.Argent;
        labEliminations.Content = BaseDeDonnee.Eliminations;
    }

    private void AnimationMort()
    {
        EffetFlou.Radius = 10;

        foreach (UIElement Children in monCanvas.Children) Children.BitmapEffect = EffetFlou;

        labQuitter.BitmapEffect = NonFloue;
        labRetour.BitmapEffect = NonFloue;
        rectJoueur.BitmapEffect = NonFloue;

        //carte.BitmapEffect = EffetFlou;

        CheminSprite = AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\";
        if (TickAnimation < 40)
            TickAnimation++;
        rectArme.Visibility = Visibility.Hidden;
        Console.WriteLine(TickAnimation);
        if (MortDroite)
            CheminSprite += "droite\\";
        else
            CheminSprite += "gauche\\";

        ApparenceJoueur.ImageSource =
            new BitmapImage(new Uri(CheminSprite + $"\\mort\\mort{TickAnimation / 10 + 1}.png"));
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
        Mort = false;
        MortDroite = true;
        RegardeADroite = true;
        Balles.Clear();
        Ennemis.Clear();
        EnnemisMorts.Clear();
        BallesMortes.Clear();
        TickAnimation = 0;
        Gauche = false;
        Droite = false;
        Haut = false;
        Bas = false;
        Tirer = false;
        NumPadUn = false;
        NumPadQuatre = false;
        ToucheX = false;
        ToucheC = false;
        ToucheR = false;
    }
}