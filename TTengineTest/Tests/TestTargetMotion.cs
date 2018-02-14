using System;
using Microsoft.Xna.Framework;
using TTengine.Comps;

namespace TTengineTest
{
    /// <summary>Testing the linear motion of objects on screen</summary>
    class TestTargetMotion : Test
    {
        public override void BuildAll()
        {
            BallSprite = "red-circle_frank-tschakert";
            var velo = new Vector2(0f,0f);
            var pos = new Vector2(300f, 300f);
            var ball = CreateMovingBall(New(), pos, velo );
            ball.AddC(new TargetMotionComp());
            ball.C<TargetMotionComp>().Target = new Vector3(800f, 500f, 0.9f);
            ball.C<TargetMotionComp>().TargetVelocity = 80;
        }

    }
}
