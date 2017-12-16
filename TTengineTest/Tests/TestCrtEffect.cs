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

        public override void BuildAll()
        {
            var fx = CreateFx(New(), "crt-lottes");
            Entity entScr;

            using (BuildTo(fx))
            {
                entScr = CreateScreenSprite(New(), Color.Black, true, 960, 720);
                ProcessFitSize(entScr, this.Channel);
            }

            var sc = entScr.C<ScreenComp>();
            var sc2 = BuildScreen;
            EffectParameterCollection p = fx.C<ScreenComp>().SpriteBatch.effect.Parameters;
            AddFunctionScript(fx, 
                (ctx, v) => {
                    p["warpX"].SetValue((float)v);
                    p["warpY"].SetValue((float)v);
                }, 
                new SineFunction { Amplitude = 0.6, Offset = 0.061, Frequency = 0.3 });

            // TODO: move some of this to factory
            p["video_size"].SetValue(new Vector2(sc.Width, sc.Height));
            p["output_size"].SetValue(new Vector2(sc2.Width, sc2.Height));
            p["texture_size"].SetValue(new Vector2(sc.Width, sc.Height));
            //p["modelViewProj"].SetValue(Matrix.Identity);

            using (BuildTo(entScr))
            {
                var t = new TestMultiChannels(); // TestSphereCollision();
                t.BuildLike(this);
                t.BuildAll();
            }
        }

    }
}
