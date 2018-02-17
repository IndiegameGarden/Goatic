// (c) 2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;

namespace TTengineTest
{
    /// <summary></summary>
    class TestGeom3D : Test
    {
        public TestGeom3D()
        {
            this.BackgroundColor = Color.Black;
        }

        public override void BuildAll()
        {
            this.BallSprite = "Op-art-circle_Marco-Braun";

            // 3D sphere 1
            var s = AddScalable( CreateSphere(New(),new Vector3(BuildScreen.Center.X,BuildScreen.Center.Y,0f),250.0f) );
            s.C<GeomComp>().Geom.Texture = TTGame.Instance.Content.Load<Texture2D>("earth8k");
            AddFunctionScript(s, (ctx, v) => { s.C<ScaleComp>().Scale = (float)v; }, new SineFunction { Amplitude = 0.12, Offset = 1.0, Frequency = 0.333 } );
            PlayerInputComp pic;
            s.AddC(pic = new PlayerInputComp());
            AddScript(s, (ctx) => { s.C<PositionComp>().PositionXY += pic.Direction * (float)ctx.Dt * 250.0f; });

            // 3D sphere 2
            var s2 = CreateSphere(New(), new Vector3(BuildScreen.Center.X - 400f, BuildScreen.Center.Y, +10f), 250.0f);
            s2.AddC(pic = new PlayerInputComp());
            AddScript(s2, (ctx) => { s2.C<PositionComp>().PositionXY += -pic.Direction * (float)ctx.Dt * 250.0f; });

            // 2D ball - follows the 3D one in (x,y)
            var b = CreateRotatingBall(New(), Vector2.Zero, Vector2.Zero, 0.05f);
            AddScalable(b,2);            
            AddScript(b, (ctx) => { b.C<PositionComp>().Position = s.C<PositionComp>().Position; });
        }

    }
}
