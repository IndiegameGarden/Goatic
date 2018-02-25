// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;

namespace TTengine.Comps
{
    /// <summary>
    /// Enables MIDI controller knobs / keyboards etc input for use in scripts and other controls.
    /// </summary>
    public class MidiInputComp: Comp
    {
        public bool IsActive = false;

        public float[] Slider;

        public float[] Knob;

    }
}
