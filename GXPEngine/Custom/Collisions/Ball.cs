using System.Runtime.Remoting;
using GXPEngine.Core;

namespace GXPEngine.Custom.Collisions
{
    public class Ball : GameObject
    {
        public Vector2 position;
        public readonly int radius;
        public readonly GameObject owner;
        public bool isBottomLine;

        public Ball(Vector2 position,int radius, GameObject owner, bool isBottomLine = false)
        {
            this.isBottomLine = isBottomLine;
            this.position = position;
            this.radius = radius;
            this.owner = owner;
        }
    }
}