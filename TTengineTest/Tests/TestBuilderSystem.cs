using System;
using Microsoft.Xna.Framework;
using System.Threading;
using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;
using Artemis;
using Artemis.Interface;

namespace TTengineTest
{
    /// <summary>Test the BuilderSystem basic operation</summary>
    class TestBuilderSystem : Test
    {

        public override void Create()
        {
            // add regular content  (non-builder created)
            Test t = new TestSphereCollision();
            t.Create();

            // add builder entity (test building in background thread)
            // this content is added while the 'game' plays
            var e = TestFactory.CreateEntity();
            e.AddComponent(new BuilderComp(TestBuilderScript1));
        }

        void TestBuilderScript1()
        {
            TestFactory.BuildTo(Channel);
            Random rnd = new Random();
            Factory.BallSprite = "paul-hardman_circle-four";
            for (int i=0; i < 185; i++)
            {
                Vector2 pos = new Vector2(1400f * (float)rnd.NextDouble(), 1000f * (float)rnd.NextDouble());
                Vector2 vel = new Vector2(6f * (float)rnd.NextDouble() - 3f, 6f * (float)rnd.NextDouble() - 3f);
                Entity e = Factory.CreateMovingBallEntityDisabled(pos,vel, 0.5 + 0.5*rnd.NextDouble());
                Thread.Sleep(2);
                //e.IsEnabled = true;
                //e.Refresh();
            }
        }

    }
}
