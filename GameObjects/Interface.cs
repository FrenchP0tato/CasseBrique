using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;




namespace CasseBrique.GameObjects
{
    internal class Interface : SpriteGameObject
    {
        Rectangle bounds;
        private SpriteFont font;

        public Interface(Scene pRoot) : base(pRoot)
        {
            bounds = new Rectangle(0, 0, 1280, 70);
            font = ServicesLocator.Get<IAssetsService>().Get<SpriteFont>("BasicText");
        }

        public override void Update(float dt)
        {
           
            base.Update(dt);
        }

        public override void Draw(SpriteBatch sb)
        {
            var gc = ServicesLocator.Get<GameController>();
            var sc = ServicesLocator.Get<IScenesManager>();

            base.Draw(sb);
            texture = new Texture2D(sb.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.Black } );

            sb.Draw(texture, bounds,Color.White);
            sb.DrawString(font, $"Nombre de vies restantes: {gc.lifes} -- Niveau actuel: {gc.currentLevel} -- Resources: {gc.ResourceTable}", Vector2.One, Color.AliceBlue);

        }

      
    }
}
