// (c) 2017-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using System.Threading;
using Artemis;
using TTengine.Comps;

namespace TTengine.Core
{
    /// <summary>
    /// Object storing a former TTFactory state. When disposed, it sets the TTFactory back to the stored state.
    /// This can be used in using ( ) { ... } blocks for building to selected screens/worlds temporarily.
    /// </summary>
    public class FactoryState : IDisposable
    {
        private EntityWorld buildWorld;
        private ScreenComp buildScreen;
        private TTFactory Factory;

        internal FactoryState(TTFactory factory, EntityWorld buildWorld, ScreenComp buildScreen)
        {
            if (factory != null)
            {
                Monitor.Enter(factory); // keep lock on factory, until FactoryState is released again
                this.Factory = factory;
                this.buildWorld = buildWorld;
                this.buildScreen = buildScreen;
            }
        }

        public void Dispose()
        {
            if (Factory != null)
            {
                Factory.BuildTo(this.buildWorld);
                Factory.BuildTo(this.buildScreen);
                Monitor.Exit(Factory);      // release the lock on factory
            }
        }
    }
}
