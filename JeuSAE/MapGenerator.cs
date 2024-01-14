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
            //ChargerImages();
            //ChargerGrille(carte);
            //DessinerFond(carte);

            Rectangle carte = mainWindow.carte;


            String fullDebugUri = AppDomain.CurrentDomain.BaseDirectory + "images\\";
            String[] lesImageEnvironnement;

            String[] uriSplit1 = fullDebugUri.Split("bin\\Debug\\net6.0-windows\\");
            String cutUri1 = uriSplit1[0] + uriSplit1[1];


            System.Drawing.Image source1 = System.Drawing.Image.FromFile(cutUri1 + "\\environnement_256x256\\grass_1.png");
            System.Drawing.Image source2 = System.Drawing.Image.FromFile(cutUri1 + "\\environnement_256x256\\grass_1.png");
            Bitmap cible = new Bitmap((int)carte.Width, (int)carte.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics graphiques = Graphics.FromImage(cible);
            graphiques.CompositingMode = CompositingMode.SourceOver; // c'est le compositingMode par défault mais juste pour être sûr



            try
            {
                String[] uriSplit = fullDebugUri.Split("bin\\Debug\\net6.0-windows\\");
                String cutUri = uriSplit[0] + uriSplit[1];

                lesImageEnvironnement = Directory.GetFiles(cutUri + "environnement_256x256\\");
            }
            catch (IndexOutOfRangeException e)
            {
                lesImageEnvironnement = Directory.GetFiles(fullDebugUri + "environnement_256x256\\");
            }


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




            foreach (String uri in listeArreteGauche)
            {
                Console.WriteLine(uri);
            }









            //var cible = new Bitmap((int)carte.Width, (int)carte.Height, PixelFormat.Format32bppArgb);
            //var graphiques = Graphics.FromImage(cible);
            //graphiques.CompositingMode = CompositingMode.SourceOver; // c'est le compositingMode par défault mais juste pour être sûr
            bool dejaRepondu = false;
            bool herbe;

            //while (!finiDeCharger)
            //{
            //source1 = System.Drawing.Image.FromFile(fullDebugUri + "environnement_16x16\\grass_1.png");
            //source2 = System.Drawing.Image.FromFile(fullDebugUri + "environnement_16x16\\grass_2.png");


            for (int i = 0; i < carte.Height; i += IMG_HEIGHT)
            {
                if (rnd.Next(1, 100) > 10)
                {
                    herbe = true;
                } else
                {
                    herbe = false;
                }

                if (i == 0)
                {// Coin haut gauche
                    if (herbe)
                    {
                        source1 = System.Drawing.Image.FromFile(listeCoinHautGauche[0]);
                    }
                    else
                    {
                        source1 = System.Drawing.Image.FromFile(listeCoinHautGauche[rnd.Next(0, listeCoinHautGauche.Count)]);
                    }
                    dejaRepondu = true;
                }
                else if (i + IMG_HEIGHT >= carte.Height && dejaRepondu == false) //Coin bas gauche
                {
                    if (herbe)
                    {
                        source1 = System.Drawing.Image.FromFile(listeCoinBasGauche[0]);
                    }
                    else
                    {
                        source1 = System.Drawing.Image.FromFile(listeCoinBasGauche[rnd.Next(0, listeCoinBasGauche.Count)]);
                    }
                    dejaRepondu = true;
                }

                for (int n = 0; n < carte.Width; n += IMG_WIDTH)
                {

                    if (rnd.Next(1, 100) > 10)
                    {
                        herbe = true;
                    }
                    else
                    {
                        herbe = false;
                    }

                    //------------------------COINS------------------------
                    if (i == 0 && n + IMG_WIDTH >= carte.Width && dejaRepondu == false) //Coin haut droit
                    {
                        if (herbe)
                        {
                            source1 = System.Drawing.Image.FromFile(listeCoinHautDroit[0]);
                        }
                        else
                        {
                            source1 = System.Drawing.Image.FromFile(listeCoinHautDroit[rnd.Next(0, listeCoinHautDroit.Count)]);
                        }
                        dejaRepondu = true;
                    }
                    else if (i + IMG_HEIGHT >= carte.Height && n + IMG_WIDTH >= carte.Width && dejaRepondu == false) //Coin bas droit
                    {
                        if (herbe)
                        {
                            source1 = System.Drawing.Image.FromFile(listeCoinBasDroit[0]);
                        }
                        else
                        {
                            source1 = System.Drawing.Image.FromFile(listeCoinBasDroit[rnd.Next(0, listeCoinBasDroit.Count)]);
                        }
                        dejaRepondu = true;
                    }

                    //------------------------ARETES------------------------
                    else if (i == 0 && dejaRepondu == false) //Arête haut
                    {
                        if (herbe)
                        {
                            source1 = System.Drawing.Image.FromFile(listeArreteHaut[0]);
                        }
                        else
                        {
                            source1 = System.Drawing.Image.FromFile(listeArreteHaut[rnd.Next(0, listeArreteHaut.Count)]);
                        }
                        dejaRepondu = true;
                    }
                    else if (n + IMG_WIDTH >= carte.Width && dejaRepondu == false) //Arête droit
                    {
                        if (herbe)
                        {
                            source1 = System.Drawing.Image.FromFile(listeArreteDroit[0]);
                        }
                        else
                        {
                            source1 = System.Drawing.Image.FromFile(listeArreteDroit[rnd.Next(0, listeArreteDroit.Count)]);
                        }
                        dejaRepondu = true;
                    }
                    else if (i + IMG_HEIGHT >= carte.Height && dejaRepondu == false) // Arête bas
                    {
                        if (herbe)
                        {
                            source1 = System.Drawing.Image.FromFile(listeArreteBas[0]);
                        }
                        else
                        {
                            source1 = System.Drawing.Image.FromFile(listeArreteBas[rnd.Next(0, listeArreteBas.Count)]);
                        }
                        dejaRepondu = true;
                    }
                    else if (n == 0 && dejaRepondu == false) // Arête gauche
                    {
                        if (herbe)
                        {
                            source1 = System.Drawing.Image.FromFile(listeArreteGauche[0]);
                        }
                        else
                        {
                            source1 = System.Drawing.Image.FromFile(listeArreteGauche[rnd.Next(0, listeArreteGauche.Count)]);
                        }
                        dejaRepondu = true;
                    }
                    else if (dejaRepondu == false) // Sol
                    {
                        if (herbe)
                        {
                            source1 = System.Drawing.Image.FromFile(listeSol[0]);
                        }
                        else
                        {
                            source1 = System.Drawing.Image.FromFile(listeSol[rnd.Next(0, listeSol.Count)]);
                        }
                        dejaRepondu = true;
                    }

                    //if (!dejaRepondu) { finiDeCharger = true; }



                    graphiques.DrawImage(source1, n, i);//TODO faire du multithreading
                    //test(source1, graphiques, n, i);

                    dejaRepondu = false;

                }

                Console.WriteLine("Génération de la map : " + Math.Round((i/carte.Height)*100) + " %");

            }


            //graphiques.DrawImage(source1, 0, 0);
            //graphiques.DrawImage(source1, source1.Width, 0);



            //}



            //#if DEBUG


            ImageBrush image = new ImageBrush();
            image.ImageSource = ToBitmapImage(cible);

            carte.Fill = image;

            /*
            System.Windows.Controls.Image img = new System.Windows.Controls.Image();
            img.Source = image.ImageSource;



            img.Width = carte.Width;
            img.Height = carte.Height;
            
            mainWindow.monCanvas.Children.Add(img);
            Canvas.SetTop(img, Canvas.GetTop(carte));
            Canvas.SetLeft(img, Canvas.GetLeft(carte));
            */

            /*
            try
            {//Si on lance en débug le path n'est pas le même donc :


                String[] uriSplit = fullDebugUri.Split("bin\\Debug\\net6.0-windows\\");
                String cutUri = uriSplit[0] + uriSplit[1];
                String pathMap = System.IO.Path.Combine(cutUri, "result.png");

                if (File.Exists(pathMap))
                {
                    File.Delete(pathMap);
                }

                cible.Save(cutUri + "result.png", ImageFormat.Png);


            }
            catch (IndexOutOfRangeException e)
            {//Et si on lance en normal :
                String pathMap = System.IO.Path.Combine(fullDebugUri, "result.png");
                if (File.Exists(pathMap))
                {
                    File.Delete(pathMap);
                }
                cible.Save(pathMap, ImageFormat.Png);
            }
            */

            //#else

            //#endif


            //cible.Save(/*"C:\\Users\\Ewan\\Desktop\\trucs_scolaires\\BUT\\SAE_DEV\\projet\\JeuSAE\\images\\result.png"*/ AppDomain.CurrentDomain.BaseDirectory + "images\\result.png", ImageFormat.Png);//TODO c'est juste ça qui marche pas correctement
#if DEBUG
            Console.WriteLine(System.AppDomain.CurrentDomain.BaseDirectory + "images\\result.png");
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
