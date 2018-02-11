// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System.Collections.Generic;
using Artemis;

/// <summary>
/// ScriptComp contains support for two main types of script:
/// 1) Delegate methods that are run as scripts OnUpdate
/// 2) IScript Objects that represent scripts with their OnUpdate/OnDraw methods
/// </summary>
namespace TTengine.Comps
{
    /// <summary>
    /// Method signature (Delegate) for scripts defined by methods/functions.
    /// </summary>
    /// <param name="sc">ScriptComp passed upon script execution, containing contextual info for script execution.</param>
    public delegate void ScriptDelegate(ScriptComp sc);

    /// <summary>
    /// A script job object for queueing or postponing script execution.
    /// </summary>
    public class ScriptJob
    {
        public ScriptDelegate Script;
        public Entity Entity;

        /// <summary>
        /// Create a new ScriptJob
        /// </summary>
        /// <param name="script">Script to run</param>
        /// <param name="ent">Entity that performs the script (for execution context)</param>
        public ScriptJob(ScriptDelegate script, Entity ent)
        {
            this.Script = script;
            this.Entity = ent;
        }
    }

    /// <summary>
    /// Interface that a script object must implement
    /// </summary>
    public interface IScript
    {
        void OnUpdate(ScriptComp sc);
        void OnDraw(ScriptComp sc);
    }

    /// <summary>
    /// The Comp that enables scripting for an Entity with one or more ordered scripts.
    /// </summary>
    public class ScriptComp: Comp
    {
        /// <summary>
        /// The Entity that this ScriptComp is attached to, used by scripts to access any Components.
        /// </summary>
        public Entity Entity = null;

        /// <summary>
        /// The list of scripts that are called every update/draw cycle
        /// </summary>
        public List<IScript> Scripts = new List<IScript>();

        /// <summary>
        /// Create new ScriptComp without any scripts yet
        /// </summary>
        /// <param name="e">Entity that this ScriptComp will be attached to</param>
        public ScriptComp(Entity e = null)
        {
            this.Entity = e;
        }     

    }
}
