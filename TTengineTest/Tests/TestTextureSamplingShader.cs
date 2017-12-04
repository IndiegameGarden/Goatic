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
    /// Shader test with texture sampling, applied to entire screen
    /// </summary>
    class TestTextureSamplingShader : Test
    {

        public override void Create()
        {
            //Factory.SetFx("Grayscale");
            var fx1 = Factory.CreateFx(Factory.New(), "Grayscale");
            BuildTo(fx1);

            var t = new TestRotation();
            t.Create();
        }

    }
}
