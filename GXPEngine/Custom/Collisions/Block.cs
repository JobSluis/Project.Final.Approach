using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GXPEngine.Components;
using GXPEngine.Core;
using GXPEngine.Custom.Collisions;

namespace GXPEngine.Custom
{
    public class Block : Sprite
    {
        public Vector2 position;
        private float radius;
        private List<Ball> caps;
        private List<LineSegment> lines;
        private Vector2 point1;
        private Vector2 point2;
        private Vector2 point3;
        private Vector2 point4;
        
        public Block(Vector2 position, string filename = "square.png") : base(filename)
        {
            this.position = position;
            radius = height / 2;
            SetXY(position);
            caps = new List<Ball>();
            lines = new List<LineSegment>();
            point1 = new Vector2(x , y);
            point2 = new Vector2(x + width, y);
            point3 = new Vector2(x + width, y + height);
            point4 = new Vector2(x , y + height);
            AddLine(point1,point2, this);
            AddLine(point2,point3, this);
            AddLine(point3,point4, this);
            AddLine(point4,point1, this);
        }

        void Update()
        {
            
        }
        
        public int GetNumberOfCaps() {
            return caps.Count;
        }

        public Ball GetCaps(int index) {
            if (index >= 0 && index < caps.Count) {
                return caps [index];
            }
            return null;
        }

        public int GetNumberOfLines()
        {
            return lines.Count;
        }
        
        public LineSegment GetLine(int index) {
            if (index >= 0 && index < lines.Count) {
                return lines [index];
            }
            return null;
        }

        protected void AddLine (Vector2 start, Vector2 end, GameObject owner) {
            
            LineSegment line = new (start, end,owner , 0xff00ff00, 3);
            lines.Add (line);
            LineSegment lineOpp = new (end, start,owner,  0xff00ff00, 3);
            lines.Add (lineOpp);
            Ball endcap = new (end, 0,owner);
            caps.Add(endcap);
            
            foreach (LineSegment l in lines)
            {
                Game.main.AddChild(l);
            }

            foreach (Ball c in caps)
            {
                Game.main.AddChild(c);
            }
        }
    }
}