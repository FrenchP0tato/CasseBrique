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
       
            AddGameObject(new Button("Game", "Continue Game", new Vector2(screen.Center.X, screen.Center.Y-40),this));
            AddGameObject(new Button("Village", "See your Raft", new Vector2(screen.Center.X, screen.Center.Y+40), this));
            AddGameObject(new Button("New Game", "Restart", new Vector2(screen.Center.X, screen.Center.Y + 100), this));
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
