﻿using System;
using System.Dynamic;
using GXPEngine.Core;

namespace GXPEngine.Custom
{
    public class Button : Sprite
    {
        public GameObject door;
        public int index;
        public Button(Vector2 position, GameObject door, int index = 0) : base("button2.png")
        {
            this.index = index;
            collider.isTrigger = true;
            this.door = door;
            SetXY(position);
        }

        void Update()
        {
            
        }

        public void Press()
        {
            Console.WriteLine("pressed");
            door.Destroy();
            AudioPlayer.PlayAudio("Sounds/Door_opens.wav");
            initializeFromTexture(Texture2D.GetInstance("button.png"));
        }
    }
}