// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using TTengine.Comps;

namespace Game1.Levels
{
    public class Level
    {
        protected Game1Factory Factory = Game1.Factory;
        protected Entity BackgroundChannel = Game1.Instance.BackgroundChannel;
        protected Entity LevelChannel = Game1.Instance.LevelChannel;

        public Entity LevelOwner;

        /// <summary>
        /// Create a new Entity within the level, still disabled i.e. not yet finalized. Can be used
        /// as input to factory building methods to further customize it.
        /// </summary>
        /// <returns>New Entity with a PositionComp that is linked to the LevelOwner position.</returns>
        protected Entity New()
        {
            var e = Factory.NewDisabled();
            var pc = new PositionComp();
            // make any new entity created here a position-child of the level owning entity. This lays
            // the level content at the right position in the overall world.
            LevelOwner.C<PositionComp>().AddChild(pc);
            e.AddComponent(pc);
            return e;
        }
    }
}
