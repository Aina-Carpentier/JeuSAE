using JeuSAE.classes;
using System;
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
using System.Windows.Shapes;

namespace JeuSAE
{
    /// <summary>
    /// Logique d'interaction pour ChoixArme.xaml
    /// </summary>
    public partial class ChoixArme : Window
    {
        public String Choix;
        public int Arme;
        private static ImageBrush Arme1 = new ImageBrush(), Arme2 = new(), Arme3 = new();
        public ChoixArme()
        {
            InitializeComponent();
            InitializeComponent();
            labArme.Margin = new Thickness(fenetreArme.Width / 2 - labArme.Width / 2, fenetreArme.Height * 0.05, 0, 0);
            labRetour.Margin = new Thickness(labRetour.Width * 0.1, fenetreArme.Height - labRetour.Height, 0, 0);

            rectArme1.Margin = new Thickness(fenetreArme.Width * 0.25 - rectArme1.Width / 2, fenetreArme.Height * 0.3, 0, 0);
            rectArme2.Margin = new Thickness(fenetreArme.Width * 0.50 - rectArme2.Width / 2, fenetreArme.Height * 0.3, 0, 0);
            rectArme3.Margin = new Thickness(fenetreArme.Width * 0.75 - rectArme3.Width / 2, fenetreArme.Height * 0.3, 0, 0);


            rectDescription.Margin = new Thickness(fenetreArme.Width * 0.5 - rectDescription.Width / 2, fenetreArme.Height * 0.55, 0, 0);
            labDescription.Width = rectDescription.Width; labDescription.Height = rectDescription.Height; labDescription.Margin = rectDescription.Margin;

            Arme1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\arme1.png"));

            //arme 2 débloquée ?
            if (MainWindow.BaseDeDonnee.Arme2)
                Arme2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\Arme2.png"));

            else
                Arme2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\Arme2V.png"));

            //arme 3 débloquée ?
            if (MainWindow.BaseDeDonnee.Arme3)
                Arme3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\Arme3.png"));

            else
                Arme3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\Arme3V.png"));

            rectArme1.Fill = Arme1; rectArme2.Fill = Arme2; rectArme3.Fill = Arme3;
        }

        private void labRetour_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Choix = "difficulte";
            this.Hide();

        }

        private void labRetour_MouseEnter(object sender, MouseEventArgs e)
        {
            labRetour.Foreground = Brushes.LightSlateGray;
        }

        private void labRetour_MouseLeave(object sender, MouseEventArgs e)
        {
            labRetour.Foreground = Brushes.Black;
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

            this.Choix = "jouer";
            this.Arme = 1;
            this.Hide();

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
            if (MainWindow.BaseDeDonnee.Arme2)
            {
                this.Choix = "jouer";
                this.Arme = 2;
                this.Hide();
            }
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
            if (MainWindow.BaseDeDonnee.Arme3)
            {
                this.Choix = "jouer";
                this.Arme = 3;
                this.Hide();

            }
        }

    }
}
