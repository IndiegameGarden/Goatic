﻿// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;

namespace TTengine.Comps
{
    /// <summary>
    /// Enables rotation of entities
    /// </summary>
    public class RotateComp: Comp
    {
        public RotateComp()
        {
        }

        /// <summary>Rotation angle in radians</summary>
        public float Rotate = 0;

        /// <summary>
        /// Rotation speed i.e. change of rotation, in radians/sec. 0 means no change.
        /// </summary>
        public float RotateSpeed = 0;

        /// <summary>
        /// Get the absolute value of rotation, also based on parent's rotation.
        /// </summary>
        public float RotateAbs
        {
            get
            {                
                if (Parent == null)
                    return Rotate;
                else if (_isRotateAbsSet)
                    return _rotateAbs;
                else
                {
                    _isRotateAbsSet = true;
                    _rotateAbs = Rotate + (Parent as RotateComp).RotateAbs;
                    return _rotateAbs;
                }
            }
        }

        /// <summary>
        /// Previous update cycle's value of RotateAbs.
        /// </summary>
        public float RotateAbsPrev = 0;

        internal float _rotateAbs = 0;
        internal bool _isRotateAbsSet = false;
    }
}
