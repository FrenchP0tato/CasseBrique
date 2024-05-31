using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace CasseBrique.GameObjects
{
    public class Brique : SpriteGameObject
    {
        private int life;
        private Resource resource;
        private int NbResource;
        
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

        public Brique(Color color, Scene root) : base(root) // a recevoir de la part de la Tilemap et du level maker
        {
            texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyBrick"); // a recevoir du constructeur des héritiers
            size.X = texture.Width;
            size.Y = texture.Height;
            this.color = color;
            life = 1;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;
            tag = "Brique";
        }

        public override void OnCollide(SpriteGameObject other)
        {
            if (other is Ball)
            {
                var ball = root.GetGameObjects<Ball>()[0];
                TakeDamage(ball.Damage);
            }
        }

        public void TakeDamage(int damage)
        {
            life -= damage;
            if (life <= 0)
            {
                DropResource(NbResource, resource);
                enable=false;
                isFree = true; }
        }

        public void DropResource(int nb, Resource resource)
        {
            ServicesLocator.Get<GameController>().GainResource(resource, nb);
        }

    }
}
