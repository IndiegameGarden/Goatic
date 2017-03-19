﻿// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Microsoft.Xna.Framework;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;
using Artemis.Interface;

namespace TTengineTest
{
    /// <summary>Tests multiple Channels. Each Channel is later rendered as a sprite onto the main screen.
    /// </summary>
    class TestMultiChannels : Test
    {
        public override void Create()
        {
            // put some content in the main channel
            var t0 = new TestScaling();
            t0.Create();

            // create a first child channel
            var ch1 = Factory.CreateChannel(Factory.New(), Color.LightSalmon, 200, 400);
            ch1.C<WorldComp>().TimeWarp = 0.333;
			ch1.C<PositionComp>().Position = new Vector2(50f, 50f);
            
            // second child channel
            var ch2 = Factory.CreateChannel(Factory.New(), Color.LightSeaGreen, 200, 400);
            ch2.C<WorldComp>().TimeWarp = 1.0;
			ch2.C<PositionComp>().Position = new Vector2(300f, 50f);

			// 3rd
            var ch3 = Factory.CreateChannel(Factory.New(), Color.LightPink, 200, 400);
            ch3.C<WorldComp>().TimeWarp = 2.0;
			ch3.C<PositionComp>().Position = new Vector2(550f, 50f);

			// 4th
            var ch4 = Factory.CreateChannel(Factory.New(), Color.LightGreen, 700, 250);
            ch4.C<WorldComp>().TimeWarp = 4.0;
			ch4.C<PositionComp>().Position = new Vector2(50f, 500f);            

			// for each channel, built the content into it.
			BuildTo(ch1);
			var t1 = new TestRelativeMotion();
			t1.Create();

			BuildTo(ch2);
			var t2 = new TestRelativeMotion();
			t2.Create();

			BuildTo(ch3);
			var t3 = new TestRelativeMotion();
			t3.Create();

			BuildTo(ch4);
			var t4 = new TestRelativeMotion();
			t4.Create();

        }

    }
}
