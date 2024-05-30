using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection;
using Microsoft.Xna.Framework.Input;


namespace CasseBrique.GameObjects
{
    public class Paddle : SpriteGameObject
    {
        private Rectangle _bounds;
        private float _speed = 800f;
        private Vector2 _targetPosition;

        public Paddle(Rectangle bounds, Scene root) : base(root)
        {
            texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("Paddle");
            _bounds = bounds;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;
            _targetPosition = new Vector2(bounds.Center.X, bounds.Bottom - texture.Height * 0.5f);
            position = _targetPosition;
            tag = "Paddle";
        }

        public void Move(Vector2 direction, float dt)
        {
            Vector2 velocity = direction * _speed;
            position += velocity * dt;
        }

        public override void Update(float dt)
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Q))
                _targetPosition.X -= _speed * dt;

            if (keyboardState.IsKeyDown(Keys.D))
                _targetPosition.X += _speed * dt;

            _targetPosition = Vector2.Clamp(_targetPosition, new Vector2(_bounds.Left + offset.X, position.Y), new Vector2(_bounds.Right - offset.X, position.Y));
            position = Vector2.Lerp(position, _targetPosition, 0.15f);
        }
    }
}
