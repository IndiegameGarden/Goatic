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
        public AvoidBehavior(float minDistance, float maxVelocity, List<Entity> avoidEntities)
        {
            this.MinDistance = minDistance;
            this.MaxVelocity = maxVelocity;
            this.AvoidEntities = avoidEntities;
        }

        public Vector2 CurrentDirection = Vector2.Zero;

        public float MinDistance = 0f;

        public float MaxVelocity = 1f;

        public List<Entity> AvoidEntities = new List<Entity>();

        public override IEnumerable<RunStatus> Execute(object context)
        {
            BTAIContext ctx = context as BTAIContext;

            var w = ctx.Entity.entityWorld;
            var me = ctx.Entity;
            var pme = me.C<PositionComp>().Position;

            Vector3 v = Vector3.Zero;
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
                        v += 1/(dist*dist) * (pme - pe);
                    else
                        v = RandomMath.RandomDirection3();
                }
            }

            if (!isAvoid)
            {
                me.C<VelocityComp>().Velocity = Vector3.Zero;
                this.CurrentDirection = Vector2.Zero;
                yield return RunStatus.Failure;
            }

            // normalize the avoidance direction vector
            v.Normalize();
            this.CurrentDirection = TTUtil.Vec2(v);
            v *= MaxVelocity;

            // set it as my velocity
            me.C<VelocityComp>().Velocity = v;

            yield return RunStatus.Success;
        }

    }
}
