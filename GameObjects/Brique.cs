using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace CasseBrique.GameObjects
{
    public class Brique : SpriteGameObject
    {
        private int life;
        
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

        public Brique(Vector2 pos, Color color, Scene root) : base(root) // a recevoir de la part de la Tilemap et du level maker
        {
            texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyBrick"); // a recevoir du constructeur des héritiers
            size.X = texture.Width;
            size.Y = texture.Height;
            position = pos - size * 0.5f;
            this.color = color;
            life = 2;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;
        }


        public void TakeDamage(int damage)
        {
            life -= damage;
            if (life <= 0)
            { isFree = true; }
        }

        public void DropResource(int nb, Resource resource)
        {

        }

    }
}
