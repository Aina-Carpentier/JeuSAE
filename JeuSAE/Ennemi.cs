using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JeuSAE
{
    public class Ennemi
    {
        private double vie;
        private double vitesse; // En pixel/tick
        private double cadenceTir; // En seconde/tir donc si = 3 alors l'ennemi tir une fois toutes les 3 secondes, donc pour 3 fois par seconde c'est approx 0.33
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
            get { return posX; }
            set { posX = value; }
        }

        public double PosY
        {
            get { return posY; }
            set { posY = value; }
        }

        public Guid Id
        {
            get { return id; }
        }

        public Rect Rect { get => new Rect(PosX, PosY, Constantes.ENNEMI_RECT_LARGEUR, Constantes.ENNEMI_RECT_HAUTEUR);}

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
                    break; //TODO finir de mettre les images sur les ennemis
                /*
            case 1:
                this.Vie = 5;
                this.Vitesse = 1;
                this.CadenceTir = 2;
                this.Nom = "Rectangle";
                break;
                */
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
                Canvas.SetLeft(ennemi.Graphique, (posJoueurX  -x));
            }
            if (Canvas.GetTop(ennemi.Graphique) + y < Canvas.GetTop(carte) || Canvas.GetTop(ennemi.Graphique) > carte.Height + Canvas.GetTop(carte))
            {
                Canvas.SetTop(ennemi.Graphique, (posJoueurY - y));
            }

            mainWindow.monCanvas.Children.Add(ennemi.Graphique);
            MainWindow.listeEnnemi.Add(ennemi);


        }


    }
}
