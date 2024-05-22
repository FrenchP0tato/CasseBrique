﻿using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace CasseBrique.Services
{
    public class AssetsService
    {
        private Dictionary<string,object> _assets = new Dictionary<string,object>();

        public AssetsService() 
        {
            ServicesLocator.Register<AssetsService>(this);
        }

        public void Load<T>(string name)
        {
            ContentManager content = ServicesLocator.Get<ContentManager>();
            T asset = content.Load<T>(name);
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