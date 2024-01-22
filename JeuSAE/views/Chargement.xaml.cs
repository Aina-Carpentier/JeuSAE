using JeuSAE.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logique d'interaction pour Chargement.xaml
    /// </summary>
    public partial class Chargement : Window
    {
        private MainWindow fenetrePrincipale;
        public Chargement(MainWindow mainWindow)
        {
            InitializeComponent();
            labChargement.Margin = new Thickness(fenetreChargement.Width / 2 - labChargement.Width / 2, fenetreChargement.Height / 2 - labChargement.Height / 2,0,0);
            fenetrePrincipale = mainWindow;
        }


        private void canvasChargement_MouseEnter(object sender, MouseEventArgs e)
        {
            MapGenerator.load(fenetrePrincipale);
            this.Close();
        }
    }
}
