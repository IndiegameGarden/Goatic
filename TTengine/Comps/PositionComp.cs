﻿// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using Microsoft.Xna.Framework;

namespace TTengine.Comps
{
    /// <summary>The Position component.</summary>
    /// just to show how to use the pool =P 
    /// (just add this annotation and extend ArtemisComponentPool =P)
    /// TODO test component pool performance once further in development.
    //[ArtemisComponentPool(InitialSize = 5, IsResizable = true, ResizeSize = 20, IsSupportMultiThread = false)]
    public class PositionComp : Comp //ComponentPoolable
    {
        /// <summary>Initializes a new zero instance of the <see cref="PositionComp" /> class.</summary>
        public PositionComp()
            : this(Vector3.Zero)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="PositionComp" /> class at given x,y coordinates and z (depth).</summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="depth">The depth (layer-depth for sprites), or z coordinate in general.</param>
        public PositionComp(float x, float y, float depth = 0.5f)
        {
            this.Position = new Vector3(x,y,depth);
        }

        /// <summary>Initializes a new instance of the <see cref="PositionComp" /> class.</summary>
        /// <param name="position">The position (x,y,z=depth).</param>
        public PositionComp(Vector3 position)
        {
            this.Position = position;
        }

        /// <summary>Gets or sets the position.</summary>
        /// <value>The position.</value>
        public Vector3 Position;

        /// <summary>
        /// The absolute position, obtained by (Position + Parent.PositionAbs)
        /// </summary>
        public Vector3 PositionAbs
        {
            get
            {
                if (_isPositionAbsSet)
                    return _positionAbs;
                else if (Parent != null)
                    _positionAbs = Position + (Parent as PositionComp).PositionAbs;
                else
                    _positionAbs = Position;
                _isPositionAbsSet = true;
                return _positionAbs;
            }
        }

        /// <summary>
        /// Previous update cycle's value of PositionAbs.
        /// </summary>
        public Vector3 PositionAbsPrev;

        /// <summary>
        /// The X, Y coordinates of Position as Vector2.
        /// </summary>
        public Vector2 PositionXY
        {
            get { return new Vector2(Position.X, Position.Y); }
            set { Position.X = value.X; Position.Y = value.Y; }
        }

        internal Vector3 _positionAbs = Vector3.Zero;
        internal bool    _isPositionAbsSet = false;

        /// <summary>Gets or sets the x coordinate.</summary>
        /// <value>The X.</value>
        public float X { get { return Position.X; }  set { Position.X = value; }  }

        /// <summary>Gets or sets the y coordinate.</summary>
        /// <value>The Y.</value>
        public float Y { get { return Position.Y; }  set { Position.Y = value; } }

        /// <summary>Gets or sets the y coordinate.</summary>
        /// <value>The Y.</value>
        public float Z { get { return Position.Z; } set { Position.Z = value; } }

        /// <summary>
        /// The z coordinate corresponds to the 'depth' of an item 0f...1f (front .... back)
        /// </summary>
        public float Depth { get { return Position.Z; } set { Position.Z = value; } }

        /// <summary>
        /// Clear the position vector to 0.
        /// </summary>
        public void Clear()
        {
            this.Position = Vector3.Zero;
        }
    }
}
