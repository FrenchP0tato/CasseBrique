
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace CasseBrique.GameObjects
{

    public class Ball : SpriteGameObject
    {
        private Vector2 _direction;
        private float _speed = 400f;
        private Vector2 _velocity;
        private Rectangle _bounds;
        private float radius => texture.Width * 0.5f;

        private bool isShot = false;
        private int _damage;

        public int Damage
        { get
            { return _damage; }
        }

        public Rectangle Collider
        {
            get
            {
                return new Rectangle((int)(position.X - offset.X), (int)(position.Y - offset.Y), texture.Width, texture.Height);
            }

        }


        public Ball(Rectangle bounds, Scene root) : base(root)
        {
            texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("BallBlue");
            color = Color.Red;
            _bounds = bounds;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;
            tag = "Ball";
            _damage = 1;
            
        }

        public void Shoot(Vector2 direction)
        {
            if (isShot == false)
            {
                _direction=direction;
                isShot = true;
                Console.WriteLine("i shoot");
            }

        }

        public override void Update(float dt)
        {
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
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                    Shoot(new Vector2(-1, -1));
                var pad = root.GetGameObjects<Paddle>()[0];
                position = new Vector2(pad.position.X-offset.X*.5f, pad.position.Y-texture.Height);
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
                // add looselife
            }
        }

        private void ResolveCollisionsWithObjects<T>() where T : SpriteGameObject
        {
            var objects = root.GetGameObjects<T>();
            foreach (SpriteGameObject obj in objects)
            {
                if (!obj.enable) continue;
                if (Collider.Intersects(obj.collider))
                {
                    float depthX = Math.Min(Collider.Right - obj.collider.Left, obj.collider.Right - Collider.Left);
                    float depthY = Math.Min(Collider.Bottom - obj.collider.Top, obj.collider.Bottom - Collider.Top);

                    if (depthX < depthY)
                    {
                        if (Collider.Right > obj.collider.Left && Collider.Left < obj.collider.Left)
                        {
                            position.X = obj.collider.Left - offset.X;
                            _direction.X *= -1;
                        }
                        else if (Collider.Left < obj.collider.Right && Collider.Right > obj.collider.Right)
                        {
                            position.X = obj.collider.Right + offset.X;
                            _direction.X *= -1;
                        }
                    }
                    else
                    {
                        if (Collider.Bottom > obj.collider.Top && Collider.Top < obj.collider.Top)
                        {
                            position.Y = obj.collider.Top - offset.Y;
                            _direction.Y *= -1;
                        }
                        else if (Collider.Top < obj.collider.Bottom && Collider.Bottom > obj.collider.Bottom)
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