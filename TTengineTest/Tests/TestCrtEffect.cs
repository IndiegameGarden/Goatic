// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Microsoft.Xna.Framework;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;
using Artemis.Interface;
using Microsoft.Xna.Framework.Graphics;
using Artemis;

namespace TTengineTest
{
    /// <summary></summary>
    class TestCrtEffect : Test
    {

        public TestCrtEffect(): base()
        {
            this.BackgroundColor = Color.Black;    
        }

        public override void BuildAll()
        {
            var fx = CreateFx(New(), "crt-lottes");
            Entity chan;

            using (BuildTo(fx))
            {
                chan = CreateChannel(New(), Color.Black, 960, 720);
                ProcessFitSize(chan, this.Channel);
            }

            // set a script for shader params
            EffectParameterCollection p = fx.C<ScreenComp>().SpriteBatch.effect.Parameters;
            AddFunctionScript(fx, 
                (ctx, v) => {
                    p["warpX"].SetValue((float)v);
                    p["warpY"].SetValue((float)v);
                }, 
                new SineFunction { Amplitude = 0.6, Offset = 0.061, Frequency = 0.3 } 
            );

            // TODO: move some of this to factory
            p["video_size"].SetValue(new Vector2(chan.C<SpriteComp>().Width, chan.C<SpriteComp>().Height));
            p["output_size"].SetValue(new Vector2(BuildScreen.Width, BuildScreen.Height));
            p["texture_size"].SetValue(new Vector2(chan.C<SpriteComp>().Width, chan.C<SpriteComp>().Height));
            //p["modelViewProj"].SetValue(Matrix.Identity);

            // fill the channel with content
            using (BuildTo(chan))
            {
                var t = new TestMultiChannels(); // TestSphereCollision();
                t.BuildLike(this);
                t.BuildAll();
            }
        }

    }
}
