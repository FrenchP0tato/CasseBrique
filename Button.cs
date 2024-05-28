using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CasseBrique
{
    internal class Button
    {
        private Texture2D texture;
        protected Vector2 position;
        private Color color = Color.White;
        protected Vector2 size;

        public Vector2 Position
        {
            get { return position; }
            private set { position = value; } //accesseur
        }

        public Vector2 Size
        {
            get { return size; }
            private set { size = value; }
        }

        public Button(Vector2 Pos,string buttontype)
        {
            if (buttontype == "Default")
            {
                this.texture = ServicesLocator.Get<IAssetsService>().Get<Texture2D>("buttonDefault");
            } 
            else if (buttontype == "Selected")
            {
                this.texture= ServicesLocator.Get<IAssetsService>().Get<Texture2D>("buttonSelected");
            }
            this.position.X = Pos.X - texture.Width*0.5f;
            this.position.Y = Pos.Y - texture.Height * 0.5f;

        }

        public void Draw(SpriteBatch sprb) 
        {
            sprb.Draw(texture, position, color);
        }

    }
}
