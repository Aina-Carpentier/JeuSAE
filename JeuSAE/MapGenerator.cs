using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
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

        public static void load()
        {
            ChargerImages();

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
            String nomFichier = Path.GetFileName(uri.LocalPath);
            foreach (String regex in REGEX)
            {
                if (Regex.IsMatch(nomFichier, regex))
                {
                    ImageBrush image = new ImageBrush();
                    image.ImageSource = new BitmapImage(uri);
                    // listOuLaCharger.Add(image);

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
