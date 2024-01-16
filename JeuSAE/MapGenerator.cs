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

namespace JeuSAE
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


        private static List<String> listeSol = new List<String>();

        private static List<String> listeCoinHautGauche = new List<String>();
        private static List<String> listeCoinHautDroit = new List<String>();
        private static List<String> listeCoinBasDroit = new List<String>();
        private static List<String> listeCoinBasGauche = new List<String>();

        private static List<String> listeArreteHaut = new List<String>();
        private static List<String> listeArreteDroit = new List<String>();
        private static List<String> listeArreteBas = new List<String>();
        private static List<String> listeArreteGauche = new List<String>();

        private static Random rnd = new Random();

        private static int IMG_WIDTH = 256;
        private static int IMG_HEIGHT = 256;


        public static void load(MainWindow mainWindow)
        {
            Rectangle carte = mainWindow.carte;
            String dossierImages = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images\\");
            String[] lesImageEnvironnement = Directory.GetFiles(dossierImages + "environnement_256x256\\");
            System.Drawing.Image imageSource = System.Drawing.Image.FromFile(dossierImages + "environnement_256x256\\grass_1.png");
            Bitmap cible = new Bitmap((int)carte.Width, (int)carte.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics graphiques = Graphics.FromImage(cible);


            foreach (String uri in lesImageEnvironnement)
            {
                String nomImage = uri.Split("\\")[uri.Split("\\").Length - 1];
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
            foreach (String uri in listeArreteGauche)
            {
                Console.WriteLine(uri);
            }
#endif

            for (int y = 0; y < carte.Height; y += IMG_HEIGHT)
            {
                bool herbe = rnd.Next(1, 100) > 20 ? true : false;

                for (int x = 0; x < carte.Width; x += IMG_WIDTH)
                {
                    if (y == 0 && x + IMG_WIDTH >= carte.Width) //Coin haut droit
                    {
                        int index = herbe ? 0 : rnd.Next(0, listeCoinHautDroit.Count);
                        imageSource = System.Drawing.Image.FromFile(listeCoinHautDroit[index]);
                    }
                    else if (y + IMG_HEIGHT >= carte.Height && x + IMG_WIDTH >= carte.Width) //Coin bas droit
                    {
                        int index = herbe ? 0 : rnd.Next(0, listeCoinBasDroit.Count);
                        imageSource = System.Drawing.Image.FromFile(listeCoinBasDroit[index]);
                    }
                    else if (y == 0 && x == 0)
                    {
                        int index = herbe ? 0 : rnd.Next(0, listeCoinHautGauche.Count);
                        imageSource = System.Drawing.Image.FromFile(listeCoinHautGauche[index]);
                    }
                    else if (y + IMG_HEIGHT >= carte.Height && x == 0) //Coin bas gauche
                    {
                        int index = herbe ? 0 : rnd.Next(0, listeCoinBasGauche.Count);
                        imageSource = System.Drawing.Image.FromFile(listeCoinBasGauche[index]);
                    }
                    //------------------------ARETES------------------------
                    else if (y == 0) //Arête haut
                    {
                        int index = herbe ? 0 : rnd.Next(0, listeArreteHaut.Count);
                        imageSource = System.Drawing.Image.FromFile(listeArreteHaut[index]);
                    }
                    else if (x + IMG_WIDTH >= carte.Width) //Arête droit
                    {
                        int index = herbe ? 0 : rnd.Next(0, listeArreteDroit.Count);
                        imageSource = System.Drawing.Image.FromFile(listeArreteDroit[index]);
                    }
                    else if (y + IMG_HEIGHT >= carte.Height) // Arête bas
                    {
                        int index = herbe ? 0 : rnd.Next(0, listeArreteBas.Count);
                        imageSource = System.Drawing.Image.FromFile(listeArreteBas[index]);
                    }
                    else if (x == 0) // Arête gauche
                    {
                        int index = herbe ? 0 : rnd.Next(0, listeArreteGauche.Count);
                        imageSource = System.Drawing.Image.FromFile(listeArreteGauche[index]);
                    }
                    else // Sol
                    {
                        int index = herbe ? 0 : rnd.Next(0, listeSol.Count);
                        imageSource = System.Drawing.Image.FromFile(listeSol[index]);
                    }

                    graphiques.DrawImage(imageSource, x, y); //TODO multithreading
                }

                Console.WriteLine("Génération de la map : " + Math.Round((y/carte.Height)*100) + " %");
            }

            ImageBrush image = new ImageBrush();
            image.ImageSource = ToBitmapImage(cible);

            carte.Fill = image;

#if DEBUG
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "images\\result.png");
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory + "images\\environnement_16x16\\grass_1.png");
#endif
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
