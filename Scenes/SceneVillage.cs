using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework.Graphics;



namespace CasseBrique
{
    public class SceneVillage : Scene
    {
        private Texture2D textureBackGround;
        private Background backGround;
        private GameController gc = ServicesLocator.Get<GameController>();
       

        public override void Load()
        {
            IScreenService screen = ServicesLocator.Get<IScreenService>();
            

            textureBackGround = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("VillageBackground");
            backGround = new Background("VillageBackground", textureBackGround, this);
            AddGameObject(backGround);

            AddGameObject(new Button("Menu", "go back to the Menu", new Vector2(100, 35),this));
            AddGameObject(new Button("Retry", "Retry previous level", new Vector2(screen.Center.X-100, 35), this));
            AddGameObject(new Button("NextLevel", "Advance your journey!", new Vector2(screen.Center.X + 100, 35), this));

        }


        public override void Update(float dt)
        {
            var sc = ServicesLocator.Get<IScenesManager>();

            if (Keyboard.GetState().IsKeyDown(Keys.M))
                sc.ChangeScene<SceneMenu>();
            base.Update(dt);
        }
    }
}