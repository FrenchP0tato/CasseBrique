using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CasseBrique.GameObjects
{
    public class SpriteGameObject : GameObject
    {
        public Texture2D texture;
        public Vector2 position;
        public Color color;
        public float rotation;
        public float scale;
        public Vector2 size;
        public Vector2 offset;

        public SpriteGameObject(Scene root) : base(true, root)
        {
            position = Vector2.Zero;
            color = Color.White;
            rotation = 0f;
            scale = 1f;
            offset=Vector2.Zero;
            
        }

        public SpriteGameObject(Vector2 position, Scene root) : base(true, root)
        {
            this.position = position;
            color = Color.White;
            rotation = 0f;
            scale = 1f;
            offset = Vector2.Zero;

        }

        public override void Draw(SpriteBatch sb)
        {
            if (texture == null) return;
            sb.Draw(texture, position, null, color, rotation, offset, scale, SpriteEffects.None, 0);
        }
    }
}
