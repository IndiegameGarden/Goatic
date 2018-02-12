// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using TTengine.Core;
using TTengine.Comps;
using TTengine.Modifiers;
using Artemis.Interface;
using TTMusicEngine;
using TTMusicEngine.Soundevents;

namespace TTengineTest
{
    /// <summary>Testing basic audio and music play</summary>
    class TestAudioBasics: Test
    {
        public override void BuildAll()
        {
            AddAudio(New(), TestScript());
            AddAudio(New(), TestRepeat());
            CreateText(New(), new Vector2(100f, 100f), "Audio scripts:\n  3.0 Single chord\n 10.0 Repeat 3x chord", Color.Blue );
            var str = "Current time    t = ";
            var t = CreateText(New(), new Vector2(100f, 200f), str, Color.Red );
            AddScript(t, (ctx) => { ctx.Entity.C<TextComp>().Text = String.Format("Current time    t = {0:000.000}" , ctx.SimTime);  } );
        }

        /// <summary>
        /// simple sample play test script
        /// </summary>
        /// <returns></returns>
        public SoundEvent TestScript()
        {
            SoundEvent soundScript = new SoundEvent("TestScript");
            SoundEvent evChord = new SampleSoundEvent("single-arpeggio-chord.wav");
            soundScript.AddEvent(3.0, evChord);
            return soundScript;
        }

        /**
         * simple test of generic Repeat feature - on a SoundEvent. Not directly applied on SampleSoundEvent here.
         */
        public SoundEvent TestRepeat()
        {
            SoundEvent soundScript = new SoundEvent("TestRepeat");

            SampleSoundEvent evDing = new SampleSoundEvent("single-arpeggio-chord.wav");
            SoundEvent dingHolderEv = new SoundEvent();
            dingHolderEv.AddEvent(0, evDing);
            dingHolderEv.Repeat = 3;
            soundScript.AddEvent(10.0, dingHolderEv);

            soundScript.UpdateDuration(60); // FIXME should not be needed
            return soundScript;
        }


    }
}
