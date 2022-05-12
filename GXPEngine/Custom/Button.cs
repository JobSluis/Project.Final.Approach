using System;
using System.Dynamic;
using GXPEngine.Core;

namespace GXPEngine.Custom
{
    public class Button : Sprite
    {
        public GameObject door;
        public Button(Vector2 position, GameObject door) : base("button2.png")
        {
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