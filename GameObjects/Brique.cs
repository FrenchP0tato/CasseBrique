using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Data.Common;



namespace CasseBrique.GameObjects
{
    public class Brique : SpriteGameObject
    {
        private int life;
        private int NbResource=1;
        private Texture2D damagedTexture;
        private string text;
        private string type;

        public Brique(string Type, Scene pRoot) : base(pRoot) 
        {
            texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyBrick"); // a recevoir du constructeur des héritiers
            damagedTexture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyBrickDamaged");
            size.X = texture.Width;
            size.Y = texture.Height;
            life = 1;
            tag = "Brique"; 
            type = Type;

            if (type == "Stone")
            {
                life = 2;
                color = Color.Gray;
                impactSound = ServicesLocator.Get<IAssetsService>().Get<SoundEffect>("ImpactStone");
            }
            if (type == "Wood")
            {
                life = 2;
                color = Color.SaddleBrown;
                impactSound = ServicesLocator.Get<IAssetsService>().Get<SoundEffect>("ImpactWood");
            }
            if (type == "Food") 
            {
                life = 1;
                color = Color.Green;
                impactSound = ServicesLocator.Get<IAssetsService>().Get<SoundEffect>("ImpactGrass");
            } 
            if (type == "Gold")
            {
                life = 1;
                color = Color.Yellow;
                impactSound = ServicesLocator.Get<IAssetsService>().Get<SoundEffect>("ImpactGold");
            } 
            if (type == "Science") 
            {
                life = 1;
                color = Color.Purple;
                impactSound = ServicesLocator.Get<IAssetsService>().Get<SoundEffect>("ImpactGold"); // Change for science specific
            }
        }

        public override void OnCollide(SpriteGameObject pOther)
        {
            if (pOther is Ball)
            {
                var ball = root.GetGameObjects<Ball>()[0];
                TakeDamage(ball.Damage);
            }
        }

        public void TakeDamage(int pDamage)
        {
            life -= pDamage;
            texture = damagedTexture;
            impactSound.Play();

            if (life <= 0)
            {
                DropResource(type, NbResource); // add animation for resource drop
                enable=false;
                isFree = true; }
        }

        public void DropResource(string type, int pNb)
        {
            ServicesLocator.Get<GameController>().GainResource(type, pNb);
        }

    }
}
