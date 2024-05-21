using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CasseBrique
{

    internal class Ball
    {
        private Texture2D texture;
        public Vector2 position;
        private Vector2 velocity;
        private Color color = Color.Red;
        public Vector2 size;

        public Ball(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = Vector2.Zero;
            this.size = new Vector2(22, 22);
        }

        public void Shoot(Vector2 direction, float speed)
        {
            direction.Normalize();
            velocity = speed * direction;
        }

        public void Update(float dt)
        {
            position += velocity * dt;
        }

        public void Draw(SpriteBatch sprb)
        {
            sprb.Draw(texture, position, color);
        }

        public void Rebound(Vector2 objectPos, Vector2 objectsize)
        {
            if (position.Y + size.Y >= objectPos.Y) // si mon Y est supérieur à mon objet
            {
                if (position.X + size.X >= objectPos.X) // alors fait: verifie si mon X est supérieur à mon objet
                {
                    if (position.X <= objectPos.X + objectsize.X) //alors fait: verifie si X est inférieur à la position de mon objet plus sa taille
                    {
                        position.Y = objectPos.Y - size.Y;
                        velocity.Y = -velocity.Y; // alors direction verticale s'inverse
                    }
                }
            }
        }


        public void CheckBounds(Point _screenSize)
        {
            if (position.X >= _screenSize.X - size.X) //rebond à droite
            {
                position.X = _screenSize.X - size.X;
                velocity.X = -velocity.X;
            }
            if (position.X <= 0)   // rebond en haut
            {
                position.X = 0;
                velocity.X = -velocity.X;
            }
            if (position.Y + size.Y >= _screenSize.Y) // rebond en bas
            {
                position.Y = _screenSize.Y - size.Y;
                velocity.Y = -velocity.Y;
            }
            if (position.Y <= 0) // rebond à gauche
            {
                position.Y = 0;
                velocity.Y = -velocity.Y;
            }

        }

    }

}