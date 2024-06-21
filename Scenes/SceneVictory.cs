using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;

namespace CasseBrique.Scenes
{
    public class SceneVictory : Scene
    {
        private GameController gc = ServicesLocator.Get<GameController>();
        private Button ScoreDays;
        private Button Victory;

        public override void Load()
        {
            IScreenService screen = ServicesLocator.Get<IScreenService>();
            Victory = new Button("Null", "VICTORY!", new Vector2(screen.Center.X, 100), this);
            AddGameObject(Victory);

            ScoreDays = new Button("Null", $"You made it to the promised land in {gc.days} days ", new Vector2(screen.Center.X, screen.Center.Y - 150), this);
            AddGameObject(ScoreDays);

            AddGameObject(new Button("New Game", "Play again to improve your score", new Vector2(screen.Center.X, screen.Bottom - 200), this));

        }

    }
}
