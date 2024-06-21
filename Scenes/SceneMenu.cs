using CasseBrique;
using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;
using System;



namespace CasseBrique
{
    public class SceneMenu : Scene
    {
        GameController gc = ServicesLocator.Get<GameController>();

        public override void Load()
        {
            IScreenService screen = ServicesLocator.Get<IScreenService>();
            AddGameObject(new Button("New Game", "Start New Game", new Vector2(screen.Center.X, screen.Center.Y - 100), this));

            if (gc.GameStarted)
            {
                AddGameObject(new Button("Game", "Continue Game", new Vector2(screen.Center.X, screen.Center.Y), this));
            }

            AddGameObject(new Button("VolumeUp", "Turn up volume", new Vector2(screen.Center.X-100, screen.Center.Y + 300), this));
            AddGameObject(new Button("VolumeDown", "Turn down volume", new Vector2(screen.Center.X + 100, screen.Center.Y + 300), this));
        }

        public override void Update(float dt)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                ServicesLocator.Get<IScenesManager>().ChangeScene<SceneGame>();
            if (Keyboard.GetState().IsKeyDown(Keys.V))
                ServicesLocator.Get<IScenesManager>().ChangeScene<SceneVillage>();
            base.Update(dt);
        }
       


    }
}
