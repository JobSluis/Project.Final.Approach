using System;
using System.Collections.Generic;
using GXPEngine.Components;
using GXPEngine.Core;

namespace GXPEngine.Custom.Collisions
{
    public class Laser : AnimationSprite
    {
        private int lastShootTime;
        private const int SHOOTINTERVAL = 750;
        private int frameNumber = 0;
        public bool isActive;

        public Laser(Vector2 position) : base("Laser_sprite_sheet1.png", 2, 5, 10)
        {
            width /= 4;
            height /= 4;
            SetXY(position);
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
                case 0:
                    collider.isTrigger = true;
                    break;
                case 4:
                    collider.isTrigger = false;
                    break;
                case 5:
                    AudioPlayer.PlayAudio("Sounds/Laser.wav");
                    break;
                case 7:
                    AudioPlayer.PlayAudio("Sounds/Laser.wav");
                    break;
                case 10:
                    frameNumber = 0;
                    break;
            }

            SetFrame(frameNumber);
        }
    }
}