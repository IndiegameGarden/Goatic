using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using System.Text;
using TTengine.Core;
using TTengine.Comps;

using Artemis;
using Artemis.Manager;
using Artemis.Attributes;
using Artemis.System;

namespace TTengine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.GeomSystem)]
    public class GeomSystem : EntityComponentProcessingSystem<GeomComp>
    {

        public override void Process(Entity entity, GeomComp sc)
        {
            ProcessTime(sc);
        }

    }

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = SystemsSchedule.GeomSystemDraw)]
    public class GeomDrawSystem : EntityComponentProcessingSystem<GeomComp, DrawComp, PositionComp>
    {
        public override void Process(Entity e, GeomComp sc, DrawComp dc, PositionComp pc)
        {
            if (!dc.IsVisible) return;

            var g = sc.Geom;
            var scr = dc.DrawScreen;
            var v = dc.DrawPosition;

            // world, view, projection , color
            //var wm = Matrix.CreateTranslation(v);
            var wm = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Left);
            if (e.HasC<ScaleComp>())    // apply object size scaling
                wm *= Matrix.CreateScale((float) e.C<ScaleComp>().Scale);
            wm *= Matrix.CreateTranslation(v); // apply position
            g.Draw(wm,scr.ViewM,scr.ProjM,dc.DrawColor);
        }
    }
}
