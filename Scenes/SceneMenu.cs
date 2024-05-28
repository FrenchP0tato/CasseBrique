using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace CasseBrique
{
    public class SceneMenu : Scene
    {
        MouseState oldMouseState;
        private SpriteFont font;
        private Button GameButton;
        private bool LeftClick;

        public override void Load()
        {
            base.Load(); 
            GameButton = new Button(new Vector2(ServicesLocator.Get<ScreenService>().center.X, ServicesLocator.Get<ScreenService>().center.Y),"Default");
            
            
        }


        public override void Update(float dt)
        {
            LeftClick = false;
            MouseState NewMouseState = Mouse.GetState();
            if (ServicesLocator.Get<UtilsService>().CheckMouseClicks(oldMouseState, NewMouseState) == true)
            {
                //Console.WriteLine("Bouton Souris dans mon menu!");
                 LeftClick=true;
                    
            }
            oldMouseState = NewMouseState;

            if (LeftClick == true)
            {
                if (ServicesLocator.Get<UtilsService>().CheckObjectClick(NewMouseState, GameButton.Position, GameButton.Size) == true)
                {
                    Console.WriteLine("Button Cliqué");
                }
                else 
                { Console.WriteLine("clicking nowhere"); }
                LeftClick = false;
            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.V))
                ServicesLocator.Get<IScenesManager>().LoadScene("Village");

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
                ServicesLocator.Get<IScenesManager>().LoadScene("Game");


            base.Update(dt);
        }

        public override void Draw(SpriteBatch sb)
        {
            font = ServicesLocator.Get<IAssetsService>().Get<SpriteFont>("BasicText");
            sb.DrawString(font, "Scene Menu, Appuyez sur V pour aller au village, et Entrer pour le jeu", Vector2.One, Color.AliceBlue);
            
           
            GameButton.Draw(sb);
            
            base.Draw(sb);
        }
    }
}
