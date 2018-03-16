// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis.Interface;
using TTengine.Core;

namespace PXengine.Comps
{
    /// <summary>
    /// Scrolls the ZoomCenter of Screen to track the Position of this Entity
    /// </summary>
    public class ScrollingComp: IComponent
    {
        public ScrollingComp(Vector2 initialPosition)
        {
            Scrolling = new TargetVector(initialPosition);
        }

        public TargetVector Scrolling;

    }
}
