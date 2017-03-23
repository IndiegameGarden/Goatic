// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Microsoft.Xna.Framework;

using Artemis;
using TTengine.Core;
using TTengine.Comps;

using Game1.Comps;
using Game1.Levels;

namespace Game1
{
    public class Game1Factory: TTFactory
    {

        public Entity CreateLevelBuilder(Level level, BuildScriptDelegate script, float x, float y, bool isLevelOwner = false)
        {
            var e = New();
            if (isLevelOwner)
                level.LevelOwner = e;
            var pc = new PositionComp();
            pc.Position = new Vector2(x, y);
            e.AddComponent(pc);
            var lc = new LevelComp(level,script,e);
            e.AddComponent(lc);
            return e;
        }

        public Entity CreateShip(Entity e)
        {
            CreateSpritelet(e, "ball");
            e.AddComponent(new ScaleComp(1.0));
            e.AddComponent(new PlayerInputComp());
            AddScript(e, ScriptMoveByGamepad);
            return e;
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
            else if (dir.Direction.X < 0f)
                vc.X -= spd;
            else if (dir.Direction.X > 0f)
                vc.X += spd;

        }

        /// <summary>
        /// create a ball Spritelet that can be scaled
        /// </summary>
        /// <param name="radius">the relative size scaling, 1 is normal</param>
        /// <returns></returns>
        public Entity CreateBall(double radius)
        {
            Entity e = New();
            CreateSpritelet(e, "paul-hardman_circle-four");
            e.AddComponent(new ScaleComp(radius));
            return e;
        }

        /// <summary>
        /// create an active ball with given position and random velocity and some weird (AI) behaviors
        /// </summary>
        /// <returns></returns>
        public Entity CreateHyperActiveBall(Vector2 pos)
        {
            var ball = CreateBall(RandomMath.RandomBetween(0.08f,0.15f));

            // position and velocity set
            ball.C<PositionComp>().Position = pos;
            ball.C<VelocityComp>().Velocity = 0.2f * new Vector2(RandomMath.RandomUnit() - 0.5f, RandomMath.RandomUnit() - 0.5f);

            /*
            // duration of entity
            ball.AddComponent(new ExpiresComp(4 + 500 * rnd.NextDouble()));

            // Behavior Tree AI
            BTAIComp ai = new BTAIComp();
            var randomWanderBehavior = new RandomWanderBehavior(1, 6);
            ai.rootNode = new PrioritySelector(randomWanderBehavior);
            ball.AddComponent(ai);

            // Modifier to adapt scale
            TTFactory.AddModifier(ball, ScaleModifierScript);

            // another adapting scale with sine rhythm
            var s = new SineFunction();
            s.Frequency = 0.5;
            s.Amplitude = 0.25;
            s.Offset = 1;
            TTFactory.AddModifier(ball, ScaleModifierScript, s);

            // modifier to adapt rotation
            TTFactory.AddModifier(ball, RotateModifierScript);

            // set different time offset initially, per ball (for the modifiers)
            ball.GetComponent<ScriptComp>().SimTime = 10 * rnd.NextDouble();
            */

            return ball;

        }

        public Entity CreateMovingTextlet(Vector2 pos, string text)
        {
            var t = New();
            CreateTextlet(t,text);
            t.C<PositionComp>().Position = pos;
            t.C<DrawComp>().DrawColor = Color.Black;
            t.C<VelocityComp>().Velocity = 0.2f * new Vector2(RandomMath.RandomUnit() - 0.5f, RandomMath.RandomUnit() - 0.5f);
            t.C<ScaleComp>().Scale = 0.5;
            return t;
        }

        public void RotateModifierScript(ScriptContext ctx, double value)
        {
            ctx.Entity.C<DrawComp>().DrawRotation = (float)value;
        }

        /// <summary>
        /// create an active ball with given position and random velocity and some weird (AI) behaviors
        /// </summary>
        /// <returns></returns>
        public Entity CreateMovingBall(Vector2 pos, Vector2 velo)
        {
            var ball = CreateBall(RandomMath.RandomBetween(0.96f, 1.08f));

            // position and velocity set
            ball.C<PositionComp>().Position = pos;
            ball.C<PositionComp>().Depth = RandomMath.RandomBetween(0.5f,0.6f); // random Z position
            ball.C<VelocityComp>().Velocity2D = velo;
            return ball;
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

        public Entity CreateHypnoScreenlet()
        {
            var e = New();
            CreateFxScreenlet(e, "Hypno");
            AddScript(e, ScriptHypno );
            return e;
        }

        void ScriptHypno(ScriptContext ctx)
        {
            float z = 17f - 15f * (float)Math.Sin(MathHelper.TwoPi * 0.03324 * ctx.SimTime);
            var effect = ctx.Entity.C<ScreenComp>().SpriteBatch.effect;
            effect.Parameters["Zoom"].SetValue(z);
            effect.Parameters["Time"].SetValue((float)ctx.SimTime);
        }

        public Entity CreateMandelbrotScreenlet()
        {
            var e = New();
            CreateFxScreenlet(e, "MandelbrotJulia");
            AddScript(e, ScriptMandelbrotFx );
            return e;
        }

        void ScriptMandelbrotFx(ScriptContext ctx)
        {
            var effect = ctx.Entity.C<ScreenComp>().SpriteBatch.effect;
            effect.Parameters["Zoom"].SetValue((float)( 3 - ctx.SimTime/20.0 ));
        }

        public Entity CreateJuliaScreenlet()
        {
            var e = New();
            CreateFxScreenlet(e, "MandelbrotJulia");
            var fx = e.C<ScreenComp>().SpriteBatch.effect;
            fx.CurrentTechnique = fx.Techniques[1]; // select Julia
            AddScript(e, ScriptJuliaFx);
            return e;
        }

        void ScriptJuliaFx(ScriptContext ctx)
        {
            var effect = ctx.Entity.C<ScreenComp>().SpriteBatch.effect;
            var t = (float) ctx.SimTime;
            effect.Parameters["JuliaSeed"].SetValue( new Vector2(0.39f - t * 0.004f , -0.2f + t * 0.003f) );
        }

    }
}
