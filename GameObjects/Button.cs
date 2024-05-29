using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CasseBrique.GameObjects
{
    internal class Button : SpriteGameObject
    {
        MouseState oldMouseState;
        private SpriteFont font;
        private bool LeftClick;
        private bool CheckClick = false;
        private string text;

        public Vector2 Position
        {
            get { return position; }
            private set { position = value; } //accesseur
        }

        public Vector2 Size
        {
            get { return size; }
            private set { size = value; }
        }

        public Button(string buttontype, Vector2 Pos, Scene root) : base(root)
        {
            font = ServicesLocator.Get<IAssetsService>().Get<SpriteFont>("BasicText");
            text = "Start Game";
            

            if (buttontype == "Default")
            {
                texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("buttonDefault");
            }
            else if (buttontype == "Selected")
            {
                texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("buttonSelected");
            }
            position.X = Pos.X - texture.Width * 0.5f;
            position.Y = Pos.Y - texture.Height * 0.5f;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;
        }

        public override void Update(float dt)
        {
            LeftClick = false;
            MouseState NewMouseState = Mouse.GetState();
            if (ServicesLocator.Get<MouseService>().CheckMouseClicks(oldMouseState, NewMouseState) == true)
            {
                Console.WriteLine("Cliqué I don't know where!");
                LeftClick = true;

            }
            oldMouseState = NewMouseState;


            if (LeftClick == true)
            {
                CheckClick = ServicesLocator.Get<MouseService>().CheckObjectClick(NewMouseState, position, size);
                if (CheckClick)
                {
                    Console.WriteLine("Button Cliqué");
                }
                else
                { Console.WriteLine("clicking nowhere"); }
                LeftClick = false;
            }
        }
    }
}
