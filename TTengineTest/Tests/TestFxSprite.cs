﻿// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis;
using TTengine.Comps;

namespace TTengineTest
{
    /// <summary>
    /// Test the FxSprite, which is a sprite rect filled with contents generated by a shader fx.
    /// </summary>
    class TestFxSprite: Test
    {

        public TestFxSprite()
        {
            this.BackgroundColor = Color.Black;
        }

        public override void Create()
        {
            var fx = CreateJuliaFxSprite(Factory.New());         
        }

        public Entity CreateJuliaFxSprite(Entity e)
        {
            Factory.CreateFxSprite(e, "MandelbrotJulia");
            var fx = e.C<ScreenComp>().SpriteBatch.effect;
            fx.CurrentTechnique = fx.Techniques[1]; // select Julia
            Factory.AddScript(e, (ctx) =>
                {
                    var effect = e.C<ScreenComp>().SpriteBatch.effect;
                    var t = (float)ctx.SimTime;
                    effect.Parameters["JuliaSeed"].SetValue(new Vector2(0.39f + t * 0.004f, -0.2f + t * 0.003f));
                }
            );

            return e;
        }



    }
}