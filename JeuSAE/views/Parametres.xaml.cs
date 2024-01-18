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
    /// Logique d'interaction pour Parametres.xaml
    /// </summary>
    public partial class Parametres : Window
    {
        public String choix;
        public Parametres()
        {
            InitializeComponent();
            labParametre.Margin = new Thickness(fenetreParametre.Width / 2 - labParametre.Width / 2, fenetreParametre.Height * 0.05, 0, 0);
            labMenu.Margin = new Thickness(labMenu.Width*0.1, fenetreParametre.Height - labMenu.Height, 0, 0);
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
