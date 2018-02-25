// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Microsoft.Xna.Framework;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;
using Artemis.Interface;

namespace TTengineTest
{
    /// <summary>Test the control of on-screen entities via MIDI controller knobs.</summary>
    class TestMidiInput: Test
    {

        public override void BuildAll()
        {
            var b = CreateBall(New(), 1f);
            var pc = b.C<PositionComp>();
            var sc = b.C<ScaleComp>();
            pc.Y = BuildScreen.Height / 2f;
            var mc = new MidiInputComp();
            b.AddC(mc);
            var w = BuildScreen.Width;
            AddScript(b, (ctx) => { if (mc.IsActive) { pc.X = w * mc.Slider[0]; sc.Scale = 1f + mc.Slider[1];  }  });

            Test t = new TestSphereCollision();
            t.BuildLike(this);
            t.BuildAll();
        }

    }
}
