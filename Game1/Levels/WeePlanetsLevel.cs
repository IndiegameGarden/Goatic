// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis;
using TTengine.Comps;

namespace Game1.Levels
{
    public class WeePlanetsLevel: Level
    {
        Entity waterPlanet;
        Entity animCircle;

        public void BuildWaterPlanetSection(ScriptContext ctx)
        {
            SetAnchorPosition(ctx);
            BuildTo(BackgroundChannel);
            waterPlanet = CreateSpritelet(New(), "wee-planet-water");
            var pc = waterPlanet.C<PositionComp>();
            pc.Position = new Vector2(0f,400f); // TODO in constructor args?
            var rc = new RotateComp();
            rc.RotateSpeed = 0.1; // TODO in constructor args?
            waterPlanet.AddComponent(rc);
            Finalize(waterPlanet);

        }

        public void BuildTestSection(ScriptContext ctx)
        {
            SetAnchorPosition(ctx);
            BuildTo(LevelChannel);
            animCircle = CreateAnimatedSpritelet(New(), "animated-fluorescent-circle", 4, 8, AnimationType.NORMAL, 4);
            var ac = animCircle.C<AnimatedSpriteComp>();
            var pc = animCircle.C<PositionComp>();
            ac.MaxFrame = 29;
            pc.Position = new Vector2(100f, 100f);
            Finalize(animCircle);
        }
    }
}
