
using Artemis;
using Artemis.Manager;
using Artemis.Attributes;
using Artemis.System;

namespace TTengine.Systems
{
    /// <summary>
    /// Queueing System that performs a .Refresh() on Entities that are queued to it. This is useful
    /// to enable new Entities and their Components cleanly, not interfering with existing Component
    /// processing.
    /// </summary>
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
