// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Util;

using Artemis;


namespace TTengineTest
{
    /// <summary>
    /// Visual "unit" tests of various aspects of the TTengine. Press keys to cycle through tests.
    /// </summary>
    public class TestGame : TTGame
    {
        public static TestFactory Factory;
        KeyboardState kbOld = Keyboard.GetState();
        int channel = 0;
        List<Entity> testChannels = new List<Entity>();

        public TestGame()
        {
            IsAudio = true;
            IsProfiling = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            // Here all the tests are created
            //DoTest(new TestPostEffects()); //FIXME
            DoTest(new TestScriptThreadedSystemForBuilding());
            DoTest(new TestTextureSamplingShader());
            DoTest(new TestTransparentChannels());
            DoTest(new TestRelativeMotion());
            DoTest(new TestMultiChannels());
            DoTest(new TestGamepad());
            DoTest(new TestModifiers());
            DoTest(new TestZoomedScreenlet());
            DoTest(new TestAudioBasics()); //FIXME? audio plays too soon
            DoTest(new TestContentLoad());
            DoTest(new TestTargetMotion());
            DoTest(new TestScaling());            
            DoTest(new TestBTAI());
            DoTest(new TestSphereCollision());
            DoTest(new TestAnimatedSprite());
            DoTest(new TestBasicShader());
            DoTest(new TestMixedShaders()); 
            DoTest(new TestLinearMotion());
            DoTest(new TestRotation());
            DoTest(new TestSpritePixelGetSet());            

            // pick the initial one and activate it
            ZapChannel(0);

        }

        protected override void UnloadContent()
        {
            foreach(Entity c in testChannels)
            {
                c.C<WorldComp>().World.UnloadContent();
            }
            base.UnloadContent();
        }

        protected override void Initialize()
        {
            Factory = new TestFactory();
            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Escape) && !kbOld.IsKeyDown(Keys.Escape))
            {
                UnloadContent();
                Exit();
            }
            
            if ((kb.IsKeyDown(Keys.Space) && !kbOld.IsKeyDown(Keys.Space)) ||
                (kb.IsKeyDown(Keys.PageDown) && !kbOld.IsKeyDown(Keys.PageDown)) )
            {
                ZapChannel(+1);
            }
            else if (kb.IsKeyDown(Keys.PageUp) && !kbOld.IsKeyDown(Keys.PageUp))
            {
                ZapChannel(-1);
            }
            kbOld = kb;
        }

        private void ZapChannel(int delta)
        {
            int nch = channel + delta;
            if (nch < 0)
                nch += testChannels.Count;
            if (nch >= testChannels.Count)
                nch -= testChannels.Count;
            if (channel != nch)
            {
                testChannels[channel].IsEnabled = false;
                testChannels[channel].Refresh();
            }
            testChannels[nch].IsEnabled = true;
            testChannels[nch].Refresh();
            channel = nch;
        }

        private void DoTest(Test test)
        {
            Factory.BuildTo(RootWorld);
            Factory.BuildTo(RootScreen);

            var ch = Factory.NewDisabled(); // a channel is disabled by default - only one turned on later.
            Factory.CreateChannel(ch, test.BackgroundColor);
            test.Channel = ch;
            testChannels.Add(ch);
            test.BuildToDefault(); // build test to the new channel (test.Channel)
            test.Create();

            // add framerate counter
            test.BuildToDefault();
            var col = TTUtil.InvertColor(test.BackgroundColor);
            Factory.CreateFrameRateCounter(Factory.New(), col, 20);

            // add test info as text
            Factory.CreateTextlet(Factory.New(),new Vector2(2f, GraphicsMgr.PreferredBackBufferHeight-40f), test.GetType().Name, col, 0f);
        }

    }

}
