using GXPEngine.Core;

namespace GXPEngine.Components
{
    /// <summary>
    /// Implements an OpenGL line
    /// </summary>
    public class LineSegment : GameObject
    {
        public Vector2 start;
        public Vector2 end;

        private uint color = 0xffffffff;
        private uint lineWidth = 1;

        public readonly GameObject owner;

        public LineSegment (float pStartX, float pStartY, float pEndX, float pEndY, uint pColor = 0xffffffff, uint pLineWidth = 1)
            : this (new Vector2 (pStartX, pStartY), new Vector2 (pEndX, pEndY), pColor, pLineWidth)
        {
        }

        public LineSegment (Vector2 pStart, Vector2 pEnd, uint pColor = 0xffffffff, uint pLineWidth = 1)
        {
            start = pStart;
            end = pEnd;
            color = pColor;
            lineWidth = pLineWidth;
			
        }
		
        public LineSegment (Vector2 pStart, Vector2 pEnd,GameObject owner , uint pColor = 0xffffffff, uint pLineWidth = 1)
        {
            start = pStart;
            end = pEnd;
            color = pColor;
            lineWidth = pLineWidth;
            this.owner = owner;
        }
	
        //------------------------------------------------------------------------------------------------------------------------
        //														RenderSelf()
        //------------------------------------------------------------------------------------------------------------------------
        protected override void RenderSelf(GLContext glContext) {
            if (game != null) {
                //Gizmos.RenderLine(start.x, start.y, end.x, end.y, color, lineWidth);
            }
        }
    }
}