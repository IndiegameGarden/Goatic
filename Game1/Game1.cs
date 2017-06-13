// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using TTengine.Core;
using TTengine.Comps;
using TTengineTest;
using Artemis;

using Game1.Levels;

namespace Game1
{
    /// <summary>
    /// Main game class.
    /// </summary>
    public class Game1 : TTGame
    {
        /// <summary>
        /// number of 'extra' pixels in total (adding ones above and below the screen) for showing a little bit more of the
        /// current game screen, for people who have monitors with a somewhat higher number of pixels vertically e.g. 4:3 aspect
        /// ratio instead of 16:9 widescreen. Game code has to ensure no crucial items (GUI elements, text) are plotted in these
        /// areas.
        /// </summary>
        const int SPARE_SCREEN_HEIGHT = 256;
        const int SCREEN_SIZE_X = 1920;
        const int SCREEN_SIZE_Y = 1080 + SPARE_SCREEN_HEIGHT;

        public static Game1Factory Factory;
        public static new Game1 Instance;

        // the channels (i.e. layers)
        public Entity GameChannel, LevelChannel, BackgroundChannel, GuiChannel;

        public Entity Ship;

        public Game1()
        {
            Instance = this;
            IsAudio = false;    // set to true if audio is needed
        }

        protected override void LoadContent()
        {
            Factory = new Game1Factory();
            Factory.BuildTo(MainChannel);

            // create the game's channel at fixed resolution, which is then auto-scaled onto any screen resolution.
            GameChannel = Factory.CreateChannel(Factory.New(), Color.Transparent, SCREEN_SIZE_X, SCREEN_SIZE_Y);
            Factory.BuildTo(GameChannel);

            var scr = GameChannel.C<WorldComp>().Screen;
            scr.BackgroundColor = Color.Black;

            // create level/background channels of same size
            BackgroundChannel = Factory.CreateChannel(Factory.New(), GameChannel);
            BackgroundChannel.C<PositionComp>().Depth = 0.7f;
            BackgroundChannel.C<WorldComp>().Screen.BackgroundColor = Color.Black;
            BackgroundChannel.Tag = "BackgroundChannel";

            LevelChannel = Factory.CreateChannel(Factory.New(), Color.Transparent, SCREEN_SIZE_X, SCREEN_SIZE_Y);
            LevelChannel.C<PositionComp>().Depth = 0.5f;
            LevelChannel.Tag = "LevelChannel";
            
            // GuiChannel = TODO

            // apply magic scaling from SCREEN_SIZE_* to current screen resolution
            Factory.ProcessChannelFit(GameChannel, MainChannel, false, true, SPARE_SCREEN_HEIGHT);

            // add framerate counter
            Factory.CreateFrameRateCounter(Factory.New(), Color.White, SPARE_SCREEN_HEIGHT/2 );

            // create the player ship
            Ship = Factory.CreateShip(Factory.New());

            // create the root level which contains the builder entities (with more level content)
            //Level root = new RootLevel();
            Level root = new TestLevel(); 
            root.Build();

            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Escape))
            {
                UnloadContent();
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void UnloadContent()
        {
            BackgroundChannel.C<WorldComp>().World.UnloadContent();
            LevelChannel.C<WorldComp>().World.UnloadContent();
            GameChannel.C<WorldComp>().World.UnloadContent();

            base.UnloadContent();
        }
    }

}