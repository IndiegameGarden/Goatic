// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis;

namespace Game1.Levels
{
    public class TestLevel: Level
    {
        public override void Build()
        {
            BuildTestShaders();
        }

        public void BuildTestShaders()
        {
            BuildTo(BackgroundChannel);
            var fxScreen = CreateHypnoScreenlet();
            BuildTo(fxScreen);
            CreateRotatingBall(new Vector2(100f, 100f), new Vector2(5f, 5f), 0.1);

            BuildTo(BackgroundChannel);
            var fxScreen2 = CreateMandelbrotScreenlet();
            BuildTo(fxScreen2);
            CreateRotatingBall(new Vector2(500f, 400f), new Vector2(5f, 5f), -0.1);

            BuildTo(BackgroundChannel);
            var fxScreen3 = CreateJuliaScreenlet();
            BuildTo(fxScreen3);
            CreateRotatingBall(new Vector2(800f, 200f), new Vector2(5f, 5f), -0.1);

        }
    }
}
