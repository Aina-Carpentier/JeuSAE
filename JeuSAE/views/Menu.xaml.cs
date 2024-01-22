using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace JeuSAE;

/// <summary>
///     Logique d'interaction pour Menu.xaml
/// </summary>
public partial class Menu : Window
{
    public string Choix;
    private readonly DispatcherTimer DispatcherTimer = new();

    public Menu()
    {
        InitializeComponent();
        canvasMenu.Height = fenetreMenu.Height;
        canvasMenu.Width = fenetreMenu.Width;

        var coteImage = canvasMenu.Width + 50;

        background1.Width = coteImage;
        background1.Height = coteImage;
        background2.Width = coteImage;
        background2.Height = coteImage;

        labJouer.Margin = new Thickness(fenetreMenu.Width / 2 - labJouer.Width / 2,
            fenetreMenu.Height * 0.2 - labJouer.Height / 2, 0, 0);
        labMagasin.Margin = new Thickness(fenetreMenu.Width / 2 - labMagasin.Width / 2,
            fenetreMenu.Height * 0.4 - labMagasin.Height / 2, 0, 0);
        labParametre.Margin = new Thickness(fenetreMenu.Width / 2 - labParametre.Width / 2,
            fenetreMenu.Height * 0.6 - labParametre.Height / 2, 0, 0);
        labQuitter.Margin = new Thickness(fenetreMenu.Width / 2 - labQuitter.Width / 2,
            fenetreMenu.Height * 0.8 - labQuitter.Height / 2, 0, 0);

        Canvas.SetLeft(background1, 0); //TODO fix the scaling and do the scroll logic
        Canvas.SetLeft(background2, background1.Width);
        //background2.Visibility = Visibility.Hidden;


        DispatcherTimer.Tick += Defilement;
        // rafraissement toutes les 16 milliseconds
        DispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
        // lancement du timer
        DispatcherTimer.Start();
    }


    private void Defilement(object sender, EventArgs e)
    {
        Canvas.SetLeft(background1, Canvas.GetLeft(background1) - 2);
        Canvas.SetLeft(background2, Canvas.GetLeft(background2) - 2);

        if (Canvas.GetLeft(background1) + background1.Width < 0) Canvas.SetLeft(background1, background1.Width);
        if (Canvas.GetLeft(background2) + background2.Width < 0) Canvas.SetLeft(background2, background2.Width);
    }

    private void labJouer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Choix = "difficulte";
        Hide();
    }

    private void labMagasin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Choix = "magasin";
        Hide();
    }

    private void labParametre_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Choix = "parametre";
        Hide();
    }

    private void labQuitter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        Choix = "quitter";
        Hide();
    }

    private void labJouer_MouseEnter(object sender, MouseEventArgs e)
    {
        labJouer.Foreground = Brushes.LightCoral;
        labJouer.Opacity = 1;
    }

    private void labJouer_MouseLeave(object sender, MouseEventArgs e)
    {
        labJouer.Foreground = Brushes.Black;
        labJouer.Opacity = 0.8;
    }

    private void labMagasin_MouseEnter(object sender, MouseEventArgs e)
    {
        labMagasin.Foreground = Brushes.Sienna;
        labMagasin.Opacity = 1;
    }

    private void labMagasin_MouseLeave(object sender, MouseEventArgs e)
    {
        labMagasin.Foreground = Brushes.Black;
        labMagasin.Opacity = 0.8;
    }

    private void labParametre_MouseEnter(object sender, MouseEventArgs e)
    {
        labParametre.Foreground = Brushes.LightSlateGray;
        labParametre.Opacity = 1;
    }

    private void labParametre_MouseLeave(object sender, MouseEventArgs e)
    {
        labParametre.Foreground = Brushes.Black;
        labParametre.Opacity = 0.8;
    }

    private void labQuitter_MouseEnter(object sender, MouseEventArgs e)
    {
        labQuitter.Foreground = Brushes.Red;
        labQuitter.Opacity = 1;
    }

    private void labQuitter_MouseLeave(object sender, MouseEventArgs e)
    {
        labQuitter.Foreground = Brushes.Black;
        labQuitter.Opacity = 0.8;
    }
}