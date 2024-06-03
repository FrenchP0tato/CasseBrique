using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace CasseBrique.GameObjects
{
    internal class Button : SpriteGameObject
    {
        MouseState oldMouseState;
        private SpriteFont font;
        private bool checkClick = false;
        private string text;
        private Texture2D highlightTexture;
        private Texture2D basicTexture;
        private String target;
        


        public Button(String pTarget, String pText, Vector2 pPosition, Scene pRoot) : base(pRoot)
        {
            font = ServicesLocator.Get<IAssetsService>().Get<SpriteFont>("BasicText");
            text = pText;
            target = pTarget;
            basicTexture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("buttonDefault");
            texture=basicTexture;
            highlightTexture= ServicesLocator.Get<IAssetsService>().Get<Texture2D>("buttonSelected");
            position.X = pPosition.X;
            position.Y = pPosition.Y;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;
                       
        }

        public override void Update(float dt)
        {
            
            MouseState NewMouseState = Mouse.GetState();
            if (ServicesLocator.Get<MouseService>().CheckMouseClicks(oldMouseState, NewMouseState) == true)
            {
                checkClick = ServicesLocator.Get<MouseService>().CheckObjectClick(NewMouseState, position, offset);
                if (checkClick)
                {
                    Console.WriteLine("Object Cliqué");
                    isFree = true;

                    if (this.target == "Game")  // pas réussi à le faire avec un Paramètre directement dans la methode load
                   {
                        ServicesLocator.Get<IScenesManager>().Load<SceneGame>();
                    }
                    if (this.target == "Menu")
                    {
                        ServicesLocator.Get<IScenesManager>().Load<SceneMenu>();
                    }
                    if (this.target == "Village")
                    {
                        ServicesLocator.Get<IScenesManager>().Load<SceneVillage>();
                    }

                }
                else
                { Console.WriteLine("clicking nowhere"); }
            }
            oldMouseState = NewMouseState;
        }

        public override void Draw(SpriteBatch sb)
        {
            
            base.Draw(sb);
            Vector2 TextOffset = new Vector2 (30, offset.Y*.5f);
            sb.DrawString(font, text, position-TextOffset, Color.Black);
        }
    }
}
