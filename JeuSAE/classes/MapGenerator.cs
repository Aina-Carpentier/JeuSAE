using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace JeuSAE.classes;

public class MapGenerator
{
    private static readonly Regex Sol_Regex = new(@"^grass_[0-9]+\.png$");

    private static readonly Regex Coin_Haut_Gauche_Regex = new(@"^grass_top_left_border_[0-9]+\.png$");
    private static readonly Regex Coin_Haut_Droit_Regex = new(@"^grass_top_right_border_[0-9]+\.png$");
    private static readonly Regex Coin_Bas_Droit_Regex = new(@"^grass_bottom_right_border_[0-9]+\.png$");
    private static readonly Regex Coin_Bas_Gauche_Regex = new(@"^grass_bottom_left_border_[0-9]+\.png$");

    private static readonly Regex Arrete_Haut_Regex = new(@"^grass_top_border_[0-9]+\.png$");
    private static readonly Regex Arrete_Droit_Regex = new(@"^grass_right_border_[0-9]+\.png$");
    private static readonly Regex Arrete_Bas_Regex = new(@"^grass_bottom_border_[0-9]+\.png$");
    private static readonly Regex Arrete_Gauche_Regex = new(@"^grass_left_border_[0-9]+\.png$");


    private static readonly List<string> ListeSol = new();

    private static readonly List<string> ListeCoinHautGauche = new();
    private static readonly List<string> ListeCoinHautDroit = new();
    private static readonly List<string> ListeCoinBasDroit = new();
    private static readonly List<string> ListeCoinBasGauche = new();

    private static readonly List<string> ListeArreteHaut = new();
    private static readonly List<string> ListeArreteDroit = new();
    private static readonly List<string> ListeArreteBas = new();
    private static readonly List<string> ListeArreteGauche = new();

    private static readonly Random Rnd = new();

    private static readonly int Img_Width = 256;
    private static readonly int Img_Height = 256;
    private static readonly int Proba_Herbe = 20;

    private static readonly string Chemin_Dossier_Images =
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images\\");

    private static readonly string Chemin_Fichiers_Images =
        Path.Combine(Chemin_Dossier_Images, "environnement_256x256\\");

    public static void load(MainWindow mainWindow)
    {
        Console.WriteLine("Chargement");
        var carte = mainWindow.carte;
        var largeurCarte = (int)carte.Width;
        var hauteurCarte = (int)carte.Height;
        var images = Directory.GetFiles(Chemin_Fichiers_Images);
        var cible = new Bitmap(largeurCarte, hauteurCarte, PixelFormat.Format32bppArgb);
        var graphiques = Graphics.FromImage(cible);

        var tasks = new List<Task>();

        foreach (var uri in images) tasks.Add(Task.Run(() => ProcessImageUri(uri)));

        Task.WaitAll(tasks.ToArray());

        mainWindow.Dispatcher.Invoke(() =>
        {
            for (var y = 0; y < hauteurCarte; y += Img_Height)
            {
                for (var x = 0; x < largeurCarte; x += Img_Width)
                {
                    var liste = new List<string>();
                    var xPlusImage = x + Img_Width;
                    var yPlusImage = y + Img_Height;

                    if (!PointEstCoin(ref liste, x, y, xPlusImage, yPlusImage, largeurCarte, hauteurCarte))
                        PointEstArete(ref liste, x, y, xPlusImage, yPlusImage, largeurCarte, hauteurCarte);

                    var herbeOuFleur = Rnd.Next(1, 100) > Proba_Herbe;
                    RecupererSourceImage(out var ImageSource, herbeOuFleur, liste);
                    graphiques.DrawImage(ImageSource, x, y);
                }

                Console.WriteLine("Génération de la map : " + Math.Round(y / carte.Height * 100) + " %");
            }

            var image = new ImageBrush();
            image.ImageSource = ToBitmapImage(cible);
            carte.Fill = image;
        });
    }

    private static void ProcessImageUri(string uri)
    {
        var nomImage = uri.Split("\\")[uri.Split("\\").Length - 1];

        if (Sol_Regex.IsMatch(nomImage))
            ListeSol.Add(uri);
        else if (Coin_Haut_Gauche_Regex.IsMatch(nomImage))
            ListeCoinHautGauche.Add(uri);
        else if (Coin_Haut_Droit_Regex.IsMatch(nomImage))
            ListeCoinHautDroit.Add(uri);
        else if (Coin_Bas_Droit_Regex.IsMatch(nomImage))
            ListeCoinBasDroit.Add(uri);
        else if (Coin_Bas_Gauche_Regex.IsMatch(nomImage))
            ListeCoinBasGauche.Add(uri);
        else if (Arrete_Haut_Regex.IsMatch(nomImage))
            ListeArreteHaut.Add(uri);
        else if (Arrete_Droit_Regex.IsMatch(nomImage))
            ListeArreteDroit.Add(uri);
        else if (Arrete_Bas_Regex.IsMatch(nomImage))
            ListeArreteBas.Add(uri);
        else if (Arrete_Gauche_Regex.IsMatch(nomImage)) ListeArreteGauche.Add(uri);
    }

    private static void PointEstArete(ref List<string> liste, int x, int y, int xPlusImage, int yPlusImage,
        int largeurCarte, int hauteurCarte)
    {
        if (y == 0) //Arête haut
            liste = ListeArreteHaut;
        else if (xPlusImage >= largeurCarte) //Arête droit
            liste = ListeArreteDroit;
        else if (yPlusImage >= hauteurCarte) // Arête bas
            liste = ListeArreteBas;
        else if (x == 0) // Arête gauche
            liste = ListeArreteGauche;
        else // Sol
            liste = ListeSol;
    }

    private static bool PointEstCoin(ref List<string> liste, int x, int y, int xPlusImage, int yPlusImage,
        int largeurCarte, int hauteurCarte)
    {
        var Res = true;
        if (y == 0 && xPlusImage >= largeurCarte) //Coin haut droit
            liste = ListeCoinHautDroit;
        else if (yPlusImage >= hauteurCarte && xPlusImage >= largeurCarte) //Coin bas droit
            liste = ListeCoinBasDroit;
        else if (y == 0 && x == 0) // Coin haut gauche
            liste = ListeCoinHautGauche;
        else if (yPlusImage >= hauteurCarte && x == 0) //Coin bas gauche
            liste = ListeCoinBasGauche;
        else Res = false;
        return Res;
    }

    private static void RecupererSourceImage(out Image image, bool herbeOuFleur, List<string> images)
    {
        var Index = herbeOuFleur ? 0 : Rnd.Next(0, images.Count);
        image = Image.FromFile(images[Index]);
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