
using Microsoft.Xna.Framework;
using Artemis;

namespace Game1.Levels
{
    public class TestLevel
    {
        public static void BuildTest1()
        {
            Game1Factory.BuildTo(Game1.InstanceG.BackgroundChannel);

            var fxScreen = Game1.Factory.CreateHypnoScreenlet();
            Game1Factory.BuildTo(fxScreen);
            Game1.Factory.CreateRotatingBall(new Vector2(100f, 100f), new Vector2(5f, 5f), 0.1);

            var fxScreen2 = Game1.Factory.CreateMandelbrotScreenlet();
            Game1Factory.BuildTo(fxScreen2);
            Game1.Factory.CreateRotatingBall(new Vector2(500f, 400f), new Vector2(5f, 5f), -0.1);

            var fxScreen3 = Game1.Factory.CreateJuliaScreenlet();
            Game1Factory.BuildTo(fxScreen3);
            Game1.Factory.CreateRotatingBall(new Vector2(800f, 200f), new Vector2(5f, 5f), -0.1);

        }
    }
}
