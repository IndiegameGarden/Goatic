﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;
using Artemis.Interface;

//using Tao.Sdl;

namespace TTengineTest
{
    /// <summary>Testing the Gamepad (XInput type only) basics</summary>
    class TestGamepad : Test
    {
        public override void Create()
        {
            var b = Factory.CreateMovingBall(Factory.New(), Factory.BuildScreen.Center, Vector2.Zero);
            b.AddC(new PlayerInputComp());
            Factory.AddScript(b, ScriptMoveByGamepad);
        }

        void ScriptMoveByGamepad(ScriptContext ctx)
        {
            var e = ctx.Entity;
            var vc = e.C<VelocityComp>();
            float spd = (float)(195 * ctx.Dt);

            var dir = e.C<PlayerInputComp>();

            if (dir.Direction.Y < 0f)
                vc.Y -= spd;
            else if (dir.Direction.Y > 0f)
                vc.Y += spd;
            else if (dir.Direction.X < 0f )
                vc.X -= spd;
            else if (dir.Direction.X > 0f )
                vc.X += spd;

        }
    }
}
