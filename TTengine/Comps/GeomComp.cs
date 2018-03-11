// (c) 2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework.Graphics;
using Artemis;
using TTengine.Core;
using TTengine.Geom;

namespace TTengine.Comps
{
    /// <summary>
    /// Component for rendering 3D geometric shapes
    /// </summary>
    public class GeomComp : Comp
    {
        public GeomComp(GeometricPrimitive g, BasicEffect fx = null)
        {
            // Create a BasicEffect, which will be used to render the primitive.
            if (fx == null)
            {
                // https://developer.xamarin.com/guides/cross-platform/game_development/monogame/3d/part2/
                fx = new BasicEffect(TTGame.Instance.GraphicsDevice)
                {
                    Alpha = 1f,
                    PreferPerPixelLighting = false,
                    TextureEnabled = false,
                    LightingEnabled = true, // http://www.gamefromscratch.com/post/2015/08/20/Monogame-Tutorial-Beginning-3D-Programming.aspx
                    FogEnabled = false
                };
                fx.EnableDefaultLighting();
            }
            this.Fx = fx;
            this.Geom = g;
        }

        /// <summary>The geometric primitive to draw. (Single one, could be a list in the future.)</summary>
        public GeometricPrimitive Geom;

        /// <summary>
        /// The Effect used to render this shape. If not given by subclass in the constructor, it
        /// will initialize to a standard BasicEffect.
        /// </summary>
        public BasicEffect Fx = null;

        /// <summary>
        /// Get/set the texture for this shape. Returns null if no texture set (yet). Setting a texture
        /// will enable it.
        /// </summary>
        public Texture2D Texture
        {
            get { return Fx.Texture; }
            set {
                Fx.Texture = value;
                Fx.TextureEnabled = true;
            }
        }

    }
}
