#region File Description
//-----------------------------------------------------------------------------
// GeometricPrimitive.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
// (c) 2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt
#endregion

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TTengine.Core
{
    /// <summary>
    /// Base class for simple geometric primitive models. This provides a vertex
    /// buffer, an index buffer, plus methods for drawing the model. Classes for
    /// specific types of primitive (CubePrimitive, SpherePrimitive, etc.) are
    /// derived from this common base, and use the AddVertex and AddIndex methods
    /// to specify their geometry.
    /// 
    /// This class is borrowed from the Primitives3D sample.
    /// </summary>
    public abstract class GeometricPrimitive : IDisposable
    {
        /// <summary>
        /// The Effect used to render this shape, e.g. a BasicEffect. If not given by subclass, it
        /// will initialize to a standard BasicEffect.
        /// </summary>
        public BasicEffect Fx = null;
        
        /// <summary>
        /// Get/set the texture for this shape. Returns null if no texture set (yet).
        /// </summary>
        public Texture2D Texture
        {
            get { return (Fx != null) ? Fx.Texture : null; }
            set
            {
                Fx.Texture = value;
                Fx.TextureEnabled = true;
            }
        }

        // During the process of constructing a primitive model, vertex
        // and index data is stored on the CPU in these managed lists.
        internal List<VertexPositionNormalTexture> vertices = new List<VertexPositionNormalTexture>();
        internal List<ushort> indices = new List<ushort>();

        // Once all the geometry has been specified, the InitializePrimitive
        // method copies the vertex and index data into these buffers, which
        // store it on the GPU ready for efficient rendering.
        internal VertexBuffer vertexBuffer;
        internal IndexBuffer indexBuffer;

        // Constructor: needs to be provided by subclasses.

        /// <summary>
        /// Adds a new vertex to the primitive model. This should only be called
        /// during the initialization process, before InitializePrimitive.
        /// </summary>
        protected void AddVertex(Vector3 position, Vector3 normal, Vector2 texCoord)
        {
            vertices.Add(new VertexPositionNormalTexture(position, normal, texCoord));
        }

        /// <summary>
        /// Adds a new index to the primitive model. This should only be called
        /// during the initialization process, before InitializePrimitive.
        /// </summary>
        protected void AddIndex(int index)
        {
            if (index > ushort.MaxValue)
                throw new ArgumentOutOfRangeException("index");

            indices.Add((ushort)index);
        }

        /// <summary>
        /// Queries the index of the current vertex. This starts at
        /// zero, and increments every time AddVertex is called.
        /// </summary>
        protected int CurrentVertex
        {
            get { return vertices.Count; }
        }

        /// <summary>
        /// Once all the geometry has been specified by calling AddVertex and AddIndex,
        /// e.g. in the subclass object's constructor,
        /// this method copies the vertex and index data into GPU format buffers, ready
        /// for efficient rendering.
        protected void InitializePrimitive()
        {
            var graphicsDevice = TTGame.Instance.GraphicsDevice;

            // Create a vertex declaration, describing the format of our vertex data.
            // see http://virtuallyprogramming.com/Fundamentals/VerticesAndIndicies.html

            // Create a vertex buffer, and copy our vertex data into it.
            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionNormalTexture), vertices.Count, BufferUsage.None);
            vertexBuffer.SetData(vertices.ToArray());

            // Create an index buffer, and copy our index data into it.
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(ushort), indices.Count, BufferUsage.None);

            indexBuffer.SetData(indices.ToArray());

            // Create a BasicEffect, which will be used to render the primitive.
            if (Fx == null)
            {
                // https://developer.xamarin.com/guides/cross-platform/game_development/monogame/3d/part2/
                Fx = new BasicEffect(graphicsDevice)
                {
                    Alpha = 1f,
                    PreferPerPixelLighting = false,
                    TextureEnabled = false,
                    LightingEnabled = true, // http://www.gamefromscratch.com/post/2015/08/20/Monogame-Tutorial-Beginning-3D-Programming.aspx
                    FogEnabled = false
                };
                Fx.EnableDefaultLighting();
            }
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~GeometricPrimitive()
        {
            Dispose(false);
        }

        /// <summary>
        /// Frees resources used by this object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Frees resources used by this object.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (vertexBuffer != null)
                    vertexBuffer.Dispose();

                if (indexBuffer != null)
                    indexBuffer.Dispose();

                if (Fx != null)
                    Fx.Dispose();
            }
        }

    }
}
