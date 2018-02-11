// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using TTMusicEngine.Soundevents;

namespace TTengine.Comps
{
    /// <summary>
    /// Enables audio/music/soundeffect playing by an Entity, via the MusicEngine
    /// </summary>
    public class AudioComp: Comp
    {
        /// <summary>The audio script to play in MusicEngine format</summary>
        public SoundEvent AudioScript = null;

        /// <summary>The relative amplitude (0...1) to play the script with</summary>
        public double Ampl = 1.0;

        public AudioComp()
        {
        }

        public AudioComp(SoundEvent script)
        {
            this.AudioScript = script;
        }

    }
}
