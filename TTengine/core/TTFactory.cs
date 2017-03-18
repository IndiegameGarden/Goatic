﻿// (c) 2010-2017 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Artemis;
using TTengine.Comps;
using TTengine.Systems;
using TTengine.Modifiers;
using TTMusicEngine.Soundevents;

namespace TTengine.Core
{
    /// <summary>
    /// The TTengine's Factory to create new basic Entities (may be half-baked, 
    /// to further customize) with customizable output to which EntityWorld or
    /// Screen the items are built.
    /// Factory's methods are 
    ///     Create....(input) for new entity creation
    ///     Add....(Entity e, input)   for adding elements to existing item
    ///     BuildTo(...)               for changing the build destination of the factory
    ///     Process...(Entity e, parameters)    for processing an entity
    /// </summary>
    public class TTFactory
    {
        /// <summary>The Artemis entity world currently used for building new Entities in.</summary>
        public EntityWorld BuildWorld;

        /// <summary>The Screen that newly built Entities in factory will by default render to.</summary>
        public ScreenComp BuildScreen;

        public void BuildTo(EntityWorld world)
        {
            BuildWorld = world;
        }

        public void BuildTo(ScreenComp screen)
        {
            BuildScreen = screen;
        }

        /// <summary>
        /// Switch factory's building output to given Channel, World or Screen
        /// </summary>
        /// <param name="e">an Entity which may contain a WorldComp, a ScreenComp, or both. In case of both,
        /// the Entity is a Channel.</param>
        public void BuildTo(Entity e)
        {
            if (e.HasComponent<WorldComp>())
            {
                var wc = e.C<WorldComp>();
                BuildWorld = wc.World;
                if (wc.Screen != null)
                    BuildScreen = wc.Screen;
            }
            if (e.HasComponent<ScreenComp>())
                BuildScreen = e.C<ScreenComp>();
        }

        /// <summary>
        /// Create simplest Entity without components within the EntityWorld currently selected
        /// for Entity construction.
        /// </summary>
        /// <returns>New empty Entity which is enabled</returns>
        public Entity New()
        {
            Entity e = BuildWorld.CreateEntity();
            return e;
        }

        /// <summary>
        /// Create simplest Entity without components within the EntityWorld currently selected
        /// for Entity construction. By default, the Entity is not enabled until it is
        /// Finalized.
        /// </summary>
        /// <returns>New empty Entity which is disabled</returns>
        public Entity NewDisabled()
        {
            Entity e = BuildWorld.CreateEntity();
            e.IsEnabled = false;
            return e;
        }

        /// <summary>
        /// Finalize Entity after having constructed all its components. This will enable it, later,
        /// via the RefresherSystem (and its calling thread) so not immediately.
        /// </summary>
        /// <param name="e">Entity to finalize and soon after activate.</param>
        public void Finalize(Entity e)
        {
            e.IsEnabled = true;
            RefresherSystem.AddToQueue(e,typeof(RefresherSystem));
        }

        /// <summary>
        /// Add Entity position and velocity, but no shape/drawability (yet)
        /// </summary>
        public Entity AddMotion(Entity e)
        {
            e.AddComponent(new PositionComp());
            e.AddComponent(new VelocityComp());
            return e;
        }

        /// <summary>
        /// Add Entity position and velocity, and mae it a drawable Entity
        /// </summary>
        public Entity AddDrawable(Entity e)
        {
            AddMotion(e);
            e.AddComponent(new DrawComp(BuildScreen));
            return e;
        }

        /// <summary>
        /// Make base Entity a Spritelet, which is a moveable sprite 
        /// </summary>
        /// <param name="graphicsFile">The content graphics file with or without extension. If
        /// extension given eg "ball.png", the uncompiled file will be loaded at runtime. If no extension
        /// given eg "ball", precompiled XNA content will be loaded (.xnb files).</param>
        public Entity CreateSpritelet(Entity e, string graphicsFile)
        {
            AddDrawable(e);
            var spriteComp = new SpriteComp(graphicsFile);
            e.AddComponent(spriteComp);
            return e;
        }

        /// <summary>
        /// Create a Spritelet with texture based on the contents of a Screen
        /// </summary>
        /// <returns></returns>
        public Entity CreateSpritelet(Entity e, ScreenComp screen)
        {
            AddDrawable(e);
            var spriteComp = new SpriteComp(screen);
            e.AddComponent(spriteComp);
            return e;
        }

        /// <summary>
        /// Create an animated sprite entity
        /// </summary>
        /// <param name="atlasBitmapFile">Filename of the sprite atlas bitmap</param>
        /// <param name="NspritesX">Number of sprites in horizontal direction (X) in the atlas</param>
        /// <param name="NspritesY">Number of sprites in vertical direction (Y) in the atlas</param>
        /// <param name="animType">Animation type chosen from AnimationType class</param>
        public Entity CreateAnimatedSpritelet(Entity e, string atlasBitmapFile, int NspritesX, int NspritesY, 
                                                     AnimationType animType = AnimationType.NORMAL )
        {
            AddDrawable(e);
            var spriteComp = new AnimatedSpriteComp(atlasBitmapFile,NspritesX,NspritesY);
            spriteComp.AnimType = animType;
            e.AddComponent(spriteComp);
            return e;
        }

        public Entity CreateSpriteField(Entity e, string fieldBitmapFile, string spriteBitmapFile)
        {
            AddDrawable(e);
            var spriteFieldComp = new SpriteFieldComp(fieldBitmapFile);
            var spriteComp = new SpriteComp(spriteBitmapFile);
            spriteFieldComp.FieldSpacing = new Vector2(spriteComp.Width, spriteComp.Height);
            e.AddComponent(spriteComp);
            e.AddComponent(spriteFieldComp);
            return e;
        }

        /// <summary>
        /// Creates a Textlet, which is a moveable piece of text.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontName"></param>
        public Entity CreateTextlet(Entity e, string text, string fontName = "Font1")
        {
            AddDrawable(e);
            e.AddComponent(new ScaleComp());
            TextComp tc = new TextComp(text, fontName);
            e.AddComponent(tc);
            return e;
        }

        /// <summary>
        /// Creates a Screenlet, an Entity that has a ScreenComp to 
        /// which graphics can be rendered. 
        /// </summary>
        /// <param name="backgroundColor">Background color of the Screenlet</param>
        /// <param name="hasRenderBuffer">if true, Screenlet will have its own render buffer</param>
        /// <param name="height">Screenlet height, if not given uses default backbuffer height</param>
        /// <param name="width">Screenlet width, if not given uses default backbuffer width</param>
        /// <returns>Newly created Entity with a ScreenComp.</returns>
        public Entity CreateScreenlet(Entity e, Color backgroundColor, bool hasRenderBuffer = false,
                                        int width = 0, int height = 0)
        {
            var sc = new ScreenComp(hasRenderBuffer, width, height);
            sc.BackgroundColor = backgroundColor;            
            e.AddComponent(sc);
            e.AddComponent(new DrawComp(BuildScreen));
            return e;
        }

        /// <summary>
        /// Creates a new Channel, which is an Entity with inside it a separate EntityWorld containing a dedicated ScreenComp to
        /// which that World renders, which can be then shown as a sprite. Parameters are same as for CreateScreenlet() above.
		/// In summary: A World inside a Sprite.
        /// </summary>
        /// <param name="backgroundColor">Background drawing color</param>
        /// <param name="width">Channel's screen width or if not given or 0 will use RenderBuffer width</param>
        /// <param name="height">Channel's screen height or if not given or 0 will use for RenderBuffer height</param>
        /// <returns></returns>
        public Entity CreateChannel(Entity e, Color backgroundColor,
                                        int width = 0, int height = 0)
        {
			var wc = new WorldComp(); // create world

			// create a screenlet Entity within the Channel's (sub) world
			Entity screenlet = wc.World.CreateEntity();
			var sc = new ScreenComp (true, width, height);
			wc.Screen = sc;				// store the Screen as part of the World
			sc.BackgroundColor = backgroundColor;
			screenlet.AddComponent (sc);

            // create the channel Entity, based on Spritelet
            CreateSpritelet(e, sc);

			// make this spritelet into a Channel by adding the World
            e.AddComponent(wc);
            return e;
        }

        /// <summary>
        /// Create a new channel with same properties as an existing (template) channel. Color and size will
        /// be the same.
        /// </summary>
        /// <param name="templateChannel">Existing channel to use as template for color and size.</param>
        public Entity CreateChannel(Entity e, Entity templateChannel) 
        {
            ScreenComp sc = templateChannel.C<WorldComp>().Screen;
            CreateChannel(e,sc.BackgroundColor, sc.Width, sc.Height);
            return e;
        }

        /// <summary>
        /// Creates an FX Screenlet that renders a layer with shader Effect to the current active BuildScreen
        /// </summary>
        /// <returns></returns>
        public Entity CreateFxScreenlet(Entity e, String effectFile)
        {
            var fx = TTGame.Instance.Content.Load<Effect>(effectFile);
            var sc = new ScreenComp(BuildScreen.RenderTarget); // renders to the existing screen buffer
            sc.SpriteBatch.effect = fx; // set the effect in SprBatch
            e.AddComponent(sc);
            //e.AddComponent(new DrawComp(sc));
            return e;
        }

        /// <summary>
        /// Creates a Scriptlet, which is an Entity that only contains a custom code script
        /// </summary>
        /// <param name="script"></param>
        public Entity CreateScriptlet(Entity e, IScript script)
        {
            e.AddComponent(new ScriptComp(script));
            return e;
        }

        /// <summary>
        /// Add audio script to Entity
        /// </summary>
        /// <param name="soundScript"></param>
        public Entity CreateAudiolet(Entity e, SoundEvent soundScript)
        {
            e.AddComponent(new AudioComp(soundScript));
            return e;
        }

        /// <summary>
        /// Creates a new FrameRateCounter. TODO: screen position set.
        /// </summary>
        /// <returns></returns>
        public Entity CreateFrameRateCounter(Entity e, Color textColor, int pixelsOffsetVertical = 0)
        {
            CreateTextlet(e,"##");
            e.C<PositionComp>().Position = new Vector2(2f, 2f + pixelsOffsetVertical);
            e.C<DrawComp>().DrawColor = textColor;
            AddScript(e, new Util.FrameRateCounter(e.C<TextComp>()));
            return e;
        }

        public void AddScript(Entity e, IScript script)
        {
            if (!e.HasComponent<ScriptComp>())
                e.AddComponent(new ScriptComp());
            var sc = e.C<ScriptComp>();
            sc.Add(script);
        }

        /// <summary>
        /// Add a script to an Entity, based on a function (delegate)
        /// </summary>
        /// <param name="e">The Entity to add script to</param>
        /// <param name="scriptCode">Script to run</param>
        /// <returns>IScript object created from the function</returns>
        public BasicScript AddScript(Entity e, ScriptDelegate scriptCode)
        {
            if (!e.HasComponent<ScriptComp>())
            {
                e.AddComponent(new ScriptComp());
            }
            var sc = e.C<ScriptComp>();
            var script = new BasicScript(scriptCode);
            sc.Add(script);
            return script;
        }

        /// <summary>
        /// Add a Modifier script to an Entity, based on a code block (delegate) and a Function
        /// </summary>
        /// <param name="e">Entity to add modifier script to</param>
        /// <param name="scriptCode">Code block (delegate) that is the script</param>
        /// <param name="func">Function whose value will be passed in ScriptContext.FunctionValue to script</param>
        /// <returns></returns>
        public ModifierScript AddModifier(Entity e, ModifierDelegate scriptCode, IFunction func)
        {
            if (!e.HasComponent<ScriptComp>())
            {
                e.AddComponent(new ScriptComp());
            }
            var sc = e.C<ScriptComp>();
            var script = new ModifierScript(scriptCode, func);
            sc.Add(script);
            return script;
        }

        /// <summary>
        /// Add a Modifier script to an Entity, based on a code block (delegate) and a VectorFunction
        /// </summary>
        /// <param name="e">Entity to add modifier script to</param>
        /// <param name="scriptCode">Code block (delegate) that is the script</param>
        /// <param name="func">Function whose value will be passed in ScriptContext.FunctionValue to script</param>
        /// <returns></returns>
        public VectorModifierScript AddModifier(Entity e, VectorModifierDelegate scriptCode, IVectorFunction func)
        {
            if (!e.HasComponent<ScriptComp>())
            {
                e.AddComponent(new ScriptComp());
            }
            var sc = e.C<ScriptComp>();
            var script = new VectorModifierScript(scriptCode, func);
            sc.Add(script);
            return script;
        }

        /// <summary>
        /// Add a Modifier script to an Entity, based on a code block (delegate) and an empty (=unity) Function
        /// </summary>
        /// <param name="e">Entity to add modifier script to</param>
        /// <param name="scriptCode">Code block (delegate) that is the script</param>
        /// <returns></returns>
        public ModifierScript AddModifier(Entity e, ModifierDelegate scriptCode)
        {
            return AddModifier(e, scriptCode, null);
        }

        /// <summary>
        /// Basic script object that can run code from a Delegate
        /// </summary>
        public class BasicScript : IScript
        {
            protected ScriptDelegate scriptCode;

            public BasicScript(ScriptDelegate scriptCode)
            {
                this.scriptCode = scriptCode;
            }

            public void OnUpdate(ScriptContext ctx)
            {
                scriptCode(ctx);
            }

        }

        /// <summary>
        /// Apply a channel fit (scale, move) such that the channelToFit will be centered in
        /// and shrunk or stretched to the extent that it optimally fits parentChannel.
        /// </summary>
        /// <param name="channelToFit"></param>
        /// <param name="parentChannel"></param>
        public void ProcessChannelFit(Entity channelToFit, Entity parentChannel, bool canStretch = true, 
                                             bool canShrink = true, int maxPixelsCutOffVertical = 0)
        {
            var scrToFit = channelToFit.C<WorldComp>().Screen;
            PositionComp pos = channelToFit.C<PositionComp>();
            SpriteComp spr = channelToFit.C<SpriteComp>();
            ScaleComp scl = channelToFit.C<ScaleComp>();
            var parentScr = parentChannel.C<WorldComp>().Screen;
            float scale = 1.0f;

            // if no scale comp yet, add one
            if (scl == null)
            {
                scl = new ScaleComp();
                channelToFit.AddComponent(scl);
            }

            // position channel to the middle of parent.
            pos.Position = parentScr.Center;
            spr.CenterToMiddle();

            // squeeze in to fit width
            if (canShrink && scrToFit.Width > parentScr.Width)
            {
                scale = ((float)parentScr.Width) / ((float)scrToFit.Width);
                // squeeze in to fit height
                if ((((scrToFit.Height - maxPixelsCutOffVertical) * scale)) > (parentScr.Height * scale))
                {
                    scale *= ((float)parentScr.Height) / ((float)(scrToFit.Height - maxPixelsCutOffVertical));
                }
            }

            // expand to fit width
            if (canStretch && scrToFit.Width < parentScr.Width)
            {
                scale = ((float)parentScr.Width) / ((float)scrToFit.Width);
                // squeeze in to fit height
                if ((scale * (float)scrToFit.Height - (float)maxPixelsCutOffVertical) > parentScr.Height)
                {
                    scale *= ((float)parentScr.Height) / ((float)(scrToFit.Height-maxPixelsCutOffVertical)) ;
                }
            }

            // apply scale
            scl.Scale = scale;

        }

    }
}
