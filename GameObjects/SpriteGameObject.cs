using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;


namespace CasseBrique.GameObjects
{
    public class SpriteGameObject : GameObject
    {
        public Texture2D texture;
        public Vector2 position;
        public Color color;
        public float rotation;
        public Vector2 scale;
        public Vector2 size;
        public Vector2 offset;
        public string tag;
        public SpriteEffects effect;
        public SoundEffect impactSound;
       

        public SpriteGameObject(Scene pRoot) : base(true, pRoot)
        {
            position = Vector2.Zero;
            color = Color.White;
            rotation = 0f;
            scale = Vector2.One;
            offset=Vector2.Zero;
            tag = "Sprite";
            effect = SpriteEffects.None;
        }

        public SpriteGameObject(Vector2 pPosition, Scene pRoot) : base(true, pRoot)
        {
            this.position = pPosition;
            color = Color.White;
            rotation = 0f;
            scale = Vector2.One;
            offset = Vector2.Zero;
        }



        public Rectangle collider
        {
            get
            {
                return new Rectangle((int)(position.X - offset.X), (int)(position.Y - offset.Y), (int)(size.X), (int)size.Y);

            }

        }

        public override void Draw(SpriteBatch sb)
        {
            if (texture==null) { texture = new Texture2D(sb.GraphicsDevice, 1, 1); }
            sb.Draw(texture, position, null, color, rotation, offset, scale, effect, 0);
        }

        public virtual void OnCollide(SpriteGameObject other) { }
    }
}
