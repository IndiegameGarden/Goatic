
using Artemis;
using Artemis.Manager;
using Artemis.Attributes;
using Artemis.System;

namespace TTengine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.RefresherSystem)]
    class RefresherSystem: QueueSystemProcessingThreadSafe
    {
        protected override void Begin()
        {
            SetQueueProcessingLimit(250, typeof(QueueSystemProcessingThreadSafe) );
            base.Begin();
        }

        public override void Process(Entity entity)
        {
            entity.Refresh();
        }
    }
}
