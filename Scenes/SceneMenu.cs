using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace CasseBrique
{
    public class SceneMenu : Scene
    {
        MouseState oldMouseState;
        SpriteFont font;
       


        public override void Update(float dt)
        {
            MouseState NewMouseState = Mouse.GetState();
            if (ServicesLocator.Get<UtilsService>().CheckMouseClicks(oldMouseState, NewMouseState) == true)
            {
                Console.WriteLine("Bouton Souris dans mon menu!");
                    }
            oldMouseState = NewMouseState;

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
            base.Draw(sb);
        }
    }
}
