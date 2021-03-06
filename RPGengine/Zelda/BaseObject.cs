﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    class BaseObject
    {
        public int ID { get; set; }
        
        private readonly List<Component> _components;

        public BaseObject()
        {
            _components = new List<Component>();
        }

        public TComponentType GetComponent<TComponentType>(ComponentType componentType) where TComponentType : Component
        {
            return _components.Find(c => c.ComponentType == componentType) as TComponentType;
        }

        public void AddComponent(Component component)
        {
            _components.Add(component);

            component.Initialize(this);
        }

        public void AddComponent(List<Component> components)
        {
            _components.AddRange(components);

            foreach (var component in components)
            {
                component.Initialize(this);
            }
        }

        public void RemoveComponent(Component component)
        {
            _components.Remove(component);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach(var component in _components)
            {
                component.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach (var component in _components)
            {
                component.Draw(spriteBatch);
            }
        }

        public bool Intersect(Rectangle rect, Vector2 pos, Manager.CameraManager cameraManager)
        {
            var position = cameraManager.WorldToScreenPosition(pos);
            var rectanglePosition = new Vector2(rect.X, rect.Y);

            if (position == rectanglePosition)
                return false;

            var collided = cameraManager.InScreenCheck(position) && rect.Intersects(new Rectangle((int)position.X, (int)position.Y, Global.TILE_SIZE, Global.TILE_SIZE));

            return collided;
        }
    }
}
