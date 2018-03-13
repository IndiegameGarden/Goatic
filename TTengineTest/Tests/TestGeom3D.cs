// (c) 2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;

namespace TTengineTest
{
    /// <summary>Test GeomSystem for 3D objects, interworking with 2D sprites</summary>
    class TestGeom3D : Test
    {
        public TestGeom3D()
        {
            this.BackgroundColor = Color.Black;
        }

        public override void BuildAll()
        {
            this.BallSprite = "Op-art-circle_Marco-Braun";

            // 3D sphere 1: Earth
            var s = CreateSphere(New(), pos: new Vector3(BuildScreen.Center.X,BuildScreen.Center.Y,0f), diameter : 550.0f, tesselation : 24);
            s.AddC<ScaleComp>(new ScaleComp());
            s.C<GeomComp>().Texture = Content.Load<Texture2D>("earth8k");
            s.AddC( new RotateComp() { RotateSpeed = 0.04f } );
            AddFunctionScript(s, (ctx, v) => { s.C<ScaleComp>().Scale = (float)v; }, new SineFunction { Amplitude = 0.12, Offset = 1.0, Frequency = 0.04 } );
            PlayerInputComp pic;
            s.AddC(pic = new PlayerInputComp());
            AddScript(s, (ctx) => { s.C<PositionComp>().PositionXY += pic.Direction * (float)ctx.Dt * 250.0f; });

            // 3D sphere 2: Moon orbits
            float radius = 500f;
            var s2 = CreateSphere(New(), pos : Vector3.Zero, diameter: 50.0f);
            s.C<PositionComp>().AddChild(s2.C<PositionComp>()); // Moves relative to parent sphere 1
            AddScript(s2, (ctx) => { s2.C<PositionComp>().Position = radius * new Vector3( (float)Math.Sin((float)ctx.SimTime) , 0f , (float)Math.Cos((float)ctx.SimTime)); });

            // 2D ball - follows the 3D one in (x,y)
            var b = CreateRotatingBall(New(), new Vector2(-300f,-300f), Vector2.Zero, 0.12f);
            b.AddC<ScaleComp>(new ScaleComp(0.7f));
            s.C<PositionComp>().AddChild(b.C<PositionComp>());
            AddScript(b, (ctx) => { b.C<PositionComp>().Position.X = 400f * (float)Math.Sin(0.5f* (float)ctx.SimTime); } );

            // 3D cube
            var c = CreateCube(New(), pos: new Vector3(100f, 100f, 50f), width: 85.0f);
            c.AddC(new RotateComp() { RotateSpeed = 0.22f });
            c.C<GeomComp>().Texture = Content.Load<Texture2D>("earth8k");

            // tesselated spheres with repeated texture
            for (int i=0; i <= 16; i+=2 )
            {
                var ts = CreateSphere(New(), pos: new Vector3(200f + i * 100f, 800f, 650f), diameter: 150f, tesselation: 3 + i, textureRepeat: 8);
                ts.C<GeomComp>().Texture = Content.Load<Texture2D>("checkerboard");

                var ts2 = CreateSphere(New(), pos: new Vector3(200f + i * 100f, 950f, 650f), diameter: 150f, tesselation: 3 + i, textureRepeat: 1);
                ts2.C<GeomComp>().Texture = Content.Load<Texture2D>("earth8k");
            }
        }

    }
}
