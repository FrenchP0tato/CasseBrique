
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace CasseBrique.GameObjects
{

    public class Ball : SpriteGameObject
    {
        private Vector2 _direction;
        private float _speed;
        private Vector2 _velocity;
        private Rectangle _bounds;

        public bool isShot = false;
        private int _damage;
        private SoundEffect ploufSound;

        public int Damage
        { get
            { return _damage; }
        }



        public Ball(Rectangle bounds, Scene root) : base(root)
        {
            texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("BallGrey");
            color = Color.IndianRed;
            _bounds = bounds;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;
            tag = "Ball";
            _damage = ServicesLocator.Get<GameController>().BallDamage;
            _speed = ServicesLocator.Get<GameController>().BallSpeed;
            ploufSound = ServicesLocator.Get<IAssetsService>().Get<SoundEffect>("plouf");
        }


        public void Shoot(Vector2 direction)
        {
            if (isShot == false)
            {
                _direction=direction;
                isShot = true;
            }

        }

        public override void Update(float dt)
        {
            if (_damage == 2) color = Color.Red;

            if (isShot)
            {
                Move(dt);
                BounceOnBounds();
                CheckOutOfbounds();
                ResolveCollisionsWithObjects<Paddle>();
                ResolveCollisionsWithObjects<Brique>();
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                    Shoot(new Vector2(-1, -1));
                var pad = root.GetGameObjects<Paddle>()[0];
                position = new Vector2(pad.position.X+pad.capSize, pad.position.Y-texture.Height);
            }
        }

        private void Move(float dt)
        {
            _direction.Normalize();
            _velocity = _direction * _speed*dt;
            position += _velocity;
        }

        private void BounceOnBounds()
        {
            if (position.X > _bounds.Right - offset.X)
            {
                position.X = _bounds.Right - offset.X;
                _direction.X *= -1;
            }
            else if (position.X < _bounds.Left + offset.X)
            {
                position.X = _bounds.Left + offset.X;
                _direction.X *= -1;
            }

            if (position.Y < _bounds.Top + offset.Y)
            {
                position.Y = _bounds.Top + offset.Y;
                _direction.Y *= -1;
            }
        }

        private void CheckOutOfbounds()
        {
            if (position.Y > _bounds.Bottom + offset.Y)
            {
                isShot = false;
                ServicesLocator.Get<GameController>().BallOut();
                ploufSound.Play(Main.MasterVolume, 0f, 0f);
            }
        }

        private void ResolveCollisionsWithObjects<T>() where T : SpriteGameObject
        {
            var objects = root.GetGameObjects<T>();
            foreach (SpriteGameObject obj in objects)
            {
                if (!obj.enable) continue;
                if (collider.Intersects(obj.collider))
                {
                    float depthX = Math.Min(collider.Right - obj.collider.Left, obj.collider.Right - collider.Left);
                    float depthY = Math.Min(collider.Bottom - obj.collider.Top, obj.collider.Bottom - collider.Top);

                    if (depthX < depthY)
                    {
                        if (collider.Right > obj.collider.Left && collider.Left < obj.collider.Left)
                        {
                            position.X = obj.collider.Left - offset.X;
                            _direction.X *= -1;
                        }
                        else if (collider.Left < obj.collider.Right && collider.Right > obj.collider.Right)
                        {
                            position.X = obj.collider.Right + offset.X;
                            _direction.X *= -1;
                        }
                    }
                    else
                    {
                        if (collider.Bottom > obj.collider.Top && collider.Top < obj.collider.Top)
                        {
                            position.Y = obj.collider.Top - offset.Y;
                            _direction.Y *= -1;
                        }
                        else if (collider.Top < obj.collider.Bottom && collider.Bottom > obj.collider.Bottom)
                        {
                            position.Y = obj.collider.Bottom + offset.Y;
                            _direction.Y *= -1;
                        }
                    }
                    _direction = Vector2.Normalize(_direction);
                    obj.OnCollide(this);
                }
            }
        }
    }
}