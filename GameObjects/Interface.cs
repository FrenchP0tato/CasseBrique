using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Security.AccessControl;




namespace CasseBrique.GameObjects
{
    internal class Interface : SpriteGameObject
    {
        Rectangle bounds;
        private SpriteFont font;
        private List<Resource> currentResources;

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
            currentResources=gc.GetResourceList();

            foreach(KeyValuePair<string, Resource> entry in ResourceData.Data )
            {
                sb.DrawString(font, $"{entry.Key}: {gc.GetResourceQty(entry.Key)}", new Vector2(entry.Value.InventorySlot * 100, 0), Color.AliceBlue);
            }

            sb.DrawString(font, $"Nombre de vies restantes: {gc.currentLifes} -- Niveau actuel: {gc.currentLevel}", new Vector2(0,50), Color.AliceBlue);
            sb.DrawString(font, $"Started?{gc.LevelStarted}", new Vector2(300, 50), Color.AliceBlue);
        }

      
    }
}
