// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using TTengine.Util;

namespace Game1
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application; creates and configures the TTGame class.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
            using (var game = new Game1())
                game.Run();
            }
#if !DEBUG
            // For release build, display message-box in case of exception
            catch (Exception ex)
            {
                MsgBox.Show("FEIG! (Fatal Error In Game)",
                  "Fatal Error - if you want you can notify the author.\n" + ex.Message + "\n" + ex.ToString());                
            }
#endif
            finally { ; }
        }
    }
#endif
}
