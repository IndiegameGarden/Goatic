﻿// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis;

namespace Game1.Levels
{
    public class TestLevel: Level
    {
        public override void Build()
        {
            BuildTestShaderBackground();
            BuildTestShaders();
        }

        public void BuildTestShaders()
        {
            BuildTo(BackgroundChannel);
            var fxScreen = CreateHypnoScreen(New());
            BuildTo(fxScreen);
            CreateRotatingBall(New(), new Vector2(100f, 100f), new Vector2(5f, 5f), 0.1f);

            BuildTo(BackgroundChannel);
            var fxScreen2 = CreateMandelbrotScreen(New());
            BuildTo(fxScreen2);
            CreateRotatingBall(New(), new Vector2(500f, 400f), new Vector2(5f, 5f), -0.1f);

            BuildTo(BackgroundChannel);
            var fxScreen3 = CreateJuliaScreen(New());
            BuildTo(fxScreen3);
            CreateRotatingBall(New(), new Vector2(800f, 200f), new Vector2(5f, 5f), -0.1f);

        }

        public void BuildTestShaderBackground()
        {
            BuildTo(BackgroundChannel);
            CreateJuliaFxSprite(New());

        }

    }
}
