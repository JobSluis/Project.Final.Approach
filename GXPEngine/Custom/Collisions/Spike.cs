using System.Collections.Generic;
using GXPEngine.Components;
using GXPEngine.Core;

namespace GXPEngine.Custom.Collisions
{
    public class Spike : Block
    {
        public Spike(Vector2 position, string filename = "spike.png") : base(position, filename)
        {
            SetXY(position);
        }
    }
}