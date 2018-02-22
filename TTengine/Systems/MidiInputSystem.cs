// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System.Collections.Generic;
using TTengine.Core;
using TTengine.Comps;
using TTMusicEngine;
using Artemis;
using Artemis.Manager;
using Artemis.Attributes;
using Artemis.System;
using Midi;

namespace TTengine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.AudioSystem)]
    public class MidiInputSystem : EntityComponentProcessingSystem<MidiInputComp>
    {
        // TODO multiple MIDI devices, multiple knobs.
        static InputDevice midiDev = null;
        static float knob = -1f;
        static HashSet<MidiInputSystem> midiListeners = new HashSet<MidiInputSystem>();

        public override void LoadContent()
        {
            midiListeners.Add(this);

            if (midiDev != null)
                return;     // init already done.

            // TODO selection of input device if != 1
            if (InputDevice.InstalledDevices.Count >= 1)
                midiDev = InputDevice.InstalledDevices[0];
            if (midiDev == null)
                return;

            // event handlers for MIDI
            //inputDevice.NoteOn += new InputDevice.NoteOnHandler(this.NoteOn);
            //inputDevice.NoteOff += new InputDevice.NoteOffHandler(this.NoteOff);
            midiDev.ControlChange += new InputDevice.ControlChangeHandler(this.MidiControlChange);
            midiDev.PitchBend += new InputDevice.PitchBendHandler(this.MidiPitchChange);
            midiDev.Open();
            midiDev.StartReceiving(null);

        }

        public override void UnloadContent()
        {
            midiListeners.Remove(this);

            if (midiListeners.Count == 0)
            {
                midiDev.StopReceiving();
                midiDev.Close();
                midiDev.RemoveAllEventHandlers();
            }            

        }

        public void MidiControlChange(ControlChangeMessage msg)
        {
            knob = ((float)msg.Value / 127.0f);
        }

        public void MidiPitchChange(PitchBendMessage msg)
        {
            knob = ((float)msg.Value / 16383.0f); ;
        }

        public override void Process(Entity entity, MidiInputComp mc)
        {
            ProcessTime(mc);
            if (knob >= 0f)
            {
                mc.knob = knob;
            }
        }
    }

}
