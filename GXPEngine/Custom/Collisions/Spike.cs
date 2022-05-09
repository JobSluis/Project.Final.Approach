using GXPEngine.Core;

namespace GXPEngine.Custom.Collisions
{
    public class Spike : Block
    {
        public Spike(Vector2 position, string filename = "triangle.png") : base(position, filename)
        {
            SetXY(position);
        }
    }
}