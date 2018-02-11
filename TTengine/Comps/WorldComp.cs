// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;

namespace TTengine.Comps
{
    /// <summary>
    /// Component that contains a (separate) EntityWorld
    /// </summary>
    public class WorldComp: Comp
    {
        /// <summary>The EntityWorld that is being contained in this component</summary>
        public EntityWorld World;

        /// <summary>
        /// The time factor for this World; 1.0 is normal, < 1.0 is slower time and > 1.0 is faster time.
        /// </summary>
        public double TimeWarp = 1.0;

		/// <summary>
		/// The Screen that World renders to, or null if not rendering to a specific Screen.
		/// </summary>
		public ScreenComp Screen = null;

        public WorldComp()
        {
            this.World = new EntityWorld();
            this.World.InitializeAll(true);
        }
    }
}
