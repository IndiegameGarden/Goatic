// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using TTengine.Core;
using TTengine.Comps;
using Artemis.System;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis;

namespace TTengine.Systems
{
    /// <summary>
    /// System that updates all the elements of DrawComp, using time-based interpolation for smooth rendering/scrolling/scaling/etc.
    /// </summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = SystemsSchedule.DrawSystemDraw)]
    public class DrawSystem : EntityComponentProcessingSystem<DrawComp>
    {
        TTGame game;
        float alpha = 0;

        protected override void Begin()
        {
            this.game = TTGame.Instance;
            this.alpha = (float)(1.0 - (game.TimeLag / game.TargetElapsedTime.TotalSeconds));
        }

        public override void Process(Entity e, DrawComp bc)
        {
            
            var pc = e.C<PositionComp>();
            var rc = e.C<RotateComp>();
            var sc = e.C<ScaleComp>();
            if (pc != null) bc.DrawPosition = Vector3.Lerp(pc.PositionAbsPrev, pc.PositionAbs, alpha);
            if (rc != null) bc.DrawRotation = MathHelper.Lerp(rc.RotateAbsPrev, rc.RotateAbs, alpha);
            if (sc != null) bc.DrawScale = MathHelper.Lerp(sc.ScaleAbsPrev, sc.ScaleAbs, alpha);
        }
    }
}
