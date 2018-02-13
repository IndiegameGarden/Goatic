using System;
using System.Collections.Generic;

using System.Text;
using TTengine.Core;
using TTengine.Comps;

using Artemis;
using Artemis.Manager;
using Artemis.Attributes;
using Artemis.System;

namespace TTengine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.ScaleSystem)]
    public class ScaleSystem : EntityComponentProcessingSystem<ScaleComp>
    {
        public override void Process(Entity entity, ScaleComp sc)
        {
            ProcessTime(sc);
            sc.ScaleAbsPrev = sc.ScaleAbs;
            // scaling logic towards target
            if (sc.ScaleSpeed > 0)
            {
                if (sc.Scale < sc.ScaleTarget)
                {
                    sc.Scale += sc.ScaleSpeed * (sc.ScaleTarget - sc.Scale);
                    if (sc.Scale > sc.ScaleTarget)
                    {
                        sc.Scale = sc.ScaleTarget;
                    }
                }
                else if (sc.Scale > sc.ScaleTarget)
                {
                    sc.Scale += sc.ScaleSpeed * (sc.ScaleTarget - sc.Scale);
                    if (sc.Scale < sc.ScaleTarget)
                    {
                        sc.Scale = sc.ScaleTarget;
                    }
                }
            }
        }

    }

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.ScaleSystemAbs)]
    public class ScaleSystemAbs : EntityComponentProcessingSystem<ScaleComp>
    {
        public override void Process(Entity entity, ScaleComp sc)
        {
            if (sc._isScaleAbsSet) return;   // skip processing if already processed through parent/child hierarchy.

            if (sc.Parent == null)
                sc._scaleAbs = sc.Scale;
            else
                sc._scaleAbs = sc.Scale *  (sc.Parent as RotateComp).RotateAbs;
            sc._isScaleAbsSet = true;
        }
    }

}