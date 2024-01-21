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
    /// Logique d'interaction pour Magasin.xaml
    /// </summary>
    public partial class Magasin : Window
    {
        public String choix;
        private static ImageBrush arme1 = new ImageBrush(), arme2 = new ImageBrush(), arme3 = new ImageBrush();
        public Magasin()
        {
            InitializeComponent();
            labMagasin.Margin = new Thickness(fenetreMagasin.Width / 2 - labMagasin.Width / 2, fenetreMagasin.Height * 0.05, 0, 0);
            labMenu.Margin = new Thickness(labMenu.Width * 0.1, fenetreMagasin.Height - labMenu.Height, 0, 0);
            rectArme1.Margin = new Thickness(fenetreMagasin.Width * 0.25 - rectArme1.Width / 2, fenetreMagasin.Height * 0.3, 0, 0);
            rectArme2.Margin = new Thickness(fenetreMagasin.Width * 0.50 - rectArme2.Width / 2, fenetreMagasin.Height * 0.3, 0, 0);
            rectArme3.Margin = new Thickness(fenetreMagasin.Width * 0.75 - rectArme3.Width / 2, fenetreMagasin.Height * 0.3, 0, 0);
            rectDescription.Margin = new Thickness(fenetreMagasin.Width * 0.5 - rectDescription.Width / 2, fenetreMagasin.Height * 0.55, 0, 0);
            labDescription.Width = rectDescription.Width; labDescription.Height = rectDescription.Height; labDescription.Margin = rectDescription.Margin;
            arme1.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\arme1.png"));
            arme2.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\arme2.png"));
            arme3.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\sprites\\personnage\\droite\\arme\\arme3.png"));
            rectArme1.Fill = arme1;
            rectArme2.Fill = arme2;
            rectArme3.Fill = arme3;
            
        }


        private void labMenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.choix = "menu";
            this.Hide();
        }

        private void labMenu_MouseEnter(object sender, MouseEventArgs e)
        {
            labMenu.Foreground = Brushes.LightSlateGray;
        }

        private void labMenu_MouseLeave(object sender, MouseEventArgs e)
        {
            labMenu.Foreground = Brushes.Black;
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

    }
}
