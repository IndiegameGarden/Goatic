// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System.Collections.Generic;
using Artemis;
using TreeSharp;

namespace TTengine.Comps
{
    /// <summary>
    /// Behavior Tree (BT) AI component that specifies which BT AI behaviors are enabled for the Entity
    /// </summary>
    public class BTAIComp: Comp
    {

        /// <summary>The root of the Behavior Tree, defined using a slightly modified TreeSharp framework. See 'TreeSharp'
        /// namespace classes, and online documentation http://bit.ly/18ihNDz </summary>
        public TreeNode Root;

        internal HashSet<Comp> CompsToEnable = new HashSet<Comp>();
        internal HashSet<Comp> CompsToDisable = new HashSet<Comp>();

        /// <summary>To let an AI (BT node) indicate a required Comp to enable in this Entity</summary>
        /// <param name="comp">The component that should be enabled, as a result of a BTAI decision</param>
        public void EnableComp(Comp comp)
        {
            CompsToEnable.Add(comp);
        }

        /// <summary>To let an AI (BT node) indicate a non-required Comp for this Entity</summary>
        /// <param name="comp">The component that should be disabled, as a result of a BTAI decision, BUT ONLY
        /// if there are no other AI states active that would require this Component.</param>
        public void DisableComp(Comp comp)
        {
            CompsToDisable.Add(comp);
        }

    }
}
