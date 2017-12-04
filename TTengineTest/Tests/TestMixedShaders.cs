﻿// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

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

            var fx1 = Factory.CreateFx(Factory.New(), "Grayscale");
            BuildTo(fx1);
            Factory.CreateRotatingBall(Factory.New(), new Vector2(100f, 100f), new Vector2(5f, 5f), 0.1);

            var fx2 = Factory.CreateFx(Factory.New(), "RandomColor");
            BuildTo(fx2);
            Factory.CreateRotatingBall(Factory.New(), new Vector2(500f, 400f), new Vector2(5f, 5f), -0.1);

            BuildToDefault();
            Factory.CreateRotatingBall(Factory.New(), new Vector2(900f, 100f), new Vector2(5f, 5f), 0.1);
        }

    }
}
