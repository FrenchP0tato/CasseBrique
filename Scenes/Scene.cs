using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using CasseBrique.GameObjects;

namespace CasseBrique
{
    public abstract class Scene //peut pas avoir d'instance de Scene uniquement, les classe qui héritent doivent implémenter les methodes abstraites
    {
        private List<GameObject>_gameObjects=new List<GameObject>();

        public virtual void Load(){ }
        public virtual void Unload() { }
        
        public void Update(float dt)
        { 
      foreach(GameObject obj in _gameObjects)
            if (obj.enable)
                    obj.Update(dt);
      for (int i=_gameObjects.Count-1; i>=0; i--)
                if (_gameObjects[i].isFree)
                {
                    _gameObjects[i].OnFree();
                    _gameObjects.RemoveAt(i);
                }
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (GameObject obj in _gameObjects)
                if (obj.enable)
                    obj.Draw(sb);
        }

        public void AddGameObject(GameObject obj)
        { obj.Start();
            _gameObjects.Add(obj);
                }
        public List<T>GetGameObjects<T>()
        {
            var gameObjects=new List<T>();
            foreach(GameObject gameObject in _gameObjects)
                if(gameObject is T typedObject)
                    gameObjects.Add(typedObject);
            return gameObjects;
        }
    }
}
