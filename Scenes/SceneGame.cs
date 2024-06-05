using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using CasseBrique.Services;



namespace CasseBrique
{
    public class SceneGame : Scene
    {
        
        public override void Load() //peut modifier pour passer des paramètres d'une scène à l'autre! Va devoir modifier pour l'etat du niveau...
        {
            IScreenService screen =ServicesLocator.Get<IScreenService>();

            Rectangle bounds = new Rectangle(0, 70, 1280, 650);

            string Level = ServicesLocator.Get<GameController>().GetLevel();

            AddGameObject(new Background("LevelBackground", ServicesLocator.Get<IAssetsService>().Get<Texture2D>(Level), this));
            AddGameObject(new Interface(this));
            AddGameObject(new Ball(bounds,this)); 
            AddGameObject(new Paddle(bounds,this));
            AddBricks(bounds);           
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

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (brickLayout[column, row] > 0)
                    {

                        float x = bounds.X + offsetX + column * (brickTexture.Width + spaceBetweenBricks);
                        float y = bounds.Y + verticaloffset + row * (brickTexture.Height + spaceBetweenBricks);
                        
                        if (brickLayout[column, row] == 1) color = Color.Gray; // pierre // remplacer par resource type 
                        if (brickLayout[column, row] == 2) color = Color.SaddleBrown; // bois
                        if (brickLayout[column, row] == 3) color = Color.Green; // Nourriture
                        if (brickLayout[column, row] == 4) color = Color.Yellow; // Gold
                        if (brickLayout[column, row] == 5) color = Color.Purple; // Science

                        Brique brick = new Brique(color, this);
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
            if (KeyboardService.HasBeenPressed(Keys.Enter))
            {
                gc.MoveToNextLevel();
                sc.Load<SceneGame>();
            }


            if (Keyboard.GetState().IsKeyDown(Keys.M))
                sc.Load<SceneMenu>();

            
            if (bricks.Count == 0)
            {
                gc.MoveToNextLevel();
                sc.Load<SceneGame>();
            }
             base.Update(dt);
            }


    }
}
