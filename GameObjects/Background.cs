using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBrique.GameObjects
{
    internal class Background : SpriteGameObject
    {
        public Background(String Tag, Scene root) : base(root)
        {
            tag= Tag;
            position = Vector2.Zero;
            texture= ServicesLocator.Get<IAssetsService>().Get<Texture2D>("MenuBackground");
        }
    }
}
