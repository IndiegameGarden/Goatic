// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;

using Microsoft.Xna.Framework;

using TTengine.Core;
using TTengine.Comps;

using Artemis;

namespace Game1
{
    public class Game1Factory: TTFactory
    {
        private static Game1Factory _instance = null;
        private Game1 _game;

        private Game1Factory(Game1 game)
        {
            _game = game;
        }

        public static Game1Factory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Game1Factory(TTGame.Instance as Game1);
                return _instance as Game1Factory;
            }
        }

        protected Random rnd = new Random();

        /// <summary>
        /// create a ball Spritelet that can be scaled
        /// </summary>
        /// <param name="radius">the relative size scaling, 1 is normal</param>
        /// <returns></returns>
        public Entity CreateBall(double radius)
        {
            Entity e = CreateSpritelet("paul-hardman_circle-four");
            e.AddComponent(new ScaleComp(radius));
            return e;
        }

        /// <summary>
        /// create an active ball with given position and random velocity and some weird (AI) behaviors
        /// </summary>
        /// <returns></returns>
        public Entity CreateHyperActiveBall(Vector2 pos)
        {
            var ball = CreateBall(0.08f + 0.07f * (float)rnd.NextDouble());

            // position and velocity set
            ball.C<PositionComp>().Position = pos;
            ball.C<VelocityComp>().Velocity = 0.2f * new Vector2((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);

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

            ball.Refresh();
            return ball;

        }

        public Entity CreateMovingTextlet(Vector2 pos, string text)
        {
            var t = CreateTextlet(text);
            t.C<PositionComp>().Position = pos;
            t.C<DrawComp>().DrawColor = Color.Black;
            t.C<VelocityComp>().Velocity = 0.2f * new Vector2((float)rnd.NextDouble() - 0.5f, (float)rnd.NextDouble() - 0.5f);
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
            var ball = CreateBall(0.96f + 0.08f * (float)rnd.NextDouble());

            // position and velocity set
            ball.C<PositionComp>().Position = pos;
            ball.C<PositionComp>().Depth = 0.5f + 0.1f * ((float)rnd.NextDouble()); // random Z position
            ball.C<VelocityComp>().Velocity2D = velo;
            ball.Refresh(); // TODO check all .Refresh() calls to see which ones are needed and which not.
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

        public static Entity CreateHypnoScreenlet()
        {
            var e = CreateFxScreenlet("Hypno");
            AddScript(e, ScriptHypno );
            return e;
        }

        static void ScriptHypno(ScriptContext ctx)
        {
            float z = 17f - 15f * (float)Math.Sin(MathHelper.TwoPi * 0.03324 * ctx.SimTime);
            var effect = ctx.Entity.C<ScreenComp>().SpriteBatch.effect;
            effect.Parameters["Zoom"].SetValue(z);
            effect.Parameters["Time"].SetValue((float)ctx.SimTime);
        }

        public static Entity CreateMandelbrotScreenlet()
        {
            var e = CreateFxScreenlet("MandelbrotJulia");
            AddScript(e, ScriptMandelbrotFx );
            return e;
        }

        static void ScriptMandelbrotFx(ScriptContext ctx)
        {
            var effect = ctx.Entity.C<ScreenComp>().SpriteBatch.effect;
            effect.Parameters["Zoom"].SetValue((float)( 3 - ctx.SimTime/20.0 ));
        }

        public static Entity CreateJuliaScreenlet()
        {
            var e = CreateFxScreenlet("MandelbrotJulia");
            var fx = e.C<ScreenComp>().SpriteBatch.effect;
            fx.CurrentTechnique = fx.Techniques[1]; // select Julia
            AddScript(e, ScriptJuliaFx);
            return e;
        }

        static void ScriptJuliaFx(ScriptContext ctx)
        {
            var effect = ctx.Entity.C<ScreenComp>().SpriteBatch.effect;
            var t = (float) ctx.SimTime;
            effect.Parameters["JuliaSeed"].SetValue( new Vector2(0.39f - t * 0.004f , -0.2f + t * 0.003f) );
        }

    }
}
