using System;
using System.Collections.Generic;

using System.Text;
using TTengine.Core;
using Artemis;

/// <summary>
/// File contains support for two main types of script:
/// 1) Delegate methods that are run as scripts OnUpdate
/// 2) IScript Objects that represent scripts with their OnUpdate/OnDraw methods
/// </summary>
namespace TTengine.Comps
{
    /// <summary>
    /// Method signature (Delegate) for scripts
    /// </summary>
    /// <param name="ctx">script context supplied during script execution</param>
    public delegate void ScriptDelegate(ScriptContext ctx);

    /// <summary>
    /// The context object passed to scripts when they are run
    /// </summary>
    public class ScriptContext
    {       
        /// <summary>Amount of time active in simulation of the parent ScriptComp or Entity, in seconds.
        /// Value may be tweaked by others (e.g. by a script, modifier, etc.).</summary>
        public double SimTime = 0;

        /// <summary>Delta time of the last Update() simulation step performed or 0 if not specified</summary>
        public double Dt = 0;

        /// <summary>
        /// The Entity that the script is attached to, or triggered by.
        /// </summary>
        public Entity Entity = null;

        /// <summary>
        /// Create new instance of ScriptContext with give values
        /// </summary>
        /// <param name="simTime"></param>
        /// <param name="dt"></param>
        /// <param name="ent"></param>
        public ScriptContext(double simTime = 0, double dt = 0, Entity ent = null)
        {
            this.SimTime = simTime;
            this.Dt = dt;
            this.Entity = ent;
        }
    }

    /// <summary>
    /// A script job consisting of a script and the context object that will be passed to it.
    /// Used for queueing or postponing script execution.
    /// </summary>
    public class ScriptJob
    {
        public ScriptDelegate Script;
        public ScriptContext Ctx;

        public ScriptJob(ScriptDelegate script, ScriptContext ctx )
        {
            this.Script = script;
            this.Ctx = ctx;
        }
    }

    /// <summary>
    /// Interface that a script object must implement
    /// </summary>
    public interface IScript
    {
        void OnUpdate(ScriptContext context);
        void OnDraw(ScriptContext context);
    }

    /// <summary>
    /// The Comp that enables scripting for an Entity with one or more ordered scripts.
    /// </summary>
    public class ScriptComp: Comp
    {
        /// <summary>Simulation time counter that is used by scripts, passed via ScriptContext</summary>
        public double SimTime;

        /// <summary>
        /// The scripts that are called every update/draw cycle
        /// </summary>
        public List<IScript> Scripts = new List<IScript>();

        /// <summary>
        /// Create new ScriptComp without any scripts yet
        /// </summary>
        public ScriptComp()
        {        
        }

        /// <summary>
        /// Create new ScriptComp with a single script already added
        /// </summary>
        /// <param name="script">script to Add initially</param>
        public ScriptComp(IScript script)
        {
            Add(script);
        }

        /// <summary>
        /// Add a new IScript script to execute to this component.
        /// </summary>
        /// <param name="script">The IScript to add and execute in next updates.</param>
        public void Add(IScript script)
        {
            this.Scripts.Add(script);
        }

    }
}
