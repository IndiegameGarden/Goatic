// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Microsoft.Xna.Framework;

using TTengine.Core;
using TTengine.Comps;

using Artemis;

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
        public Entity CreateBall(Entity e, double radius)
        {
            CreateSpritelet(e,this.BallSprite);
            e.C<SpriteComp>().CenterToMiddle();
            e.AddComponent(new ScaleComp(radius));
            return e;
        }

        /// <summary>
        /// create an active ball with given position and random velocity and some weird (AI) behaviors
        /// </summary>
        /// <returns></returns>
        public Entity CreateMovingBall(Entity e, Vector2 pos, Vector2 velo, double radius = 1.0)
        {
            var ball = CreateBall(e,radius);

            // position and velocity set
            ball.C<PositionComp>().Position = pos;
            ball.C<PositionComp>().Depth = 0.5f + 0.1f * ((float)rnd.NextDouble()); // random Z position
            ball.C<VelocityComp>().Velocity2D = velo;
            return ball;
        }

        public Entity CreateMovingBall(Entity e, Vector3 pos, Vector2 velo)
        {
            return CreateMovingBall(e, new Vector2(pos.X, pos.Y), velo);
        }

        public Entity CreateRotatingBall(Entity e, Vector2 pos, Vector2 velo, double rotSpeed)
        {
            var ball = CreateMovingBall(e, pos, velo);
            ball.C<ScaleComp>().Scale = 0.7;
            var rc = new RotateComp();
            rc.RotateSpeed = rotSpeed;
            ball.AddComponent(rc);
            return ball;
        }

        public Entity CreateTextlet(Entity e, Vector2 pos, string text, Color col)
        {
            CreateTextlet(e,text);
            e.C<PositionComp>().Position = pos;
            e.C<PositionComp>().Depth = 0f + 0.1f * ((float)rnd.NextDouble()); // random Z position
            e.C<DrawComp>().DrawColor = col;
            e.C<ScaleComp>().Scale = 0.8;
            return e;
        }

    }

}
