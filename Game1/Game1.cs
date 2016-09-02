// (c) 2010-2016 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Behaviors;
using TTengine.Modifiers;
using TTengine.Util;
using TTengineTest;

using Artemis;
using Artemis.Interface;
using TreeSharp;

namespace Game1
{
    /// <summary>
    /// Main game class.
    /// </summary>
    public class Game1 : TTGame
    {
        const int SPARE_SCREEN_HEIGHT = 256;

        public Game1Factory Factory;
        public Entity GameChannel, LevelChannel, BackgroundChannel, GuiChannel;

        public Game1()
        {
            IsAudio = false;    // set to true if audio is needed
        }

        protected override void Initialize()
        {
            Factory = Game1Factory.Instance;

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void LoadContent()
        {
            // create the game's channel at fixed resolution, which is then auto-scaled onto any screen resolution.
            GameChannel = Game1Factory.CreateChannel(Color.Black, 1366, 768 + SPARE_SCREEN_HEIGHT);
            // create level/background channels of same size
            LevelChannel = Game1Factory.CreateChannel(GameChannel);
            BackgroundChannel = Game1Factory.CreateChannel(GameChannel);
            // GuiChannel =
            Game1Factory.BuildTo(GameChannel);

            var scr = GameChannel.GetComponent<WorldComp>().Screen;
            scr.BackgroundColor = Color.White;

            // apply magic scaling to screen resolution
            Game1Factory.ProcessChannelFit(GameChannel, MainChannel, true, true, SPARE_SCREEN_HEIGHT);

            // add framerate counter
            Game1Factory.CreateFrameRateCounter(Color.Black, SPARE_SCREEN_HEIGHT/2 );

            // add content 
            Test t = new TestSphereCollision();
            t.Create();


            base.LoadContent();
        }                  
    }

}
