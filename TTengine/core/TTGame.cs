﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TTengine.Core;
using TTengine.Util;
using Artemis;
//using TTengine.Modifiers; // TODO
using TTMusicEngine;
using TTMusicEngine.Soundevents;

namespace TTengine.Core
{
    public abstract class TTGame: Game
    {
        /// <summary>If set true, starts the TTMusicEngine</summary>
        public bool IsMusicEngine = false;

        /// <summary>The currently running (single) instance of TTGame</summary>
        public static TTGame Instance = null;

        public GraphicsDeviceManager Graphics;

        public Screenlet Screen;
        
        public MusicEngine MusicEngine;

        /// <summary>The Artemis entity world</summary>
        public EntityWorld World;

        public TTGame()
        {
            Instance = this;
            IsFixedTimeStep = false;
            Content.RootDirectory = "Content";

#if DEBUG
            graphics.SynchronizeWithVerticalRetrace = false;
#else
            Graphics.SynchronizeWithVerticalRetrace = true;
#endif

        }

        protected override void Initialize()
        {
            TTengineMaster.Create(this);

            // open the TTMusicEngine
            musicEngine = MusicEngine.GetInstance();
            musicEngine.AudioPath = "Content";
            if (!musicEngine.Initialize())
                throw new Exception(musicEngine.StatusMsg);

            // create screen for drawing to
            Screen = new Screenlet(Graphics.PreferredBackBufferWidth, Graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            world.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.Black);
            this.Screen.BeginDraw();
            this.World.Draw();
            this.Screen.EndDraw();
        }

    }
}