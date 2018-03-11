// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;

namespace TTengineTest
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application; builds and configures the testgame class.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new TestGame() )
                game.Run();
        }
    }
#endif
}
