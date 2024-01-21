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
        public Magasin()
        {
            InitializeComponent();
            labMagasin.Margin = new Thickness(fenetreMagasin.Width / 2 - labMagasin.Width / 2, fenetreMagasin.Height * 0.05, 0, 0);
            labMenu.Margin = new Thickness(labMenu.Width * 0.1, fenetreMagasin.Height - labMenu.Height, 0, 0);
            rectArme1.Margin = new Thickness(fenetreMagasin.Width * 0.25 - rectArme1.Width / 2, fenetreMagasin.Height * 0.3, 0, 0);
            rectArme2.Margin = new Thickness(fenetreMagasin.Width * 0.50 - rectArme2.Width / 2, fenetreMagasin.Height * 0.3, 0, 0);
            rectArme3.Margin = new Thickness(fenetreMagasin.Width * 0.75 - rectArme3.Width / 2, fenetreMagasin.Height * 0.3, 0, 0);
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
    }
}
