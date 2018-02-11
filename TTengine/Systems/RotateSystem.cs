// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;
using TTengine.Comps;

namespace TTengine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.RotateSystem)]
    public class RotateSystem : EntityComponentProcessingSystem<RotateComp>
    {

        public override void Process(Entity entity, RotateComp rc)
        {
            ProcessTime(rc);
            rc.Rotate += rc.RotateSpeed * Dt;            
        }
    }

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.RotateToDrawrotateSystem)]
    public class RotateToDrawrotateSystem : EntityComponentProcessingSystem<RotateComp, DrawComp>
    {

        public override void Process(Entity entity, RotateComp rotComp, DrawComp drawComp)
        {
            drawComp.DrawRotation = (float)rotComp.RotateAbs;
        }

    }

}