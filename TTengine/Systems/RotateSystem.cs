// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using TTengine.Comps;

namespace TTengine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.RotateSystem)]
    public class RotateSystem : EntityComponentProcessingSystem<RotateComp>
    {

        public override void Process(Entity entity, RotateComp rc)
        {
            ProcessTime(rc);
            rc.RotateAbsPrev = rc.RotateAbs;
            rc.Rotate += rc.RotateSpeed * (float)Dt;            
        }
    }
}