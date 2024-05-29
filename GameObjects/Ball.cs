using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CasseBrique.GameObjects
{

    public class Ball : SpriteGameObject
    {
        public Vector2 _direction;
        private float _speed = 400f;
        private Rectangle _bounds;
        public float radius => texture.Width * 0.5f;

        public bool isShot = false;


        public Ball(Rectangle bounds, Vector2 direction, Scene root) : base(root)
        {
            texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("BallBlue");
            position = ServicesLocator.Get<IScreenService>().Center; //x et y de l'origine, pas centrée
            color = Color.Red;
            _bounds = bounds;
            _direction = direction;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;
        }

        public void Shoot(Vector2 direction)
        {
            if (isShot == false)
            {
                isShot = true;
                direction.Normalize();
                Console.WriteLine("i shoot");
            }

        }

        public override void Update(float dt)
        {
            if (isShot)
            {
                Move(dt);
                Bounce();
                CheckOutOfbounds();
                //add Method for colliders
            }
            else
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                    Shoot(new Vector2(-2, -1));
                var pad = root.GetGameObjects<Paddle>()[0];
                position=new Vector2(pad.position.X, pad.position.Y);
            }
        }

        public void Move(float dt)
        {
            _direction.Normalize();
            Vector2 velocity = _direction * _speed;
            position += velocity * dt;
        }

        private void Bounce()
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
            _direction.Normalize();
        }

        private void CheckOutOfbounds()
        {
        if (position.Y > _bounds.Bottom - offset.Y)
            {
                isShot = false;
                // add looselife
            }
        }   

}