using GXPEngine.Core;

namespace GXPEngine.Custom
{
    public class KeyCollectable : Sprite
    {
        public KeyCollectable(Vector2 position) : base("key.png")
        {
            collider.isTrigger = true;
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