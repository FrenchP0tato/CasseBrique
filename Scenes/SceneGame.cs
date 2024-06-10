using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using CasseBrique.Services;
using Microsoft.Xna.Framework.Media;



namespace CasseBrique
{
    public class SceneGame : Scene
    {
        bool isPaused = false;

        public override void Load() //peut modifier pour passer des paramètres d'une scène à l'autre! Va devoir modifier pour l'etat du niveau...
        {
            IScreenService screen =ServicesLocator.Get<IScreenService>();

            Rectangle bounds = new Rectangle(0, 70, 1280, 650);

            string Level = ServicesLocator.Get<GameController>().GetLevel();

            Song GameSong=ServicesLocator.Get<IAssetsService>().Get<Song>("CoolSong");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(GameSong);
            MediaPlayer.Volume = 0.1f;


            AddGameObject(new Background("LevelBackground", ServicesLocator.Get<IAssetsService>().Get<Texture2D>(Level), this));
            AddGameObject(new Interface(this));
            AddGameObject(new Ball(bounds,this)); 
            AddGameObject(new Paddle(bounds,this));
            AddBricks(bounds);
            ResourceData.PopulateData();
        }

        private void AddBricks(Rectangle bounds)
        {
            var brickLayout = ServicesLocator.Get<GameController>().GetBricksLayout();
            var brickTexture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyBrick");
            int columns = brickLayout.GetLength(0); 
            int rows = brickLayout.GetLength(1);

            int spaceBetweenBricks = 5;
            int verticaloffset = 10;

            float totalWidth =(columns)*(brickTexture.Width+spaceBetweenBricks)-spaceBetweenBricks;
            float offsetX = (bounds.Width - totalWidth)*0.5f;

            Color color = Color.Black;
            string type="BasicBrick";

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (brickLayout[column, row] > 0)
                    {

                        float x = bounds.X + offsetX + column * (brickTexture.Width + spaceBetweenBricks);
                        float y = bounds.Y + verticaloffset + row * (brickTexture.Height + spaceBetweenBricks);
                        
                        if (brickLayout[column, row] == 1) type = "Stone"; 
                        if (brickLayout[column, row] == 2) type = "Wood"; 
                        if (brickLayout[column, row] == 3) type = "Food";
                        if (brickLayout[column, row] == 4) type = "Gold";
                        if (brickLayout[column, row] == 5) type = "Science"; 

                        Brique brick = new Brique(type, this);
                        brick.position = new Vector2(x, y);
                        AddGameObject(brick);
                    }
                }
            }
        }


        public override void Update(float dt)
         {
            var bricks = GetGameObjects<Brique>();
            var gc = ServicesLocator.Get<GameController>();
            var sc = ServicesLocator.Get<IScenesManager>();



            KeyboardService.GetState();

            if (KeyboardService.HasBeenPressed(Keys.P))
            {
                isPaused=!isPaused;
            }

            if (isPaused) return;
            
            if (KeyboardService.HasBeenPressed(Keys.Enter))
            {
                gc.MoveToNextLevel();
                sc.Load<SceneGame>();
            }

            if (KeyboardService.HasBeenPressed(Keys.M))
            {
                sc.Load<SceneMenu>(); // must find a way that this doesn't unload the scene! 
            }
            
            if (bricks.Count == 0)
            {
                gc.MoveToNextLevel();
                sc.Load<SceneGame>();
            }
             base.Update(dt);
            }


    }
}
