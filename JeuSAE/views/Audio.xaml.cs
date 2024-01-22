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

namespace JeuSAE.views
{
    /// <summary>
    /// Logique d'interaction pour Audio.xaml
    /// </summary>
    public partial class Audio : Window
    {
        public string Choix;
        public int sonSFX, sonMusique;
        public Audio(int AudioSFX, int AudioMusique)
        {
            sonSFX = AudioSFX; sonMusique = AudioMusique;
            InitializeComponent();
            labAudio.Margin = new Thickness(fenetreAudio.Width / 2 - labAudio.Width / 2, fenetreAudio.Height * 0.1, 0, 0);
            labRetour.Margin = new Thickness(labRetour.Width * 0.1, fenetreAudio.Height - labRetour.Height, 0, 0);


            labSFX.Margin = new Thickness(fenetreAudio.Width * 0.3 - labSFX.Width/2, fenetreAudio.Height * 0.4, 0, 0);
            sliderSFX.Margin = new Thickness(fenetreAudio.Width * 0.5 - sliderMusique.Width / 2, fenetreAudio.Height * 0.4, 0, 0);
            labMusique.Margin = new Thickness(fenetreAudio.Width * 0.3 - labMusique.Width / 2, fenetreAudio.Height * 0.65, 0, 0);
            sliderMusique.Margin = new Thickness(fenetreAudio.Width * 0.5 - sliderMusique.Width / 2, fenetreAudio.Height * 0.65, 0, 0);

            sliderSFX.Value = AudioSFX;
            sliderMusique.Value = AudioMusique;
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

        private void sliderMusique_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sonMusique = (int)sliderMusique.Value;
        }

        private void sliderSFX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            sonSFX = (int)sliderSFX.Value;
        }

        private void labRetour_MouseLeave(object sender, MouseEventArgs e)
        {
            labRetour.Foreground = Brushes.Black;
        }

    }
}
