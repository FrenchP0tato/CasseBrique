using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;



namespace CasseBrique
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AssetsService _assetsServices;
        private ScreenService _screenService; 
        private ScenesManager _scenesManager;
        public static float MasterVolume { get; set; }

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {    //enregistrement des services
            new GameController();
            _assetsServices=new AssetsService(Content);
            _screenService= new ScreenService(_graphics);
            new MouseService();
            _scenesManager = new ScenesManager();
            ResourceData.PopulateData();

            _screenService.SetSize(1280, 720);
            MasterVolume = 0.05f;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice); 
            
            //Textures
            _assetsServices.Load<Texture2D>("Paddle");
            _assetsServices.Load<Texture2D>("BallGrey");
            _assetsServices.Load<Texture2D>("GreyBrick");
            _assetsServices.Load<Texture2D>("GreyBrickDamaged");
            _assetsServices.Load<Texture2D>("buttonDefault");
            _assetsServices.Load<Texture2D>("buttonSelected");
            _assetsServices.Load<Texture2D>("GreenButton");
            _assetsServices.Load<Texture2D>("GreyButton");

            //Sons:
            _assetsServices.Load<Song>("CoolSong");
            _assetsServices.Load<SoundEffect>("ImpactPaddle");
            _assetsServices.Load<SoundEffect>("ImpactStone");
            _assetsServices.Load<SoundEffect>("ImpactWood");
            _assetsServices.Load<SoundEffect>("ImpactGrass");
            _assetsServices.Load<SoundEffect>("ImpactGold");
            _assetsServices.Load<SoundEffect>("plouf");


            //Autres Assets
            _assetsServices.Load<SpriteFont>("BasicText");
            _assetsServices.Load<Texture2D>("VillageBackground");

            for (int i= 1; i <= ServicesLocator.Get<GameController>().maxLevel; i++)
                {
                _assetsServices.Load<Texture2D>($"Level{i}");
            }
            _scenesManager.ChangeScene<SceneMenu>();

        }

        protected override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds; 

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();  

            _scenesManager.Update(dt);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black); 
            
            _spriteBatch.Begin();
            _scenesManager.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
