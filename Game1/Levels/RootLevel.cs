﻿// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis;
using TTengine.Comps;

namespace Game1.Levels
{
    /// <summary>
    /// The one root level that will trigger builds of all other levels / content via
    /// LevelBuilder entities. Builds are triggered when e.g. the player gets close enough.
    /// </summary>
    public class RootLevel: Level
    {
        public void Build()
        {
            Factory.BuildTo(LevelChannel);
            var lev = new WeePlanetsLevel();
            Factory.CreateLevel(lev, lev.BuildWaterPlanetSection, 400f, 100f);
            Factory.CreateLevel(lev, lev.BuildTestSection, 700f, 1400f);

        }
    }
}
