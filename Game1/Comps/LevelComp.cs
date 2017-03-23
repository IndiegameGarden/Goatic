﻿// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using Artemis.Interface;
using TTengine.Comps;

using Game1.Levels;

namespace Game1.Comps
{
    public class LevelComp: IComponent
    {
        public LevelComp(Level level, BuildScriptDelegate script, Entity triggerEntity)
        {
            this.Level = level;
            this.BuildScript = script;
            this.TriggerEntity = triggerEntity;
        }

        /// <summary>
        /// Flag indicating if this component can currently trigger a build (true) or not (false).
        /// Is reset to false after a single trigger happens.
        /// </summary>
        public bool CanTrigger = true;

        /// <summary>
        /// The level object that this component will build from
        /// </summary>
        public Level Level;

        /// <summary>
        /// The script within the level that this component will build, when triggered
        /// </summary>
        public BuildScriptDelegate BuildScript;

        /// <summary>
        /// The radius (distance) or less that will trigger the building action if the 
        /// triggering Entity gets close.
        /// </summary>
        public double TriggerRadius = 1000.0;

        /// <summary>
        /// The Entity that triggers the level build, if it comes close enough.
        /// </summary>
        public Entity TriggerEntity;
    }
}
