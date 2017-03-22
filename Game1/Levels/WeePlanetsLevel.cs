// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis;
using TTengine.Comps;

namespace Game1.Levels
{
    public class WeePlanetsLevel: Level
    {
        Entity waterPlanet;

        public void BuildWaterPlanetSection()
        {
            Factory.BuildTo(BackgroundChannel);
            waterPlanet = Factory.CreateSpritelet(New(), "wee-planet-water");
            var pc = waterPlanet.C<PositionComp>();
            pc.Position = new Vector2(0f,400f);
            var rc = new RotateComp();
            rc.RotateSpeed = 0.1;
            waterPlanet.AddComponent(rc);
            Factory.Finalize(waterPlanet);

        }

        public void BuildTestSection()
        {
            // TODO
        }
    }
}
