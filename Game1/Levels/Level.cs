// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis;
using TTengine.Comps;

namespace Game1.Levels
{
    /// <summary>
    /// A level class, containing level-specific build scripts (for building level sections) and
    /// level-specific factory methods for creating Entities. By default Entities in the Level are
    /// creating at a position relative to its AnchorPosition.
    /// </summary>
    public abstract class Level: Game1Factory
    {
        /// <summary>
        /// The background channel to which level's background Entities are built. By default, equals
        /// the game's default BackgroundChannel.
        /// </summary>
        protected Entity BackgroundChannel = Game1.Instance.BackgroundChannel;

        /// <summary>
        /// The foreground (level) channel to which level's foreground Entities are built. By default, equals
        /// the game's default LevelChannel.
        /// </summary>
        protected Entity LevelChannel = Game1.Instance.LevelChannel;

        /// <summary>
        /// Relative position to which newly created Entities in this level are anchored (only on 
        /// creation time). Build scripts may shift the anchor position to and fro. A LevelComp
        /// (background builder object) will typically set the AnchorPosition initially.
        /// </summary>
        protected Vector2 AnchorPosition = Vector2.Zero;

        /// <summary>
        /// Called at start of each build script in order to set the AnchorPosition to
        /// the current position of the Entity that triggers/spawns the building of this
        /// level section. This will modify the AnchorPosition of the Level.
        /// </summary>
        /// <param name="ctx">The context that was used to call the script that calls this method.</param>
        protected void SetAnchorPosition(ScriptContext ctx)
        {
            AnchorPosition = ctx.Entity.C<PositionComp>().PositionAbs;
        }

        // Hijack the Finalize method to allow shifting new entities positions by AnchorPosition
        public override void Finalize(Entity e)
        {
            if (e.HasC<PositionComp>())
                e.C<PositionComp>().Position += AnchorPosition;
            base.Finalize(e);            
        }

        public virtual void Build()
        {
            // can be overridden with actual code
        }

    }
}
