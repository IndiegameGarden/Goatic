// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

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
