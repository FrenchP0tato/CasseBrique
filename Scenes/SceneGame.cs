using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using CasseBrique.Services;
using Microsoft.Xna.Framework.Media;
using CasseBrique.Scenes;

/* pb to fix:
- Improve button texture (transparent background)
- Change Font
- test bricks.clear more

improvements:
- Make Button texture flexible like Pad
- add level specific musics
- More varied Bonus, incl. Ball Directional choice when shooting(+), extra ball (++)
- more levels
- 3 colors for ball and pads 
 */


namespace CasseBrique
{
    public class SceneGame : Scene
    {
        bool isPaused = false;
        public List<Brique> currentBricksList;
        private Paddle myPaddle;
        public Ball myBall;
        GameController gc = ServicesLocator.Get<GameController>();
        IScenesManager sc = ServicesLocator.Get<IScenesManager>();

        public override void Load() 
        {
            IScreenService screen = ServicesLocator.Get<IScreenService>();
            
            Rectangle bounds = new Rectangle(0, 70, 1280, 650);

            string Level = gc.GetLevel();

            Song GameSong = ServicesLocator.Get<IAssetsService>().Get<Song>("CoolSong");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(GameSong);
            MediaPlayer.Volume = 0.05f;

            myPaddle = new Paddle(bounds, this);
            myBall = new Ball(bounds, this);
            currentBricksList = new List<Brique>();

            AddGameObject(new Background("LevelBackground", ServicesLocator.Get<IAssetsService>().Get<Texture2D>(Level), this));
            AddGameObject(new Interface(this));
            AddGameObject(myPaddle);
            AddGameObject(myBall);
            gc.GameStarted=true;

            if (gc.LevelStarted)
            {
                currentBricksList = gc.CurrentBricksList;
                foreach (Brique b in currentBricksList)
                {
                    AddGameObject(b);
                }
            }
            else
            {
                AddNewBricks(bounds);
                gc.LevelStarted = true;
                if (gc.currentLevel==1) gc.GainResource("Food", 4);
                // for testing only, to REMOVE before release
                if (gc.currentLevel == 1) gc.GainResource("Wood", 20);
                if (gc.currentLevel == 1) gc.GainResource("Gold", 20);
                if (gc.currentLevel == 1) gc.GainResource("Science", 20);
            }
        }



        public override void Unload()
        {
            gc.CurrentBricksList= GetGameObjects<Brique>();
            base.Unload();
        }

        private void AddNewBricks(Rectangle bounds)
        {
            var brickLayout = gc.GetBricksLayout();
            var brickTexture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyBrick");
            int columns = brickLayout.GetLength(0); 
            int rows = brickLayout.GetLength(1);

            int spaceBetweenBricks = 3;
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
            
            
            KeyboardService.GetState();

            // for testing only - to delete before release
            if (KeyboardService.HasBeenPressed(Keys.Enter))
            {
                gc.MoveToNextLevel();
                sc.ChangeScene<SceneGame>();
            }
            if (KeyboardService.HasBeenPressed(Keys.Back))
            {
                bricks.Clear();
            }



            if (KeyboardService.HasBeenPressed(Keys.P))
            {
                isPaused=!isPaused;
            }

            if (isPaused) return;
            
            

            if (KeyboardService.HasBeenPressed(Keys.M)) 
            {
                if (myBall.isShot) return;
                sc.ChangeScene<SceneMenu>(); 
            }
            
            if (bricks.Count == 0)
            { if (gc.currentLevel == gc.maxLevel) sc.ChangeScene<SceneVictory>();
               else sc.ChangeScene<SceneVillage>();
            }

             base.Update(dt);
            }


    }
}
