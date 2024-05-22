using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasseBrique.Services
{
    public class ScreenService
    {
        public ScreenService(int width, int height) 
        {
        SetSize(width, height);
        ServicesLocator.Register<ScreenService>(this);
        }

       public void SetSize(int width, int height)
        {
            var graphics = ServicesLocator.Get<GraphicsDeviceManager>();
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();
        }

        public int width => ServicesLocator.Get<GraphicsDeviceManager>().PreferredBackBufferWidth;
        public int height => ServicesLocator.Get<GraphicsDeviceManager>().PreferredBackBufferHeight;
        public int top => 0;
        public int left => 0;
        public int right => width;
        public int bottom => height;
        public Vector2 topLeft => Vector2.Zero;
        public Vector2 botRight => new Vector2(width,height);
        public Vector2 center => botRight * .5f;
    }
}
