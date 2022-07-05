using GXPEngine.Core;

namespace GXPEngine.Custom
{
    public class ExitDoor : Sprite
    {
        public ExitDoor(Vector2 position) : base("door2.png", false)
        {
            collider.isTrigger = true;
            SetXY(position);
        }

        void Update()
        {
            
        }
    }
}