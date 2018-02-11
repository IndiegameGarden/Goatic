// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using TTengine.Comps;

namespace TTengine.Core
{
    /// <summary>
    /// A context object that is passed to TreeNodes in the TreeSharp based
    /// BTAISystem. Contains references to objects and information useful
    /// for the AI.
    /// </summary>
    public class BTAIContext
    {
        /// <summary>The Entity being updated</summary>
        public Entity Entity;

        /// <summary>The BTAIComp of Entity, which triggers BTAI processing</summary>
        public BTAIComp BTAI;
        
        /// <summary>Delta t, the simulation time passed since last Update() in seconds. Equal to Delta time from the EntityWorld.</summary>
        public double Dt = 0.0;

    }
}
