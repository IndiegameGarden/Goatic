using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;
using Artemis.Interface;

namespace TTengineTest
{
    /// <summary>Tests a transparent channel and two transparent sprites right of it. The background should show through.
    /// </summary>
    class TestTransparentChannels : Test
    {
        public override void Create()
        {
            // put some content in the background channel
            var chbg = TestFactory.CreateChannel(this.BackgroundColor);
            BuildTo(chbg);
            var t0 = new TestSphereCollision();
            t0.Create();

            BlendState blend = new BlendState();
            blend.AlphaBlendFunction = BlendFunction.Add;
            blend.AlphaSourceBlend = Blend.SourceAlpha;
            blend.AlphaDestinationBlend = Blend.InverseSourceAlpha;
            blend.ColorSourceBlend = Blend.SourceAlpha;
            blend.ColorDestinationBlend = Blend.InverseSourceAlpha;

            var scr = Channel.GetComponent<WorldComp>().Screen;
            scr.SpriteBatch.blendState = blend; // custom blending of my sub-channels

            BuildToDefault();
            // create a first child channel - should become transparent
            var ch1 = TestFactory.CreateChannel(Color.Transparent, 600, 400);
			ch1.GetComponent<PositionComp>().Position = new Vector2(50f, 50f);
            
            // second item - a regular sprite with transparency
            var spr1 = TestFactory.CreateSpritelet("red-circle_frank-tschakert_runtime-load.png"); // ("Op-art-circle_Marco-Braun");
            spr1.GetComponent<PositionComp>().Position = new Vector2(800f, 50f);

            // 3rd item - a regular compiled content sprite with transparency
            var spr2 = TestFactory.CreateSpritelet("red-circle_frank-tschakert"); 
            spr2.GetComponent<PositionComp>().Position = new Vector2(1000f, 50f);

			// for channel, build the content into it.
			BuildTo(ch1);
			var t1 = new TestScaling();
			t1.Create();

        }

    }
}
