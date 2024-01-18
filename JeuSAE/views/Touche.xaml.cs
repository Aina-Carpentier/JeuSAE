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
        public String choix;
        public Touche()
        {
            InitializeComponent();
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
