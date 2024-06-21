using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;

using Microsoft.Xna.Framework.Graphics;
using System.Reflection.Emit;
using System;



namespace CasseBrique
{
    public class SceneVillage : Scene
    {
        private GameController gc = ServicesLocator.Get<GameController>();

        public override void Load()
        {
            IScreenService screen = ServicesLocator.Get<IScreenService>();
            Rectangle bounds = new Rectangle(0, 70, 1280, 650);

            AddGameObject(new Background("BoatBackground", ServicesLocator.Get<IAssetsService>().Get<Texture2D>("VillageBackground"), this));
            AddGameObject(new Interface(this));

            AddGameObject(new Bonus("DamageUp", "Ball Damage UP", "Science",20, new Vector2(screen.Center.X-200, 300), this));
            AddGameObject(new Bonus("MaxLifes", "+1 Max Balls", "Gold", 20, new Vector2(screen.Center.X - 200, 350), this));
            AddGameObject(new Bonus("PadSizeUp", "Pad Size UP", "Wood", 20, new Vector2(screen.Center.X + 200, 300), this));
            AddGameObject(new Bonus("PadSpeedUp", "Pad Speed UP", "Stone", 20, new Vector2(screen.Center.X + 200, 350), this));


            AddGameObject(new Button("Menu", "Back to the Menu", new Vector2(100, screen.Bottom-50),this));

            
            AddGameObject(new Button("Continue", "Continue", new Vector2(screen.Center.X - 100, screen.Bottom - 50), this));

            if (gc.CurrentBricksList.Count > 0)
            {
                AddGameObject(new Button("Retry", $"Continue level (-{gc.FoodConsumption} food)", new Vector2(screen.Center.X - 100, screen.Bottom - 50), this));
            }
            
            AddGameObject(new Button("NextLevel", $"Move On (-{ gc.FoodConsumption } food)", new Vector2(screen.Center.X + 100, screen.Bottom - 50), this));

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