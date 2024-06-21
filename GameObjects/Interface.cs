using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;



namespace CasseBrique.GameObjects
{
    internal class Interface : SpriteGameObject
    {
        Rectangle bounds;
        private SpriteFont font;
        private List<Resource> currentResources;
        IScreenService screen = ServicesLocator.Get<IScreenService>();

        GameController gc = ServicesLocator.Get<GameController>();
        
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
            base.Draw(sb);
            texture = new Texture2D(sb.GraphicsDevice, 1, 1);
            texture.SetData(new[] { Color.Black } );

            sb.Draw(texture, bounds,Color.White);
            currentResources = gc.ListResources;
            
            DrawCenteredText(font,"Resources in storage", new Vector2(900, 10), sb);

            foreach (KeyValuePair<string, Resource> entry in ResourceData.Data )
            {
                sb.DrawString(font, $"{entry.Key}: {gc.GetResourceQty(entry.Key)}", new Vector2(680+entry.Value.InventorySlot * 100, 40), Color.AliceBlue);
            }
            
       
            DrawCenteredText(font,$"Level:{gc.currentLevel}", new Vector2(screen.Center.X, 20), sb);
            DrawCenteredText(font, $"Day:{gc.days}", new Vector2(screen.Center.X, 35), sb);

           
            sb.DrawString(font, $"Remaining balls: {gc.currentLifes}", new Vector2(20, 10), Color.AliceBlue);

        }

        private Vector2 TextOffset(SpriteFont font, string text, Vector2 origin)
        {
            Vector2 textSize = font.MeasureString(text);
            Vector2 offset = new Vector2(origin.X - textSize.X / 2, origin.Y);

            return offset;
        }

        private void DrawCenteredText(SpriteFont font, string text, Vector2 origin, SpriteBatch sb)
        {
            Vector2 offset = TextOffset(font, text, origin);   
            sb.DrawString(font, text, offset, Color.AliceBlue);

        }
    }
}
