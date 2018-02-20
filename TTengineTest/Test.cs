// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Artemis;
using TTengine.Core;
using TTengine.Comps;

namespace TTengineTest
{
    /// <summary>
    /// Template class for a single TTengine test
    /// </summary>
    public abstract class Test : TestFactory
    {
        /// <summary>default background color for this test</summary>
        public Color BackgroundColor = Color.White;

        /// <summary>font color for this test</summary>
        public Color FontColor = Color.Black;

        /// <summary>The Channel onto which this Test will render</summary>
        public Entity Channel;

        /// <summary>
        /// Create all the entities for this specific test in a background building process,
        /// through calling BuildAll(). FIXME experimental.
        /// </summary>
        public void BuildAllInBackground(bool isActivatePostBuild)
        {
            if (isActivatePostBuild)
                AddBackgroundScript(New(), BuildAllInBackgroundScriptWithActivate );
            else
                AddBackgroundScript(New(), BuildAllInBackgroundScript);
        }

        private void BuildAllInBackgroundScript(ScriptComp ctx)
        {
            using (BuildTo(Channel))
            {
                this.BuildAll();
            }
        }

        private void BuildAllInBackgroundScriptWithActivate(ScriptComp ctx)
        {
            using (BuildTo(Channel))
            {
                this.BuildAll();
                Finalize(Channel);
            }
        }

        /// <summary>
        /// Create all the entities for this specific test
        /// </summary>
        public abstract void BuildAll();

    }
}
