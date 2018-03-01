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
    /// <summary>
    /// 3D Geometry system Update cycle
    /// </summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Update, Layer = SystemsSchedule.GeomSystem)]
    public class GeomSystem : EntityComponentProcessingSystem<GeomComp>
    {

        public override void Process(Entity entity, GeomComp sc)
        {
            ProcessTime(sc);
        }

    }

    /// <summary>
    /// 3D Geometry system draw cycle, which draws the shapes.
    /// More info on issues: http://xboxforums.create.msdn.com/forums/t/99090.aspx 
    /// </summary>
    [ArtemisEntitySystem(GameLoopType = GameLoopType.Draw, Layer = SystemsSchedule.GeomSystemDraw)]
    public class GeomDrawSystem : EntityComponentProcessingSystem<GeomComp, DrawComp, PositionComp>
    {
        protected override void Begin()
        {
            TTGame.Instance.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        }

        public override void Process(Entity e, GeomComp sc, DrawComp dc, PositionComp pc)
        {
            if (!dc.IsVisible) return;

            var g = sc.Geom;
            var scr = dc.DrawScreen;

            // world, view, projection , color
            //var wm = Matrix.CreateTranslation(v);
            var wm = Matrix.CreateWorld(Vector3.Zero, Vector3.Forward, Vector3.Left);
            wm *= Matrix.CreateRotationY(dc.DrawRotation);
            wm *= Matrix.CreateScale(dc.DrawScale);
            wm *= Matrix.CreateTranslation(dc.DrawPosition); // apply position
            this.Draw(g, wm, scr.ViewM, scr.ProjM, dc.DrawColor);
        }

        /// <summary>
        /// Draws the primitive model, using a BasicEffect shader g.Fx
        /// </summary>
        public void Draw(GeometricPrimitive g, Matrix world, Matrix view, Matrix projection, Color color)
        {
            // Set BasicEffect parameters.
            g.Fx.World = world; // * Matrix.CreateRotationY(-MathHelper.PiOver2);
            g.Fx.View = view;
            g.Fx.Projection = projection;
            g.Fx.DiffuseColor = color.ToVector3();
            g.Fx.Alpha = color.A / 255.0f;

            GraphicsDevice device = g.Fx.GraphicsDevice;
            //device.DepthStencilState = DepthStencilState.Default;  // enable depth-buffer
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

            /// Draws the primitive model, using the specified effect. 
            /// any renderstatesmust be set before you doing this.

            // Set our vertex declaration, vertex buffer, and index buffer.
            device.SetVertexBuffer(g.vertexBuffer);
            device.Indices = g.indexBuffer;
            int primitiveCount = g.indices.Count / 3;

            foreach (EffectPass effectPass in g.Fx.CurrentTechnique.Passes)
            {
                effectPass.Apply();
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, primitiveCount);
            }

        }
    }
}
