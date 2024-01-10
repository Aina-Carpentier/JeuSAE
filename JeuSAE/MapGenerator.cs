using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JeuSAE
{
    internal class MapGenerator
    {
        private const string COIN_HAUT_DROIT_REGEX = "grass_top_right_border_[0-9]+";
        private const string COIN_DROIT_REGEX = "grass_right_border_[0-9]+";
        private const string COIN_BAS_DROIT_REGEX = "grass_bottom_right_border_[0-9]+";
        private const string COIN_HAUT_GAUCHE_REGEX = "grass_top_left_border_[0-9]+";
        private const string COIN_GAUCHE_REGEX = "grass_left_border_[0-9]+";
        private const string COIN_BAS_GAUCHE_REGEX = "grass_bottom_left_border_[0-9]+";

        private readonly static List<String> REGEX = new List<string> { COIN_BAS_DROIT_REGEX, COIN_BAS_GAUCHE_REGEX, COIN_DROIT_REGEX, COIN_GAUCHE_REGEX, COIN_HAUT_DROIT_REGEX, COIN_HAUT_GAUCHE_REGEX };

        private readonly static List<ImageBrush> COIN_HAUT_DROITE_IMAGES = new List<ImageBrush> { };
        private readonly static List<ImageBrush> COIN_HAUT_GAUCHE_IMAGES = new List<ImageBrush> { };
        private readonly static List<ImageBrush> COIN_BAS_DROITE_IMAGES = new List<ImageBrush> { };
        private readonly static List<ImageBrush> COIN_BAS_GAUCHE_IMAGES = new List<ImageBrush> { };
        private readonly static List<ImageBrush> COIN_DROITE_IMAGES = new List<ImageBrush> { };
        private readonly static List<ImageBrush> COIN_GAUCHE_IMAGES = new List<ImageBrush> { };

        private static double IMG_WIDTH = 512;
        private static double IMG_HEIGHT = 512;

        public static void load(Grid carte)
        {
            ChargerImages();
            ChargerGrille(carte);
            DessinerFond(carte);
        }

        private static void ChargerGrille(Grid carte)
        {
            int currentX = 0;
            int currentY = 0;
            
            while (currentX < carte.Width)
            {
                carte.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(IMG_WIDTH, GridUnitType.Pixel) });
                currentX += (int)IMG_WIDTH;
            }

            while (currentY < carte.Height)
            {
                carte.RowDefinitions.Add(new RowDefinition { Height =  new GridLength(IMG_HEIGHT, GridUnitType.Pixel) });
                currentY += (int)IMG_HEIGHT;
            }
        }

        private static void DessinerFond(Grid carte)
        {
            int nombreImageDansTableauLargeur = carte.ColumnDefinitions.Count;
            int nombreImageDansTableauHauteur = carte.RowDefinitions.Count;

            Console.WriteLine("Largeur image nombre: " + nombreImageDansTableauLargeur);
            Console.WriteLine("Hauteur image nombre: " + nombreImageDansTableauHauteur);

            foreach (ColumnDefinition colonne in carte.ColumnDefinitions)
            {
                foreach (RowDefinition ligne in carte.RowDefinitions)
                {
                    ImageBrush image = ImageAletatoireParmi(COIN_BAS_GAUCHE_IMAGES);
                    Image img = new Image();
                    img.Source = image.ImageSource;
                    img.Width = IMG_WIDTH; 
                    img.Height = IMG_HEIGHT;

                    Grid.SetColumn(img, carte.ColumnDefinitions.IndexOf(colonne));
                    Grid.SetRow(img, carte.RowDefinitions.IndexOf(ligne));
                    carte.Children.Add(img);
                }
            }
        }

        private static ImageBrush ImageAletatoireParmi(List<ImageBrush> images)
        {
            Random random = new Random();
            int index = random.Next(images.Count);
            ImageBrush imageBrush = images[index];
            return imageBrush;
        }

        private static void ChargerImages()
        {
            string[] tableau = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "images\\");

            foreach (string file in tableau)
            {
                if (File.Exists(file))
                {
                    ChargerImage(new Uri(file));
                }
            }
        }

        private static void ChargerImage(Uri uri)
        {
            String nomFichier = System.IO.Path.GetFileName(uri.LocalPath);
            foreach (String regex in REGEX)
            {
                if (Regex.IsMatch(nomFichier, regex))
                {
                    ImageBrush image = new ImageBrush();
                    image.ImageSource = new BitmapImage(uri);
                    // listOuLaCharger.Add(image);

                    if (IMG_WIDTH == 0 && IMG_HEIGHT == 0)
                    {
                        IMG_WIDTH = image.ImageSource.Width;
                        IMG_HEIGHT = image.ImageSource.Height;
                    }

                    switch (regex)
                    {
                        case COIN_HAUT_DROIT_REGEX: COIN_HAUT_DROITE_IMAGES.Add(image); break;
                        case COIN_HAUT_GAUCHE_REGEX: COIN_HAUT_GAUCHE_IMAGES.Add(image); break;
                        case COIN_BAS_DROIT_REGEX: COIN_BAS_DROITE_IMAGES.Add(image); break;
                        case COIN_BAS_GAUCHE_REGEX: COIN_BAS_GAUCHE_IMAGES.Add(image); break;
                        case COIN_DROIT_REGEX: COIN_DROITE_IMAGES.Add(image); break;
                        case COIN_GAUCHE_REGEX: COIN_GAUCHE_IMAGES.Add(image); break;
                    }

#if DEBUG
                    Console.WriteLine("Chargement de l'image " + nomFichier + " terminé");
#endif
                }
            }
        }
    }
}
