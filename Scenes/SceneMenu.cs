using CasseBrique;
using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;


namespace CasseBrique
{
    public class SceneMenu : Scene
    {
       
        public override void Load()
        {
            IScreenService screen = ServicesLocator.Get<IScreenService>();
       
            AddGameObject(new Button("Default", new Vector2(screen.Center.X, screen.Bottom - 30),this));
            
        }

        public override void Update(float dt)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
                ServicesLocator.Get<IScenesManager>().Load<SceneGame>();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.V))
                ServicesLocator.Get<IScenesManager>().Load<SceneVillage>();
            base.Update(dt);
        }
        // Ancienement dans le draw: sb.DrawString(font, "Scene Menu, Appuyez sur V pour aller au village, et Entrer pour le jeu", Vector2.One, Color.AliceBlue);

    }
}
