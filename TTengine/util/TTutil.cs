// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace TTengine.Util
{
    /// <summary>
    /// general utility methods that are handy in creating your games
    /// </summary>
    public class TTUtil
    {
        /// <summary>
        /// Rounds the components of a Vector2 in-place
        /// </summary>
        /// <param name="input">Vector2 to round</param>
        public static void Round(ref Vector2 input)
        {
            input.X = (float)Math.Round(input.X);
            input.Y = (float)Math.Round(input.Y);
        }

        /// <summary>
        /// Convert Vector3 into Vector2, removing the Z coordinate
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Vec2(Vector3 v)
        {
            return new Vector2(v.X, v.Y);
        }

        /// <summary>
        /// Convert Vector2 into Vector3, adding a Z coordinate of 0
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Vec3(Vector2 v)
        {
            return new Vector3(v.X, v.Y, 0f);
        }

        /// <summary>
        /// Count the number of lines in a string
        /// </summary>
        /// <param name="s">The string</param>
        /// <returns>The linecount, or 0 if empty string</returns>
        public static int LineCount(string s)
        {
            if (s.Length == 0)
                return 0;
            int result = 1;
            foreach (char c in s)
            {
                if (c.Equals('\n'))
                {
                    result++;
                }
            }
            return result;
        }

        /// <summary>
        /// Invert color
        /// </summary>
        /// <param name="c">color to invert</param>
        /// <returns>Inverted color, "1-c"</returns>
        public static Color InvertColor(Color c)
        {
            return new Color(new Vector3(1f, 1f, 1f) - c.ToVector3());
        }

    }
}
