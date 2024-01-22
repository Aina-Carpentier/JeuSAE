using JeuSAE.classes;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace JeuSAE;

/// <summary>
///     Logique d'interaction pour PreJeu.xaml
/// </summary>
public partial class PreJeu : Window
{
    public string Choix;

    public PreJeu()
    {
        InitializeComponent();
        labRetour.Margin = new Thickness(labRetour.Width * 0.1, fenetrePreJeu.Height - labRetour.Height, 0, 0);
        labChoix.Margin =
            new Thickness(fenetrePreJeu.Width / 2 - labChoix.Width / 2, fenetrePreJeu.Height * 0.05, 0, 0);
        labFacile.Margin =
            new Thickness(fenetrePreJeu.Width / 2 - labFacile.Width / 2, fenetrePreJeu.Height * 0.3, 0, 0);
        labMoyen.Margin =
            new Thickness(fenetrePreJeu.Width / 2 - labMoyen.Width / 2, fenetrePreJeu.Height * 0.45, 0, 0);
        labDifficile.Margin = new Thickness(fenetrePreJeu.Width / 2 - labDifficile.Width / 2,
            fenetrePreJeu.Height * 0.6, 0, 0);
    }

    private void labRetour_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Choix = "menu";
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

    private void labFacile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Choix = "jouer";
        Hide();
    }

    private void labFacile_MouseEnter(object sender, MouseEventArgs e)
    {
        labFacile.Foreground = Brushes.Lime;
    }

    private void labFacile_MouseLeave(object sender, MouseEventArgs e)
    {
        labFacile.Foreground = Brushes.Black;
    }

    private void labMoyen_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Choix = "jouer";
        Hide();
    }

    private void labMoyen_MouseEnter(object sender, MouseEventArgs e)
    {
        labMoyen.Foreground = Brushes.Yellow;
    }

    private void labMoyen_MouseLeave(object sender, MouseEventArgs e)
    {
        labMoyen.Foreground = Brushes.Black;
    }

    private void labDifficile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Choix = "jouer";
        Hide();
    }

    private void labDifficile_MouseEnter(object sender, MouseEventArgs e)
    {
        labDifficile.Foreground = Brushes.Red;
    }

    private void labDifficile_MouseLeave(object sender, MouseEventArgs e)
    {
        labDifficile.Foreground = Brushes.Black;
    }
}