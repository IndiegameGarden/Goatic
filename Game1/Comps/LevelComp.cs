

using Artemis.Interface;
using TTengine.Comps;

using Game1.Levels;

namespace Game1.Comps
{
    public class LevelComp: IComponent
    {
        public LevelComp(Level level, BuildScriptDelegate script)
        {
            this.Level = level;
            this.BuildScript = script;
        }

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
        /// player gets close.
        /// </summary>
        public double TriggerRadius = 1000.0;
    }
}
