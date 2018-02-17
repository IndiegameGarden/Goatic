// (c) 2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using TTengine.Core;

namespace TTengine.Comps
{
    /// <summary>
    /// Component for rendering 3D geometric shapes
    /// </summary>
    public class GeomComp : Comp
    {
        public GeomComp(GeometricPrimitive g)
        {
            this.Geom = g;
        }

        /// <summary>The geometric primitive to draw. (Single one, could be a list in the future.)</summary>
        public GeometricPrimitive Geom;
       
    }
}
