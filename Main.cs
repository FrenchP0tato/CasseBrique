using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml.Linq;

// Questions pour Nicolas:
// Aime pas devoir tjr envoyer la balle dans la meme direction, rebonds intelligents? Ou juste envoi dans direction choisie? => Bonus?
// Pour une liste de resources, liste ou tableau? ou juste ensemble de valeurs gérées par le GameController? 
// tout simplement; tableau avec 5 valeurs?? 5 int? Mais du coup est-ce qu'on peut faire des operations sur les éléments dans le tableau? 
// Classe resource utile pour creer, mais ensuite pas utile. J'ai juste besoin de stocker des int de resources, du coup, besoin d'une liste? Mais en meme temps, je sais cb de resources differentes j'ai, donc tableau?
// Vu que pas de draw dans mes objets, comment fait un effet - ex le bouton s'illumine avant de disparaitre


namespace CasseBrique
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AssetsService _assetsServices;
        private ScreenService _screenService; //necessaires ou non? 
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

            _screenService.SetSize(1280, 720);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice); 
            

            _assetsServices.Load<Texture2D>("Paddle");
            _assetsServices.Load<Texture2D>("BallBlue");
            _assetsServices.Load<Texture2D>("GreyBrick");
            _assetsServices.Load<Texture2D>("GreyBrickDamaged");
            _assetsServices.Load<Texture2D>("buttonDefault");
            _assetsServices.Load<Texture2D>("buttonSelected");
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
