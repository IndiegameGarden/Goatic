﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Artemis;
using TTengine.Comps;

namespace TTengine.Core
{
    /// <summary>
    /// The Singleton Factory to create new Entities, and other things - 
    /// typically subclass this with your own factory, dedicated to the game.
    /// </summary>
    public class TTFactory
    {
        private static TTFactory _instance = null;
        private static TTGame _game = null;

        public virtual TTFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TTFactory();
                return _instance;
            }
        }

        // creates the instance, linked to a TTGame
        protected TTFactory()
        {
            _game = TTGame.Instance;
        }

        /// <summary>
        /// Create simplest Entity without components
        /// </summary>
        /// <returns></returns>
        public static Entity CreateEntity()
        {
            return _game.ActiveWorld.CreateEntity();
        }

        /// <summary>
        /// Create a Gamelet, which is an Entity with position and velocity but no shape/drawability (yet).
        /// </summary>
        /// <returns></returns>
        public static Entity CreateGamelet()
        {
            Entity e = CreateEntity();
            e.AddComponent(new PositionComp());
            e.AddComponent(new VelocityComp());
            e.Refresh();
            return e;
        }

        /// <summary>
        /// Create a Spritelet, which is a collidable, moveable sprite in TTengine
        /// </summary>
        /// <param name="graphicsFile">The content graphics file with or without extension. If
        /// extension given eg "ball.png", the uncompiled file will be loaded at runtime. If no extension
        /// given eg "ball", precompiled XNA content will be loaded (.xnb files).</param>
        /// <returns></returns>
        public static Entity CreateSpritelet(string graphicsFile)
        {
            Entity e = CreateEntity();
            e.AddComponent(new PositionComp());
            e.AddComponent(new VelocityComp());
            var spriteComp = new SpriteComp(graphicsFile);
            e.AddComponent(spriteComp);
            float radius = spriteComp.Width/2.0f;
            e.AddComponent(new ShapeComp(radius));
            e.AddComponent(new DrawComp());
            e.Refresh();
            return e;
        }

        /// <summary>
        /// Creates a Textlet, which is a moveable piece of text. (TODO: font)
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Entity CreateTextlet(string text)
        {
            Entity e = CreateGamelet();
            e.AddComponent(new ScaleComp());
            e.AddComponent(new DrawComp());
            TextComp tc = new TextComp(text);
            tc.Font = _game.Content.Load<SpriteFont>("TTDebugFont"); // FIXME allow other fonts
            e.AddComponent(tc);
            return e;
        }

    }
}

