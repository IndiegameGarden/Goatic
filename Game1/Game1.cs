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
        public Game1Factory Factory;
        public Entity IntroChannel, GameChannel;

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
            //IntroChannel = Game1Factory.CreateChannel(Color.Black);
            GameChannel = Game1Factory.CreateChannel(Color.Black);

            var scr = GameChannel.GetComponent<ScreenComp>();
            scr.BackgroundColor = Color.White;

            // add framerate counter
            Game1Factory.CreateFrameRateCounter(Color.Black);

            // add several sprites             
            for (float x = 0.1f; x < 1.6f; x += 0.3f)
            {
                for (float y = 0.1f; y < 1f; y += 0.24f)
                {
                    var pos = new Vector2(x * scr.Width, y * scr.Height);
                    Factory.CreateHyperActiveBall(pos);
                    Factory.CreateMovingTextlet(pos,"This is the\nTTengine test. !@#$1234");
                    //break;
                }
                //break;
            }

            base.LoadContent();
        }                  
    }

}
