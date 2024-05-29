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
        private float _speed;
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
            _speed = 400f;
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                Shoot(new Vector2(-2, -1));

            if (isShot)
            {
                Move(dt);
                Bounce();
            }
            else
            {
                // position = pos - size * 0.5f; -find a way to get paddle position
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
            if (position.X > _bounds.Right - radius)
            {
                position.X = _bounds.Right - radius;
                _direction.X *= -1;
            }
            else if (position.X < _bounds.Left + radius)
            {
                position.X = _bounds.Left + radius;
                _direction.X *= -1;
            }
            if (position.Y > _bounds.Bottom - radius)
            {
                position.Y = _bounds.Bottom - radius;
                _direction.Y *= -1;
            }
            else if (position.Y < _bounds.Top + radius)
            {
                position.Y = _bounds.Top + radius;
                _direction.Y *= -1;
            }
        }



        public void CheckBricks(List<Brique> briques)
        {
            foreach (var brick in briques)
            {
                ServicesLocator.Get<UtilsService>().CheckBallBrickCollision(this, brick);
            }
        }

        public void PadRebound(SpriteGameObject pad) // a revoir pour rajouter les autres cotés!
        {
            if (position.Y + size.Y >= pad.position.Y) // si ma balle est sous mon objet // a remplacer par entre le haut et le bas de mon objet
            {
                if (position.X + size.X >= pad.position.X) // si ma balle est à droite de mon objet
                {
                    if (position.X <= pad.position.X + pad.size.X) // si ma alors fait: verifie si X de me balle est inférieur à la position de mon objet plus sa taille
                    {
                        position.Y = pad.position.Y - size.Y;
                        _direction.Y = -_direction.Y;   // rebond horizontal vers le haut
                    }
                }
            }
        }

    }

}