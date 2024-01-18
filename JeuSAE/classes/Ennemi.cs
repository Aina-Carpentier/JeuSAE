using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JeuSAE.classes
{
    public class Ennemi
    {
        private double vie;
        private double vitesse; // En pixel/tick
        private double cadenceTir; // En tick/tir donc si = 3 alors l'ennemi tir une fois tous les 3 tick, donc pour 3 fois par seconde c'est approx 20
        private double cooldownTir = 180; // La valeur utiliser pour calculer quand l'ennemi tire en fonction de la cadence
        private int type;
        private string nom;
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


        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public double PosX
        {
            get { return Canvas.GetLeft(graphique); }
            set { posX = value; }
        }

        public double PosY
        {
            get { return Canvas.GetTop(graphique); }
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
            Graphique.Width = Constantes.ENNEMI_RECT_LARGEUR;
            Graphique.Height = Constantes.ENNEMI_RECT_LARGEUR;

            switch (type)
            {
                case 0: // Triangle équilatéral
                    Vie = Constantes.VIE_TRIANGLE_EQ;
                    Vitesse = Constantes.VITESSE_TRIANGLE_EQ;
                    CadenceTir = Constantes.CADENCE_TRIANGLE_EQ;
                    Nom = Constantes.NOM_TRIANGLE_EQ;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "triangle.png"));// dossierImage c'est un Uri donc ça vas peut-être bugger
                    break;
                case 1: // Carré
                    Vie = Constantes.VIE_CARRE;
                    Vitesse = Constantes.VITESSE_CARRE;
                    CadenceTir = Constantes.CADENCE_CARRE;
                    Nom = Constantes.NOM_CARRE;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "carre.png"));
                    break;
                case 2: // Pentagone
                    Vie = Constantes.VIE_PENTAGONE;
                    Vitesse = Constantes.VITESSE_PENTAGONE;
                    CadenceTir = Constantes.CADENCE_PENTAGONE;
                    Nom = Constantes.NOM_PENTAGONE;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "pentagone.png"));
                    break;
                case 3: // Hexagone
                    Vie = Constantes.VIE_HEXAGONE;
                    Vitesse = Constantes.VITESSE_HEXAGONE;
                    CadenceTir = Constantes.CADENCE_HEXAGONE;
                    Nom = Constantes.NOM_HEXAGONE;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "hexagone.png"));
                    break;
                case 4: // Heptagone
                    Vie = Constantes.VIE_HEPTAGONE;
                    Vitesse = Constantes.VITESSE_HEPTAGONE;
                    CadenceTir = Constantes.CADENCE_HEPTAGONE;
                    Nom = Constantes.NOM_HEPTAGONE;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "heptagone.png"));
                    break;
                case 5: // Octogone
                    Vie = Constantes.VIE_OCTOGONE;
                    Vitesse = Constantes.VITESSE_OCTOGONE;
                    CadenceTir = Constantes.CADENCE_OCTOGONE;
                    Nom = Constantes.NOM_OCTOGONE;
                    ennemiImage.ImageSource = new BitmapImage(new Uri(dossierSprites + "octogone.png"));
                    break;
                case 6: // Cercle
                    Vie = Constantes.VIE_CERCLE;
                    Vitesse = Constantes.VITESSE_CERCLE;
                    CadenceTir = Constantes.CADENCE_CERCLE;
                    Nom = Constantes.NOM_CERCLE;
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
            return HashCode.Combine(Id);
        }

        public override string? ToString()
        {
            return Nom;
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
                x = x * -1;
            }
            if (random.Next(0, 2) == 0)
            {
                y = y * -1;
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



            Panel.SetZIndex(ennemi.Graphique, 1);
            Console.WriteLine("width : " + Canvas.GetLeft(ennemi.Graphique) + " et " + Canvas.GetLeft(carte));

            if (Canvas.GetLeft(ennemi.Graphique) + x < Canvas.GetLeft(carte) || Canvas.GetLeft(ennemi.Graphique) > carte.Width + Canvas.GetLeft(carte))
            {
                Canvas.SetLeft(ennemi.Graphique, posJoueurX - x);
            }
            if (Canvas.GetTop(ennemi.Graphique) + y < Canvas.GetTop(carte) || Canvas.GetTop(ennemi.Graphique) > carte.Height + Canvas.GetTop(carte))
            {
                Canvas.SetTop(ennemi.Graphique, posJoueurY - y);
            }
            ennemi.PosX = Canvas.GetLeft(ennemi.Graphique);
            ennemi.PosY = Canvas.GetTop(ennemi.Graphique);



            mainWindow.monCanvas.Children.Add(ennemi.Graphique);
            MainWindow.listeEnnemi.Add(ennemi);


        }

        public void Tir()
        {
            if (CooldownTir <= 0)
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

                double posJoueurX = mainWindow.fenetrePrincipale.Width / 2 - mainWindow.rectJoueur.Width * 0.75;
                double posJoueurY = mainWindow.fenetrePrincipale.Height / 2 - mainWindow.rectJoueur.Height * 0.75;

                Vector2 vecteurDeplace = new Vector2((float)PosX - (float)posJoueurX, (float)PosY - (float)posJoueurY);
                // Pas besoin de normaliser le vecteur car la classe Balle le fait déjà


                Balle balle = new Balle(5, 20, 1, id.ToString(), 0, PosX + (float)Graphique.Width / 2, PosY + (float)Graphique.Height / 2, -vecteurDeplace);
                mainWindow.monCanvas.Children.Add(balle.Graphique);
                mainWindow.listeBalle.Add(balle);

                CooldownTir = CadenceTir;
            }
            else
            {
                CooldownTir--;
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
