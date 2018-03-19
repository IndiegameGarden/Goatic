// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Artemis;
using TreeSharp;
using TTengine.Core;
using TTengine.Comps;
using TTengine.Util;

namespace TTengine.Behaviors
{
    /// <summary>
    /// Behavior to avoid any other entity.
    /// </summary>
    public class AvoidBehavior : TreeNode
    {
        public AvoidBehavior(float minDistance, float maxForce, List<Entity> avoidEntities)
        {
            this.MinDistance = minDistance;
            this.MaxForce = maxForce;
            this.AvoidEntities = avoidEntities;
        }

        public Vector2 CurrentDirection = Vector2.Zero;

        public float MinDistance = 0f;

        public float MaxForce = 1f;

        public List<Entity> AvoidEntities = new List<Entity>();

        public override IEnumerable<RunStatus> Execute(object context)
        {
            BTAIContext ctx = context as BTAIContext;

            var w = ctx.Entity.entityWorld;
            var me = ctx.Entity;
            var pme = me.C<PositionComp>().Position;

            Vector3 f = Vector3.Zero;   // force vector
            bool isAvoid = false;
            foreach(Entity e in AvoidEntities)
            {
                if (e == me) continue;
                var pe = e.C<PositionComp>().Position;
                float dist = Vector3.Distance(pe, pme);
                if (dist < MinDistance)
                {
                    isAvoid = true;
                    if (dist > 0f)
                        f += 1/(dist*dist) * (pme - pe);    // TODO set linear, quadratic, maxForce, etc avoidance.
                    else
                        f = RandomMath.RandomDirection3();
                }
            }

            if (!isAvoid)
            {
                this.CurrentDirection = Vector2.Zero;
                yield return RunStatus.Failure;
            }

            // normalize the avoidance direction vector
            f.Normalize();
            this.CurrentDirection = TTUtil.Vec2(f);
            f *= MaxForce;

            // set it as my force
            me.C<ForcesComp>().Force += f;

            yield return RunStatus.Success;
        }

    }
}
