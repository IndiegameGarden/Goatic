// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using Artemis.System;
using Artemis.Attributes;
using Artemis.Manager;
using TTengine.Core;
using TTengine.Comps;

namespace TTengine.Systems
{
    /// <summary>
    /// System to blink an Entity's DrawComp.IsVisible on and off with regular pattern.
    /// TODO consider a soft (faded) blink as well.
    /// </summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.BlinkSystem)]
    public class BlinkSystem : EntityComponentProcessingSystem<BlinkComp>
    {
        public override void Process(Entity entity, BlinkComp bc)
        {
            double tprev = bc.SimTime % bc.TimePeriod;
            ProcessTime(bc);
            double t = bc.SimTime % bc.TimePeriod;
            if (t <= bc.TimeOn)
            {
                bc.isVisible = true;
                if (tprev > bc.TimeOn)  // Blinks On
                    entity.C<DrawComp>().IsVisible = true;
            }
            else
            {
                bc.isVisible = false;
                if (tprev <= bc.TimeOn) // Blinks Off
                    entity.C<DrawComp>().IsVisible = false;
            }            
        }

    }
}
