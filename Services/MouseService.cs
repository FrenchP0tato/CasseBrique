using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;


namespace CasseBrique
{
    
    internal class MouseService
    {
        public MouseService() 
        {
            ServicesLocator.Register(this);
        }

        public bool CheckMouseClicks(MouseState oldMouseState, MouseState NewMouseState)
        {
            if (NewMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            { return true; }
            else return false;
        }

        public bool CheckObjectClick(MouseState NewMouseState, Vector2 objectPos, Vector2 offset)
        {
            if (NewMouseState.X >= objectPos.X-offset.X &&
                NewMouseState.Y >= objectPos.Y- offset.Y &&
                NewMouseState.X <= objectPos.X + offset.X &&
                NewMouseState.Y <= objectPos.Y + offset.Y)
                
            {
                return true;
            }
            else
            {
             return false;
            }
        }
    }
}


