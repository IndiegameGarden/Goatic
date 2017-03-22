
using Artemis;
using Artemis.Attributes;
using Artemis.System;
using Artemis.Manager;
using TTengine.Comps;

using Game1.Comps;

namespace Game1.Systems
{

    /// <summary>The expiration system.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.LevelBuilderSystem)]
    public class LevelBuilderSystem : EntityComponentProcessingSystem<LevelComp>
    {
        
        public override void Process(Entity entity, LevelComp lc)
        {
            // TODO
        }
    }
}