using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection;
using Microsoft.Xna.Framework.Input;


namespace CasseBrique.GameObjects
{
    public class Paddle : SpriteGameObject
    {
        private Rectangle _bounds;
        private float _speed=800f;
        private Vector2 _targetPosition;

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

        public Paddle(Rectangle bounds, Vector2 StartingPosition, Scene root) : base(root)
        {
            texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("Paddle");
            position = StartingPosition - size * 0.5f;
            _bounds = bounds;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;
            _targetPosition = new Vector2(bounds.Center.X, bounds.Bottom - texture.Height * 0.5f);
        }

        public void Move(Vector2 direction, float dt)
        {
            Vector2 velocity = direction * _speed;
            position += velocity * dt;
            Bounce();
        }

        public override void Update(float dt)
        {
            var keyboardState=Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D))
                Move(new Vector2(1, 0), dt);

            if (keyboardState.IsKeyDown(Keys.Q))
                Move(new Vector2(-1, 0), dt);
        }

        private void Bounce()
        {
            if (position.X > _bounds.Right - offset.X)
            {
                position.X = _bounds.Right - offset.X;

            }
            else if (position.X < _bounds.Left + offset.X)
            {
                position.X = _bounds.Left + offset.X;

            }
            if (position.Y > _bounds.Bottom - offset.Y)
            {
                position.Y = _bounds.Bottom - offset.Y;

            }
            else if (position.Y < _bounds.Top + offset.Y)
            {
                position.Y = _bounds.Top + offset.Y;

            }
        }
    }
}
