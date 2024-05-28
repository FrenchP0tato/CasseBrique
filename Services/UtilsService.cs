using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.CompilerServices;

namespace CasseBrique
{
    public class UtilsService
    {
        public UtilsService()
        {
            ServicesLocator.Register<UtilsService>(this);
        }


        public bool CheckRoundCollision(Vector2 ObjectPos, float ObjectRadius, Vector2 TargetPos, float TargetRadius)
        {
            float distance = Vector2.Distance(ObjectPos, TargetPos);
            return distance < (ObjectRadius + TargetRadius);
        }


        public void CheckBallBrickCollision(Ball ball, Brique brick) //remplacer par sprite? ou classe square objects
        {
            float brickWidth = brick.size.X;
            float brickHeight = brick.size.Y;

            float closestX = MathHelper.Clamp(ball.position.X, brick.position.X - brickWidth * 0.5f, brick.position.X + brickWidth * 0.5f);
            float closestY = MathHelper.Clamp(ball.position.Y, brick.position.Y - brickHeight * 0.5f, brick.position.Y + brickHeight * 0.5f);

            float distanceX = ball.position.X - closestX;
            float distanceY = ball.position.Y - closestY;

            float distanceSquared = distanceX * distanceX + distanceY * distanceY;

            if (distanceSquared < ball.radius * ball.radius) // collision! 
            {
                float overlapX = ball.radius - Math.Abs(distanceX);
                float overlapY=ball.radius - Math.Abs(distanceY);
                
                if (overlapX < overlapY) // check si colision laterale
                {
                    ball.velocity.X= -ball.velocity.X;
                }
                else
                {
                    ball.velocity.Y= -ball.velocity.Y; // check si collision horizontale
                }
            }
        }

        public bool CheckMouseClicks(MouseState oldMouseState, MouseState NewMouseState)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
                ServicesLocator.Get<IScenesManager>().LoadScene("Game");
            if (NewMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
            return true;
            }
            return false;
        }

        public bool CheckObjectClick(MouseState NewMouseState, Vector2 objectPos, Vector2 objectSize)
        {
            if (NewMouseState.X>=objectPos.X &&
                NewMouseState.Y>=objectPos.Y &&
                NewMouseState.X<=objectPos.X+objectSize.X &&
                NewMouseState.X<=objectPos.Y+objectSize.Y)
            {
                Console.WriteLine("image cliquée");
                return true;
            }
            else return false;
        }

    }
}
