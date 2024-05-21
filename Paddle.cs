using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace CasseBrique
{
    internal class Paddle
    {
        private Texture2D texture;
        protected Vector2 position;
        private Color color = Color.Red;
        protected Vector2 size;

        public Vector2 Position
        {
            get { return position; }
            private set { position = value; } //accesseur
        }

        public Vector2 Size
        {
            get { return size; }
            private set { size = value; }
        }

        public Paddle(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            this.size = new Vector2(100, 24);
        }

        public void Move(Vector2 direction, float speed, float dt)
        {
            position += speed * direction*dt;
        }

        public void Draw(SpriteBatch sprb)
        {
            sprb.Draw(texture, position, color);
        }

        public void CheckBounds(Point _screenSize)
        {
            if (position.X >= _screenSize.X - size.X) 
            {
                position.X = _screenSize.X - size.X;
            }
            if (position.X <= 0)   
            {
                position.X = 0;
            }
            if (position.Y + size.Y >= _screenSize.Y)
            {
                position.Y = _screenSize.Y - size.Y;
            }
            if (position.Y <= 0)
            {
                position.Y = 0;
            }
        }

    }
}
