// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using Artemis.Attributes;
using Artemis.System;
using Artemis.Manager;
using TTengine.Comps;
using TTengine.Systems;

using Game1.Comps;

namespace Game1.Systems
{

    /// <summary>
    /// Level building system. LevelComps can be triggered by an Entity getting close,
    /// which then starts a build
    /// </summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.LevelBuilderSystem)]
    public class LevelBuilderSystem : EntityComponentProcessingSystem<LevelComp,PositionComp,ScriptComp>
    {
        
        public override void Process(Entity entity, LevelComp lc, PositionComp pc, ScriptComp sc)
        {
            if (lc.CanTrigger)          // check if ready to trigger
            {
                var pct = lc.TriggerEntity.C<PositionComp>(); // the trigger's position
                float dist = (pct.PositionAbs - pc.PositionAbs).Length();   // calc distance
                if (dist <= lc.TriggerRadius)   // if close enough 
                {
                    lc.CanTrigger = false;      // prevent next round trigger
                    var job = new ScriptJob(lc.BuildScript, entity );
                    ScriptThreadedSystem.AddToQueue(job);       // put script in bg job processing queue
                }
            }
        }
    }
}
