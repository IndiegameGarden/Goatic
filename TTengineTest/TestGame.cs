// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TTengine.Core;
using TTengine.Comps;
using TTengine.Util;
using Artemis;
using Artemis.Utils;


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
        List<Test> tests = new List<Test>();
        Entity textOverlayChannel;

        public TestGame()
        {
            IsAudio = true;
            IsProfiling = true;
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            Factory = new TestFactory();
            Factory.BuildToRoot();

            // Here all the tests are created
            //DoTest(new TestPostEffects()); //FIXME
            AddTest(new TestGeom3D());
            AddTest(new TestMidiInput());
            AddTest(new TestAnimatedSprite());
            AddTest(new TestGamepad());
            AddTest(new TestAudioBasics());
            AddTest(new TestFxSprite());
            AddTest(new TestFxSprite2());
            AddTest(new TestCrtEffect());
            AddTest(new TestMixedShaders());
            AddTest(new TestTextureSamplingShader());
            AddTest(new TestBasicShader());
            AddTest(new TestLinearMotion());
            AddTest(new TestRotation());
            AddTest(new TestModifiers());
            AddTest(new TestScriptThreadedSystemForBuilding());
            AddTest(new TestTransparentChannels());
            AddTest(new TestRelativeMotion());
            AddTest(new TestMultiChannels());
            AddTest(new TestZoomedScreen());
            AddTest(new TestContentLoad());
            AddTest(new TestTargetMotion());
            AddTest(new TestScaling());
            AddTest(new TestBTAI());
            AddTest(new TestSphereCollision());
            AddTest(new TestSpritePixelGetSet());

            // create the text overlay channel that overlays any test's channel.
            this.textOverlayChannel = CreateTextOverlayChannel();

            // pick the initial one and activate it
            ZapChannel(0);
        }

        protected override void UnloadContent()
        {
            foreach(Test t in tests)
            {
                t.Channel.C<WorldComp>().World.UnloadContent();
            }
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // check if test content has been built already.
            var test = tests[channel];
            if (!test.IsBuilt)
            {
                test.IsBuilt = true;
                using (test.BuildTo(test.Channel))  // since the test class is the factory for itself, needed.
                {
                    test.BuildAll();
                }
            }

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

        // cycle through the list of tests by an amount delta, activating the new channel and de-activating the old one.
        private void ZapChannel(int delta)
        {
            int nch = channel + delta;
            if (nch < 0)
                nch += tests.Count;
            if (nch >= tests.Count)
                nch -= tests.Count;
            if (channel != nch)
            {
                tests[channel].Channel.IsEnabled = false;
                tests[channel].Channel.Refresh();
            }
            channel = nch;
            var test = tests[channel];
            test.Channel.IsEnabled = true;
            test.Channel.Refresh();

            // update text overlays with font color of the test and show name of test
            Bag<Entity> t = this.textOverlayChannel.C<WorldComp>().World.EntityManager.GetEntities(Aspect.All(new Type[]{ typeof(TextComp) }));
            foreach (Entity e in t)
            {
                e.C<DrawComp>().DrawColor = test.FontColor;
                e.C<TextComp>().Text = test.GetType().Name;
            }

        }

        // create a text overlay with test info
        private Entity CreateTextOverlayChannel()
        {
            var ch = Factory.CreateChannel(Factory.New(), Color.Transparent);
            using (Factory.BuildTo(ch))
            {
                // create framerate counter stats
                Factory.CreateFrameRateCounter(Factory.New(), Color.White, 20);

                // add test info as text
                Factory.CreateText(Factory.New(), new Vector2(2f, GraphicsMgr.PreferredBackBufferHeight - 40f), "TestGame", Color.White, 0f);
            }
            return ch;
        }

        // add a new test to the list, not building the content yet.
        private void AddTest(Test test )
        {
            test.Channel = Factory.NewDisabled(); // a channel is disabled by default - turned on later.
            Factory.CreateChannel(test.Channel, test.BackgroundColor);
            test.FontColor = TTUtil.InvertColor(test.BackgroundColor);
            tests.Add(test);
        }

    }

}
