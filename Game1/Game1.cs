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

        public static Game1Factory Factory;
        public Entity GameChannel, LevelChannel, BackgroundChannel, GuiChannel;

        public static Game1 InstanceG
        {
            get { return Instance as Game1; }
        }

        public Game1()
        {
            IsAudio = false;    // set to true if audio is needed
        }

        protected override void Initialize()
        {
            Factory = new Game1Factory(this);

            base.Initialize();
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

        protected override void LoadContent()
        {
            // create the game's channel at fixed resolution, which is then auto-scaled onto any screen resolution.
            const int SCREEN_SIZE_X = 1366;
            const int SCREEN_SIZE_Y = 668 + SPARE_SCREEN_HEIGHT;
            GameChannel = Game1Factory.CreateChannel(Color.Transparent, SCREEN_SIZE_X, SCREEN_SIZE_Y);
            Game1Factory.BuildTo(GameChannel);

            var scr = GameChannel.C<WorldComp>().Screen;
            scr.BackgroundColor = Color.Black;

            // create level/background channels of same size
            BackgroundChannel = Game1Factory.CreateChannel(GameChannel);
            BackgroundChannel.C<PositionComp>().Depth = 0.7f;
            BackgroundChannel.C<WorldComp>().Screen.BackgroundColor = Color.Black;
            BackgroundChannel.Tag = "BackgroundChannel";
            
            LevelChannel = Game1Factory.CreateChannel(Color.Transparent, SCREEN_SIZE_X, SCREEN_SIZE_Y);
            LevelChannel.C<PositionComp>().Depth = 0.5f;
            LevelChannel.Tag = "LevelChannel";
            // GuiChannel = TODO

            // apply magic scaling to screen resolution
            Game1Factory.ProcessChannelFit(GameChannel, MainChannel, true, true, SPARE_SCREEN_HEIGHT);

            // add framerate counter
            Game1Factory.CreateFrameRateCounter(Color.White, SPARE_SCREEN_HEIGHT/2 );

            Game1Factory.BuildTo(LevelChannel);

            // add content 
            Test t = new TestSphereCollision();
            t.Create();

            // add builder entity (test level building)
            var e = Game1Factory.CreateEntity();
            e.AddComponent(new BuilderComp(TestLevel.BuildTest1));

            // test bg content
            Game1Factory.BuildTo(BackgroundChannel);


            base.LoadContent();
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
