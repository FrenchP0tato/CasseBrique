using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;




namespace CasseBrique.GameObjects
{
    public class Paddle : SpriteGameObject
    {
        private Rectangle _bounds;
        private float _speed = 800f;
        private Vector2 _targetPosition;
        
        public Paddle(Rectangle pBounds, Scene pRoot) : base(pRoot)
        {
            texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("Paddle");
            _bounds = pBounds;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;
            _targetPosition = new Vector2(pBounds.Center.X, pBounds.Bottom - texture.Height * 0.5f);
            position = _targetPosition;
            tag = "Paddle";
            color = Color.SaddleBrown;
            impactSound = ServicesLocator.Get<IAssetsService>().Get<SoundEffect>("ImpactPaddle");
        }

        public void Move(Vector2 pDirection, float dt)
        {
            Vector2 velocity = pDirection * _speed;
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

        public override void OnCollide(SpriteGameObject other)
        {
            impactSound.Play(); // deplacer dans le Sprite Game object?
            base.OnCollide(other);
        }
    }
}
