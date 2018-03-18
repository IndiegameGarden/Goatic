// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Artemis;
using Artemis.System;
using Artemis.Attributes;
using Artemis.Manager;
using TTengine.Core;
using TTengine.Comps;
using TreeSharp;

namespace TTengine.Systems
{
    /// <summary>
    /// A Behavior Tree (BT) based AI system for Entities
    /// </summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.BTAISystem)]
    public class BTAISystem : EntityComponentProcessingSystem<BTAIComp>
    {
        private BTAIContext ctx = new BTAIContext();

        protected override void Begin()
        {
            // once per update-cycle, set timing in context object
            ctx.Dt = this.Dt;
        }

        public override void Process(Entity entity, BTAIComp bc)
        {
            ProcessTime(bc);

            // set context object to current situation / entity
            ctx.Entity = entity;
            ctx.BTAI = bc;

            if (bc.Root.LastStatus == null)
                bc.Root.Start(ctx);
            else if (bc.Root.LastStatus == RunStatus.Success || bc.Root.LastStatus == RunStatus.Failure)
                bc.Root.Start(ctx);
            bc.Root.Tick(ctx);

            // after every BTAI Tree execution, check which comps are enabled/disabled as a result
            foreach (var c in bc.CompsToDisable)
            {
                if (!bc.CompsToEnable.Contains(c))
                    throw new NotImplementedException();
            }
            // 'enable' request always higher priority than a 'disable' request.
            foreach (var c in bc.CompsToEnable)
            {
                throw new NotImplementedException();
            }
            bc.CompsToEnable.Clear();
            bc.CompsToDisable.Clear();
        }

    }
}
