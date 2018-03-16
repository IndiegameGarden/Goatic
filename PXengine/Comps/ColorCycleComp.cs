// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis;
using TTengine.Core;
using TTengine.Comps;
using Microsoft.Xna.Framework;

namespace PXengine.Comps
{
    public class ColorCycleComp: Comp
    {
        public double timePeriod;
        public double timePeriodR, timePeriodG, timePeriodB, timePeriodA;
        public Color minColor;
        public Color maxColor;

        public ColorCycleComp(float timePeriod)
        {
            this.timePeriod = timePeriod;
            timePeriodR = timePeriod;
            timePeriodG = timePeriod;
            timePeriodB = timePeriod;
            timePeriodA = timePeriod;
        }

    }
}
