// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Artemis;
using TTengine.Comps;
using TTengine.Util;
using TTMusicEngine;

namespace TTengine.Core
{
    /// <summary>
    /// The Game Template. The base class for your game if you want to use TTengine.
    /// </summary>
    public abstract class TTGame: Game
    {
        /// <summary>The currently running (single) instance of TTGame</summary>
        public static TTGame Instance;

        /// <summary>If true, starts both the MusicEngine and AudioSystem during init.</summary>
        public bool IsAudio = true;

        /// <summary>If true, will use standard ESC-key-to-exit routine.</summary>
        public bool IsEscapeToExit = true;

        /// <summary>When true, loop time profiling using CountingTimers is enabled.</summary>
        public bool IsProfiling = false;

        /// <summary>The XNA GraphicsDeviceManager for this Game</summary>
        public GraphicsDeviceManager GraphicsMgr;

        /// <summary>The audio/music engine, or null if none initialized</summary>
        public MusicEngine AudioEngine;

        /// <summary>The one root Artemis World into which everything else lives</summary>
        public EntityWorld RootWorld;

        /// <summary>Root screen where everything is drawn to.</summary>
        public ScreenComp RootScreen;

        /// <summary>
        /// lag is how much time (sec) the fixed timestep (gametime) updates lag to the actual time.
        /// This is used for controlling the World Updates and also for smooth interpolated rendering.
        /// </summary>
        public double TimeLag = 0.0;

        /// <summary>
        /// total Game Time in seconds, as noted in the last Update() or Draw() call that took place.
        /// </summary>
        public double GameTime = 0.0;

        /// <summary>
        /// Simulation time for world updates, which tries to keep close to GameTime (a bit ahead of GameTime is the target - allows interpolation rendering).
        /// </summary>
        public double SimTime = 0.0;

        /// <summary>Timer used for profiling: recording duration of total Update() cycle</summary>
        public CountingTimer ProfilingTimerUpdate = new CountingTimer();

        /// <summary>Timer used for profiling: recording duration of total Draw() cycle</summary>
        public CountingTimer ProfilingTimerDraw = new CountingTimer();

        /// <summary>The factory for creating the default TTGame Entities with</summary>
        internal static TTFactory Factory;

        /// <summary>
        /// Constructor
        /// </summary>
        public TTGame()
        {
            Instance = this;

            // XNA related init that needs to be in constructor (or at least before Initialize())
            GraphicsMgr = new GraphicsDeviceManager(this);
            GraphicsMgr.GraphicsProfile = GraphicsProfile.HiDef;
            IsFixedTimeStep = false; // handle own fixed timesteps in Update() code
            TargetElapsedTime = TimeSpan.FromMilliseconds(8); // the time step per Update() call
            Content.RootDirectory = "Content";
#if DEBUG
            IsProfiling = true;
            GraphicsMgr.SynchronizeWithVerticalRetrace = true; // false -> FPS as fast as possible
#else
            GraphicsMgr.SynchronizeWithVerticalRetrace = true;
#endif
            int myWindowWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int myWindowHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            GraphicsMgr.PreferredBackBufferWidth = myWindowWidth;
            GraphicsMgr.PreferredBackBufferHeight = myWindowHeight;
            GraphicsMgr.IsFullScreen = false;
            Window.IsBorderless = true;
        }

        protected override void Initialize()
        {
            Factory = new TTFactory();

            // make root world and build to it
            RootWorld = new EntityWorld();
            RootWorld.InitializeAll(true);
            Factory.BuildTo(RootWorld);

            // make root screen and build to it
            RootScreen = new ScreenComp();
            Factory.BuildTo(RootScreen);

            // the MusicEngine
            if (IsAudio)
            {
                AudioEngine = MusicEngine.GetInstance();
                if (!AudioEngine.Initialize())
                    throw new Exception(AudioEngine.StatusMsg);
            }

            base.Initialize();

        }

        protected override void Update(GameTime gameTime)
        {
            if (IsProfiling)
            {
                ProfilingTimerUpdate.Start();
                ProfilingTimerUpdate.CountUp();
            }

            // keep record of the latest GameTime
            this.GameTime = gameTime.TotalGameTime.TotalSeconds;
            // simulation fixed time step dt
            double dt = TargetElapsedTime.TotalSeconds;
            // see http://gameprogrammingpatterns.com/game-loop.html
            // advance our TimeLag by the amount of real time passed since Update().
            TimeLag += gameTime.ElapsedGameTime.TotalSeconds;

            // run one or more World.Update() rounds with fixed time step, to catch
            // up the TimeLag and even be slightly ahead into the future (TimeLag < 0 goal)
            while (TimeLag >= 0)
            {
                RootWorld.Update(TargetElapsedTime /* = dt */ );
                this.SimTime += dt;
                TimeLag -= dt;
            }

            // check for ESC key to exit
            KeyboardState kb = Keyboard.GetState();
            if (IsEscapeToExit && kb.IsKeyDown(Keys.Escape))
            {
                UnloadContent();
                Exit();
            }

            base.Update(gameTime);

            if (IsProfiling)
            {
                ProfilingTimerUpdate.Update();
                ProfilingTimerUpdate.Stop();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (IsProfiling)
            {
                ProfilingTimerDraw.Start();
                ProfilingTimerDraw.CountUp();
            }
            
            // keep track of the latest GameTime seen in a Draw() or Update() call
            this.GameTime = gameTime.TotalGameTime.TotalSeconds;
            RootScreen.SpriteBatch.BeginParameterized();
            RootWorld.Draw();   // draw world including all sub-worlds/sub-channels
            GraphicsDevice.SetRenderTarget(null);
            RootScreen.SpriteBatch.End();
            base.Draw(gameTime);

            if (IsProfiling)
            {
                ProfilingTimerDraw.Update();
                ProfilingTimerDraw.Stop();
            }
        }

    }
}
