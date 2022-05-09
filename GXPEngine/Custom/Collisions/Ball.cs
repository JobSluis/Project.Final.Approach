using System.Runtime.Remoting;
using GXPEngine.Core;

namespace GXPEngine.Custom.Collisions
{
    public class Ball : GameObject
    {
        public Vector2 position;
        public readonly int radius;
        public readonly GameObject owner;

        public Ball(Vector2 position,int radius, GameObject owner)
        {
            this.position = position;
            this.radius = radius;
            this.owner = owner;
        }
    }
}