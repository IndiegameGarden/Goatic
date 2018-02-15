
namespace TTengine.Systems
{
    #region Using statements

    using System;
    using Microsoft.Xna.Framework.Input;
    using Artemis;
    using Artemis.Attributes;
    using Artemis.Manager;
    using Artemis.System;
    using Microsoft.Xna.Framework;
    using TTengine.Comps;

    #endregion

    /// <summary>The movement system.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.RotateSystem)]
    public class PlayerInputSystem : EntityComponentProcessingSystem<PlayerInputComp>
    {
        KeyboardState   kb = new KeyboardState(), 
                        kbOld;
        GamePadState pad;

        protected override void Begin()
        {
            kbOld = kb;
            kb = Keyboard.GetState();
            pad = GamePad.GetState(PlayerIndex.One); // TODO assumes single player always
        }

        /// <summary>Processes the specified entity.</summary>
        /// <param name="entity">The entity.</param>
        public override void Process(Entity entity, PlayerInputComp pic)
        {
            bool DL = false, DR = false , DU = false , DD = false;

            pic.DirectionPrev = pic.Direction;            
            pic.Direction = Vector2.Zero;
            if (pic.Player != PlayerIndex.One)  // TODO handle input for >1 players
                return;

            // gamepad input
            if (pad.IsConnected)
            {
                // D-Pad
                DL |= pad.IsButtonDown(Buttons.DPadLeft);
                DR |= pad.IsButtonDown(Buttons.DPadRight);
                DU |= pad.IsButtonDown(Buttons.DPadUp);
                DD |= pad.IsButtonDown(Buttons.DPadDown);

                // analog joysticks input
                Vector2 stks = pad.ThumbSticks.Left + pad.ThumbSticks.Right;
                stks.Y = -stks.Y;
                pic.Direction += stks;
            }

            // keyboard arrow keys and WASD
            if (kb.Equals(kbOld))
            {
                DL |= kb.IsKeyDown(Keys.Left);
                DR |= kb.IsKeyDown(Keys.Right);
                DU |= kb.IsKeyDown(Keys.Up);
                DD |= kb.IsKeyDown(Keys.Down);

                DL |= kb.IsKeyDown(Keys.A);
                DR |= kb.IsKeyDown(Keys.D);
                DU |= kb.IsKeyDown(Keys.W);
                DD |= kb.IsKeyDown(Keys.S);
            }
            else
            {
                // key change - look at what's last pressed
                DL |= kb.IsKeyDown(Keys.Left) && !kbOld.IsKeyDown(Keys.Left);
                DR |= kb.IsKeyDown(Keys.Right) && !kbOld.IsKeyDown(Keys.Right);
                DU |= kb.IsKeyDown(Keys.Up) && !kbOld.IsKeyDown(Keys.Up);
                DD |= kb.IsKeyDown(Keys.Down) && !kbOld.IsKeyDown(Keys.Down);

                DL |= kb.IsKeyDown(Keys.A) && !kbOld.IsKeyDown(Keys.A);
                DR |= kb.IsKeyDown(Keys.D) && !kbOld.IsKeyDown(Keys.D);
                DU |= kb.IsKeyDown(Keys.W) && !kbOld.IsKeyDown(Keys.W);
                DD |= kb.IsKeyDown(Keys.S) && !kbOld.IsKeyDown(Keys.S);
            }


            // act on the input
            if (DL && DR)
                pic.Direction.X += pic.DirectionPrev.X; // both keys down -> keep old direction vector
            else if (DL)
                pic.Direction += -Vector2.UnitX;
            else if (DR)
                pic.Direction += Vector2.UnitX;

            if (DU && DD)
                pic.Direction.Y += pic.DirectionPrev.Y; // both keys down -> keep old direction vector
            else if (DU)
                pic.Direction -= Vector2.UnitY;
            else if (DD)
                pic.Direction += Vector2.UnitY;

            if (pic.Direction != Vector2.Zero)
                pic.Direction.Normalize();
        }
    }

}