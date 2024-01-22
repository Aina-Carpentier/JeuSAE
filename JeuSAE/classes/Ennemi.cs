using JeuSAE.classes;
using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JeuSAE
{
    public class Ennemi
    {
        public double Vie;
        public double Vitesse; // En pixel/tick
        public double CadenceTir; // En tick/tir donc si = 3 alors l'ennemi tir une fois tous les 3 tick, donc pour 3 fois par seconde c'est approx 20
        private double CooldownTir = 180; // La valeur utiliser pour calculer quand l'ennemi tire en fonction de la cadence
        public int Type;
        public string Nom;
        public double PosX;
        public double PosY;
        public Guid Id = Guid.NewGuid();
        public System.Windows.Shapes.Rectangle Graphique;
        private ImageBrush EnnemiImage = new();
        private Uri DossierSprites = new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\");
        private static Random Random = new();

        public Rect Rect { get => new (PosX, PosY, Constantes.ENNEMI_RECT_LARGEUR, Constantes.ENNEMI_RECT_HAUTEUR); }

        public Ennemi(int type, double posX, double posY)
        {
            Type = type;

            Graphique = new System.Windows.Shapes.Rectangle()  /*(PosX, PosY, Constantes.ENNEMI_RECT_LARGEUR, Constantes.ENNEMI_RECT_HAUTEUR)*/;
            this.Graphique.Width = Constantes.ENNEMI_RECT_LARGEUR;
            this.Graphique.Height = Constantes.ENNEMI_RECT_LARGEUR;

            switch (type)
            {
                case 0: // Triangle équilatéral
                    this.Vie = Constantes.VIE_TRIANGLE_EQ;
                    this.Vitesse = Constantes.VITESSE_TRIANGLE_EQ;
                    this.CadenceTir = Constantes.CADENCE_TRIANGLE_EQ;
                    this.Nom = Constantes.NOM_TRIANGLE_EQ;
                    EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "triangle.png"));// dossierImage c'est un Uri donc ça vas peut-être bugger
                    break;
                case 1: // Carré
                    this.Vie = Constantes.VIE_CARRE;
                    this.Vitesse = Constantes.VITESSE_CARRE;
                    this.CadenceTir = Constantes.CADENCE_CARRE;
                    this.Nom = Constantes.NOM_CARRE;
                    EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "carre.png"));
                    break;
                case 2: // Pentagone
                    this.Vie = Constantes.VIE_PENTAGONE;
                    this.Vitesse = Constantes.VITESSE_PENTAGONE;
                    this.CadenceTir = Constantes.CADENCE_PENTAGONE;
                    this.Nom = Constantes.NOM_PENTAGONE;
                    EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "pentagone.png"));
                    break;
                case 3: // Hexagone
                    this.Vie = Constantes.VIE_HEXAGONE;
                    this.Vitesse = Constantes.VITESSE_HEXAGONE;
                    this.CadenceTir = Constantes.CADENCE_HEXAGONE;
                    this.Nom = Constantes.NOM_HEXAGONE;
                    EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "hexagone.png"));
                    break;
                case 4: // Heptagone
                    this.Vie = Constantes.VIE_HEPTAGONE;
                    this.Vitesse = Constantes.VITESSE_HEPTAGONE;
                    this.CadenceTir = Constantes.CADENCE_HEPTAGONE;
                    this.Nom = Constantes.NOM_HEPTAGONE;
                    EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "heptagone.png"));
                    break;
                case 5: // Octogone
                    this.Vie = Constantes.VIE_OCTOGONE;
                    this.Vitesse = Constantes.VITESSE_OCTOGONE;
                    this.CadenceTir = Constantes.CADENCE_OCTOGONE;
                    this.Nom = Constantes.NOM_OCTOGONE;
                    EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "octogone.png"));
                    break;
                case 6: // Cercle
                    this.Vie = Constantes.VIE_CERCLE;
                    this.Vitesse = Constantes.VITESSE_CERCLE;
                    this.CadenceTir = Constantes.CADENCE_CERCLE;
                    this.Nom = Constantes.NOM_CERCLE;
                    EnnemiImage.ImageSource = new BitmapImage(new Uri(DossierSprites + "cercle.png"));
                    break; //TODO ajouter plus d'ennemis si on a des idées
            }
            PosX = posX;
            PosY = posY;
            Graphique.Fill = EnnemiImage;
        }

        public override bool Equals(object? obj)
        {
            return obj is Ennemi Ennemi &&
                   Id.Equals(Ennemi.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }

        public override string? ToString()
        {
            return this.Nom;
        }

        public static void SpawnUnBoss(MainWindow mainWindow)
        {
            System.Windows.Shapes.Rectangle Carte = mainWindow.carte;
            double PosJoueurX = mainWindow.fenetrePrincipale.Width / 2;
            double PosJoueurY = mainWindow.fenetrePrincipale.Height / 2;

            int Y = Random.Next(0, 2000);
            int X = Random.Next(500, 2000) - Y;

            if (Random.Next(0, 2) == 0)
            {
                X = X * (-1);
            }
            if (Random.Next(0, 2) == 0)
            {
                Y = Y * (-1);
            }

            Boss Ennemi = new Boss(0, X, Y);
            Canvas.SetLeft(Ennemi.Graphique, PosJoueurX + X);
            Canvas.SetTop(Ennemi.Graphique, PosJoueurY + Y);

            if (Canvas.GetLeft(Ennemi.Graphique) + X < Canvas.GetLeft(Carte) || Canvas.GetLeft(Ennemi.Graphique) > Carte.Width + Canvas.GetLeft(Carte))
            {
                Canvas.SetLeft(Ennemi.Graphique, (PosJoueurX - X));
            }
            if (Canvas.GetTop(Ennemi.Graphique) + Y < Canvas.GetTop(Carte) || Canvas.GetTop(Ennemi.Graphique) > Carte.Height + Canvas.GetTop(Carte))
            {
                Canvas.SetTop(Ennemi.Graphique, (PosJoueurY - Y));
            }
            Ennemi.PosX = Canvas.GetLeft(Ennemi.Graphique);
            Ennemi.PosY = Canvas.GetTop(Ennemi.Graphique);

            mainWindow.monCanvas.Children.Add(Ennemi.Graphique);
            MainWindow.Ennemis.Add(Ennemi);
        }

        public static void SpawnUnEnnemi(MainWindow mainWindow)
        {
            System.Windows.Shapes.Rectangle Carte = mainWindow.carte;
            double PosJoueurX = mainWindow.fenetrePrincipale.Width / 2;
            double PosJoueurY = mainWindow.fenetrePrincipale.Height / 2;



            int Y = Random.Next(0, 2000);
            int X = Random.Next(500, 2000) - Y;



            if (Random.Next(0, 2) == 0)
            {
                X = X * (-1);
            }
            if (Random.Next(0, 2) == 0)
            {
                Y = Y * (-1);
            }

#if DEBUG
            //Console.WriteLine("pos Joueur x : " + posJoueurX);
            //Console.WriteLine("pos Joueur y : " + posJoueurY);
            //Console.WriteLine("pos Joueur canvas x : " + Canvas.GetLeft(mainWindow));
            //Console.WriteLine("pos Joueur canvas y : " + Canvas.GetTop(mainWindow.rectJoueur));

#endif

            Ennemi Ennemi = new Ennemi(Random.Next(0, 7), X, Y);
            Canvas.SetLeft(Ennemi.Graphique, PosJoueurX + X);
            Canvas.SetTop(Ennemi.Graphique, PosJoueurY + Y);



            Canvas.SetZIndex(Ennemi.Graphique, 1);
            Console.WriteLine("width : " + Canvas.GetLeft(Ennemi.Graphique) + " et " + Canvas.GetLeft(Carte));

            if (Canvas.GetLeft(Ennemi.Graphique) + X < Canvas.GetLeft(Carte) || Canvas.GetLeft(Ennemi.Graphique) > Carte.Width + Canvas.GetLeft(Carte))
            {
                Canvas.SetLeft(Ennemi.Graphique, (PosJoueurX - X));
            }
            if (Canvas.GetTop(Ennemi.Graphique) + Y < Canvas.GetTop(Carte) || Canvas.GetTop(Ennemi.Graphique) > Carte.Height + Canvas.GetTop(Carte))
            {
                Canvas.SetTop(Ennemi.Graphique, (PosJoueurY - Y));
            }
            Ennemi.PosX = Canvas.GetLeft(Ennemi.Graphique);
            Ennemi.PosY = Canvas.GetTop(Ennemi.Graphique);



            mainWindow.monCanvas.Children.Add(Ennemi.Graphique);
            MainWindow.Ennemis.Add(Ennemi);


        }


        public static void SpawnUnEnnemi(MainWindow mainWindow, int type)
        {
            System.Windows.Shapes.Rectangle Carte = mainWindow.carte;
            double PosJoueurX = mainWindow.fenetrePrincipale.Width / 2;
            double PosJoueurY = mainWindow.fenetrePrincipale.Height / 2;



            int Y = Random.Next(0, 2000);
            int X = Random.Next(500, 2000) - Y;



            if (Random.Next(0, 2) == 0)
            {
                X = X * (-1);
            }
            if (Random.Next(0, 2) == 0)
            {
                Y = Y * (-1);
            }

            Ennemi Ennemi = new Ennemi(type, X, Y);
            Canvas.SetLeft(Ennemi.Graphique, PosJoueurX + X);
            Canvas.SetTop(Ennemi.Graphique, PosJoueurY + Y);



            Canvas.SetZIndex(Ennemi.Graphique, 1);
            Console.WriteLine("width : " + Canvas.GetLeft(Ennemi.Graphique) + " et " + Canvas.GetLeft(Carte));

            if (Canvas.GetLeft(Ennemi.Graphique) + X < Canvas.GetLeft(Carte) || Canvas.GetLeft(Ennemi.Graphique) > Carte.Width + Canvas.GetLeft(Carte))
            {
                Canvas.SetLeft(Ennemi.Graphique, (PosJoueurX - X));
            }
            if (Canvas.GetTop(Ennemi.Graphique) + Y < Canvas.GetTop(Carte) || Canvas.GetTop(Ennemi.Graphique) > Carte.Height + Canvas.GetTop(Carte))
            {
                Canvas.SetTop(Ennemi.Graphique, (PosJoueurY - Y));
            }
            Ennemi.PosX = Canvas.GetLeft(Ennemi.Graphique);
            Ennemi.PosY = Canvas.GetTop(Ennemi.Graphique);



            mainWindow.monCanvas.Children.Add(Ennemi.Graphique);
            MainWindow.Ennemis.Add(Ennemi);


        }



        public void Tir()
        {
            if (this.CooldownTir <= 0)
            {
                MainWindow MainWindow = (MainWindow)Application.Current.MainWindow;

                double PosJoueurX = MainWindow.fenetrePrincipale.Width / 2 - MainWindow.rectJoueur.Width * 0.75;
                double PosJoueurY = MainWindow.fenetrePrincipale.Height / 2 - MainWindow.rectJoueur.Height * 0.75;

                Vector2 VecteurDeplace = new Vector2((float)this.PosX - (float)PosJoueurX, (float)this.PosY - (float)PosJoueurY);
                // Pas besoin de normaliser le vecteur car la classe Balle le fait déjà
                Balle Balle;
                if (this.Type == 6)
                {
                    Balle = new Balle(Constantes.VITESSE_BALLE, 20, 2, this.Id.ToString(), 0, PosX + (float)this.Graphique.Width / 2, PosY + (float)this.Graphique.Height / 2, -VecteurDeplace, 1);
                }
                else if (this.Type == 9) //Boss
                {
                    Balle = new Balle(12, 45, 3, this.Id.ToString(), 0, PosX + (float)this.Graphique.Width / 2, PosY + (float)this.Graphique.Height / 2, -VecteurDeplace, 1);
                }
                else
                {
                    Balle = new Balle(Constantes.VITESSE_BALLE, 20, 1, this.Id.ToString(), 0, PosX + (float)this.Graphique.Width / 2, PosY + (float)this.Graphique.Height / 2, -VecteurDeplace, 1);
                }

                MainWindow.monCanvas.Children.Add(Balle.Graphique);
                MainWindow.Balles.Add(Balle);

                this.CooldownTir = this.CadenceTir;
            }
            else
            {
                this.CooldownTir--;
            }


        }


        /*private void DeplaceTir()
        {
            MainWindow mainWindow = (MainWindow) Application.Current.MainWindow;

            double posJoueurX = mainWindow.fenetrePrincipale.Width / 2 - mainWindow.rectJoueur.Width * 0.75;
            double posJoueurY = mainWindow.fenetrePrincipale.Height / 2 - mainWindow.rectJoueur.Height * 0.75;

            Vector2 vecteurDeplace = new Vector2((float)this.PosX - (float)posJoueurX, (float)this.PosY - (float)posJoueurY);
            Vector2 vecteurDeplaceNormalise = Vector2.Normalize(vecteurDeplace);

            double newPosBalleX = this.PosX - (vecteurDeplaceNormalise.X * this.);
            double newPosBalleY = this.PosY - (vecteurDeplaceNormalise.Y * this.Vitesse);

            



        }*/



    }
}
