// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;
using Artemis.Interface;

namespace TTengineTest
{
    /// <summary>Zooms in on a part of a rendered Screenlet. Useful for e.g. scrolling level.</summary>
    class TestZoomedScreenlet : Test
    {

        public TestZoomedScreenlet()
        {
            BackgroundColor = Color.DarkGray;
        }

        public override void Create()
        {
            // dedicated screen for rendering the level using blocky (non interpolated) graphics bitmap
            var levScr = Factory.CreateScreenlet(Factory.New(), Color.Black,true);
            levScr.C<ScreenComp>().SpriteBatch.samplerState = SamplerState.PointClamp; // nice 'n blocky
            BuildTo(levScr);
            var s = Factory.CreateSprite(Factory.New(), "Quest14-Level1.png");
            s.C<SpriteComp>().Center = new Vector2(532f, 227f);
            s.AddC(new ScaleComp(1.0));
            var targFunc = new MoveToTargetFunction();
            targFunc.CurrentValue.X = 1.0f;
            targFunc.Target.X = 15.0f;
            targFunc.Speed = 3;
            Factory.AddScript(s, (ctx) => { s.C<ScaleComp>().Scale = targFunc.Value(ctx).X; } );
            s.C<PositionComp>().Position = Channel.C<WorldComp>().Screen.Center;

            // -- main channel: shows the child channel using a sprite
            BuildToDefault();
            var scr1 = Factory.CreateSprite(Factory.New(), levScr.C<ScreenComp>());
            scr1.C<PositionComp>().Depth = 0.9f;
            // some non-blocky graphics in front of level; using default Spritebatch
            var t1 = new TestAnimatedSprite();
            t1.Create();                    

        }

    }
}
