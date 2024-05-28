using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CasseBrique
{
    public class SceneGame : Scene
    {

        private Ball MyBall;
        private Paddle MyPaddle;
        private Brique BriqueTest;

        private readonly float shootingspeed = 300f;
        private readonly float paddleMovespeed = 400f;

        List<Brique> _BriqueList; // pas reussi à faire gérer la liste par ma classe brique: je n'arrivais pas à l'appeler. Ai essayé dans constructeur mais pas réussi


        public override void Load()
        {
            _BriqueList = GenerateBricks(1, 10, 50, 50, new Vector2(100, 50));
            MyPaddle = new Paddle(new Vector2(ServicesLocator.Get<ScreenService>().center.X, ServicesLocator.Get<ScreenService>().bottom - 30));

            MyBall = new Ball((MyPaddle.Position + MyPaddle.Size * 0.5f));

            BriqueTest = new Brique(new Vector2(400, 400), Color.Gray);

            base.Load();
        }


        public override void Update(float dt)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.M))
                ServicesLocator.Get<IScenesManager>().LoadScene("Menu");

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                MyBall.Shoot(new Vector2(-2, -1), shootingspeed);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D))
                MyPaddle.Move(new Vector2(1, 0), paddleMovespeed, dt);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                MyPaddle.Move(new Vector2(-1, 0), paddleMovespeed, dt);

            MyBall.Update(dt,MyPaddle.Position+MyPaddle.Size*0.5f);
            //MyBall.Rebound(MyPaddle.Position, MyPaddle.Size);
            MyBall.PadRebound(MyPaddle);
            MyBall.CheckBricks(_BriqueList);
            base.Update(dt);
        }

        public override void Draw(SpriteBatch sb)
        {

            MyBall.Draw(sb);
            MyPaddle.Draw(sb);
            BriqueTest.Draw(sb);

            foreach (var brick in _BriqueList)
            {
                brick.Draw(sb);
            }

            base.Draw(sb);
        }


        private List<Brique> GenerateBricks(int rows, int columns, int brickWidth, int brickHeight, Vector2 Startposition) // arrive pas à mettre dans bricks. 
        {
            var bricksList = new List<Brique>();

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Vector2 position = new Vector2(Startposition.X + col * (brickWidth + 20), Startposition.Y + row * (brickHeight + 20));
                    bricksList.Add(new Brique(position, Color.Gray));
                }
            }

            return bricksList;
        }


    }
}
