// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis;
using TTengine.Comps;

namespace Game1.Levels
{
    /// <summary>
    /// The one root level that will trigger builds of all other levels / content via
    /// LevelBuilder entities. Builds are triggered when e.g. the player gets close enough to
    /// the LevelBuilder.
    /// </summary>
    public class RootLevel: Level
    {
        public override void Build()
        {
            BuildTo(LevelChannel);

            if (false)
            {
                var lev = new WeePlanetsLevel();
                CreateLevelBuilder(New(), lev, lev.BuildTestSection, 700f, 100f);
                CreateLevelBuilder(New(), lev, lev.BuildWaterPlanetSection, 400f, 100f);
            }
            else
            {
                var lev = new NovasLevel();
                CreateLevelBuilder(New(), lev, lev.BuildSection1, 400f, 100f);
            }

        }
    }
}
