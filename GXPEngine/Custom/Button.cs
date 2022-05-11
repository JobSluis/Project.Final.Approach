using System.Dynamic;
using GXPEngine.Core;

namespace GXPEngine.Custom
{
    public class Button : Sprite
    {
        private GameObject door;
        public Button(Vector2 position, GameObject door) : base("circle.png")
        {
            this.door = door;
            SetXY(position);
        }

        void Update()
        {
            
        }

        public void Press()
        {
            //TODO add the door
        }
    }
}