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
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace JeuSAE.classes
{
    public class MapGenerator
    {

        private static readonly Regex Sol_Regex = new Regex(@"^grass_[0-9]+\.png$");

        private static readonly Regex Coin_Haut_Gauche_Regex = new Regex(@"^grass_top_left_border_[0-9]+\.png$");
        private static readonly Regex Coin_Haut_Droit_Regex = new Regex(@"^grass_top_right_border_[0-9]+\.png$");
        private static readonly Regex Coin_Bas_Droit_Regex = new Regex(@"^grass_bottom_right_border_[0-9]+\.png$");
        private static readonly Regex Coin_Bas_Gauche_Regex = new Regex(@"^grass_bottom_left_border_[0-9]+\.png$");

        private static readonly Regex Arrete_Haut_Regex = new Regex(@"^grass_top_border_[0-9]+\.png$");
        private static readonly Regex Arrete_Droit_Regex = new Regex(@"^grass_right_border_[0-9]+\.png$");
        private static readonly Regex Arrete_Bas_Regex = new Regex(@"^grass_bottom_border_[0-9]+\.png$");
        private static readonly Regex Arrete_Gauche_Regex = new Regex(@"^grass_left_border_[0-9]+\.png$");


        private static readonly List<string> ListeSol = new List<string>();

        private static readonly List<string> ListeCoinHautGauche = new List<string>();
        private static readonly List<string> ListeCoinHautDroit = new List<string>();
        private static readonly List<string> ListeCoinBasDroit = new List<string>();
        private static readonly List<string> ListeCoinBasGauche = new List<string>();

        private static readonly List<string> ListeArreteHaut = new List<string>();
        private static readonly List<string> ListeArreteDroit = new List<string>();
        private static readonly List<string> ListeArreteBas = new List<string>();
        private static readonly List<string> ListeArreteGauche = new List<string>();

        private static readonly Random Rnd = new Random();

        private static readonly int Img_Width = 256;
        private static readonly int Img_Height = 256;
        private static readonly int Proba_Herbe = 20;

        private static readonly string Chemin_Dossier_Images = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images\\");
        private static readonly string Chemin_Fichiers_Images = System.IO.Path.Combine(Chemin_Dossier_Images, "environnement_256x256\\");

        public static void load(MainWindow mainWindow)
        {
            Console.WriteLine("Chargement");
            Rectangle carte = mainWindow.carte;
            int largeurCarte = (int)carte.Width;
            int hauteurCarte = (int)carte.Height;
            string[] images = Directory.GetFiles(Chemin_Fichiers_Images);
            Bitmap cible = new Bitmap(largeurCarte, hauteurCarte, PixelFormat.Format32bppArgb);
            Graphics graphiques = Graphics.FromImage(cible);

            List<Task> tasks = new List<Task>();

            foreach (string uri in images)
            {
                tasks.Add(Task.Run(() => ProcessImageUri(uri)));
            }

            Task.WaitAll(tasks.ToArray());

            mainWindow.Dispatcher.Invoke(() =>
            {
                for (int y = 0; y < hauteurCarte; y += Img_Height)
                {
                    for (int x = 0; x < largeurCarte; x += Img_Width)
                    {
                        List<string> liste = new List<string>();
                        int xPlusImage = x + Img_Width;
                        int yPlusImage = y + Img_Height;

                        if (!PointEstCoin(ref liste, x, y, xPlusImage, yPlusImage, largeurCarte, hauteurCarte))
                            PointEstArete(ref liste, x, y, xPlusImage, yPlusImage, largeurCarte, hauteurCarte);

                        bool herbeOuFleur = Rnd.Next(1, 100) > Proba_Herbe;
                        RecupererSourceImage(out System.Drawing.Image ImageSource, herbeOuFleur, liste);
                        graphiques.DrawImage(ImageSource, x, y);
                    }

                    Console.WriteLine("Génération de la map : " + Math.Round(y / carte.Height * 100) + " %");
                }

                ImageBrush image = new ImageBrush();
                image.ImageSource = ToBitmapImage(cible);
                carte.Fill = image;
            });
        }

        private static void ProcessImageUri(string uri)
        {
            string nomImage = uri.Split("\\")[uri.Split("\\").Length - 1];

            if (Sol_Regex.IsMatch(nomImage))
            {
                ListeSol.Add(uri);
            }
            else if (Coin_Haut_Gauche_Regex.IsMatch(nomImage))
            {
                ListeCoinHautGauche.Add(uri);
            }
            else if (Coin_Haut_Droit_Regex.IsMatch(nomImage))
            {
                ListeCoinHautDroit.Add(uri);
            }
            else if (Coin_Bas_Droit_Regex.IsMatch(nomImage))
            {
                ListeCoinBasDroit.Add(uri);
            }
            else if (Coin_Bas_Gauche_Regex.IsMatch(nomImage))
            {
                ListeCoinBasGauche.Add(uri);
            }
            else if (Arrete_Haut_Regex.IsMatch(nomImage))
            {
                ListeArreteHaut.Add(uri);
            }
            else if (Arrete_Droit_Regex.IsMatch(nomImage))
            {
                ListeArreteDroit.Add(uri);
            }
            else if (Arrete_Bas_Regex.IsMatch(nomImage))
            {
                ListeArreteBas.Add(uri);
            }
            else if (Arrete_Gauche_Regex.IsMatch(nomImage))
            {
                ListeArreteGauche.Add(uri);
            }
        }

        private static void PointEstArete(ref List<string> liste, int x, int y, int xPlusImage, int yPlusImage, int largeurCarte, int hauteurCarte)
        {
            if (y == 0) //Arête haut
            {
                liste = ListeArreteHaut;
            }
            else if (xPlusImage >= largeurCarte) //Arête droit
            {
                liste = ListeArreteDroit;
            }
            else if (yPlusImage >= hauteurCarte) // Arête bas
            {
                liste = ListeArreteBas;
            }
            else if (x == 0) // Arête gauche
            {
                liste = ListeArreteGauche;
            }
            else // Sol
            {
                liste = ListeSol;
            }
        }

        private static bool PointEstCoin(ref List<string> liste, int x, int y, int xPlusImage, int yPlusImage, int largeurCarte, int hauteurCarte)
        {
            bool Res = true;
            if (y == 0 && xPlusImage >= largeurCarte) //Coin haut droit
            {
                liste = ListeCoinHautDroit;
            }
            else if (yPlusImage >= hauteurCarte && xPlusImage >= largeurCarte) //Coin bas droit
            {
                liste = ListeCoinBasDroit;
            }
            else if (y == 0 && x == 0) // Coin haut gauche
            {
                liste = ListeCoinHautGauche;
            }
            else if (yPlusImage >= hauteurCarte && x == 0) //Coin bas gauche
            {
                liste = ListeCoinBasGauche;
            }
            else Res = false;
            return Res;
        }

        private static void RecupererSourceImage(out System.Drawing.Image image, bool herbeOuFleur, List<string> images)
        {
            int Index = herbeOuFleur ? 0 : Rnd.Next(0, images.Count);
            image = System.Drawing.Image.FromFile(images[Index]);
        }


        private static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var Memory = new MemoryStream())
            {
                bitmap.Save(Memory, ImageFormat.Png);
                Memory.Position = 0;

                var BitmapImage = new BitmapImage();
                BitmapImage.BeginInit();
                BitmapImage.StreamSource = Memory;
                BitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                BitmapImage.EndInit();
                BitmapImage.Freeze();

                return BitmapImage;
            }
        }
    }
}
