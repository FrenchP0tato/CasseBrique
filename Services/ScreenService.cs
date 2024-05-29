using Microsoft.Xna.Framework;


namespace CasseBrique
{
    public interface IScreenService
    {
         int Width { get; }
         int Height { get; }
        int Top { get; }
        int Left { get; }
        int Right { get; }
        int Bottom { get; }
        Vector2 TopLeft { get; }
        Vector2 BotRight { get; }
        Vector2 Center { get; }
        Rectangle Bounds { get; }
    }
    public sealed class ScreenService:IScreenService // aucune classe ne peut hériter
    {
        private GraphicsDeviceManager _graphicsDeviceManager;
        public ScreenService(GraphicsDeviceManager graphicsDeviceManager) 
        {
        _graphicsDeviceManager =graphicsDeviceManager;
        ServicesLocator.Register<IScreenService>(this);
        }

       public void SetSize(int width, int height)
        {

            _graphicsDeviceManager.PreferredBackBufferWidth = width;
            _graphicsDeviceManager.PreferredBackBufferHeight = height;
            _graphicsDeviceManager.ApplyChanges();
        }

        public int Width => _graphicsDeviceManager.PreferredBackBufferWidth;
        public int Height => _graphicsDeviceManager.PreferredBackBufferHeight;
        public int Top => 0;
        public int Left => 0;
        public int Right => Width;
        public int Bottom => Height;
        public Vector2 TopLeft => Vector2.Zero;
        public Vector2 BotRight => new (Width,Height);
        public Vector2 Center => BotRight * .5f;
        public Rectangle Bounds => new (Top,Left,Width,Height);
    }
}
