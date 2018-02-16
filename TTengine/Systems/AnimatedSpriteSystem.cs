// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using Artemis.Attributes;
using Artemis.Manager;
using Artemis.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TTengine.Core;
using TTengine.Comps;

namespace TTengine.Systems
{
    /// <summary>The system for rendering animated sprites.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.AnimatedSpriteSystem)]
    public class AnimatedSpriteSystem : EntityComponentProcessingSystem<AnimatedSpriteComp, PositionComp, DrawComp>
    {

        public override void Process(Entity entity, AnimatedSpriteComp asc, PositionComp pc, DrawComp dc)
        {
            asc.FrameTimeRemaining -= Dt;
            asc.PrevFrame = asc.CurrentFrame;

            while (asc.FrameTimeRemaining <= 0)
            {
                // Next frame
                switch (asc.AnimType)
                {
                    case AnimationType.NORMAL:
                        asc.CurrentFrame++;
                        if (asc.CurrentFrame > asc.MaxFrame || asc.CurrentFrame == asc.TotalFrames)
                            asc.CurrentFrame = asc.MinFrame;
                        break;

                    case AnimationType.REVERSE:
                        asc.CurrentFrame--;
                        if (asc.CurrentFrame < asc.MinFrame || asc.CurrentFrame < 0)
                            asc.CurrentFrame = asc.MaxFrame;
                        break;

                    case AnimationType.PINGPONG:
                        asc.CurrentFrame += asc.pingpongDelta;
                        if (asc.CurrentFrame > asc.MaxFrame || asc.CurrentFrame == asc.TotalFrames)
                        {
                            asc.CurrentFrame -= 2;
                            asc.pingpongDelta = -asc.pingpongDelta;
                        }
                        else if (asc.CurrentFrame < asc.MinFrame || asc.CurrentFrame < 0)
                        {
                            asc.CurrentFrame += 2;
                            asc.pingpongDelta = -asc.pingpongDelta;
                        }
                        break;
                }
                // give time again to new frame
                asc.FrameTimeRemaining += asc.FrameDt;

            } // while
        }

    }

    /// <summary>The draw system for rendering animated sprites.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = SystemsSchedule.AnimatedSpriteSystemDraw)]
    public class AnimatedSpriteSystemDraw : EntityComponentProcessingSystem<AnimatedSpriteComp, PositionComp, DrawComp>
    {

        public override void Process(Entity entity, AnimatedSpriteComp asc, PositionComp pc, DrawComp dc)
        {
            ScreenComp scr = dc.DrawScreen;

            // apply the "alpha" linear interpolation between Update() states - then decide which animation frame is closest to show.
            int frame;
            if (dc.DrawLerp >= 0.5f)
                frame = asc.CurrentFrame;
            else
                frame = asc.PrevFrame;

            // draw sprite from sprite atlas
            TTSpriteBatch sb = scr.SpriteBatch;
            int row = (int)((float)frame / (float)asc.Nx);
            int column = frame % asc.Nx;
            Rectangle sourceRectangle = new Rectangle(asc.px * column, asc.py * row, asc.px, asc.py);

            sb.Draw(asc.Texture, dc.DrawPositionXY, sourceRectangle, dc.DrawColor,
                dc.DrawRotation, asc.Center, dc.DrawScale, SpriteEffects.None, dc.LayerDepth);

        }
    }
}