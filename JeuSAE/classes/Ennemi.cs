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
        private double vie;
        private double vitesse; // En pixel/tick
        private double cadenceTir; // En tick/tir donc si = 3 alors l'ennemi tir une fois tous les 3 tick, donc pour 3 fois par seconde c'est approx 20
        private double cooldownTir = 180; // La valeur utiliser pour calculer quand l'ennemi tire en fonction de la cadence
        private int type;
        private String nom;
        private double posX;
        private double posY;
        private Guid id = Guid.NewGuid();
        private System.Windows.Shapes.Rectangle graphique;
        private ImageBrush ennemiImage = new ImageBrush();
        private Uri dossierSprites = new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\");
        private static Random random = new Random();


        public double Vie
        {
            get { return vie; }
            set { vie = value; }
        }


        public double Vitesse
        {
            get { return vitesse; }
            set { vitesse = value; }
        }

        public double CadenceTir
        {
            get { return cadenceTir; }
            set { cadenceTir = value; }
        }

        public double CooldownTir
        {
            get { return cooldownTir; }
            set { cooldownTir = value; }
        }


        public int Type
        {
            get { return type; }
            set { type = value; }
        }


        public String Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public double PosX
        {
            get { return Canvas.GetLeft(this.graphique); }
            set { posX = value; }
        }

        public double PosY
        {
            get { return Canvas.GetTop(this.graphique); }
            set { posY = value; }
        }

        public Guid Id
        {
            get { return id; }
        }

        public Rect Rect { get => new Rect(PosX, PosY, Constantes.ENNEMI_RECT_LARGEUR, Constantes.ENNEMI_RECT_HAUTEUR); }

        public System.Windows.Shapes.Rectangle Graphique { get => graphique; set => graphique = value; }

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
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "triangle.png"));// dossierImage c'est un Uri donc ça vas peut-être bugger
                    break;
                case 1: // Carré
                    this.Vie = Constantes.VIE_CARRE;
                    this.Vitesse = Constantes.VITESSE_CARRE;
                    this.CadenceTir = Constantes.CADENCE_CARRE;
                    this.Nom = Constantes.NOM_CARRE;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "carre.png"));
                    break;
                case 2: // Pentagone
                    this.Vie = Constantes.VIE_PENTAGONE;
                    this.Vitesse = Constantes.VITESSE_PENTAGONE;
                    this.CadenceTir = Constantes.CADENCE_PENTAGONE;
                    this.Nom = Constantes.NOM_PENTAGONE;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "pentagone.png"));
                    break;
                case 3: // Hexagone
                    this.Vie = Constantes.VIE_HEXAGONE;
                    this.Vitesse = Constantes.VITESSE_HEXAGONE;
                    this.CadenceTir = Constantes.CADENCE_HEXAGONE;
                    this.Nom = Constantes.NOM_HEXAGONE;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "hexagone.png"));
                    break;
                case 4: // Heptagone
                    this.Vie = Constantes.VIE_HEPTAGONE;
                    this.Vitesse = Constantes.VITESSE_HEPTAGONE;
                    this.CadenceTir = Constantes.CADENCE_HEPTAGONE;
                    this.Nom = Constantes.NOM_HEPTAGONE;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "heptagone.png"));
                    break;
                case 5: // Octogone
                    this.Vie = Constantes.VIE_OCTOGONE;
                    this.Vitesse = Constantes.VITESSE_OCTOGONE;
                    this.CadenceTir = Constantes.CADENCE_OCTOGONE;
                    this.Nom = Constantes.NOM_OCTOGONE;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "octogone.png"));
                    break;
                case 6: // Cercle
                    this.Vie = Constantes.VIE_CERCLE;
                    this.Vitesse = Constantes.VITESSE_CERCLE;
                    this.CadenceTir = Constantes.CADENCE_CERCLE;
                    this.Nom = Constantes.NOM_CERCLE;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "cercle.png"));
                    break; //TODO ajouter plus d'ennemis si on a des idées
            }
            PosX = posX;
            PosY = posY;
            Graphique.Fill = ennemiImage;
        }

        public override bool Equals(object? obj)
        {
            return obj is Ennemi ennemi &&
                   Id.Equals(ennemi.Id);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id);
        }

        public override string? ToString()
        {
            return this.Nom;
        }


        public static void SpawnUnEnnemi(MainWindow mainWindow)
        {
            System.Windows.Shapes.Rectangle carte = mainWindow.carte;
            double posJoueurX = mainWindow.fenetrePrincipale.Width / 2;
            double posJoueurY = mainWindow.fenetrePrincipale.Height / 2;



            int y = random.Next(0, 2000);
            int x = random.Next(500, 2000) - y;



            if (random.Next(0, 2) == 0)
            {
                x = x * (-1);
            }
            if (random.Next(0, 2) == 0)
            {
                y = y * (-1);
            }

#if DEBUG
            //Console.WriteLine("pos Joueur x : " + posJoueurX);
            //Console.WriteLine("pos Joueur y : " + posJoueurY);
            //Console.WriteLine("pos Joueur canvas x : " + Canvas.GetLeft(mainWindow));
            //Console.WriteLine("pos Joueur canvas y : " + Canvas.GetTop(mainWindow.rectJoueur));

#endif

            Ennemi ennemi = new Ennemi(random.Next(0, 7), x, y);
            Canvas.SetLeft(ennemi.Graphique, posJoueurX + x);
            Canvas.SetTop(ennemi.Graphique, posJoueurY + y);



            Canvas.SetZIndex(ennemi.Graphique, 1);
            Console.WriteLine("width : " + Canvas.GetLeft(ennemi.Graphique) + " et " + Canvas.GetLeft(carte));

            if (Canvas.GetLeft(ennemi.Graphique) + x < Canvas.GetLeft(carte) || Canvas.GetLeft(ennemi.Graphique) > carte.Width + Canvas.GetLeft(carte))
            {
                Canvas.SetLeft(ennemi.Graphique, (posJoueurX - x));
            }
            if (Canvas.GetTop(ennemi.Graphique) + y < Canvas.GetTop(carte) || Canvas.GetTop(ennemi.Graphique) > carte.Height + Canvas.GetTop(carte))
            {
                Canvas.SetTop(ennemi.Graphique, (posJoueurY - y));
            }
            ennemi.PosX = Canvas.GetLeft(ennemi.Graphique);
            ennemi.PosY = Canvas.GetTop(ennemi.Graphique);



            mainWindow.monCanvas.Children.Add(ennemi.Graphique);
            MainWindow.listeEnnemi.Add(ennemi);


        }


        public static void SpawnUnEnnemi(MainWindow mainWindow, int type)
        {
            System.Windows.Shapes.Rectangle carte = mainWindow.carte;
            double posJoueurX = mainWindow.fenetrePrincipale.Width / 2;
            double posJoueurY = mainWindow.fenetrePrincipale.Height / 2;



            int y = random.Next(0, 2000);
            int x = random.Next(500, 2000) - y;



            if (random.Next(0, 2) == 0)
            {
                x = x * (-1);
            }
            if (random.Next(0, 2) == 0)
            {
                y = y * (-1);
            }

            Ennemi ennemi = new Ennemi(type, x, y);
            Canvas.SetLeft(ennemi.Graphique, posJoueurX + x);
            Canvas.SetTop(ennemi.Graphique, posJoueurY + y);



            Canvas.SetZIndex(ennemi.Graphique, 1);
            Console.WriteLine("width : " + Canvas.GetLeft(ennemi.Graphique) + " et " + Canvas.GetLeft(carte));

            if (Canvas.GetLeft(ennemi.Graphique) + x < Canvas.GetLeft(carte) || Canvas.GetLeft(ennemi.Graphique) > carte.Width + Canvas.GetLeft(carte))
            {
                Canvas.SetLeft(ennemi.Graphique, (posJoueurX - x));
            }
            if (Canvas.GetTop(ennemi.Graphique) + y < Canvas.GetTop(carte) || Canvas.GetTop(ennemi.Graphique) > carte.Height + Canvas.GetTop(carte))
            {
                Canvas.SetTop(ennemi.Graphique, (posJoueurY - y));
            }
            ennemi.PosX = Canvas.GetLeft(ennemi.Graphique);
            ennemi.PosY = Canvas.GetTop(ennemi.Graphique);



            mainWindow.monCanvas.Children.Add(ennemi.Graphique);
            MainWindow.listeEnnemi.Add(ennemi);


        }



        public void Tir()
        {
            if (this.CooldownTir <= 0)
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

                double posJoueurX = mainWindow.fenetrePrincipale.Width / 2 - mainWindow.rectJoueur.Width * 0.75;
                double posJoueurY = mainWindow.fenetrePrincipale.Height / 2 - mainWindow.rectJoueur.Height * 0.75;

                Vector2 vecteurDeplace = new Vector2((float)this.PosX - (float)posJoueurX, (float)this.PosY - (float)posJoueurY);
                // Pas besoin de normaliser le vecteur car la classe Balle le fait déjà
                Balle balle;
                if (this.Type == 6)
                {
                    balle = new Balle(5, 20, 2, this.id.ToString(), 0, PosX + (float)this.Graphique.Width / 2, PosY + (float)this.Graphique.Height / 2, -vecteurDeplace, 1);
                }
                else
                {
                    balle = new Balle(5, 20, 1, this.id.ToString(), 0, PosX + (float)this.Graphique.Width / 2, PosY + (float)this.Graphique.Height / 2, -vecteurDeplace, 1);
                }

                mainWindow.monCanvas.Children.Add(balle.Graphique);
                mainWindow.listeBalle.Add(balle);

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
