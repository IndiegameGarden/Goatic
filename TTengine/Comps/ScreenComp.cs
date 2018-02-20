// (c) 2010-2015 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Artemis.Interface;
using TTengine.Core;
using System;

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
        /// <summary>create a ScreenComp for a screen of given dimensions, with optionally a RenderTarget2D rendering buffer.</summary>
        /// <param name="hasRenderBuffer">If true creates a render buffer; if false uses the game's default render (back)buffer.</param>
        /// <param name="x">Width of render buffer, or 0 to use current screen size.</param>
        /// <param name="y">Height of render buffer, or 0 to use current screen size.</param>
        public ScreenComp(bool hasRenderBuffer = false, int x = 0, int y = 0)
        {
            SpriteBatch = new TTSpriteBatch(TTGame.Instance.GraphicsDevice);
            if (hasRenderBuffer)
            {
                if (x == 0 && y == 0)
                {
                    x = TTGame.Instance.GraphicsDevice.PresentationParameters.BackBufferWidth;
                    y = TTGame.Instance.GraphicsDevice.PresentationParameters.BackBufferHeight;
                }
                renderTarget = new RenderTarget2D(TTGame.Instance.GraphicsDevice, x, y, true, 
                    SurfaceFormat.Color, DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents); // for 3D: http://xboxforums.create.msdn.com/forums/t/99090.aspx
            }
            InitScreenDimensions();
        }

        /// <summary>
        /// Create a new ScreenComp that renders to the given RenderTarget2D.
        /// </summary>
        /// <param name="renderTarget">Render buffer to render this screen's contents to.</param>
        public ScreenComp(RenderTarget2D renderTarget)
        {
            SpriteBatch = new TTSpriteBatch(TTGame.Instance.GraphicsDevice);
            this.renderTarget = renderTarget;
            InitScreenDimensions();
        }

        /// <summary>Background color for screen</summary>
        public Color BackgroundColor = Color.Transparent;

        /// <summary>Visibility status of screen (used when rendering)</summary>
        public bool IsVisible = true;

        /// <summary>The center pixel coordinate of the screen</summary>
        public Vector2 Center { get; private set; }

        /// <summary>The zoom-in factor, used for showing only a part of a screen and for translation 
        /// of other coordinate systems to pixel coordinates. FIXME zoom or camera manipulation?</summary>
        public float Zoom = 1.0f;

        /// <summary>The center coordinate, in either pixel or custom coordinates, for applying Zoom. FIXME zoom or camera manipulation?</summary>
        public Vector2 ZoomCenter;

        /// <summary>Get the RenderTarget for this screen. If null, the screen renders to the Game's default backbuffer.
        /// </summary>
        public RenderTarget2D RenderTarget
        {
            get { return renderTarget; }
        }

        /// <summary>Width of screen in pixels</summary>
        public int Width { get { return screenWidth; } }

        /// <summary>Height of screen in pixels</summary>
        public int Height { get { return screenHeight; } }

        /// <summary>Screen aspectratio</summary> 
        public float AspectRatio { get { return aspectRatio;  } }

        /// <summary>
        /// The Field of View (FOV) for the default camera that's used when rendering objects on this screen.
        /// </summary>
        public float FOV
        {
            get { return fov;}
            set {
                fov = value;
                // Create a view and projection matrix for our camera, to match 2D coordinate system exactly
                float z = Center.Y / (float)Math.Tan(fov/2f);
                ViewM = Matrix.CreateLookAt(new Vector3(Center.X, Center.Y, -z), new Vector3(Center.X, Center.Y, 0f), Vector3.Down);                
                ProjM = Matrix.CreatePerspectiveFieldOfView(fov, aspectRatio, 0.1f, 9500f); // TODO near and far planes setting logic                
            }
        }

        /// <summary>
        /// View and Projection matrices used for the 3D objects on this screen
        /// <seealso cref="FOV"/>
        /// </summary>
        public Matrix ViewM, ProjM;

        /// <summary>The default spritebatch associated to this screen, for drawing to it</summary>
        public TTSpriteBatch SpriteBatch;

        #region Private and internal variables        
        private int screenWidth = 0;
        private int screenHeight = 0;
        private float aspectRatio, fov;
        private RenderTarget2D renderTarget;
        #endregion

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
            this.FOV = MathHelper.Pi/8;

        }

    }
}
