using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Net.NetworkInformation;


namespace CasseBrique
{
    public class SceneVillage : Scene
    {
        MouseState oldMouseState;
        SpriteFont font;



        public override void Update(float dt)
        {
            MouseState NewMouseState = Mouse.GetState();
            if (ServicesLocator.Get<UtilsService>().CheckMouseClicks(oldMouseState, NewMouseState) == true)
            {
                Console.WriteLine("Bouton Souris dans mon Village!");
               
            }
            oldMouseState = NewMouseState;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.M))
                ServicesLocator.Get<IScenesManager>().LoadScene("Menu");

            base.Update(dt);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            font = ServicesLocator.Get<IAssetsService>().Get<SpriteFont>("BasicText");
            spriteBatch.DrawString(font, "Scene Village / Appuyez sur M pour aller au Menu", Vector2.One, Color.AliceBlue);
            base.Draw(spriteBatch);

            
        }

        
    }
}