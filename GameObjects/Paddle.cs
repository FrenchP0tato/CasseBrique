using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Reflection;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;




namespace CasseBrique.GameObjects
{
    internal class Paddle : SpriteGameObject
    {
        private Rectangle _bounds;
        private float _speed;
        private Vector2 _targetPosition;
        GameController gc = ServicesLocator.Get<GameController>();
        public int capSize = 20;
        private Rectangle textureSize;

        public Paddle(Rectangle pBounds, Scene pRoot) : base(pRoot)
        {
            texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("Paddle");
            _bounds = pBounds;
            size.X = gc.PaddleSize;
            size.Y = texture.Height;
            textureSize=new Rectangle(0,0, texture.Width, texture.Height);
            
            offset= size * 0.5f;
          
            _targetPosition = new Vector2(pBounds.Center.X+capSize, pBounds.Bottom - texture.Height * 0.5f);
            position = _targetPosition;

            tag = "Paddle";
            color = Color.Red;
            impactSound = ServicesLocator.Get<IAssetsService>().Get<SoundEffect>("ImpactPaddle");
            _speed=gc.PaddleSpeed;
            
        }

        public override void Update(float dt)
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Q))
                _targetPosition.X -= _speed * dt;

            if (keyboardState.IsKeyDown(Keys.D))
                _targetPosition.X += _speed * dt;

            _targetPosition = Vector2.Clamp(_targetPosition, new Vector2(_bounds.Left + offset.X, position.Y), new Vector2(_bounds.Right - offset.X-capSize*2, position.Y));
            position = Vector2.Lerp(position, _targetPosition, 0.15f);
        }


        public override Rectangle collider
        {
            get
            {
                return new Rectangle((int)(position.X - offset.X), (int)(position.Y - offset.Y), (int)size.X+capSize*2, (int)size.Y);

            }

        }

        public override void OnCollide(SpriteGameObject other)
        {
            impactSound.Play(Main.MasterVolume, 0f, 0f);
            base.OnCollide(other);
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture,position-offset, new Rectangle(0,0,capSize,textureSize.Height), color);
            for (int i= 0; i< size.X; i++)
            {
                sb.Draw(texture, position-offset + new Vector2(capSize, 0) + new Vector2(i, 0), new Rectangle(capSize, 0, 1, textureSize.Height), color);
            }
            sb.Draw(texture, position-offset + new Vector2(capSize, 0) + new Vector2(size.X, 0), new Rectangle(textureSize.Width - capSize, 0, capSize, textureSize.Height), color);
        }
    }
}
