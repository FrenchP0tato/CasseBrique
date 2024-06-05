using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace CasseBrique.GameObjects
{
    public class Brique : SpriteGameObject
    {
        private int life;
        private Resource resource;
        private int NbResource;
        private Texture2D damagedTexture;
        private string text;
      
        public Brique(Color pColor, Scene pRoot) : base(pRoot) 
        {
            texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyBrick"); // a recevoir du constructeur des héritiers
            damagedTexture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyBrickDamaged");
            size.X = texture.Width;
            size.Y = texture.Height;
            this.color = pColor;
            life = 2;
            //offset = size * 0.5f; pas nécessaire car géré par le Game Controller
            tag = "Brique";
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
            if (life <= 0)
            {
                DropResource(NbResource, resource); // add animation for resource drop
                enable=false;
                isFree = true; }
        }

        public void DropResource(int pNb, Resource pResource)
        {
            ServicesLocator.Get<GameController>().GainResource(pResource, pNb);
        }

    }
}
