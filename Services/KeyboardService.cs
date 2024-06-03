using Microsoft.Xna.Framework.Input;

namespace CasseBrique.Services
{
    public class KeyboardService
    {
        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;

        public KeyboardService()
        {
            ServicesLocator.Register<KeyboardService>(this);
        }

        public static KeyboardState GetState()
        {
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            return currentKeyState;
        }

        public static bool IsPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public static bool HasBeenPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }
    }
}
