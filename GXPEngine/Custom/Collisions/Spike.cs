using System.Collections.Generic;
using GXPEngine.Components;
using GXPEngine.Core;

namespace GXPEngine.Custom.Collisions
{
    public class Spike : Block
    {
        public Vector2 position;
        private float radius;
        private List<Ball> caps;
        private List<LineSegment> lines;
        private Vector2 point1;
        private Vector2 point2;
        private Vector2 point3;
        private Vector2 point4;
        
        public Spike(Vector2 position, string filename = "spike.png") : base(position, filename)
        {
            SetXY(position);
            caps = new List<Ball>();
            lines = new List<LineSegment>();
            point1 = new Vector2(x , y);
            point2 = new Vector2(x + width, y);
            point3 = new Vector2(x + width, y + height);
            point4 = new Vector2(x , y + height);
            AddLine(point1,point2, this , false);
            AddLine(point2,point3, this, false);
            AddLine(point3,point4, this, true);
            AddLine(point4,point1, this, false);
        }
    }
}