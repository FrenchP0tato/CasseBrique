using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace CasseBrique.GameObjects
{
    internal class Background : SpriteGameObject
    {
        
        public Background(String pTag, Texture2D pTexture, Scene pRoot) : base(pRoot)
        {
            tag= pTag;
            position = new Vector2(0,70);
            texture= pTexture;
        }
    }
}
