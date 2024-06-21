using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;

namespace CasseBrique.Scenes
{
    public class SceneGameOver : Scene
    {
        private GameController gc = ServicesLocator.Get<GameController>();
        private Button ScoreDays;
        private Button ScoreLevels;
        private Button GameOver;

        public override void Load()
        {
            IScreenService screen = ServicesLocator.Get<IScreenService>();
            GameOver = new Button("Null", "Game Over", new Vector2(screen.Center.X, 100), this);
            AddGameObject(GameOver);


            ScoreDays = new Button("Null", $"You have survived {gc.days} days ", new Vector2(screen.Center.X, screen.Center.Y - 150), this);
            AddGameObject(ScoreDays);

            ScoreLevels = new Button("Null", $"And reached level {gc.currentLevel} ", new Vector2(screen.Center.X, screen.Center.Y), this);
            AddGameObject(ScoreLevels);

            AddGameObject(new Button("New Game", "Try Again", new Vector2(screen.Center.X, screen.Bottom-200), this));

        }

    }
}
