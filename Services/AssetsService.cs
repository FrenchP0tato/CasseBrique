using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CasseBrique
{
    public interface IAssetsService
    {
        public T Get<T>(string name);
    }

    public sealed class AssetsService : IAssetsService
    {
        private Dictionary<string,object> _assets = new Dictionary<string,object>();
        private ContentManager _contentManager;

        public AssetsService(ContentManager contentManager) 
        {
            _contentManager = contentManager;
            ServicesLocator.Register<IAssetsService>(this);
        }

        public void Load<T>(string name)
        {
            if (_assets.ContainsKey(name))
                throw new InvalidOperationException($"Assets service: Asset with name {name} is already registered");
            T asset = _contentManager.Load<T>(name);
            _assets.Add(name, asset);
        }

        public T Get<T>(string name) 
        {
            if (!_assets.ContainsKey(name))
                throw new InvalidOperationException($"Assets service: Asset with name {name} is not registered");
            return (T)_assets[name];
        }

    }
}
