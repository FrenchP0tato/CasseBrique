using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using CasseBrique.Services;
using Microsoft.Xna.Framework.Media;


// To do: Résolu le pb de conserver les briques. Est-ce propre?
// pb pour coder les changements de scale des sprite: e.g. pour mon paddle
// Coder les bonus / Scene village

namespace CasseBrique
{
    public class SceneGame : Scene
    {
        bool isPaused = false;
        public List<Brique> currentBricksList;
        public Paddle myPaddle;
        public Ball myBall;
        GameController gc = ServicesLocator.Get<GameController>();


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
            }

            

            // ici peut ajouter logiques pour modifier les valeurs de Mypaddle et MyBall!! 
        }

        public override void Unload()
        {
            var gc = ServicesLocator.Get<GameController>();
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
            var sc = ServicesLocator.Get<IScenesManager>();
            GameController gc = ServicesLocator.Get<GameController>();

            KeyboardService.GetState();

            if (KeyboardService.HasBeenPressed(Keys.P))
            {
                isPaused=!isPaused;
            }

            if (isPaused) return;
            
            if (KeyboardService.HasBeenPressed(Keys.Enter))
            {
                gc.MoveToNextLevel();
                sc.ChangeScene<SceneGame>();
            }

            if (KeyboardService.HasBeenPressed(Keys.M))
            {
                sc.ChangeScene<SceneMenu>(); // must find a way that this doesn't unload the scene! 
            }
            
            if (bricks.Count == 0)
            {
                gc.MoveToNextLevel();
                sc.ChangeScene<SceneGame>();
            }
             base.Update(dt);
            }


    }
}
