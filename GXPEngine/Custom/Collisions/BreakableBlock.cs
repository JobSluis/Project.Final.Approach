using System.Collections.Generic;
using GXPEngine.Components;
using GXPEngine.Core;

namespace GXPEngine.Custom.Collisions
{
    public class BreakableBlock : Block
    {
        private List<Ball> caps;
        private List<LineSegment> lines;
        private Vector2 point1;
        private Vector2 point2;
        private Vector2 point3;
        private Vector2 point4;
        public BreakableBlock(Vector2 position) : base(position,"Checkers.png")
        {
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

        public void Break()
        {
            Destroy();
        }
        
        protected override void OnDestroy()
        {
            MyGame myGame = (MyGame) game;
            myGame.blocks.Remove(this);
            myGame.breakable.Remove(this);
        }
    }
}