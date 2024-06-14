using CasseBrique.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


// Questions pour Nicolas:
// Comment garder les briques "en cours" quand on accède au Menu?
// Aime pas devoir tjr envoyer la balle dans la meme direction, rebonds intelligents? Ou juste envoi dans direction choisie? => Bonus?
// Vu que pas de draw dans mes objets, comment fait un effet - ex le bouton s'illumine avant de disparaitre


namespace CasseBrique
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AssetsService _assetsServices;
        private ScreenService _screenService; 
        private ScenesManager _scenesManager;

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
            new UtilsService();
            new MouseService();
            _scenesManager = new ScenesManager();
            ResourceData.PopulateData();

            _screenService.SetSize(1280, 720);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice); 
            
            //Textures
            _assetsServices.Load<Texture2D>("Paddle");
            _assetsServices.Load<Texture2D>("BallBlue");
            _assetsServices.Load<Texture2D>("GreyBrick");
            _assetsServices.Load<Texture2D>("GreyBrickDamaged");
            _assetsServices.Load<Texture2D>("buttonDefault");
            _assetsServices.Load<Texture2D>("buttonSelected");

            //Sons:
            _assetsServices.Load<Song>("CoolSong");
            _assetsServices.Load<SoundEffect>("ImpactPaddle");
            _assetsServices.Load<SoundEffect>("ImpactStone");
            _assetsServices.Load<SoundEffect>("ImpactWood");
            _assetsServices.Load<SoundEffect>("ImpactGrass");
            _assetsServices.Load<SoundEffect>("ImpactGold");


            //Autres Assets
            _assetsServices.Load<SpriteFont>("BasicText");
            _assetsServices.Load<Texture2D>("VillageBackground");



            for (int i= 1; i <= ServicesLocator.Get<GameController>().maxLevel; i++)
                {
                _assetsServices.Load<Texture2D>($"Level{i}");
            }
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
            GraphicsDevice.Clear(Color.Black); //comment la changer dans une scene? -> Trouvé: utiliser SB.GraphicsDevice
            
            _spriteBatch.Begin();
            _scenesManager.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
