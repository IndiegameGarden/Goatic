

using System;
using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;
using TTengine.Core;
using TTengine.Comps;

namespace TTengine.Systems
{

    /// <summary>System to limit the velocity vector to MaxSpeed.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.VelocitySystem)]
    public class VelocitySystem : EntityComponentProcessingSystem<VelocityComp>
    {

        /// <summary>Processes the specified entity.</summary>>
        public override void Process(Entity entity, VelocityComp vc)
        {
            if (vc.Speed > vc.MaxSpeed) // speed limit of velocity vector
                vc.Speed = vc.MaxSpeed;

        }
    }

    /// <summary>System to set the total forces to 0 at start of each update round.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.ForcesPreSystem)]
    public class ForcesPreSystem : EntityComponentProcessingSystem<ForcesComp>
    {

        /// <summary>Processes the specified entity.</summary>>
        public override void Process(Entity entity, ForcesComp fc)
        {
            fc.Force = Vector3.Zero;
        }
    }

    /// <summary>System to limit the force vector to MaxForce.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.ForcesSystem)]
    public class ForcesSystem : EntityComponentProcessingSystem<ForcesComp>
    {

        /// <summary>Processes the specified entity.</summary>>
        public override void Process(Entity entity, ForcesComp fc)
        {
            if (fc.Magnitude > fc.MaxForce) // speed limit of velocity vector
                fc.Magnitude = fc.MaxForce;

        }
    }

    /// <summary>System to apply acceleration onto the velocity vector.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.ForcesSystem)]
    public class AccelerationSystem : EntityComponentProcessingSystem<VelocityComp,ForcesComp>
    {

        /// <summary>Processes the specified entity.</summary>>
        public override void Process(Entity entity, VelocityComp vc, ForcesComp fc)
        {
            Vector3 a = fc.Force / fc.Mass;     // acceleration
            vc.Velocity += (float)Dt * a;

        }
    }

}
