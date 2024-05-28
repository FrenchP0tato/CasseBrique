
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CasseBrique
{
    public interface IScenesManager
    {
        public void LoadScene(string sceneName);
    }


    internal class ScenesManager : IScenesManager
    {
        private Scene _currentScene;
        private Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();

        public ScenesManager() 
        {
            ServicesLocator.Register<IScenesManager>(this);
                }


        public void RegisterScene(string sceneName, Scene scene)
        {
            if (_scenes.ContainsKey(sceneName))
                throw new InvalidOperationException($"Scene Manager: Scene of name {sceneName} is already registered.");
            _scenes[sceneName] = scene;
        }

        public void LoadScene(string sceneName)
        {
            if (!_scenes.ContainsKey(sceneName))
                throw new InvalidOperationException($"Scene Manager: Scene of name {sceneName} not registered.");
            if (_currentScene != null) _currentScene.Unload();
            _currentScene = _scenes[sceneName];
            _currentScene.Load();


        }

        public void Update(float dt)
        {
            if (_currentScene != null)
                _currentScene.Update(dt);
        }

        public void Draw(SpriteBatch sb)
        {
            if (_currentScene != null)
                _currentScene.Draw(sb);

        }


    }
}