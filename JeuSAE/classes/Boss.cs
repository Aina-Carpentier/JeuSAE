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
        private ImageBrush Image = new ImageBrush();
        private Uri DossierSprites = new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\boss\\");
        Random Random = new Random();

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
                    Image.ImageSource = new BitmapImage(new Uri(DossierSprites + $"betrix\\{Random.Next(1,6)}.png"));// dossierImage c'est un Uri donc ça vas peut-être bugger
                    break;
            }
            Type = 9;
            PosX = posX;
            PosY = posY;
            Graphique.Fill = Image;
        }
    }
}