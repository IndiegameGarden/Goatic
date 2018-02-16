// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System.Collections.Generic;
using Artemis.Interface;

namespace Artemis
{
    /// <summary>
    /// optional base class for any components that implement IComponent. It provides 
    /// parent/child mutual component relations and SimTime keeping per component.
    /// TODO can be extended with OnNewParent() event.
    /// </summary>
    public abstract class Comp: IComponent
    {
        /// <summary>Children components of this component; if null there are none (yet)</summary>
        public List<Comp> Children = null;

        /// <summary>Simulation time counter for this component, where 0 is time of creation.</summary>
        public double SimTime = 0.0;

        /// <summary>Delta time used in last simulation step for this Component, or 0 if not yet simulated or if paused.</summary>
        public double Dt = 0.0;

        /// <summary>If true, the Component is paused and its SimTime does not advance.</summary>
        public bool IsPaused = false;

        private Comp _parent = null;

        /// <summary>
        /// The parent component of this one, or null if none.
        /// </summary>
        public Comp Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Add a new Comp as a child of this one. If c already is a child of someone, it gets removed as child there first. 
        /// </summary>
        /// <param name="c">The Comp to add as child.</param>
        public void AddChild(Comp c)
        {
            if (Children == null)
                Children = new List<Comp>();

            if (!Children.Contains(c))
            {
                if (c._parent != null && c._parent != this)
                    c._parent.RemoveChild(c);
                Children.Add(c);
            }
            c._parent = this;
        }

        /// <summary>
        /// Remove a Comp c as a child of this one. If c is not a child, does nothing. Parent of c
        /// will be set to null.
        /// </summary>
        /// <param name="c">Child Comp to remove as a child.</param>
        public void RemoveChild(Comp c)
        {
            if (Children == null)
                return;
            if (!Children.Contains(c))
                return;
            Children.Remove(c);
            c._parent = null;
        }

    }
}
