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
    public class BuilderSystem : EntityComponentProcessingSystem<BuilderComp>
    {
        BackgroundBuilder bgBuilder = null;

        public override void LoadContent()
        {
            bgBuilder = new BackgroundBuilder(this);
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

        /*
        protected override bool CheckProcessing()
        {
            bool c = base.CheckProcessing();
            if (!c)
                return false;
            // don't add new jobs while the builder is busy.
            if (bgBuilder.IsBusy())
                return false;
            else
                return true;
        }
        */

        public override void Process(Entity entity, BuilderComp bc)
        {
            // add any active buildercomp as a new job, if not busy and if not already triggered
            if (!bc.HasTriggered /*&& !bgBuilder.IsBusy()*/ )
            {
                bc.HasTriggered = true; // flag so it is not triggered again.
                bgBuilder.AddJob(bc); // separate builder thread building
                //bc.BuildScript(); // test: in-current-thread building
            }

        }
    }

}
