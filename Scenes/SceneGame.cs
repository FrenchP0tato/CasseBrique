using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CasseBrique
{
    public class SceneGame : Scene
    {
        public override void Load()
        {
            IScreenService screen =ServicesLocator.Get<IScreenService>();
            Rectangle bounds=screen.Bounds;
            AddGameObject(new Ball(bounds,this)); 
            AddGameObject(new Paddle(bounds,this));
            AddBricks(bounds);            
        }

        private void AddBricks(Rectangle bounds)
        {
            var brickLayout = ServicesLocator.Get<GameController>().GetBricksLayout();
            var brickTexture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("GreyBrick");
            int columns = brickLayout.GetLength(0); // vraiment je comprends pas pourquoi get Lenght de 0 ou 1 ca fait des colonnes ou des lignes...
            int rows = brickLayout.GetLength(1);

            int spaceBetweenBricks = 10;
            int verticaloffset = 10;

            float totalWidth =columns*(brickTexture.Width+spaceBetweenBricks)-spaceBetweenBricks;
            float offsetX = (bounds.Width - totalWidth) * .5f;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    float x =bounds.X+offsetX+column*(brickTexture.Width+spaceBetweenBricks);
                    float y = bounds.Y + verticaloffset + row * (brickTexture.Height + spaceBetweenBricks);

                    Brique brick = new Brique(Color.Gray,this);
                    brick.position= new Vector2(x,y);
                    AddGameObject(brick);
                }
            }
        }


        public override void Update(float dt)
         {
             if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.M))
                ServicesLocator.Get<IScenesManager>().Load<SceneMenu>();
             base.Update(dt);
            }
    }
}
