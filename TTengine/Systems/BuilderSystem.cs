// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using TTengine.Comps;

namespace TTengine.Systems
{
    /// <summary>System that builds new entities in a background thread.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.BuilderSystem)]
    public class BuilderSystem : QueueSystemProcessingThreadSafe<BuildScriptDelegate>
    {
        BackgroundBuilder bgBuilder = null;

        public override void LoadContent()
        {
            SetQueueProcessingLimit(10, typeof(BuilderSystem));
            bgBuilder = new BackgroundBuilder();
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            this.IsEnabled = false;
            if (bgBuilder != null)
                bgBuilder.Stop();
            bgBuilder = null;
            base.UnloadContent();
        }

        public static void AddToQueue(BuildScriptDelegate script)
        {
            AddToQueue(script,typeof(BuilderSystem));
        }

        public override void Process(BuildScriptDelegate script)
        {
            bgBuilder.AddJob(script); // separate builder thread building
        }
    }

}
