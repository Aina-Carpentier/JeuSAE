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
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Touche : Window
    {
        public String choix, ecoute;
        public Key tHaut, tBas, tDroite, tGauche;
        public Touche(Key toucheHaut, Key toucheBas, Key toucheDroite, Key toucheGauche)
        {
            InitializeComponent();
            labRetour.Margin = new Thickness(labRetour.Width * 0.1, fenetreTouche.Height - labRetour.Height, 0, 0);
            labTouche.Margin = new Thickness(fenetreTouche.Width / 2 - labTouche.Width / 2, fenetreTouche.Height * 0.05, 0, 0);
            rectConfig.Margin = new Thickness(fenetreTouche.Width / 2 - rectConfig.Width / 2, fenetreTouche.Height /2 - rectConfig.Height /2, 0, 0);
            labConfig.Margin = new Thickness(fenetreTouche.Width / 2 - labConfig.Width / 2, fenetreTouche.Height /2 - labConfig.Height/2, 0, 0);

            tHaut = toucheHaut;
            tBas = toucheBas;
            tDroite = toucheDroite;
            tGauche = toucheGauche;
            
            tbHaut.Text = tHaut.ToString();
            tbBas.Text = toucheBas.ToString();
            tbDroite.Text = toucheDroite.ToString();
            tbGauche.Text = toucheGauche.ToString();


            labHaut.Margin = new Thickness(fenetreTouche.Width * 0.35 - labHaut.Width / 2, fenetreTouche.Height * 0.2, 0, 0);
            tbHaut.Margin = new  Thickness(fenetreTouche.Width * 0.50 - tbHaut.Width / 2, fenetreTouche.Height * 0.2, 0, 0);
            butHaut.Margin = new Thickness(fenetreTouche.Width * 0.65 - butHaut.Width / 2, fenetreTouche.Height * 0.2, 0, 0);

            labBas.Margin = new Thickness(fenetreTouche.Width * 0.35 - labBas.Width / 2, fenetreTouche.Height * 0.35, 0, 0);
            tbBas.Margin = new Thickness(fenetreTouche.Width * 0.50 - tbBas.Width / 2, fenetreTouche.Height * 0.35, 0, 0);
            butBas.Margin = new Thickness(fenetreTouche.Width * 0.65 - butBas.Width / 2, fenetreTouche.Height * 0.35, 0, 0);

            labGauche.Margin = new Thickness(fenetreTouche.Width * 0.35 - labGauche.Width / 2, fenetreTouche.Height * 0.5, 0, 0);
            tbGauche.Margin = new Thickness(fenetreTouche.Width * 0.50 - tbGauche.Width / 2, fenetreTouche.Height * 0.5, 0, 0);
            butGauche.Margin = new Thickness(fenetreTouche.Width * 0.65 - butGauche.Width / 2, fenetreTouche.Height * 0.5, 0, 0);

            labDroite.Margin = new Thickness(fenetreTouche.Width * 0.35 - labDroite.Width / 2, fenetreTouche.Height * 0.65, 0, 0);
            tbDroite.Margin = new Thickness(fenetreTouche.Width * 0.50 - tbDroite.Width / 2, fenetreTouche.Height * 0.65, 0, 0);
            butDroite.Margin = new Thickness(fenetreTouche.Width * 0.65 - butDroite.Width / 2, fenetreTouche.Height * 0.65, 0, 0);
        }

        private void labRetour_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.choix = "parametre";
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

        private void butHaut_Click(object sender, RoutedEventArgs e)
        {
            rectConfig.Visibility = Visibility.Visible;
            labConfig.Visibility = Visibility.Visible;
            ecoute = "haut";
        }

        private void butBas_Click(object sender, RoutedEventArgs e)
        {
            rectConfig.Visibility = Visibility.Visible;
            labConfig.Visibility = Visibility.Visible;
            ecoute = "bas";
        }

        private void butGauche_Click(object sender, RoutedEventArgs e)
        {
            rectConfig.Visibility = Visibility.Visible;
            labConfig.Visibility = Visibility.Visible;
            ecoute = "gauche";
        }

        private void butDroite_Click(object sender, RoutedEventArgs e)
        {
            rectConfig.Visibility = Visibility.Visible;
            labConfig.Visibility = Visibility.Visible;
            ecoute = "droite";
        }

        private void canvasTouche_KeyDown(object sender, KeyEventArgs e)
        {
            if (ecoute == "haut")
            {
                tbHaut.Text = e.Key.ToString();
                tHaut = e.Key;
            }
            else if (ecoute == "bas")
            {
                tbBas.Text = e.Key.ToString();
                tBas = e.Key;
            }
                
            else if (ecoute == "gauche")
            {
                tbGauche.Text = e.Key.ToString();
                tGauche = e.Key;
            }
                
            else if (ecoute == "droite")
            {
                tbDroite.Text = e.Key.ToString();
                tDroite = e.Key;
            }
                

            ecoute = "";
            rectConfig.Visibility = Visibility.Hidden;
            labConfig.Visibility = Visibility.Hidden;
        }


    }
}
