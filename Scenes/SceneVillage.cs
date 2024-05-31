using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;



namespace CasseBrique
{
    public class SceneVillage : Scene
    {
        public override void Update(float dt)
        {
            var gc = ServicesLocator.Get<GameController>();
            var sc = ServicesLocator.Get<IScenesManager>();

            AddGameObject(new Background("MenuBackground", this));

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.M))
                sc.Load<SceneMenu>();
        }
    }
}