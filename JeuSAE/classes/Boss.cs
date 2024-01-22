using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JeuSAE.classes;

public class Boss : Ennemi
{
    private readonly Uri DossierSprites = new(AppDomain.CurrentDomain.BaseDirectory + "images\\boss\\");
    private readonly ImageBrush Image = new();
    private readonly Random Random = new();

    public Boss(int type, double posX, double posY) : base(type, posX, posY)
    {
        Type = type;

        Graphique = new Rectangle();
        Graphique.Width = Constantes.BOSS_RECT_LARGEUR;
        Graphique.Height = Constantes.BOSS_RECT_HAUTEUR;

        switch (type)
        {
            case 0: // Triangle équilatéral
                Vie = Constantes.VIE_BOSS;
                Vitesse = Constantes.VITESSE_BOSS;
                CadenceTir = Constantes.CADENCE_BOSS;
                Nom = Constantes.NOM_BOSS;
                Image.ImageSource =
                    new BitmapImage(new Uri(DossierSprites +
                                            $"betrix\\{Random.Next(1, 6)}.png")); // dossierImage c'est un Uri donc ça vas peut-être bugger
                break;
        }

        Type = 9;
        PosX = posX;
        PosY = posY;
        Graphique.Fill = Image;
    }
}