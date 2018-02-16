// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

namespace TTengine.Systems
{
    /// <summary>
    /// Defines the order in which all TTengine Systems are executed
    /// </summary>
    public class SystemsSchedule
    {
        // Systems in UPDATE loop
        public const int
            WorldSystem         = 0,        // world simulation goes depth-first.
            PositionSystem      = 2,
            TargetMoveSystem    = 2,
            RotateSystem        = 2,
            ScaleSystem         = 2,
            GeomSystem          = 2,
            PositionSystemPosAbs = 3,
            RotateSystemAbs     = 3,
            ScaleSystemAbs      = 3,
            AnimatedSpriteSystem = 3,
            BlinkSystem         = 4,
            ExpirationSystem    = 4,
            ScriptSystem        = 4,
            BTAISystem          = 4,
            AudioSystem         = 4,
            CollisionSystem     = 5,
            BuilderSystem       = 6;

        // Systems in DRAW loop
        public const int
            WorldSystemDraw         = 0,        // world drawing goes depth-first.
            ScreenPreSystemDraw     = 1,
            AudioSystemDraw         = 1,
            DrawSystemDraw          = 1,
            ScriptSystemDraw        = 2,
            SpriteRenderSystemDraw  = 2,
            AnimatedSpriteSystemDraw = 2,
            TextRenderSystemDraw = 2,
            ScreenPostSystemDraw    = 3,
            GeomSystemDraw = 4;
    }
}
