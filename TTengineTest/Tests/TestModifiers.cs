using System;
using Microsoft.Xna.Framework;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;
using Artemis.Interface;
using Artemis;

namespace TTengineTest
{
    /// <summary></summary>
    class TestModifiers : Test
    {

        public override void Create()
        {
            Factory.BallSprite = "paul-hardman_circle-four";

            // ball 1
            var velo = new Vector2(3f, 0.3f);
            var ball = Factory.CreateMovingBall(Factory.New(), new Vector2(95f, 250f), velo);

            // Modifier: adapting scale with sine rhythm
            var sineFunc = new SineFunction();
            sineFunc.Frequency = 0.5;
            sineFunc.Amplitude = 0.25;
            sineFunc.Offset = 1;
            Factory.AddModifier(ball, MyScaleModifierScript, sineFunc);

            // modifier script to adapt rotation
            Factory.AddModifier(ball, MyRotateModifierScript);

            // ball 2
            var ball2 = Factory.CreateMovingBall(Factory.New(), new Vector2(695f, 450f), velo);
            ball2.C<ScaleComp>().Scale = 0.5;

            // script with anonymous delegate code block - for rotation
            Factory.AddScript(ball2, delegate(ScriptContext ctx) { 
                    ctx.Entity.C<DrawComp>().DrawRotation = (float)ctx.SimTime; 
                });

            // TargetModifier to set its position towards a target
            //var tm = new TargetModifier<PositionComp>(delegate(PositionComp pc, Vector3 pos) { pc.Position = pos; }, 
            //                    ball2.GetComponent<PositionComp>());
            var targFunc = new MoveToTargetFunction();
            targFunc.Target = new Vector2(0f, 0f);
            targFunc.CurrentValue = ball2.C<PositionComp>().Position;
            targFunc.Speed = 40;
            Factory.AddModifier(ball2,
                delegate(ScriptContext ctx, Vector2 pos) { ctx.Entity.C<PositionComp>().Position = pos; },
                targFunc);

        }

        void MyScaleModifierScript(ScriptContext ctx, double value)
        {
            ctx.Entity.C<ScaleComp>().Scale = (0.4 + value * 0.3);            
        }

        void MyRotateModifierScript(ScriptContext ctx, double value)
        {
            ctx.Entity.C<DrawComp>().DrawRotation = (float)value;
        }

    }
}
