using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Threading;

namespace CasseBrique.GameObjects
{
    internal class Button : SpriteGameObject
    {
        protected MouseState oldMouseState;
        protected SpriteFont font;
        protected bool checkClick = false;
        protected string text;
        protected Texture2D highlightTexture;
        protected Texture2D basicTexture;
        protected String target;
        protected GameController gc = ServicesLocator.Get<GameController>();


        public Button(String pTarget, String pText, Vector2 pPosition, Scene pRoot) : base(pRoot)
        {
            font = ServicesLocator.Get<IAssetsService>().Get<SpriteFont>("BasicText");
            text = pText;
            target = pTarget;
            basicTexture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("buttonDefault");
            
            highlightTexture= ServicesLocator.Get<IAssetsService>().Get<Texture2D>("buttonSelected");
            texture = highlightTexture;
            position.X = pPosition.X;
            position.Y = pPosition.Y;
            size.X = texture.Width;
            size.Y = texture.Height;
            offset = size * 0.5f;

            if (this.target == "null") { texture = basicTexture; }
        }

        public override void Update(float dt)
        {
            MouseState NewMouseState = Mouse.GetState();
            if (ServicesLocator.Get<MouseService>().CheckMouseClicks(oldMouseState, NewMouseState) == true) 
            {
                checkClick = ServicesLocator.Get<MouseService>().CheckObjectClick(NewMouseState, position, offset);
                if (checkClick)
                {
                    if (this.target == "Game")
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
                        if (gc.GameStarted) gc.Reset();
                        ServicesLocator.Get<IScenesManager>().ChangeScene<SceneGame>();
                    }
                    if (this.target == "Retry")
                    {
                        if  (gc.MoveToNextDay()) 
                        {
                            gc.currentLifes = gc.MaxLifes;
                            ServicesLocator.Get<IScenesManager>().ChangeScene<SceneGame>(); 
                        }
                    }
                    if (this.target == "Continue")
                    {
                            ServicesLocator.Get<IScenesManager>().ChangeScene<SceneGame>();
                    }
                    if (this.target == "NextLevel")
                    {
                        if (gc.MoveToNextDay())
                        { gc.MoveToNextLevel();
                            ServicesLocator.Get<IScenesManager>().ChangeScene<SceneGame>();
                        }
                 
                    }
                    if (this.target == "VolumeUp")
                    {
                        MediaPlayer.Volume = MediaPlayer.Volume + 0.10f;
                        Main.MasterVolume = Main.MasterVolume + 0.10f;

                    }
                    if (this.target == "VolumeDown")
                    {
                        MediaPlayer.Volume = MediaPlayer.Volume - 0.10f;
                        Main.MasterVolume = Main.MasterVolume - 0.10f;
                    }

                    if (this.target =="Quit")
                    {
                        
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

        protected Vector2 TextOffset(SpriteFont font, string text, Vector2 origin)
        {
            Vector2 textSize = font.MeasureString(text);
            Vector2 offset = new Vector2(origin.X - textSize.X / 2, origin.Y-textSize.Y/2);

            return offset;
        }

    }
}
