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
            Entity screen;
            using (BuildTo(fx))
            {
                screen = CreateScreen(New(), this.BackgroundColor, true, 1920, 1280);
                screen = CreateSprite(screen, screen.C<ScreenComp>()); // TODO could be in one call, both
            }

            var sc = screen.C<ScreenComp>();
            var sc2 = BuildScreen;

            EffectParameterCollection p = fx.C<ScreenComp>().SpriteBatch.effect.Parameters;
            p["video_size"].SetValue(new Vector2(sc2.Width, sc2.Height));
            p["output_size"].SetValue(new Vector2(sc2.Width, sc2.Height));
            p["texture_size"].SetValue(new Vector2(sc.Width, sc.Height));
            p["modelViewProj"].SetValue(Matrix.Identity);
            using (BuildTo(screen))
            {
                var t = new TestSphereCollision();
                t.BuildLike(this);
                t.BuildAll();
            }
        }

    }
}
