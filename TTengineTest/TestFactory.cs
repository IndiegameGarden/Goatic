using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Behaviors;
using TTengine.Modifiers;
using TTengine.Util;

using Artemis;
using Artemis.Interface;
using TreeSharp;

namespace TTengineTest
{
    /// <summary>
    /// Factory to create new game-specific entities
    /// </summary>
    public class TestFactory: TTFactory
    {
        /// <summary>Can change here the sprite name used for creating all Ball type entities</summary>
        public string BallSprite = "red-circle_frank-tschakert";

        protected Random rnd = new Random();

        /// <summary>
        /// create a ball Spritelet that can be scaled
        /// </summary>
        /// <param name="radius">the relative size scaling, 1 is normal</param>
        /// <returns></returns>
        public Entity CreateBall(double radius)
        {
            Entity e = CreateSpritelet(this.BallSprite);
            e.C<SpriteComp>().CenterToMiddle();
            e.AddComponent(new ScaleComp(radius));
            return e;
        }

        /// <summary>
        /// create an active ball with given position and random velocity and some weird (AI) behaviors
        /// </summary>
        /// <returns></returns>
        public Entity CreateMovingBall(Vector2 pos, Vector2 velo)
        {
            var ball = CreateBall(0.96f + 0.08f * (float)rnd.NextDouble());

            // position and velocity set
            ball.C<PositionComp>().Position = pos;
            ball.C<PositionComp>().Depth = 0.5f + 0.1f * ((float)rnd.NextDouble()); // random Z position
            ball.C<VelocityComp>().Velocity2D = velo;
            return ball;
        }

        public Entity CreateMovingBallEntityDisabled(Vector2 pos, Vector2 velo, double radius)
        {
            Entity e = CreateEntity();
            e.IsEnabled = false;
            e.AddComponent(new PositionComp());
            e.AddComponent(new VelocityComp());
            e.AddComponent(new DrawComp(BuildScreen));
            var spriteComp = new SpriteComp(this.BallSprite);
            e.AddComponent(spriteComp);
            e.C<SpriteComp>().CenterToMiddle();
            e.AddComponent(new ScaleComp(radius));
            return e;
        }

        public Entity CreateMovingBall(Vector3 pos, Vector2 velo)
        {
            return CreateMovingBall(new Vector2(pos.X, pos.Y), velo);
        }

        public Entity CreateRotatingBall(Vector2 pos, Vector2 velo, double rotSpeed)
        {
            var ball = CreateMovingBall(pos, velo);
            ball.C<ScaleComp>().Scale = 0.7;
            var rc = new RotateComp();
            rc.RotateSpeed = rotSpeed;
            ball.AddComponent(rc);
            return ball;
        }

        public Entity CreateTextlet(Vector2 pos, string text, Color col)
        {
            var txt = CreateTextlet(text);
            txt.C<PositionComp>().Position = pos;
            txt.C<PositionComp>().Depth = 0f + 0.1f * ((float)rnd.NextDouble()); // random Z position
            txt.C<DrawComp>().DrawColor = col;
            txt.C<ScaleComp>().Scale = 0.8;
            return txt;
        }

    }

}
