using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// to do urgent:
// gerrer les colisions carrées!! 


namespace CasseBrique
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AssetsService _assetsServices;
        private ScreenService _screenService; //necessaires ou non? 
        private UtilsService _utilsService;
        private ScenesManager _scenesManager;
        private MouseService _mouseService;



        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {    //enregistrement des services
            
            _assetsServices = new AssetsService(Content);
            _screenService = new ScreenService(_graphics);
            _utilsService = new UtilsService();
            _mouseService = new MouseService();
            _scenesManager = new ScenesManager();

            _screenService.SetSize(1280, 720);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _assetsServices.Load<Texture2D>("CityLevela");
            _assetsServices.Load<Texture2D>("Paddle");
            _assetsServices.Load<Texture2D>("BallBlue");
            _assetsServices.Load<Texture2D>("GreyBrick");
            _assetsServices.Load<Texture2D>("buttonDefault");
            _assetsServices.Load<Texture2D>("buttonSelected");
            _assetsServices.Load<SpriteFont>("BasicText");


            _scenesManager.Load<SceneGame>();

        }

        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; //"casté" ma variable: forcé un type

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();  // pas réussi à bouger ca car exit utilise une class de game

            _scenesManager.Update(dt);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue); //comment la changer dans une scene? 

            _spriteBatch.Begin();
            _scenesManager.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
