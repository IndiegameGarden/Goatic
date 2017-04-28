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
    ///
    /// </summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.InputToMotionSystem)]
    public class InputToMotionSystem : EntityComponentProcessingSystem<PlayerInputComp,InputToMotionComp>
    {
        
        public override void Process(Entity entity, PlayerInputComp pic, InputToMotionComp imc)
        {
            var vc = entity.C<VelocityComp>();
            float spd = (float)(imc.Speed * this.Dt);

            if (pic.Direction.Y < 0f)
                vc.Y -= spd;
            else if (pic.Direction.Y > 0f)
                vc.Y += spd;
            else if (pic.Direction.X < 0f)
                vc.X -= spd;
            else if (pic.Direction.X > 0f)
                vc.X += spd;
        }
    }
}
