
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CasseBrique
{
    public interface IScenesManager
    {
        void Load<T>() where T : Scene, new();
    }

    
    public sealed class ScenesManager : IScenesManager
    {
        private Scene _currentScene;

        public ScenesManager() 
        {

                        ServicesLocator.Register<IScenesManager>(this);
                }

        public void Load<T>() where T : Scene, new()
        {
            var type=typeof(T);
            if (_currentScene != null) _currentScene.Unload();
            _currentScene = new T();
            _currentScene.Load();
        }

        public void Update(float dt) => _currentScene?.Update(dt);
        public void Draw(SpriteBatch sb) => _currentScene?.Draw(sb);
        
    }
}