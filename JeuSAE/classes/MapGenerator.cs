using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Shapes;
using Rectangle = System.Windows.Shapes.Rectangle;
using System.Diagnostics;
using System.Windows.Xps.Packaging;

namespace JeuSAE.classes
{
    public class MapGenerator
    {

        private static readonly Regex SOL_REGEX = new Regex(@"^grass_[0-9]+\.png$");

        private static readonly Regex COIN_HAUT_GAUCHE_REGEX = new Regex(@"^grass_top_left_border_[0-9]+\.png$");
        private static readonly Regex COIN_HAUT_DROIT_REGEX = new Regex(@"^grass_top_right_border_[0-9]+\.png$");
        private static readonly Regex COIN_BAS_DROIT_REGEX = new Regex(@"^grass_bottom_right_border_[0-9]+\.png$");
        private static readonly Regex COIN_BAS_GAUCHE_REGEX = new Regex(@"^grass_bottom_left_border_[0-9]+\.png$");

        private static readonly Regex ARRETE_HAUT_REGEX = new Regex(@"^grass_top_border_[0-9]+\.png$");
        private static readonly Regex ARRETE_DROIT_REGEX = new Regex(@"^grass_right_border_[0-9]+\.png$");
        private static readonly Regex ARRETE_BAS_REGEX = new Regex(@"^grass_bottom_border_[0-9]+\.png$");
        private static readonly Regex ARRETE_GAUCHE_REGEX = new Regex(@"^grass_left_border_[0-9]+\.png$");


        private static List<string> listeSol = new List<string>();

        private static List<string> listeCoinHautGauche = new List<string>();
        private static List<string> listeCoinHautDroit = new List<string>();
        private static List<string> listeCoinBasDroit = new List<string>();
        private static List<string> listeCoinBasGauche = new List<string>();

        private static List<string> listeArreteHaut = new List<string>();
        private static List<string> listeArreteDroit = new List<string>();
        private static List<string> listeArreteBas = new List<string>();
        private static List<string> listeArreteGauche = new List<string>();

        private static Random rnd = new Random();

        private static int IMG_WIDTH = 256;
        private static int IMG_HEIGHT = 256;
        private static int PROBA_HERBE = 20;

        private static string CHEMIN_DOSSIER_IMAGES = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images\\");
        private static string CHEMIN_FICHIERS_IMAGES = System.IO.Path.Combine(CHEMIN_DOSSIER_IMAGES, "environnement_256x256\\");

        public static void load(MainWindow mainWindow)
        {
            Rectangle carte = mainWindow.carte;
            int largeurCarte = (int)carte.Width;
            int hauteurCarte = (int)carte.Height;
            string[] images = Directory.GetFiles(CHEMIN_FICHIERS_IMAGES);
            System.Drawing.Image imageSource = System.Drawing.Image.FromFile(CHEMIN_FICHIERS_IMAGES + "grass_1.png");
            Bitmap cible = new Bitmap(largeurCarte, hauteurCarte, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics graphiques = Graphics.FromImage(cible);


            foreach (string uri in images)
            {
                string nomImage = uri.Split("\\")[uri.Split("\\").Length - 1];
                if (SOL_REGEX.IsMatch(nomImage))
                {
                    listeSol.Add(uri);
                }
                else if (COIN_HAUT_GAUCHE_REGEX.IsMatch(nomImage))
                {
                    listeCoinHautGauche.Add(uri);
                }
                else if (COIN_HAUT_DROIT_REGEX.IsMatch(nomImage))
                {
                    listeCoinHautDroit.Add(uri);
                }
                else if (COIN_BAS_DROIT_REGEX.IsMatch(nomImage))
                {
                    listeCoinBasDroit.Add(uri);
                }
                else if (COIN_BAS_GAUCHE_REGEX.IsMatch(nomImage))
                {
                    listeCoinBasGauche.Add(uri);
                }

                else if (ARRETE_HAUT_REGEX.IsMatch(nomImage))
                {
                    listeArreteHaut.Add(uri);
                }
                else if (ARRETE_DROIT_REGEX.IsMatch(nomImage))
                {
                    listeArreteDroit.Add(uri);
                }
                else if (ARRETE_BAS_REGEX.IsMatch(nomImage))
                {
                    listeArreteBas.Add(uri);
                }
                else if (ARRETE_GAUCHE_REGEX.IsMatch(nomImage))
                {
                    listeArreteGauche.Add(uri);
                }

            }

#if DEBUG
            foreach (string uri in listeArreteGauche)
            {
                Console.WriteLine(uri);
            }
#endif

            for (int y = 0; y < hauteurCarte; y += IMG_HEIGHT)
            {
                for (int x = 0; x < largeurCarte; x += IMG_WIDTH)
                {
                    List<string> liste = new List<string>();
                    int xPlusImage = x + IMG_WIDTH;
                    int yPlusImage = y + IMG_HEIGHT;

                    if (!PointEstCoin(ref liste, x, y, xPlusImage, yPlusImage, largeurCarte, hauteurCarte)) PointEstArete(ref liste, x, y, xPlusImage, yPlusImage, largeurCarte, hauteurCarte);

                    bool herbeOuFleur = rnd.Next(1, 100) > PROBA_HERBE;
                    RecupererSourceImage(out imageSource, herbeOuFleur, liste);
                    graphiques.DrawImage(imageSource, x, y); //TODO multithreading
                }

                Console.WriteLine("Génération de la map : " + Math.Round(y / carte.Height * 100) + " %");
            }

            ImageBrush image = new ImageBrush();
            image.ImageSource = ToBitmapImage(cible);
            carte.Fill = image;
        }

        private static void PointEstArete(ref List<string> liste, int x, int y, int xPlusImage, int yPlusImage, int largeurCarte, int hauteurCarte)
        {
            if (y == 0) //Arête haut
            {
                liste = listeArreteHaut;
            }
            else if (xPlusImage >= largeurCarte) //Arête droit
            {
                liste = listeArreteDroit;
            }
            else if (yPlusImage >= hauteurCarte) // Arête bas
            {
                liste = listeArreteBas;
            }
            else if (x == 0) // Arête gauche
            {
                liste = listeArreteGauche;
            }
            else // Sol
            {
                liste = listeSol;
            }
        }

        private static bool PointEstCoin(ref List<string> liste, int x, int y, int xPlusImage, int yPlusImage, int largeurCarte, int hauteurCarte)
        {
            bool res = true;
            if (y == 0 && xPlusImage >= largeurCarte) //Coin haut droit
            {
                liste = listeCoinHautDroit;
            }
            else if (yPlusImage >= hauteurCarte && xPlusImage >= largeurCarte) //Coin bas droit
            {
                liste = listeCoinBasDroit;
            }
            else if (y == 0 && x == 0) // Coin haut gauche
            {
                liste = listeCoinHautGauche;
            }
            else if (yPlusImage >= hauteurCarte && x == 0) //Coin bas gauche
            {
                liste = listeCoinBasGauche;
            }
            else res = false;
            return res;
        }

        private static void RecupererSourceImage(out System.Drawing.Image image, bool herbeOuFleur, List<string> images)
        {
            int index = herbeOuFleur ? 0 : rnd.Next(0, images.Count);
            image = System.Drawing.Image.FromFile(images[index]);
        }


        private static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
