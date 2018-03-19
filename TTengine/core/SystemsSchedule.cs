// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

namespace TTengine.Core
{
    /// <summary>
    /// Defines the order in which all TTengine Systems are executed
    /// </summary>
    public class SystemsSchedule
    {
        // Systems in UPDATE loop
        public const int
            WorldSystem         =  0,        // world simulation goes depth-first.
            ForcesPreSystem     = 10,            
            TargetMoveSystem    = 50,
            RotateSystem        = 50,
            ScaleSystem         = 50,
            GeomSystem          = 50,
            PositionSystemPosAbs= 60,
            RotateSystemAbs     = 60,
            ScaleSystemAbs      = 60,
            AnimatedSpriteSystem= 60,
            BlinkSystem         = 70,
            ExpirationSystem    = 70,
            ScriptSystem        = 70,
            BTAISystem          = 70,
            AudioSystem         = 70,
            CollisionSystem     = 70,
            BuilderSystem       = 80,
            ForcesSystem        = 85,
            VelocitySystem      = 90,
            PositionSystem      = 100;

        // Systems in DRAW loop
        public const int
            WorldSystemDraw         =  0,        // world drawing goes depth-first.
            ScreenPreSystemDraw     = 10,
            AudioSystemDraw         = 10,
            DrawSystemDraw          = 10,
            ScriptSystemDraw        = 20,
            SpriteRenderSystemDraw  = 20,
            AnimatedSpriteSystemDraw= 20,
            TextRenderSystemDraw    = 20,
            ScreenPostSystemDraw    = 30,
            GeomSystemDraw          = 40;
    }
}
