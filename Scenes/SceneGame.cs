using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CasseBrique
{
    public class SceneGame : Scene
    {

        List<Brique> _BriqueList; // pas reussi à faire gérer la liste par ma classe brique: je n'arrivais pas à l'appeler. Ai essayé dans constructeur mais pas réussi


        public override void Load()
        {
            IScreenService screen =ServicesLocator.Get<IScreenService>();
            
            AddGameObject(new Ball(screen.Bounds,Vector2.One,this)); // to redo: manage position on the Paddle to start with
            AddGameObject(new Paddle(screen.Bounds,new Vector2(screen.Center.X,screen.Bottom-30),this));

            _BriqueList = GenerateBricks(1, 10, 50, 50, new Vector2(100, 50));
            
            AddGameObject(new Brique(new Vector2(400, 400), Color.Gray,this));

        }


        public override void Update(float dt)
         {
             if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.M))
                ServicesLocator.Get<IScenesManager>().Load<SceneMenu>();
             base.Update(dt);
            }

        private List<Brique> GenerateBricks(int rows, int columns, int brickWidth, int brickHeight, Vector2 Startposition) // arrive pas à mettre dans bricks. 
        {
            var bricksList = new List<Brique>();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Vector2 position = new Vector2(Startposition.X + col * (brickWidth + 20), Startposition.Y + row * (brickHeight + 20));
                    bricksList.Add(new Brique(position, Color.Gray,this));
                }
            }

            return bricksList;
        }


    }
}
