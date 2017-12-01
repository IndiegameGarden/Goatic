﻿using System;
using System.Collections.Generic;

using Artemis;
using Artemis.Interface;
using TTengine.Core;
using TTengine.Comps;

namespace TTengine.Modifiers
{
    /// <summary>
    /// Method signature (Delegate) for function scripts
    /// </summary>
    /// <param name="ctx">script context supplied during script execution</param>
    /// <param name="functionValue">value of computed function that is passed to script code</param>
    public delegate void FunctionScriptDelegate(ScriptComp ctx, double functionValue);

    /// <summary>
    /// A script object that first computes a function and then calls a delegate (piece of code) to use
    /// that function value. The script usually will modify one or more parameters of Components.
    /// </summary>
    public class FunctionScript: IScript
    {
        /// <summary>
        /// The script code
        /// </summary>
        public FunctionScriptDelegate ScriptCode;

        /// <summary>
        /// The Function that is computed and whose value is passed to the ScriptCode.
        /// </summary>
        public IFunction Function;

        /// <summary>
        /// Create new ModifierScript
        /// </summary>
        /// <param name="code"></param>
        /// <param name="function">if null is passed, the unity function f(x)=x is applied as Function</param>
        public FunctionScript(FunctionScriptDelegate code, IFunction function)
        {
            this.ScriptCode = code;
            this.Function = function;
        }

        /// <summary>
        /// Implements IScript
        /// </summary>
        /// <param name="ctx"></param>
        public void OnUpdate(ScriptComp ctx)
        {
            double v;
            if (Function == null)
                v = ctx.SimTime;
            else
                v = Function.Value(ctx.SimTime);
            ScriptCode(ctx,v);
        }

        public void OnDraw(ScriptComp ctx) { ; }
    }
}

/*
 function can be pulled into script (object).

 creation of script in factory: supply an IFunction object, or IVectorFunction, also with potentially its input (pointer to it)
 AddScript( NewFunctionScript( code-delegate , Functions.NewSin(params) ) )
 */