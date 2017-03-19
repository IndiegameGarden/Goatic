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
    /// Shader test with texture sampling, creates an EffectScreenlet and renders sprites to it
    /// </summary>
    class TestTextureSamplingShader : Test
    {

        public override void Create()
        {
            var fxScreen = Factory.CreateFxScreenlet(Factory.New(), "Grayscale");
            BuildTo(fxScreen);

            var t = new TestRotation();
            t.Create();
        }

    }
}
