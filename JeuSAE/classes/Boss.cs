using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JeuSAE.classes
{
    public class Boss : Ennemi
    {
        private ImageBrush image = new ImageBrush();
        private Uri dossierSprites = new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\boss\\");

        public Boss(int type, double posX, double posY) : base(type, posX, posY)
        {
            Type = type;

            Graphique = new System.Windows.Shapes.Rectangle();
            this.Graphique.Width = Constantes.BOSS_RECT_LARGEUR;
            this.Graphique.Height = Constantes.BOSS_RECT_HAUTEUR;

            switch (type)
            {
                case 0: // Triangle équilatéral
                    this.Vie = Constantes.VIE_BOSS;
                    this.Vitesse = Constantes.VITESSE_BOSS;
                    this.CadenceTir = Constantes.CADENCE_BOSS;
                    this.Nom = Constantes.NOM_BOSS;
                    image.ImageSource = new BitmapImage(new Uri(dossierSprites + "betrix\\1.png"));// dossierImage c'est un Uri donc ça vas peut-être bugger
                    break;
            }
            PosX = posX;
            PosY = posY;
            Graphique.Fill = image;
        }
    }
}
