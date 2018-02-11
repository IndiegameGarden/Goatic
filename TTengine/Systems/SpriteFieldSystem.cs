// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

namespace TTengine.Systems
{
    #region Using statements

    using System;

    using Artemis;
    using Artemis.Attributes;
    using Artemis.Manager;
    using Artemis.System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using TTengine.Core;
    using TTengine.Comps;

    #endregion

    /// <summary>The system for rendering a sprite field - a screen full of sprites based on a colored level-layout bitmap.</summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = SystemsSchedule.SpriteRenderSystemDraw)]
    public class SpriteFieldSystem : EntityComponentProcessingSystem<SpriteFieldComp, SpriteComp, PositionComp, DrawComp>
    {
        public override void Process(Entity entity, SpriteFieldComp fieldComp, SpriteComp spriteComp, PositionComp posComp, DrawComp drawComp)
        {
            if (!drawComp.IsVisible)
                return;

            // check which screen to render to
            ScreenComp scr = drawComp.DrawScreen;

            // update drawpos - FIXME , repeated in systems?
            drawComp.DrawPosition = posComp.PositionAbs;

            TTSpriteBatch sb = scr.SpriteBatch;

            // topleft corner and grid size
            int x0 = (int)Math.Round(fieldComp.FieldPos.X);
            int y0 = (int)Math.Round(fieldComp.FieldPos.Y);
            int Nx = fieldComp.NumberSpritesX;
            int Twidth = fieldComp.Texture.Width;
            int Ny = fieldComp.NumberSpritesY;
            float dx = fieldComp.FieldSpacing.X;
            float dy = fieldComp.FieldSpacing.Y;
            var dp = drawComp.DrawPositionXY;

            // draw sprites loops
            var tex = spriteComp.Texture;
            float rot = drawComp.DrawRotation;
            Vector2 ctr = spriteComp.Center;
            float sc = drawComp.DrawScale;
            float laydepth = drawComp.LayerDepth;

            for (int x = 0; x < Nx; x++)
            {
                for (int y = 0; y < Ny; y++)
                {
                    Vector2 pos = dp + new Vector2(x * dx, y * dy);
                    Color col = fieldComp.fieldData[(x0+x) + (y0+y)*Twidth ];
                    sb.Draw(tex, pos, null, col,
                        rot, ctr, sc, SpriteEffects.None, laydepth);
                    laydepth += float.Epsilon;
                }
            }

        }

    }
}