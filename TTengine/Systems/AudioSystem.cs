// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using TTengine.Core;
using TTengine.Comps;
using TTMusicEngine;

using Artemis;
using Artemis.Manager;
using Artemis.Attributes;
using Artemis.System;

namespace TTengine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.AudioSystem)]
    public class AudioSystem : EntityComponentProcessingSystem<AudioComp>
    {
        public override void Process(Entity entity, AudioComp ac)
        {
            ProcessTime(ac);
        }
    }

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = SystemsSchedule.AudioSystemDraw)]
    public class AudioSystemRender : EntityComponentProcessingSystem<AudioComp>
    {
        RenderParams rp = new RenderParams();
        MusicEngine audioEngine = null;

        protected override void Begin()
        {
            audioEngine = TTGame.Instance.AudioEngine;
            if (audioEngine != null)
                audioEngine.Update(); // to be called once every frame
        }

        public override void Process(Entity entity, AudioComp ac)
        {
            rp.Time = ac.SimTime;
            rp.Ampl = ac.Ampl;
            audioEngine.Render(ac.AudioScript, rp);
        }

    }

}
