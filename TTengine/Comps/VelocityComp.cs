﻿// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

namespace TTengine.Comps
{
    using System;
    using Microsoft.Xna.Framework;
    using Artemis.Interface;

    /// <summary>Velocity and acceleration of an Entity, contributing to its position change</summary>
    public class VelocityComp : IComponent
    {
        /// <summary>Initializes a new instance of the <see cref="VelocityComp" /> class to zero velocity.</summary>
        public VelocityComp()
            : this(0f, 0f, 0f)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="VelocityComp" /> class.</summary>
        /// <param name="velocity">The velocity.</param>
        public VelocityComp(float x, float y, float z = 0f)
        {
            Velocity = new Vector3(x, y, z);
        }

        /// <summary>Gets or sets the velocity vector.</summary>
        public Vector3 Velocity;

        /// <summary>Maximum bound for the speed (velocity vector magnitude)</summary>
        public float MaxSpeed = float.MaxValue;

        /// <summary>Gets or sets the velocity X and Y components.</summary>
        /// <value>The 2D velocity.</value>
        public Vector2 VelocityXY
        {
            get
            {
                return new Vector2(Velocity.X, Velocity.Y);
            }

            set
            {
                Velocity.X = value.X;
                Velocity.Y = value.Y;
            }
        }

        public float X { get { return Velocity.X; } set { Velocity.X = value; } }
        public float Y { get { return Velocity.Y; } set { Velocity.Y = value; } }
        public float Z { get { return Velocity.Z; } set { Velocity.Z = value; } }

        /// <summary>Gets or sets the speed, adapting the magnitude of the velocity vector.</summary>
        public float Speed
        {
            get
            {
                return Velocity.Length();
            }
            set
            {
                Velocity.Normalize();
                Velocity *= value;
            }
        }

    }
}