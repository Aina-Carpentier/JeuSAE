using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace JeuSAE;

/// <summary>
///     Logique d'interaction pour Window1.xaml
/// </summary>
public partial class Touche : Window
{
    public string Choix, Ecoute;
    public Key Haut, Bas, Droite, Gauche;

    public Touche(Key toucheHaut, Key toucheBas, Key toucheDroite, Key toucheGauche)
    {
        InitializeComponent();
        labRetour.Margin = new Thickness(labRetour.Width * 0.1, fenetreTouche.Height - labRetour.Height, 0, 0);
        labTouche.Margin =
            new Thickness(fenetreTouche.Width / 2 - labTouche.Width / 2, fenetreTouche.Height * 0.05, 0, 0);
        rectConfig.Margin = new Thickness(fenetreTouche.Width / 2 - rectConfig.Width / 2,
            fenetreTouche.Height / 2 - rectConfig.Height / 2, 0, 0);
        labConfig.Margin = new Thickness(fenetreTouche.Width / 2 - labConfig.Width / 2,
            fenetreTouche.Height / 2 - labConfig.Height / 2, 0, 0);

        Haut = toucheHaut;
        Bas = toucheBas;
        Droite = toucheDroite;
        Gauche = toucheGauche;

        tbHaut.Text = Haut.ToString();
        tbBas.Text = toucheBas.ToString();
        tbDroite.Text = toucheDroite.ToString();
        tbGauche.Text = toucheGauche.ToString();


        labHaut.Margin =
            new Thickness(fenetreTouche.Width * 0.35 - labHaut.Width / 2, fenetreTouche.Height * 0.2, 0, 0);
        tbHaut.Margin = new Thickness(fenetreTouche.Width * 0.50 - tbHaut.Width / 2, fenetreTouche.Height * 0.2, 0, 0);
        butHaut.Margin =
            new Thickness(fenetreTouche.Width * 0.65 - butHaut.Width / 2, fenetreTouche.Height * 0.2, 0, 0);

        labBas.Margin = new Thickness(fenetreTouche.Width * 0.35 - labBas.Width / 2, fenetreTouche.Height * 0.35, 0, 0);
        tbBas.Margin = new Thickness(fenetreTouche.Width * 0.50 - tbBas.Width / 2, fenetreTouche.Height * 0.35, 0, 0);
        butBas.Margin = new Thickness(fenetreTouche.Width * 0.65 - butBas.Width / 2, fenetreTouche.Height * 0.35, 0, 0);

        labGauche.Margin = new Thickness(fenetreTouche.Width * 0.35 - labGauche.Width / 2, fenetreTouche.Height * 0.5,
            0, 0);
        tbGauche.Margin = new Thickness(fenetreTouche.Width * 0.50 - tbGauche.Width / 2, fenetreTouche.Height * 0.5, 0,
            0);
        butGauche.Margin = new Thickness(fenetreTouche.Width * 0.65 - butGauche.Width / 2, fenetreTouche.Height * 0.5,
            0, 0);

        labDroite.Margin = new Thickness(fenetreTouche.Width * 0.35 - labDroite.Width / 2, fenetreTouche.Height * 0.65,
            0, 0);
        tbDroite.Margin = new Thickness(fenetreTouche.Width * 0.50 - tbDroite.Width / 2, fenetreTouche.Height * 0.65, 0,
            0);
        butDroite.Margin = new Thickness(fenetreTouche.Width * 0.65 - butDroite.Width / 2, fenetreTouche.Height * 0.65,
            0, 0);
    }

    private void labRetour_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Choix = "parametre";
        Hide();
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
        Ecoute = "haut";
    }

    private void butBas_Click(object sender, RoutedEventArgs e)
    {
        rectConfig.Visibility = Visibility.Visible;
        labConfig.Visibility = Visibility.Visible;
        Ecoute = "bas";
    }

    private void butGauche_Click(object sender, RoutedEventArgs e)
    {
        rectConfig.Visibility = Visibility.Visible;
        labConfig.Visibility = Visibility.Visible;
        Ecoute = "gauche";
    }

    private void butDroite_Click(object sender, RoutedEventArgs e)
    {
        rectConfig.Visibility = Visibility.Visible;
        labConfig.Visibility = Visibility.Visible;
        Ecoute = "droite";
    }

    private void canvasTouche_KeyDown(object sender, KeyEventArgs e)
    {
        if (Ecoute == "haut")
        {
            tbHaut.Text = e.Key.ToString();
            Haut = e.Key;
        }
        else if (Ecoute == "bas")
        {
            tbBas.Text = e.Key.ToString();
            Bas = e.Key;
        }

        else if (Ecoute == "gauche")
        {
            tbGauche.Text = e.Key.ToString();
            Gauche = e.Key;
        }

        else if (Ecoute == "droite")
        {
            tbDroite.Text = e.Key.ToString();
            Droite = e.Key;
        }


        Ecoute = "";
        rectConfig.Visibility = Visibility.Hidden;
        labConfig.Visibility = Visibility.Hidden;
    }
}