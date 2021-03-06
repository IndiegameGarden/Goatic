﻿// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TreeSharp;
using TTengine.Core;
using TTengine.Comps;

namespace TTengine.Behaviors
{
    /// <summary>
    /// Random wandering behavior implemented by periodic direction change of Entity.
    /// Direction changes at random moments within a defined change interval.
    /// </summary>
    public class RandomWanderBehavior : TreeNode
    {
        /// <summary>
        /// Create new random wandering behavior
        /// </summary>
        /// <param name="minDirectionChangeTime">the lowest time value (seconds) of the random change interval.</param>
        /// <param name="maxDirectionChangeTime">the highest time value (seconds) of the random change interval.</param>
        public RandomWanderBehavior(double minDirectionChangeTime, double maxDirectionChangeTime)
        {
            this.MinDirectionChangeTime = minDirectionChangeTime;
            this.MaxDirectionChangeTime = maxDirectionChangeTime;
        }

        /// <summary>
        /// Current direction the Entity is wandering to
        /// </summary>
        public Vector2 CurrentDirection = Vector2.Zero;

        /// <summary>the lowest time value (seconds) of the random change interval. Can be modified during operation.</summary>
        public double MinDirectionChangeTime;

        /// <summary>the highest time value (seconds) of the random change interval. Can be modified during operation.</summary>
        public double MaxDirectionChangeTime;


        double dirChangeTime = 0f;
        double timeSinceLastChange = 0f;
        
        public override IEnumerable<RunStatus> Execute(object context)
        {
            BTAIContext ctx = context as BTAIContext;

            // time keeping
            timeSinceLastChange += ctx.Dt;

            // direction changing
            if (timeSinceLastChange >= dirChangeTime)
            {
                timeSinceLastChange = 0f;
                // TODO: define a double functino also
                dirChangeTime = (double) RandomMath.RandomBetween((float)MinDirectionChangeTime, (float)MaxDirectionChangeTime);
                // TODO: length-preservation in VelocityComp
                var v = ctx.Entity.C<VelocityComp>().Velocity;
                CurrentDirection = RandomMath.RandomDirection() * v.Length();
                OnExecute(ctx);
            }

            yield return RunStatus.Success;
        }

        /// <summary>
        /// the external execution of the behavior, can be overridden. Default implementation sets the
        /// new CurrentDirection into VelocityComp of the Entity
        /// </summary>
        /// <param name="context">BT Entity/processing information</param>
        protected virtual void OnExecute(BTAIContext context)
        {
            context.Entity.C<VelocityComp>().VelocityXY = new Vector2(CurrentDirection.X, CurrentDirection.Y);
        }


    }
}
