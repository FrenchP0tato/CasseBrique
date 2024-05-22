
using CasseBrique.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CasseBrique
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AssetsService _assetsServices;
        private ScreenService _screenService;

        private Ball MyBall;
        private Paddle MyPaddle;
        private readonly float shootingspeed = 300f;
        private readonly float paddleMovespeed = 400f;



        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {    //enregistrement des services
            ServicesLocator.Register<ContentManager>(Content); 
            ServicesLocator.Register<GraphicsDeviceManager>(_graphics);
            _assetsServices = new AssetsService();
            _screenService = new ScreenService(1240,720);            
  
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _assetsServices.Load<Texture2D>("BallBlue");
            MyBall = new Ball(_screenService.center) ;

            _assetsServices.Load<Texture2D>("Paddle");
            MyPaddle = new Paddle(new Vector2(_screenService.center.X,_screenService.bottom-30));

        }

        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; //"casté" ma variable: forcé un type

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                MyBall.Shoot(new Vector2(2,1), shootingspeed);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.D))
                MyPaddle.Move(new Vector2(1, 0), paddleMovespeed,dt);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Q))
                MyPaddle.Move(new Vector2(-1, 0), paddleMovespeed,dt);

            
            MyBall.Update(dt);
            MyBall.Rebound(MyPaddle.Position, MyPaddle.Size);

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
