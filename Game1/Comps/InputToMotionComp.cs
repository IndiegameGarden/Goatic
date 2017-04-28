// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using Artemis.Interface;
using TTengine.Comps;

namespace Game1.Comps
{
    /// <summary>
    /// Translates PlayerInputComp input to entity motion
    /// </summary>
    public class InputToMotionComp: IComponent
    {

        public float Speed = 200.0f;

        public InputToMotionComp()
        {
        }

    }
}
