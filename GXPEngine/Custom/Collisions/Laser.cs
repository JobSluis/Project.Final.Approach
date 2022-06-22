using System;
using System.Collections.Generic;
using GXPEngine.Components;
using GXPEngine.Core;

namespace GXPEngine.Custom.Collisions
{
    public class Laser : AnimationSprite
    {
        private int lastShootTime;
        private const int SHOOTINTERVAL = 1000;
        private int frameNumber = 1;
        private List<Ball> caps;
        private List<LineSegment> lines;
        private Vector2 point1;
        private Vector2 point2;
        private Vector2 point3;
        private Vector2 point4;
        public bool isActive;
        public Laser(Vector2 position) : base("Laser_sprite_sheet1.png", 2, 5, 10)
        {
            width /= 4;
            height /= 4;
            SetXY(position);
            caps = new List<Ball>();
            lines = new List<LineSegment>();
            point1 = new Vector2(x + width/2 - 32 , y + 72);
            point2 = new Vector2(x + width/2 + 32, y + 72);
            point3 = new Vector2(x + width/2 + 32, y + height);
            point4 = new Vector2(x + width/2 - 32 , y + height);
            AddLine(point1,point2, this);
            AddLine(point2,point3, this);
            AddLine(point3,point4, this);
            AddLine(point4,point1, this);
        }

        void Update()
        {
            
            if (Time.time > lastShootTime)
            {
                frameNumber++;
                lastShootTime = Time.time + SHOOTINTERVAL;
            }
            //TODO animate the laser correctly
            switch (frameNumber)
            {
                case 1:
                    isActive = false;
                    break;
                case 5:
                    AudioPlayer.PlayAudio("Sounds/Laser.wav");
                    isActive = true;
                    break;
                case 8:
                    AudioPlayer.PlayAudio("Sounds/Laser.wav");
                    break;
                case 11:
                    frameNumber = 1;
                    break;
            }

            SetFrame(frameNumber);
        }

        private void AddLine (Vector2 start, Vector2 end, GameObject owner) {
            
            LineSegment line = new (start, end,owner , false, 0xff00ff00, 3);
            lines.Add (line);
            LineSegment lineOpp = new (end, start,owner,false ,   0xff00ff00, 3);
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
    }
}