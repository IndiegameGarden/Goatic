// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using TTengine.Comps;

namespace TTengineTest
{
    /// <summary>Testing the Gamepad and keyboard user input basics</summary>
    class TestGamepad : Test
    {
        public override void BuildAll()
        {
            var b = CreateMovingBall(New(), BuildScreen.Center, Vector2.Zero);
            b.AddC(new PlayerInputComp());
            AddScript(b, ScriptMoveByGamepad);
            AddScript(b, ScriptBounceBorders);
        }

        void ScriptMoveByGamepad(ScriptComp ctx)
        {
            var e = ctx.Entity;
            var vc = e.C<VelocityComp>();
            var dir = e.C<PlayerInputComp>();

            float spd = (float)(195 * ctx.Dt);

            if (dir.Direction.Y < 0f)
                vc.Y -= spd;
            else if (dir.Direction.Y > 0f)
                vc.Y += spd;
            else if (dir.Direction.X < 0f )
                vc.X -= spd;
            else if (dir.Direction.X > 0f )
                vc.X += spd;

        }

        void ScriptBounceBorders(ScriptComp ctx)
        {
            var e = ctx.Entity;
            var vc = e.C<VelocityComp>();
            var pc = e.C<PositionComp>();
            var scr = e.C<DrawComp>().DrawScreen;

            if (pc.Position.X < 0f)
            {
                pc.Position.X = 0f;
                vc.Velocity.X = -vc.Velocity.X;
            }
            else if (pc.Position.X > scr.Width)
            {
                pc.Position.X = scr.Width;
                vc.Velocity.X = -vc.Velocity.X;
            }
            if (pc.Position.Y < 0f)
            {
                pc.Position.Y = 0f;
                vc.Velocity.Y = -vc.Velocity.Y;
            }
            else if (pc.Position.Y > scr.Height)
            {
                pc.Position.Y = scr.Height;
                vc.Velocity.Y= -vc.Velocity.Y;
            }

        }
    }
}
