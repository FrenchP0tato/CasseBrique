using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;


namespace CasseBrique
{
    internal class MouseService
    {
        public MouseService() 
        {
            ServicesLocator.Register<MouseService>(this);
        }

        public bool CheckMouseClicks(MouseState oldMouseState, MouseState NewMouseState)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
            { ServicesLocator.Get<IScenesManager>().Load<SceneGame>(); }

            if (NewMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            { return true; }
            else return false;
        }

        public bool CheckObjectClick(MouseState NewMouseState, Vector2 objectPos, Vector2 objectSize)
        {
            if (NewMouseState.X >= objectPos.X &&
                NewMouseState.Y >= objectPos.Y &&
                NewMouseState.X <= objectPos.X + objectSize.X &&
                NewMouseState.X <= objectPos.Y + objectSize.Y)
            {
                Console.WriteLine("object cliqué");
                return true;
            }
            else
                Console.WriteLine("rien cliqué");
                    return false;
        }
    }
}


