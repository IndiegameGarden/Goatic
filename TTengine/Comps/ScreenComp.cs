// (c) 2010-2015 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Artemis.Interface;
using TTengine.Core;

namespace TTengine.Comps
{
    /// <summary>
    /// Screen to which graphics are rendered together with a TTSpriteBatch, 
    /// optionally containing a separate RenderBuffer.
    /// Attaching a ScreenComp to an Entity makes it a Screen.
    /// <seealso cref="ScreenSystem"/>
    /// </summary>
    public class ScreenComp: IComponent
    {
        /// <summary>create a ScreenComp of given dimensions with optionally a RenderTarget.
        /// If (0,0) given, uses default backbuffer size </summary>
        public ScreenComp(bool hasRenderBuffer, int x = 0, int y = 0)
        {
            SpriteBatch = new TTSpriteBatch(TTGame.Instance.GraphicsDevice);
            if (hasRenderBuffer)
            {
                if (x == 0 && y == 0)
                {
                    x = TTGame.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth;
                    y = TTGame.Instance.GraphicsDevice.PresentationParameters.BackBufferHeight;
                }
                renderTarget = new RenderTarget2D(TTGame.Instance.GraphicsDevice, x, y);
            }
            InitScreenDimensions();
        }

        public ScreenComp(RenderTarget2D renderTarget)
        {
            SpriteBatch = new TTSpriteBatch(TTGame.Instance.GraphicsDevice);
            this.renderTarget = renderTarget;
            InitScreenDimensions();
        }

        public Color BackgroundColor = Color.Transparent;

        public bool IsVisible = true;

        /// <summary>The center pixel coordinate of the screen</summary>
        public Vector2 Center { get; private set; }

        /// <summary>The zoom-in factor, used for showing only a part of a screen and for translation 
        /// of other coordinate systems to pixel coordinates.</summary>
        public float Zoom = 1.0f;

        /// <summary>The center coordinate, in either pixel or custom coordinates, for applying Zoom</summary>
        public Vector2 ZoomCenter;

        /// <summary>Get the RenderTarget. If null, the screen renders to the default backbuffer.
        /// </summary>
        public RenderTarget2D RenderTarget
        {
            get
            {
                return renderTarget;
            }
        }

        /// <summary>Width of screen in pixels</summary>
        public int Width { get { return screenWidth; } }

        /// <summary>Height of screen in pixels</summary>
        public int Height { get { return screenHeight; } }

        /// <summary>Screen aspectratio</summary> 
        public float AspectRatio { get { return aspectRatio;  } }

        /// <summary>The default spritebatch associated to this screen, for drawing to it</summary>
        public TTSpriteBatch SpriteBatch;

        public  Matrix ViewM, ProjM;

        #region Private and internal variables        
        private int screenWidth = 0;
        private int screenHeight = 0;
        private float aspectRatio;
        private /*internal*/ RenderTarget2D renderTarget;
        #endregion

        /// <summary>
        /// translate a Vector2 relative coordinate to pixel coordinates for this Screen
        /// </summary>
        /// <param name="pos">relative coordinate to translate</param>
        /// <returns>translated to pixels coordinate</returns>
        public Vector2 ToPixels(Vector2 pos)
        {
            var v = (pos - ZoomCenter) * Zoom + Center;
            return v;
        }

        /// <summary>
        /// If RenderTarget or the TTGame screenbuffer (window) changed size, call this method
        /// to get all the proper dimensions variables set again. This call will reset
        /// ZoomCenter to the (new) center.
        /// </summary>
        public void InitScreenDimensions()
        {
            if (renderTarget != null)
            {
                screenWidth = renderTarget.Width;
                screenHeight = renderTarget.Height;
            }
            else
            {
                screenWidth = TTGame.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth;
                screenHeight = TTGame.Instance.GraphicsDevice.PresentationParameters.BackBufferHeight;
            }

            aspectRatio = (float)screenWidth / (float)screenHeight;
            Center = new Vector2( (float)screenWidth/2.0f, (float)screenHeight/2.0f);
            ZoomCenter = Center;

            // 3d geometry
            // Create a view and projection matrix for our camera
            ViewM = Matrix.CreateLookAt( new Vector3(Center.X,Center.Y, -400f), new Vector3(Center.X, Center.Y, 0f), Vector3.Down);
            ProjM = Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi/1.67f, TTGame.Instance.GraphicsDevice.Viewport.AspectRatio, 0.01f, 500f);
            //ProjM = Matrix.CreatePerspective(screenWidth,screenHeight, 0.01f, 1000f);


        }

    }
}
