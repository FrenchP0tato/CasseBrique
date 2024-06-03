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
        
        public Vector2 Size
        {
            get { return size; }
            private set { size = value; }
        }


        public Vector2 Position
        {
            get { return position; }
            private set { position = value; } //accesseur
        }

        public Brique(Color pColor, Scene pRoot) : base(pRoot) // a recevoir de la part de la Tilemap et du level maker
        {
            texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyBrick"); // a recevoir du constructeur des héritiers
            damagedTexture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyBrickDamaged");
            size.X = texture.Width;
            size.Y = texture.Height;
            this.color = pColor;
            life = 2;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;
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
