// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;
using TTengine.Core;
using TTengine.Comps;

namespace TTengine.Systems
{
    /// <summary>System to move entities to a target position. TODO: smooth arrival behaviour.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.TargetMoveSystem)]
    public class TargetMotionSystem : EntityComponentProcessingSystem<PositionComp, TargetMotionComp>
    {

        public override void Process(Entity entity, PositionComp posComp, TargetMotionComp targetComp)
        {
            if (!targetComp.isTargetSet)
                return;
            Vector3 v = targetComp.targetPos - posComp.Position;
            if (v.LengthSquared() > 0 ){
                Vector3 vm = v;
                vm.Normalize();
                vm *= (float)(targetComp.TargetVelocity * Dt);
                if (vm.LengthSquared() > v.LengthSquared())
                {
                    // target reached
                    targetComp.isTargetSet = false;
                    posComp.Position = targetComp.targetPos;
                }
                else
                {
                    posComp.Position += vm;
                }
            }
        }
    }

}
