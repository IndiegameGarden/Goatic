﻿// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Microsoft.Xna.Framework;
using Artemis;
using TTengine.Core;

namespace TTengineTest
{
    /// <summary>
    /// Template class for a TTengine test
    /// </summary>
    public abstract class Test
    {
        public Test()
        {
            this.Factory = TestGame.Factory;
        }

        /// <summary>default background color for this test</summary>
        public Color BackgroundColor = Color.White;

        /// <summary>
        /// font color for this test
        /// </summary>
        public Color FontColor = Color.Black;

        /// <summary>The Channel onto which this Test will render</summary>
        public Entity Channel;

        /// <summary>
        /// Factory used by test to create Entities
        /// </summary>
        public TestFactory Factory;

        /// <summary>
        /// Create all the entities for this specific test
        /// </summary>
        public abstract void Create();

        /// <summary>
        /// set Factory building output to another screen, world or channel
        /// <seealso cref="BuildToDefault"/>
        /// </summary>
        /// <param name="screen"></param>
        public FactoryState BuildTo(Entity screen)
        {
            return Factory.BuildTo(screen);
        }

        /// <summary>
        /// restore Factory building to default screen that was made for this test
        /// </summary>
        public void BuildToDefault()
        {
            Factory.BuildTo(Channel);
        }
    }
}
