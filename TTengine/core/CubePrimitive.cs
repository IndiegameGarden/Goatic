// (c) 2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTengine.Core
{
    /// <summary>
    /// Geometric primitive class for drawing cubes.
    /// </summary>
    public class CubePrimitive : GeometricPrimitive
    {
        protected static Vector3[] normals =
                 {
                new Vector3(0, 0, 1),
                new Vector3(0, 0, -1),
                new Vector3(1, 0, 0),
                new Vector3(-1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, -1, 0),
            };

        /// <summary>
        /// Constructs a new cube primitive
        /// </summary>
        public CubePrimitive(float width)
        {
            // Create each face in turn. 
            foreach (Vector3 normal in normals)
            {
                // Get two vectors perpendicular to the face normal and to each other. 
                Vector3 side1 = new Vector3(normal.Y, normal.Z, normal.X);
                Vector3 side2 = Vector3.Cross(normal, side1);
                var r = width / 2.0f;

                // Six indices (two triangles) per face. 
                AddIndex(CurrentVertex + 0);
                AddIndex(CurrentVertex + 1);
                AddIndex(CurrentVertex + 2);

                AddIndex(CurrentVertex + 0);
                AddIndex(CurrentVertex + 2);
                AddIndex(CurrentVertex + 3);

                // Four vertices per face. 
                AddVertex((normal - side1 - side2) * r, normal, new Vector2(0, 0) );
                AddVertex((normal - side1 + side2) * r, normal, new Vector2(0, 1));
                AddVertex((normal + side1 + side2) * r, normal, new Vector2(1, 1));
                AddVertex((normal + side1 - side2) * r, normal, new Vector2(1, 0));
            }
            
            InitializePrimitive();
        }
    }
}
