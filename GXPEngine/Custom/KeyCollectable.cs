using GXPEngine.Core;

namespace GXPEngine.Custom
{
    public class KeyCollectable : Sprite
    {
        public KeyCollectable(Vector2 position) : base("colors.png")
        {
            SetXY(position);
        }

        void Update()
        {
            
        }

        public void Pickup()
        {
            Destroy();
        }
    }
}