// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Microsoft.Xna.Framework;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;
using Artemis.Interface;

namespace TTengineTest
{
    /// <summary>
    /// Shader test showing sprites that use different shader fx, including no-shader.
    /// </summary>
    public class TestMixedShaders : Test
    {

        public override void Create()
        {
            Factory.BallSprite = "paul-hardman_circle-four";

            var fx1 = Factory.CreateFx(Factory.New(), "Grayscale");
            using (Factory.BuildTo(fx1))
                Factory.CreateRotatingBall(Factory.New(), pos: new Vector2(100f, 100f), velo: new Vector2(5f, 5f), rotSpeed: 0.1);

            var fx2 = Factory.CreateFx(Factory.New(), "RandomColor");
            using (Factory.BuildTo(fx2))
                Factory.CreateRotatingBall(Factory.New(), new Vector2(500f, 400f), new Vector2(5f, 5f), -0.1);

            var fx3 = Factory.CreateFx(Factory.New(), "FixedColor");
            using (Factory.BuildTo(fx3))
                Factory.CreateRotatingBall(Factory.New(), new Vector2(1100f, 300f), new Vector2(1f, -2f), 0.034);

            var fx4 = Factory.CreateFx(Factory.New(), "Bloom1");
            using (Factory.BuildTo(fx4))
                Factory.CreateRotatingBall(Factory.New(), new Vector2(900f, 680f), new Vector2(-1f, -1f), -0.34);

            // no shader effect
            Factory.CreateRotatingBall(Factory.New(), new Vector2(900f, 100f), new Vector2(5f, 5f), 0.1);
        }

    }
}
