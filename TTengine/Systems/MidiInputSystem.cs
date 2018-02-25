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

    public abstract class MidiInputDevice
    {
        protected InputDevice midiDev = null;

        ~MidiInputDevice()
        {
            Stop();
        }

        protected virtual void Init()
        {
            midiDev.Open();
            midiDev.StartReceiving(null);
        }

        protected virtual void Stop()
        {
            if (midiDev != null)
            {
                midiDev.StopReceiving();
                midiDev.Close();
                midiDev.RemoveAllEventHandlers();
                midiDev = null;
            }
        }

        public bool IsActive()
        {
            return (midiDev != null);
        }

        protected bool Detect(string name)
        {
            if (midiDev != null)
                return true;

            for (int i = 0; i < InputDevice.InstalledDevices.Count; i++)
            {
                if (InputDevice.InstalledDevices[i].Name.StartsWith(name))
                {
                    midiDev = InputDevice.InstalledDevices[i];
                    Init();
                    return true;
                }
            }
            return false;

        }
    }

    /// <summary>
    /// Singleton Korg nanoKONTROL2 MIDI input device: 8 sliders, 8 knobs
    /// </summary>
    public class MidiInputKorgNanoKontrol2 : MidiInputDevice
    {
        public float[] Slider = new float[8] { -1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f };
        public float[] Knob = new float[8] { -1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f };

        private static MidiInputKorgNanoKontrol2 inst = null;

        protected override void Init()
        {
            // event handlers for MIDI
            midiDev.NoteOn += new InputDevice.NoteOnHandler(this.MidiNoteOn);   // Korg nanoKONTROL buttons
            midiDev.NoteOff += new InputDevice.NoteOffHandler(this.MidiNoteOff);
            midiDev.ControlChange += new InputDevice.ControlChangeHandler(this.MidiControlChange);
            midiDev.PitchBend += new InputDevice.PitchBendHandler(this.MidiPitchChange);

            base.Init();
        }

        public static MidiInputKorgNanoKontrol2 Instance
        {
            get
            {
                if (inst == null)
                    inst = new MidiInputKorgNanoKontrol2();
                return inst;
            }
        }

        public static bool Detect()
        {
            return Instance.Detect("nanoKONTROL");
        }

        public void MidiControlChange(ControlChangeMessage msg)
        {
            int k = (int)msg.Control & 0x07;
            Knob[k] = ((float)msg.Value / 127.0f);
        }

        public void MidiPitchChange(PitchBendMessage msg)
        {
            int k = (int)msg.Channel & 0x07;
            Slider[k] = ((float)msg.Value / 16383.0f);
            //System.Console.WriteLine(msg.)
        }

        public void MidiNoteOn(NoteOnMessage msg)
        {
            Pitch p = msg.Pitch;
        }

        public void MidiNoteOff(NoteOffMessage msg)
        {
            Pitch p = msg.Pitch;
        }
    }

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.AudioSystem)]
    public class MidiInputSystem : EntityComponentProcessingSystem<MidiInputComp>
    {
        private static float[] defaultSliderArray = new float[] { -1f, -1f, -1f, -1f, -1f, -1f, -1f, -1f };

        public override void LoadContent()
        {
            MidiInputKorgNanoKontrol2.Detect();
        }

        public override void UnloadContent()
        {
        }

        public override void Process(Entity entity, MidiInputComp mc)
        {
            ProcessTime(mc);
            var korg = MidiInputKorgNanoKontrol2.Instance;
            if (korg.IsActive())
            {
                mc.Slider = korg.Slider;
                mc.Knob = korg.Knob;
                mc.IsActive = true;
            }
            else
            {
                mc.Slider = defaultSliderArray;
                mc.Knob = defaultSliderArray;
                mc.IsActive = false;
            }
        }
    }

}
