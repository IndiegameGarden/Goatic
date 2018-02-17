// (c) 2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TTengine.Core;
using TTengine.Comps;
using Artemis;
using Artemis.Manager;
using Artemis.Attributes;
using Artemis.System;

namespace TTengine.Systems
{
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.GeomSystem)]
    public class GeomSystem : EntityComponentProcessingSystem<GeomComp>
    {

        public override void Process(Entity entity, GeomComp sc)
        {
            ProcessTime(sc);
        }

    }

    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = SystemsSchedule.GeomSystemDraw)]
    public class GeomDrawSystem : EntityComponentProcessingSystem<GeomComp, DrawComp, PositionComp>
    {
        public override void Process(Entity e, GeomComp sc, DrawComp dc, PositionComp pc)
        {
            if (!dc.IsVisible) return;

            var g = sc.Geom;
            var scr = dc.DrawScreen;

            // world, view, projection , color
            //var wm = Matrix.CreateTranslation(v);
            var wm = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Left);
            wm *= Matrix.CreateScale(dc.DrawScale);
            wm *= Matrix.CreateTranslation(dc.DrawPosition); // apply position
            this.Draw(g, g.Fx, wm, scr.ViewM, scr.ProjM, dc.DrawColor);
        }

        /// <summary>
        /// Draws the primitive model, using a BasicEffect shader with default
        /// lighting. Unlike the other Draw overload where you specify a custom
        /// effect, this method sets important renderstates to sensible values
        /// for 3D model rendering, so you do not need to set these states before
        /// you call it.
        /// </summary>
        public void Draw(GeometricPrimitive g, Effect fx, Matrix world, Matrix view, Matrix projection, Color color)
        {
            // Set BasicEffect parameters.
            g.Fx.World = world; // * Matrix.CreateRotationY(-MathHelper.PiOver2);
            g.Fx.View = view;
            g.Fx.Projection = projection;
            g.Fx.DiffuseColor = color.ToVector3();
            g.Fx.Alpha = color.A / 255.0f;

            GraphicsDevice device = fx.GraphicsDevice;
            device.DepthStencilState = DepthStencilState.Default;
            //device.SamplerStates[1].AddressU = TextureAddressMode.Wrap;
            //device.SamplerStates[1].AddressV = TextureAddressMode.Wrap;

            if (color.A < 255)
            {
                // Set renderstates for alpha blended rendering.
                device.BlendState = BlendState.AlphaBlend;
            }
            else
            {
                // Set renderstates for opaque rendering.
                device.BlendState = BlendState.Opaque;
            }

            /// <summary>
            /// Draws the primitive model, using the specified effect. Unlike the other
            /// Draw overload where you just specify the world/view/projection matrices
            /// and color, this method does not set any renderstates, so you must make
            /// sure all states are set to sensible values before you call it.
            /// </summary>

            // Set our vertex declaration, vertex buffer, and index buffer.
            device.SetVertexBuffer(g.vertexBuffer);
            device.Indices = g.indexBuffer;
            int primitiveCount = g.indices.Count / 3;

            foreach (EffectPass effectPass in fx.CurrentTechnique.Passes)
            {
                effectPass.Apply();
                //graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Count, 0, primitiveCount);
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, primitiveCount);
            }

        }
    }
}
