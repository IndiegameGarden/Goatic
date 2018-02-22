// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using TTMusicEngine.Soundevents;

namespace TTengine.Comps
{
    /// <summary>
    /// Enables MIDI controller knobs / keyboards etc input for use in scripts and other controls.
    /// </summary>
    public class MidiInputComp: Comp
    {
        public float knob = -1f;    // negative means invalid
    }
}
