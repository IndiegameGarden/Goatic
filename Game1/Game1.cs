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
        /// <summary>
        /// number of 'extra' pixels in total (adding ones above and below the screen) for showing a little bit more of the
        /// current game screen, for people who have monitors with a somewhat higher number of pixels vertically e.g. 4:3 aspect
        /// ratio instead of 16:9 widescreen. Game code has to ensure no crucial items (GUI elements, text) are plotted in these
        /// areas.
        /// </summary>
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
            GameChannel = Game1Factory.CreateChannel(Color.Transparent, 1366, 668 + SPARE_SCREEN_HEIGHT);
            Game1Factory.BuildTo(GameChannel);

            // create level/background channels of same size
            BackgroundChannel = Game1Factory.CreateChannel(GameChannel);
            BackgroundChannel.GetComponent<PositionComp>().Depth = 0.7f;
            BackgroundChannel.GetComponent<WorldComp>().Screen.BackgroundColor = Color.Transparent;
            BackgroundChannel.Tag = "BackgroundChannel";
            LevelChannel = Game1Factory.CreateChannel(Color.Transparent,800,600); //GameChannel);
            LevelChannel.GetComponent<PositionComp>().Depth = 0.5f;
            LevelChannel.GetComponent<WorldComp>().Screen.BackgroundColor = Color.Transparent;
            LevelChannel.Tag = "LevelChannel";
            // GuiChannel = TODO

            var scr = GameChannel.GetComponent<WorldComp>().Screen;
            scr.BackgroundColor = Color.Black;

            // apply magic scaling to screen resolution
            Game1Factory.ProcessChannelFit(GameChannel, MainChannel, true, true, SPARE_SCREEN_HEIGHT);

            // add framerate counter
            Game1Factory.CreateFrameRateCounter(Color.White, SPARE_SCREEN_HEIGHT/2 );

            Game1Factory.BuildTo(LevelChannel);

            // add content 
            Test t = new TestSphereCollision();
            t.Create();

            // test bg content
            Game1Factory.BuildTo(BackgroundChannel);
            t = new TestMixedShaders();
            t.Channel = BackgroundChannel;
            t.Create();

            base.LoadContent();
        }                  
    }

}
