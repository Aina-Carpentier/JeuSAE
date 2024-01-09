using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JeuSAE
{
    internal class MapGenerator
    {
        private const string COIN_HAUT_DROIT_REGEX = "grass_top_right_border_[0-9]+";
        private readonly static List<ImageBrush> COIN_HAUT_DROITE_IMAGES = new List<ImageBrush> { };
        private readonly static List<Uri> CHEMIN_IMAGES = new List<Uri> { };

        public static void load()
        {
            ChargerCheminsImages();
            ChargerImagesCoinDroit();
        }

        private static void ChargerCheminsImages()
        {
            string[] tableau = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "images\\");

            foreach (string file in tableau)
            {
                if (File.Exists(file))
                {
                    CHEMIN_IMAGES.Add(new Uri(file));
                }
            }
        }

        private static void ChargerImagesCoinDroit()
        {
            foreach (Uri uri in CHEMIN_IMAGES)
            {
                String nomFichier = System.IO.Path.GetFileName(uri.LocalPath);

                if (Regex.IsMatch(nomFichier, COIN_HAUT_DROIT_REGEX))
                {
                    ImageBrush image = new ImageBrush();
                    image.ImageSource = new BitmapImage(uri);
                    COIN_HAUT_DROITE_IMAGES.Add(image);

#if DEBUG
                    Console.WriteLine("Chargement du coin droit " + nomFichier + " terminé" + COIN_HAUT_DROITE_IMAGES[COIN_HAUT_DROITE_IMAGES.Count - 1]);
#endif
                }
            }
        }
    }
}
