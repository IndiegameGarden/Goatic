// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis;
using TTengine.Comps;

namespace Game1.Levels
{
    public class NovasLevel: Level
    {

        public override void Build()
        {
            ;
        }

        public void BuildSection1(ScriptComp ctx)
        {
            SetAnchorPosition(ctx);
            BuildTo(LevelChannel);

            Finalize(CreateNovaBall(New(), 200, 200));
            Finalize(CreateNovaBall(New(), 800, 200));

        }

    }
}
