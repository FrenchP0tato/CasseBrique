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


        public override void Load()
        {

            textureBackGround = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("VillageBackground");
            backGround = new Background("VillageBackground", textureBackGround, this);
            AddGameObject(backGround);
           
        }


        public override void Update(float dt)
        {
            var gc = ServicesLocator.Get<GameController>();
            var sc = ServicesLocator.Get<IScenesManager>();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.M))
                sc.Load<SceneMenu>();
        }
    }
}