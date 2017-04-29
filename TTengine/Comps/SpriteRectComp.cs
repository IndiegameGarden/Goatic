// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

using TTengine.Core;

using Artemis;
using Artemis.Interface;

namespace TTengine.Comps
{
    /// <summary>
    /// Component for a rectangle-only sprite; i.e. a single pixel texture of a color that's being stretched onto
    /// a given Rectangle.
    /// </summary>
    public class SpriteRectComp : IComponent
    {
        public int Width = 0;
        public int Height = 0;

        /// <summary>
        /// Create an entire-screen-filling rectangle (with both Width/Height set to 0)
        /// </summary>
        public SpriteRectComp()
        {

        }

        /// <summary>
        /// Create a rectangle sprite of given width and height
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public SpriteRectComp(int width, int height)
        {
            Width = width;
            Height = height;
        }

    }
}
