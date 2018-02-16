// (c) 2010-2018 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt

using Microsoft.Xna.Framework;
using TTengine.Comps;

namespace TTengineTest
{
    /// <summary>Testing the animation of sprites using spritesheets</summary>
    class TestAnimatedSprite : Test
    {
        public override void BuildAll()
        {
            // create animated sprite from 4x4 sprite atlas bitmap
            var s1 = CreateAnimatedSprite(New(), "SmileyWalk",4,4,AnimationType.NORMAL, 0.400);
            s1.C<PositionComp>().PositionXY = new Vector2(200f, 300f);

            var s2 = CreateAnimatedSprite(New(), "SmileyWalk", 4, 4, AnimationType.REVERSE, 0.100);
            s2.C<PositionComp>().PositionXY = new Vector2(400f, 300f);

            var s3 = CreateAnimatedSprite(New(), "SmileyWalk", 4, 4, AnimationType.NORMAL, 0.050);
            s3.C<PositionComp>().PositionXY = new Vector2(600f, 300f);

            var s4 = CreateAnimatedSprite(New(), "SmileyWalk", 4, 4, AnimationType.PINGPONG, 0.025);
            s4.C<PositionComp>().PositionXY = new Vector2(200f, 400f);
            s4.C<AnimatedSpriteComp>().MinFrame = 4;
            s4.C<AnimatedSpriteComp>().MaxFrame = 12;

            var s5 = CreateAnimatedSprite(New(), "SmileyWalk", 4, 4, AnimationType.NORMAL, 0.010);
            s5.C<PositionComp>().PositionXY = new Vector2(400f, 400f);
        }

    }
}
