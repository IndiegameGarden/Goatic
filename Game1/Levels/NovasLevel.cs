// (c) 2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis;
using TTengine.Comps;

namespace Game1.Levels
{
    public class NovasLevel: Level
    {

        public void BuildSection1(ScriptContext ctx)
        {
            SetAnchorPosition(ctx);
            BuildTo(LevelChannel);

            Finalize(Game1.Factory.CreateNovaBall(200, 200));
            Finalize(Game1.Factory.CreateNovaBall(800, 200));

        }

    }
}
