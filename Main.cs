
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CasseBrique
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Ball MyBall;
        public Point _screenSize;
        private Paddle MyPaddle;
        public Vector2 _MyPaddlePos;
        public Vector2 _MyPaddleSize;
        private float shootingspeed = 300f;
        private float paddleMovespeed = 400f;

        


        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _screenSize = new Point(1240,720);
            _graphics.PreferredBackBufferWidth = _screenSize.X;
            _graphics.PreferredBackBufferHeight = _screenSize.Y;
            _graphics.ApplyChanges();
            
  
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            MyBall = new Ball(Content.Load<Texture2D>("Ball"), Vector2.One);
            MyPaddle = new Paddle(Content.Load<Texture2D>("Paddle"), new Vector2(10,_screenSize.Y-30));
            _MyPaddleSize = MyPaddle.size;

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                MyBall.Shoot(new Vector2(2,1), shootingspeed);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D))
                MyPaddle.Move(new Vector2(1, 0), paddleMovespeed);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                MyPaddle.Move(new Vector2(-1, 0), paddleMovespeed);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; //"casté" ma variable: forcé un type
            MyPaddle.Update(dt);
            MyPaddle.CheckBounds(_screenSize);
            _MyPaddlePos = MyPaddle.position;
            Console.WriteLine(_MyPaddlePos);
            
            MyBall.Update(dt);
            MyBall.CheckBounds(_screenSize);
            MyBall.Rebound(_MyPaddlePos, _MyPaddleSize); //marche PAS!!!!

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            MyBall.Draw(_spriteBatch);
            MyPaddle.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
