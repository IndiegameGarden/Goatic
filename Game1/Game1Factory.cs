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

        public Entity CreateLevelBuilder(Level level, ScriptDelegate script, float x, float y)
        {
            var e = New();
            var pc = new PositionComp(x,y);
            e.AddC(pc);
            var lc = new LevelComp(level,script,e);
            e.AddC(lc);
            return e;
        }

        public Entity CreateShip(Entity e)
        {
            CreateSprite(e, "ball");
            e.AddC(new ScaleComp(2.0));
            e.AddC(new PlayerInputComp());
            e.AddC(new InputToMotionComp());
            return e;
        }

        public Entity CreateNovaBall(float x, float y, int type=1)
        {
            Entity fx = CreateFxScreenlet(New(), "Nova");
            Entity nov = CreateSprite(New(), "supernova"+type);
            nov.C<SpriteComp>().CenterToMiddle();
            var sc = new ScaleComp(2);
            nov.AddC(sc);
            var pc = nov.C<PositionComp>();
            pc.Position = new Vector2(x, y);
            var rc = new RotateComp();
            rc.RotateSpeed = 0.1; // TODO in constructor args?
            nov.AddC(rc);
            nov.C<DrawComp>().DrawScreen = fx.C<ScreenComp>(); // draw nova onto the Shader/FX screen
            return nov;
        }

        /// <summary>
        /// create a ball Sprite that can be scaled
        /// </summary>
        /// <param name="radius">the relative size scaling, 1 is normal</param>
        /// <returns></returns>
        public Entity CreateBall(double radius)
        {
            Entity e = New();
            CreateSprite(e, "paul-hardman_circle-four");
            e.AddC(new ScaleComp(radius));
            return e;
        }

        public Entity CreateMovingTextlet(Vector2 pos, string text)
        {
            var t = New();
            CreateText(t,text);
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
        /// create a moving ball with given position and velocity
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
            ball.AddC(rc);
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

        public Entity CreateJuliaFxSprite()
        {            
            var e = CreateFxSprite(New(), "MandelbrotJulia");
            var fx = e.C<ScreenComp>().SpriteBatch.effect;
            fx.CurrentTechnique = fx.Techniques[1]; // select Julia
            AddScript(e, ScriptJuliaFx);
            return e;
        }

        void ScriptJuliaFx(ScriptContext ctx)
        {
            var effect = ctx.Entity.C<ScreenComp>().SpriteBatch.effect;
            var t = (float) ctx.SimTime;
            effect.Parameters["JuliaSeed"].SetValue( new Vector2(0.39f + t * 0.004f , -0.2f + t * 0.003f) );
        }

    }
}
