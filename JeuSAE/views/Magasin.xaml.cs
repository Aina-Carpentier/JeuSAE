using JeuSAE.classes;
using JeuSAE.data;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JeuSAE
{
    /// <summary>
    /// Logique d'interaction pour Magasin.xaml
    /// </summary>
    public partial class Magasin : Window
    {
        private static readonly BaseDeDonnee BaseDeDonnee = JsonUtilitaire.LireFichier(Constantes.CHEMIN_BDD);
        public String? Choix;
        private static ImageBrush Arme1 = new ImageBrush(), Arme2 = new(), Arme3 = new(), Diamant = new();

        public Magasin()
        {
            InitializeComponent();
            labMagasin.Margin = new Thickness(fenetreMagasin.Width / 2 - labMagasin.Width / 2, fenetreMagasin.Height * 0.05, 0, 0);
            labMenu.Margin = new Thickness(labMenu.Width * 0.1, fenetreMagasin.Height - labMenu.Height, 0, 0);

            rectArme1.Margin = new Thickness(fenetreMagasin.Width * 0.25 - rectArme1.Width / 2, fenetreMagasin.Height * 0.3, 0, 0);
            rectArme2.Margin = new Thickness(fenetreMagasin.Width * 0.50 - rectArme2.Width / 2, fenetreMagasin.Height * 0.3, 0, 0);
            labPrix1.Margin = new Thickness(fenetreMagasin.Width * 0.50 - labPrix1.Width / 2, fenetreMagasin.Height * 0.4, 0, 0);
            rectArme3.Margin = new Thickness(fenetreMagasin.Width * 0.75 - rectArme3.Width / 2, fenetreMagasin.Height * 0.3, 0, 0);
            labPrix2.Margin = new Thickness(fenetreMagasin.Width * 0.75 - labPrix2.Width / 2, fenetreMagasin.Height * 0.4, 0, 0);

            rectDescription.Margin = new Thickness(fenetreMagasin.Width * 0.5 - rectDescription.Width / 2, fenetreMagasin.Height * 0.55, 0, 0);
            rectPDV.Margin = new Thickness(fenetreMagasin.Width * 0.5 - rectPDV.Width / 2, fenetreMagasin.Height * 0.6, 0, 0);
            rectVitesse.Margin = new Thickness(fenetreMagasin.Width * 0.5 - rectVitesse.Width / 2, fenetreMagasin.Height * 0.75, 0, 0);
            labPDV.Margin = new Thickness(fenetreMagasin.Width * 0.15 - labPDV.Width /2, fenetreMagasin.Height *0.6, 0, 0);
            labVitesse.Margin = new Thickness(fenetreMagasin.Width * 0.15 - labVitesse.Width / 2, fenetreMagasin.Height * 0.75, 0, 0);
            
            butPDV.Margin = new Thickness(fenetreMagasin.Width * 0.85 - butPDV.Width / 2, fenetreMagasin.Height * 0.6, 0, 0);
            butVitesse.Margin = new Thickness(fenetreMagasin.Width * 0.85 - butVitesse.Width / 2, fenetreMagasin.Height * 0.75, 0, 0);
            rectVitesseRempli.Margin = rectVitesse.Margin; rectPDVRempli.Margin = rectPDV.Margin;

            butPDV.Content = $"Cout : {prixPDV()}";
            butVitesse.Content = $"Cout : {prixVitesse()}";
            labDiamant.Content = $"x {BaseDeDonnee.Argent}";
            labPrix1.Content = $"Prix : {Constantes.PRIX_ARME}";
            labPrix2.Content = $"Prix : {Constantes.PRIX_ARME}";

            labDescription.Width = rectDescription.Width; labDescription.Height = rectDescription.Height; labDescription.Margin = rectDescription.Margin;
            Arme1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\arme1.png"));

            //arme 2 débloquée ?
            if (BaseDeDonnee.Arme2) 
            {
                Arme2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\Arme2.png"));
                labPrix1.Visibility = Visibility.Hidden;
            }
            else
                Arme2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\Arme2V.png"));

            //arme 3 débloquée ?
            if (BaseDeDonnee.Arme3)
            {
                Arme3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\Arme3.png"));
                labPrix2.Visibility = Visibility.Hidden;
            }
            else
                Arme3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\Arme3V.png"));

            Diamant.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\diamant.png"));

            rectVitesseRempli.Width = 100 * BaseDeDonnee.AmeliorationVitesse;
            rectPDVRempli.Width = 100 * BaseDeDonnee.AmeliorationPdv;
            rectArme1.Fill = Arme1; rectArme2.Fill = Arme2; rectArme3.Fill = Arme3;
            rectDiamant.Fill = Diamant;

        }


        private void labMenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Choix = "menu";
            this.Hide();
        }

        private void labMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            labMenu.Foreground = Brushes.LightSlateGray;
        }

        private void labMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            labMenu.Foreground = Brushes.Black;
            //Ca modifie pas le .json bizarre ?
            JsonUtilitaire.Ecriture(BaseDeDonnee, Constantes.CHEMIN_BDD);
        }

        private void rectArme1_MouseEnter(object sender, MouseEventArgs e)
        {
            rectDescription.Visibility = Visibility.Visible;
            labDescription.Content = "Une arme avec une cadence de tir moyenne\net des dégats moyens.\nUne très bonne arme pour les débutants.";
        }

        private void rectArme1_MouseLeave(object sender, MouseEventArgs e)
        {
            rectDescription.Visibility = Visibility.Hidden;
            labDescription.Content = "";
        }

        private void rectArme1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void rectArme2_MouseEnter(object sender, MouseEventArgs e)
        {
            rectDescription.Visibility = Visibility.Visible;
            labDescription.Content = "Une arme avec une cadence de tir faible\nmais des dégats élevés.\nUne arme idéale pour ceux à la recherche \nd'une forte puissance de feu.";
        }

        private void rectArme2_MouseLeave(object sender, MouseEventArgs e)
        {
            rectDescription.Visibility = Visibility.Hidden;
            labDescription.Content = "";
        }

        private void rectArme2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
        private void rectArme3_MouseEnter(object sender, MouseEventArgs e)
        {
            rectDescription.Visibility = Visibility.Visible;
            labDescription.Content = "Une arme avec une cadence de tir élevé\nmais des dégats faibles.\nUne arme idéale pour ceux qui souhaitent\nsubmerger leurs adversaires de balles \n(ou ceux qui ne savent pas viser).";
        }



        private void rectArme3_MouseLeave(object sender, MouseEventArgs e)
        {
            rectDescription.Visibility = Visibility.Hidden;
            labDescription.Content = "";
        }

        private void rectArme3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void butPDV_Click(object sender, RoutedEventArgs e)
        {
            if (BaseDeDonnee.Argent >= prixPDV())
            {
                BaseDeDonnee.Argent -= prixPDV();
                BaseDeDonnee.AmeliorationPdv += 1;
                rectPDVRempli.Width += 100;
                butPDV.Content = $"Cout : {prixPDV()}";
                labDiamant.Content = $"x {BaseDeDonnee.Argent}";

            }
        }

        private void butVitesse_Click(object sender, RoutedEventArgs e)
        {
            if (BaseDeDonnee.Argent >= prixVitesse())
            {
                BaseDeDonnee.Argent -= prixVitesse();
                BaseDeDonnee.AmeliorationVitesse += 1;
                rectVitesseRempli.Width += 100;
                butVitesse.Content = $"Cout : {prixVitesse()}";
                labDiamant.Content = $"x {BaseDeDonnee.Argent}";

            }
        }
        private int prixPDV()
        {
            return (int)(Constantes.PRIX_BASE_AMELIORATION * Math.Pow(2, BaseDeDonnee.AmeliorationPdv));
        }

        private int prixVitesse()
        {
            return (int)(Constantes.PRIX_BASE_AMELIORATION * Math.Pow(2, BaseDeDonnee.AmeliorationVitesse));

        }
    }
}
