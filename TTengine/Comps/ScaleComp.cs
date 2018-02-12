// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;

namespace TTengine.Comps
{
    /// <summary>
    /// Component for scale (size) modification
    /// </summary>
    public class ScaleComp : Comp
    {
        public ScaleComp():
            this(1.0f)
        {
        }

        public ScaleComp(float scale)
        {
            this.Scale = scale;
        }

        /// <summary>
        /// the relative size scaling factor, 1.0 being normal scale
        /// </summary>
        public float Scale = 1.0f;

        /// <summary>
        /// set a target for Scale value
        /// </summary>
        public float ScaleTarget = 1.0f;

        /// <summary>
        /// speed for changing Scale towards ScaleTarget (speed can be 0: no change)
        /// </summary>
        public float ScaleSpeed = 0.0f;

        /// <summary>
        /// The absolute scale, obtained by multiplying this Entity's scale with its
        /// parent absolute scale.
        /// </summary>
        public float ScaleAbs
        {
            get
            {
                if (Parent == null)
                    return Scale;
                else
                    return Scale * (Parent as ScaleComp).ScaleAbs;                
            }
        }

        public float ScaleAbsPrev = 0.0f;
    }
}
