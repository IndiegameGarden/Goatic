// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using TTengine.Core;
using TTengine.Comps;
using TTengine.Behaviors;
using TreeSharp;
using Artemis;
using System.Collections.Generic;

namespace TTengineTest
{
    /// <summary>
    /// Basic test of the Treesharp Behavior Tree AI system
    /// </summary>
    class TestBTAI : Test
    {

        public override void BuildAll()
        {
            List<Entity> avoidList = new List<Entity>();
            const int   N_BIGBALLS = 20,
                        N_SMALLBALLS = 60;

            // 10 wandering balls
            for (int i = 0; i < N_BIGBALLS; i++)
            {
                var ball = CreateBall(New(), 1f);
                ball.C<PositionComp>().Position = RandomMath.RandomPosition(BuildScreen.Width, BuildScreen.Height);
                ball.C<VelocityComp>().VelocityXY = RandomMath.RandomDirection() * 60f;

                // Behavior Tree AI
                BTAIComp ai = new BTAIComp();
                var randomWanderBehavior = new RandomWanderBehavior(1, 6);
                ai.Root = new PrioritySelector(randomWanderBehavior);
                ball.AddC(ai);

                avoidList.Add(ball);
            }

            // avoiding balls
            List<Entity> avoidList2 = new List<Entity>();
            this.BallSprite = "red-circle";
            for (int i = 0; i < N_SMALLBALLS; i++)
            {
                var ball = CreateBall(New(), 0.1f);             
                ball.C<PositionComp>().Position = RandomMath.RandomPosition(BuildScreen.Width, BuildScreen.Height);

                // Behavior Tree AI: avoid big balls with hi prio, avoid small balls with lower prio and slower.
                BTAIComp ai = new BTAIComp();
                ai.Root = new PrioritySelector(new TreeNode[] {
                                        new AvoidBehavior(minDistance: 160f, maxVelocity: 320f, avoidEntities: avoidList),
                                        new AvoidBehavior(minDistance: 80f, maxVelocity: 120f, avoidEntities: avoidList2)
                              } );
                ball.AddC(ai);

                avoidList2.Add(ball);
            }
        }
    }
}
