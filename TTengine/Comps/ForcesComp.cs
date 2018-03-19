// (c) 2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis.Interface;

namespace TTengine.Comps
{ 
    /// <summary>Newtonian forces that influence the Velocity through acceleration.</summary>
    public class ForcesComp : IComponent
    {
        /// <summary>Initializes a new instance with zero force.</summary>
        public ForcesComp()
            : this(0f, 0f, 0f)
        {
        }

        /// <summary>Initializes a new instance.</summary>
        public ForcesComp(float x, float y, float z = 0f)
        {
            Force = new Vector3(x, y, z);
        }

        /// <summary>Total force acting upon the Entity in this update round.</summary>
        public Vector3 Force;

        /// <summary>Maximum force that is possible for Entity.</summary>
        public float MaxForce = float.MaxValue;

        public float Mass = 1f;

        /// <summary>Gets or sets the Force X and Y components.</summary>
        public Vector2 ForceXY
        {
            get
            {
                return new Vector2(Force.X, Force.Y);
            }

            set
            {
                Force.X = value.X;
                Force.Y = value.Y;
            }
        }

        public float X { get { return Force.X; } set { Force.X = value; } }
        public float Y { get { return Force.Y; } set { Force.Y = value; } }
        public float Z { get { return Force.Z; } set { Force.Z = value; } }

        /// <summary>Get or change the magnitude of the force vector.</summary>
        public float Magnitude
        {
            get
            {
                return Force.Length();
            }
            set
            {
                Force.Normalize();
                Force *= value;
            }
        }

    }
}