// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Microsoft.Xna.Framework;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;
using Artemis.Interface;

namespace TTengineTest
{
    /// <summary></summary>
    class TestGeom3D : Test
    {

        public override void BuildAll()
        {
            //this.BackgroundColor = Color.Transparent;
            this.BallSprite = "Op-art-circle_Marco-Braun";

            var s = AddScalable( CreateSphere(New(),new Vector3(20f,0f,0f),25.0f) );
            AddFunctionScript(s, (ctx, v) =>
                {
                    s.C<ScaleComp>().Scale = v;
                }, new SineFunction { Amplitude = 0.5, Offset = 1.0, Frequency = 0.333 }
            );
            var pic = new PlayerInputComp();
            s.AddC(pic);
            AddScript(s, (ctx) =>
                {
                    s.C<PositionComp>().Position += pic.Direction;
                });

            CreateRotatingBall(New(), new Vector2(900f, 700f), Vector2.Zero, 0.05);
        }

    }
}
