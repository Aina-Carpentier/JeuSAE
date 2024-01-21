using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace JeuSAE;

/// <summary>
///     Logique d'interaction pour Parametres.xaml
/// </summary>
public partial class Parametres : Window
{
    public string choix;

    public Parametres()
    {
        InitializeComponent();
        labParametre.Margin = new Thickness(fenetreParametre.Width / 2 - labParametre.Width / 2,
            fenetreParametre.Height * 0.05, 0, 0);
        labRetour.Margin = new Thickness(labRetour.Width * 0.1, fenetreParametre.Height - labRetour.Height, 0, 0);
        labSon.Margin = new Thickness(fenetreParametre.Width / 2 - labSon.Width / 2, fenetreParametre.Height * 0.3, 0,
            0);
        labTouche.Margin = new Thickness(fenetreParametre.Width / 2 - labTouche.Width / 2,
            fenetreParametre.Height * 0.45, 0, 0);
    }

    private void labRetour_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        choix = "menu";
        Hide();
    }

    private void labSon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        choix = "son";
        Hide();
    }

    private void labTouche_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        choix = "touche";
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

    private void labTouche_MouseEnter(object sender, MouseEventArgs e)
    {
        labTouche.Foreground = Brushes.LightSlateGray;
    }

    private void labTouche_MouseLeave(object sender, MouseEventArgs e)
    {
        labTouche.Foreground = Brushes.Black;
    }

    private void labSon_MouseEnter(object sender, MouseEventArgs e)
    {
        labSon.Foreground = Brushes.LightSlateGray;
    }

    private void labSon_MouseLeave(object sender, MouseEventArgs e)
    {
        labSon.Foreground = Brushes.Black;
    }
}