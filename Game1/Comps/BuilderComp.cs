// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Artemis;
using Artemis.Interface;

namespace Game1.Comps
{
    public delegate void BuildScriptDelegate();

    public class BuilderComp: IComponent
    {
        public BuildScriptDelegate BuildScript;
        public bool HasTriggered = false;

        public BuilderComp(BuildScriptDelegate buildScript)
        {
            this.BuildScript = buildScript;
        }
    }
}
