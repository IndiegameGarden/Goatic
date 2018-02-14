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
            rc._isRotateAbsSet = false; // reset the 'set' marker for the below system.            
        }
    }

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.RotateSystemAbs)]
    public class RotateSystemAbs : EntityComponentProcessingSystem<RotateComp>
    {

        public override void Process(Entity entity, RotateComp rc)
        {
            if (rc._isRotateAbsSet) return;   // skip processing if already processed through parent/child hierarchy.

            if (rc.Parent == null)
                rc._rotateAbs = rc.Rotate;
            else
                rc._rotateAbs = rc.Rotate + (rc.Parent as RotateComp).RotateAbs;
            rc._isRotateAbsSet = true;
        }
    }
}