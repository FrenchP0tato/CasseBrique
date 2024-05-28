
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CasseBrique
{

    public class Ball
    {
        private Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        private Color color = Color.Red;
        public Vector2 size;
        public float radius;
        public bool isShot=false;

        public Ball(Vector2 StartingPosition)
        {
            this.texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("BallBlue");
            this.velocity = Vector2.Zero;
            this.size = new Vector2(22, 22);
            this.position = StartingPosition-size*0.5f; //x et y de l'origine, pas centrée
            this.radius = 11;
        }

        public void Shoot(Vector2 direction, float speed)
        {
            if (isShot == false)
            {
                isShot = true;
                direction.Normalize();
                velocity = speed * direction;
                Console.WriteLine("i shoot");
            }
            
        }

        public void Update(float dt, Vector2 pos)
        {
            if (isShot)
            {
                position += velocity * dt;
                CheckBounds(ServicesLocator.Get<ScreenService>().botRight);
            }
            else
            {
                position = pos - size * 0.5f;
            }
         }

        public void Draw(SpriteBatch sprb)
        {
            sprb.Draw(texture, position, color);
        }

        public void CheckBricks(List<Brique> briques)
        {
            foreach (var brick in briques)
            {

                ServicesLocator.Get<UtilsService>().CheckBallBrickCollision(this, brick);
               //    {  velocity = -velocity; }
            }
        }

        public void Rebound(Vector2 objectPos, Vector2 objectsize) // a revoir pour rajouter les autres cotés!
        {
            if (position.Y + size.Y >= objectPos.Y) // si ma balle est sous mon objet // a remplacer par entre le haut et le bas de mon objet
            {
                if (position.X + size.X >= objectPos.X) // si ma balle est à droite de mon objet
                {

                    if (position.X <= objectPos.X + objectsize.X) // si ma alors fait: verifie si X de me balle est inférieur à la position de mon objet plus sa taille
                    {
                        position.Y = objectPos.Y - size.Y;
                        velocity.Y = -velocity.Y;   // rebond horizontal vers le haut
                    }
                }
            }
        }

        public void PadRebound(Paddle pad) // a revoir pour rajouter les autres cotés!
        {
            if (position.Y + size.Y >= pad.Position.Y) // si ma balle est sous mon objet // a remplacer par entre le haut et le bas de mon objet
            {
                if (position.X + size.X >= pad.Position.X) // si ma balle est à droite de mon objet
                {
                    if (position.X <= pad.Position.X + pad.Size.X) // si ma alors fait: verifie si X de me balle est inférieur à la position de mon objet plus sa taille
                    {
                        position.Y = pad.Position.Y - size.Y;
                        velocity.Y = -velocity.Y;   // rebond horizontal vers le haut
                    }
                }
            }
        }

        public void BrickRebound(Brique b) // a revoir pour rajouter les autres cotés!
        {
            if (position.Y <= b.Position.Y+b.Size.Y) // si ma balle est sous mon objet // a remplacer par entre le haut et le bas de mon objet
            { if (position.Y>=b.Position.Y-size.Y) // si ma balle est au dessus de mon objet
                { if (position.X >= b.Position.X-size.X) // si ma balle est à droite de l'origine de mon objet
                    { if (position.X <= b.Position.X+b.Size.X) // si ma balle est à gauche du bout de mon objet
                        {
                            if (position.Y<= b.Position.Y)
                            {
                                position.Y = b.Position.Y;
                                velocity.Y = -velocity.Y; 
                            }
                            if (position.Y >=b.Position.Y)
                            { 
                                position.Y=b.Position.Y;
                                velocity.Y = -velocity.Y;
                            }
                            if (position.X >= b.Position.X)
                            {
                                position.X =b.Position.X;
                                velocity.X = -velocity.X;
                            }
                            if (position.X <=b.Position.X)
                            {
                                position.X=b.Position.X;
                                velocity.X = -velocity.X;
                            }
                        }
                    }

                }
                
            }
        }

        public void CheckBounds(Vector2 _screenSize)
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
            if (position.Y + size.Y >= _screenSize.Y) // rebond en bas // remplacer par perte d'une vie!! 
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