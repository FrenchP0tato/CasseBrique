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
        private GameController gc = ServicesLocator.Get<GameController>();


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
                    
                    isFree = true;

                    if (this.target == "Game")  // pas réussi à le faire avec un Paramètre directement dans la methode load
                   {
                        ServicesLocator.Get<IScenesManager>().ChangeScene<SceneGame>();
                    }
                    if (this.target == "Menu")
                    {
                        ServicesLocator.Get<IScenesManager>().ChangeScene<SceneMenu>();
                    }
                    if (this.target == "Village")
                    {
                        ServicesLocator.Get<IScenesManager>().ChangeScene<SceneVillage>();
                    }
                    if (this.target== "New Game")
                    {
                        gc.Reset();
                        ServicesLocator.Get<IScenesManager>().ChangeScene<SceneGame>();
                    }
                    if (this.target == "Retry")
                    {
                        if (gc.MoveToNextDay()) ServicesLocator.Get<IScenesManager>().ChangeScene<SceneGame>();
                        else gc.GameOver();
                    }
                    if (this.target == "NextLevel")
                    {
                        gc.MoveToNextLevel();
                        gc.MoveToNextDay();
                        ServicesLocator.Get<IScenesManager>().ChangeScene<SceneGame>();
                    }
                }
                
            }
            oldMouseState = NewMouseState;
        }

        public override void Draw(SpriteBatch sb)
        {
            
            base.Draw(sb);
            Vector2 offset = TextOffset(font, text, position);
        
            sb.DrawString(font, text, offset, Color.Black);

        }

        private Vector2 TextOffset(SpriteFont font, string text, Vector2 origin)
        {
            Vector2 textSize = font.MeasureString(text);
            Vector2 offset = new Vector2(origin.X - textSize.X / 2, origin.Y-textSize.Y/2);

            return offset;
        }

    }
}
