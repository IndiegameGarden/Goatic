// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using Artemis.Manager;
using Artemis.Attributes;
using Artemis.System;
using TTengine.Core;
using TTengine.Comps;

namespace TTengine.Systems
{
    /// <summary>
    /// System that runs the Update() cycle of scripts attached to Entity.
    /// </summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.ScriptSystem)]
    public class ScriptSystemUpdate : EntityComponentProcessingSystem<ScriptComp>
    {
        public override void Process(Entity entity, ScriptComp sc)
        {
            ProcessTime(sc);
            
            foreach (var script in sc.Scripts)
                script.OnUpdate(sc);
        }

    }

    /// <summary>
    /// System that runs the Draw() cycle of scripts attached to Entity.
    /// </summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = SystemsSchedule.ScriptSystemDraw)]
    public class ScriptSystemDraw : EntityComponentProcessingSystem<ScriptComp>
    {
        public override void Process(Entity entity, ScriptComp sc)
        {
            foreach (var script in sc.Scripts)
                script.OnDraw(sc);
        }
    }
}
