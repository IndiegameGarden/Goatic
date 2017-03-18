using System;
using Microsoft.Xna.Framework;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;
using Artemis.Interface;

namespace TTengineTest
{
    /// <summary>
    /// Shader test showing sprites that use different shaders, including no-shader.
    /// </summary>
    public class TestMixedShaders : Test
    {

        public override void Create()
        {
            Factory.BallSprite = "paul-hardman_circle-four";

            var fxScreen = Factory.CreateFxScreenlet(Factory.New(), "Grayscale");
            BuildTo(fxScreen);
            Factory.CreateRotatingBall(new Vector2(100f, 100f), new Vector2(5f, 5f), 0.1);

            var fxScreen2 = Factory.CreateFxScreenlet(Factory.New(), "RandomColor");
            BuildTo(fxScreen2);
            Factory.CreateRotatingBall(new Vector2(500f, 400f), new Vector2(5f, 5f), -0.1);

            BuildToDefault();
            Factory.CreateRotatingBall(new Vector2(900f, 100f), new Vector2(5f, 5f), 0.1);
        }

    }
}
