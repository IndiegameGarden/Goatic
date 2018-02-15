
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
                DL |= pad.IsButtonDown(Buttons.DPadLeft);
                DR |= pad.IsButtonDown(Buttons.DPadRight);
                DU |= pad.IsButtonDown(Buttons.DPadUp);
                DD |= pad.IsButtonDown(Buttons.DPadDown);

                // joysticks input
                Vector2 stks = pad.ThumbSticks.Left;
                stks.Y = -stks.Y;
                DL |= (stks.X < -0.01f);
                DR |= (stks.X > +0.01f);
                DU |= (stks.Y < -0.01f);
                DD |= (stks.Y > +0.01f);

                stks = pad.ThumbSticks.Right;
                stks.Y = -stks.Y;
                DL |= (stks.X < -0.01f);
                DR |= (stks.X > +0.01f);
                DU |= (stks.Y < -0.01f);
                DD |= (stks.Y > +0.01f);
            }

            // keyboard input
            if (kb.Equals(kbOld))
            {
                if (kb.IsKeyDown(Keys.Left) && kb.IsKeyDown(Keys.Right))
                    pic.Direction.X += pic.DirectionPrev.X; // both keys down -> keep old direction vector
                else if (kb.IsKeyDown(Keys.Left))
                    pic.Direction += -Vector2.UnitX;
                else if (kb.IsKeyDown(Keys.Right))
                    pic.Direction += Vector2.UnitX;

                if (kb.IsKeyDown(Keys.Up) && kb.IsKeyDown(Keys.Down))
                    pic.Direction.Y += pic.DirectionPrev.Y; // both keys down -> keep old direction vector
                else if (kb.IsKeyDown(Keys.Up))
                    pic.Direction -= Vector2.UnitY;
                else if (kb.IsKeyDown(Keys.Down))
                    pic.Direction += Vector2.UnitY;

            }
            else
            {
                // key change - adapt to new key smoothly
                if (kb.IsKeyDown(Keys.Left) && !kbOld.IsKeyDown(Keys.Left))
                    pic.Direction += -Vector2.UnitX;
                else if (kb.IsKeyDown(Keys.Right) && !kbOld.IsKeyDown(Keys.Right))
                    pic.Direction += Vector2.UnitX;
                if (kb.IsKeyDown(Keys.Up) && !kbOld.IsKeyDown(Keys.Up))
                    pic.Direction -= Vector2.UnitY;
                else if (kb.IsKeyDown(Keys.Down) && !kbOld.IsKeyDown(Keys.Down))
                    pic.Direction += Vector2.UnitY;
            }
        }
    }

}